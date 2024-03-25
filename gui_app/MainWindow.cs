using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.Versioning;
using System.Windows.Forms;
using libwcit.Management.DiskManagement;
using libwcit.Management.PrivilegesManager;
using libwcit.Utilities.Deployment;
using Microsoft.Dism;

namespace wit
{
    [SupportedOSPlatform("windows")]
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ValidateDiskLetter(object? sender, EventArgs e)
        {
            if (EfiDrive.Text.Length > 0)
            {
                while (DestinationDrive.Text.ToString() == EfiDrive.Text.ToString())
                {
                    if (true)
                    {
                        MessageBox.Show("The OS drive cannot be the same as the bootloader drive.",
                                        "Duplicate drive letters",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                        DestinationDrive.Text = null;
                        EfiDrive.Text = null;
                        break;
                    }
                }
            }
        }

        private void GetDiskLetters(object sender, EventArgs e)
        {
            List<string> DiskLetters = [
                "A:\\",
                "B:\\",
                "C:\\",
                "D:\\",
                "E:\\",
                "F:\\",
                "G:\\",
                "H:\\",
                "I:\\",
                "J:\\",
                "K:\\",
                "L:\\",
                "M:\\",
                "N:\\",
                "O:\\",
                "P:\\",
                "Q:\\",
                "R:\\",
                "S:\\",
                "T:\\",
                "U:\\",
                "V:\\",
                "W:\\",
                "X:\\",
                "Y:\\",
                "Z:\\"
            ];

            try
            {
                DriveInfo[] drives = DriveInfo.GetDrives();

                foreach (DriveInfo drive in drives)
                {
                    foreach (string letter in DiskLetters.ToList())
                    {
                        if (drive.Name == letter)
                        {
                            DiskLetters.Remove(letter);
                        }
                    }
                }

                DestinationDrive.Items.AddRange(DiskLetters.ToArray());
                EfiDrive.Items.AddRange(DiskLetters.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GetDisksData(object sender, EventArgs e)
        {
            // Set up DiskList columns
            DiskList.Columns.Add("DiskNumber", "Disk");
            DiskList.Columns.Add("Model", "Model");
            DiskList.Columns.Add("DeviceID", "Device ID");

            // Query disk drive information using WMI
            WqlObjectQuery DeviceTable = new("SELECT * FROM Win32_DiskDrive");
            ManagementObjectSearcher DeviceInfo = new(DeviceTable);

            foreach (ManagementObject o in DeviceInfo.Get().Cast<ManagementObject>())
            {
                // Display information in the DiskList
                DiskList.Rows.Add(o["Index"], o["Model"], o["DeviceID"]);

                // Sort by DiskNumber ascending
                DiskList.Sort(DiskList.Columns[0], ListSortDirection.Ascending);
            }

            DiskList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            DiskNumber.Maximum = DiskList.Rows.Count;
        }

        private void GetImageInfo(object sender, EventArgs e)
        {
            if (!GetPrivileges.IsUserAdmin())
            {
                throw new UnauthorizedAccessException("Insufficient privileges to start DISM API.");
            }
            else
            {
                DismApi.Initialize(DismLogLevel.LogErrorsWarningsInfo);
            }

            if (NewDeploy.ImageFile == null)
            {
                OpenFileDialog OpenFileDialog = new()
                {
                    Filter = "ESD file (*.esd)|*.esd|WIM file (*.wim)|*.wim",
                    CheckFileExists = true,
                    CheckPathExists = true,
                    AddExtension = true,
                };

                OpenFileDialog.ShowDialog();

                NewDeploy.ImageFile = OpenFileDialog.FileName;

                if (string.IsNullOrWhiteSpace(OpenFileDialog.FileName))
                {
                    MessageBox.Show("No image file was selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    NewDeploy.ImageFile = null;

                    return;
                }
                else if (!OpenFileDialog.FileName.Contains("install"))
                {
                    MessageBox.Show("Invalid image file. It must be 'install.wim' or 'install.esd'.", "Invalid file",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    throw new InvalidDataException("Invalid image file. It must be 'install.wim' or 'install.esd'.");
                }
            }

            DismImageInfoCollection dismImageInfos = DismApi.GetImageInfo(NewDeploy.ImageFile);
            ImageFilePath.Text = NewDeploy.ImageFile;

            ImageList.Columns.Add("ImageIndex", "Index");
            ImageList.Columns.Add("ImageName", "Image name");

            foreach (DismImageInfo DismImage in dismImageInfos)
            {
                WindowsEditionIndex.Minimum = DismImage.ImageIndex - DismImage.ImageIndex;
                WindowsEditionIndex.Maximum = DismImage.ImageIndex;
                ImageList.Rows.Add(DismImage.ImageIndex, DismImage.ImageName);
            }

            DismApi.CleanupMountpoints();
            DismApi.Shutdown();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            try
            {
                GetDisksData(sender, e);
                GetDiskLetters(sender, e);

                WindowsEditionIndex.Enabled = false;

                DestinationDrive.SelectedIndexChanged += ValidateDiskLetter;
                EfiDrive.SelectedIndexChanged += ValidateDiskLetter;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CloseApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ChooseISOImage_Click(object sender, EventArgs e)
        {
            try
            {
                GetImageInfo(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            SystemDrives.FormatDisk((int)DiskNumber.Value, DestinationDrive.Text, EfiDrive.Text);
            NewDeploy.ApplyImage(ImageFilePath.Text, DestinationDrive.Text, (int)WindowsEditionIndex.Value);
        }

        private void AboutWindow_Click(object sender, EventArgs e)
        {
            new AboutWindow().ShowDialog(this);
        }
    }
}
