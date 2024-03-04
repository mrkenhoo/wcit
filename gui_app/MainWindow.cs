using libwcit.Management.DiskManagement;
using libwcit.Utilities.Deployment;
using Microsoft.Dism;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace gui_app
{
    [SupportedOSPlatform("windows")]
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetDisksData(object sender, EventArgs e)
        {
            // Set up DiskList columns
            DiskList.Columns.Add("DiskNumber", "Disk Number");
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

            DiskNumber.Maximum = DiskList.Rows.Count;
        }

        private void GetImageInfo(object sender, EventArgs e)
        {
            DismApi.Initialize(DismLogLevel.LogErrorsWarningsInfo);

            if (NewDeploy.ImageFile == null)
            {
                OpenFileDialog OpenFileDialog = new()
                {
                    Filter = "ESD file (*.esd)|*.esd|WIM file (*.wim)|*.wim",
                    //Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
                    CheckFileExists = true,
                    CheckPathExists = true,
                    AddExtension = true,
                };

                OpenFileDialog.ShowDialog();

                NewDeploy.ImageFile = OpenFileDialog.FileName;

                if (string.IsNullOrWhiteSpace(OpenFileDialog.FileName))
                {
                    MessageBox.Show("No image file was selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    throw new Exception("No image file was selected.");
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
                WindowsEditionIndex.Minimum = DismImage.ImageIndex - DismImage.ImageIndex + 1;
                WindowsEditionIndex.Maximum = DismImage.ImageIndex;
                ImageList.Rows.Add(DismImage.ImageIndex, DismImage.ImageName);
            }

            DismApi.CleanupMountpoints();
            DismApi.Shutdown();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            GetDisksData(sender, e);
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
            if (string.IsNullOrWhiteSpace(DestinationDrive.Text))
            {
                MessageBox.Show($"Error: DestinationDrive is not set", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (DestinationDrive.Text.Length > 2 || DestinationDrive.Text.StartsWith(':') || !DestinationDrive.Text.EndsWith(':'))
            {
                MessageBox.Show($"Invalid value at DestinationDrive: {DestinationDrive.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(EfiDrive.Text))
            {
                MessageBox.Show($"Error: EfiDrive is not set", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (EfiDrive.Text.Length > 2 || EfiDrive.Text.StartsWith(':') || !EfiDrive.Text.EndsWith(':'))
            {
                MessageBox.Show($"Invalid value at EFiDrive: {EfiDrive.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (decimal.Equals(DiskNumber.Value, -1))
            {
                MessageBox.Show($"Error: DiskNumber is not set", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.Equals(DestinationDrive.Text, EfiDrive.Text) ||
                string.Equals(DestinationDrive.Text, ImageFilePath.Text) ||
                string.Equals(EfiDrive.Text, ImageFilePath.Text))
            {
                MessageBox.Show($"Error: DestinationDrive ({DestinationDrive.Text}) is the same as EfiDrive ({EfiDrive.Text}).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SystemDrives.FormatDisk((int)DiskNumber.Value, DestinationDrive.Text, EfiDrive.Text);
                NewDeploy.ApplyImage(ImageFilePath.Text, DestinationDrive.Text, (int)WindowsEditionIndex.Value);
            }
        }
    }
}
