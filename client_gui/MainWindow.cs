using libwcit.Management.DiskManagement;
using libwcit.Utilities.Deployment;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Management;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace client_gui
{
    [SupportedOSPlatform("windows")]
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
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
                string? diskNumber = o["Index"].ToString();
                string? model = o["Model"].ToString();
                string? deviceID = o["DeviceID"].ToString();

                // Display information in the DiskList
                DiskList.Rows.Add(diskNumber, model, deviceID);

                // Sort by DiskNumber ascending
                DiskList.Sort(DiskList.Columns[0], ListSortDirection.Ascending);
            }
        }

        private void CloseApplication(object sender, EventArgs e)
        {
            Application.Exit();
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
            else if (string.Equals(DestinationDrive.Text, EfiDrive.Text) || string.Equals(DestinationDrive.Text, SourceDrive.Text) || string.Equals(EfiDrive.Text, SourceDrive.Text))
            {
                MessageBox.Show($"Error: DestinationDrive ({DestinationDrive.Text}) is the same as EfiDrive ({EfiDrive.Text}).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SystemDrives.FormatDrive((int)DiskNumber.Value, DestinationDrive.Text, EfiDrive.Text);
                NewDeploy.ApplyImage(SourceDrive.Text, DestinationDrive.Text, (int)WindowsEditionIndex.Value);
            }
        }
    }
}
