using Microsoft.Dism;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.Versioning;
using System.Windows.Forms;
using WindowsInstallerLib.Management.DiskManagement;
using WindowsInstallerLib.Management.Installer;
using WindowsInstallerLib.Management.PrivilegesManager;
using WindowsInstallerLib.Utilities.Deployment;

namespace wit
{
    [SupportedOSPlatform("windows")]
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            switch (GetPrivileges.IsUserAdmin())
            {
                case true:
                    InitializeComponent();
                    break;
                case false:
                    MessageBox.Show("You must have administrator privileges to run this program.",
                                    "Insufficient privileges", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new UnauthorizedAccessException("You must have administrator privileges to run this program.");
            }
        }

        private void ValidateDiskLetter(object? sender, EventArgs e)
        {
            switch (EfiDrive.Text.Length > 0)
            {
                case true:
                    while (DestinationDrive.Text == EfiDrive.Text)
                    {
                        if (true)
                        {
                            MessageBox.Show("The OS drive letter cannot be the same as the bootloader drive.",
                                            "Duplicate letters",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);

                            DestinationDrive.Text = null;
                            EfiDrive.Text = null;
                            break;
                        }
                    }
                    break;
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
                foreach (DriveInfo drive in Disks.GetDisksT())
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
            DiskList.Columns.Add("DiskNumber", "Disk");
            DiskList.Columns.Add("Model", "Model");
            DiskList.Columns.Add("DeviceID", "Device ID");

            WqlObjectQuery DeviceTable = new("SELECT * FROM Win32_DiskDrive");
            ManagementObjectSearcher DeviceInfo = new(DeviceTable);

            foreach (ManagementObject o in DeviceInfo.Get().Cast<ManagementObject>())
            {
                DiskList.Rows.Add(o["Index"], o["Model"], o["DeviceID"]);

                DiskList.Sort(DiskList.Columns[0], ListSortDirection.Ascending);
            }

            DiskList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            DiskNumber.Maximum = DiskList.Rows.Count - 1;
        }

        private void GetImageInfo(object sender, EventArgs e)
        {
            try
            {
                if (ImageFilePath.Text == "No image file is selected.")
                {
                    OpenFileDialog OpenFileDialog = new()
                    {
                        Filter = "ESD file (*.esd)|*.esd|WIM file (*.wim)|*.wim",
                        CheckFileExists = true,
                        CheckPathExists = true,
                        AddExtension = true,
                    };

                    OpenFileDialog.ShowDialog();

                    ImageFilePath.Text = "No image file is selected";

                    if (string.IsNullOrWhiteSpace(OpenFileDialog.FileName))
                    {
                        MessageBox.Show("No image file was selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        ImageFilePath.Text = "No image file is selected";

                        return;
                    }
                    else if (!OpenFileDialog.FileName.Contains("install"))
                    {
                        MessageBox.Show("Invalid image file. It must be 'install.wim' or 'install.esd'.", "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        throw new InvalidDataException("Invalid image file. It must be 'install.wim' or 'install.esd'.");
                    }

                    ImageFilePath.Text = OpenFileDialog.FileName;
                }

                DismImageInfoCollection imageInfoCollection = NewDeploy.GetImageInfoT(NewInstallation.ImageFilePath);

                WindowsEditionIndex.Minimum = 1;
                WindowsEditionIndex.Maximum = imageInfoCollection.Count;

                WindowsEditionIndex.Enabled = true;

                ImageList.Columns.Add("ImageIndex", "Index");
                ImageList.Columns.Add("ImageName", "Image name");

                foreach (DismImageInfo DismImage in imageInfoCollection)
                {
                    ImageList.Rows.Add(DismImage.ImageIndex, DismImage.ImageName);
                }
            }
            catch(DismException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                DismApi.Shutdown();
            }
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
            NewInstallation.DestinationDrive = DestinationDrive.Text;
            NewInstallation.EfiDrive = EfiDrive.Text;
            NewInstallation.DiskNumber = (int)DiskNumber.Value;
            NewInstallation.ImageFilePath = ImageFilePath.Text;
            NewInstallation.ImageIndex = (int)WindowsEditionIndex.Value;

            Disks.FormatDisk(NewInstallation.DiskNumber, NewInstallation.EfiDrive, NewInstallation.DestinationDrive);
            NewDeploy.ApplyImage(NewInstallation.ImageFilePath, NewInstallation.DestinationDrive, NewInstallation.ImageIndex);
            NewDeploy.InstallBootloader(NewInstallation.DestinationDrive, NewInstallation.EfiDrive, NewInstallation.FirmwareType);
        }

        private void AboutWindow_Click(object sender, EventArgs e)
        {
            new AboutWindow().ShowDialog(this);
        }

        private void RescanDisks_Click(object sender, EventArgs e)
        {
            DiskList.Columns.Clear();
            DiskList.Rows.Clear();
            GetDisksData(sender, e);
        }
    }
}
