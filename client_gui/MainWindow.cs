using libwcit.Utilities.Deployment;
using System.ComponentModel;
using System.Data;
using System.Management;
using System.Runtime.Versioning;

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
            if (string.IsNullOrEmpty(DestinationDrive.Text))
            {
                throw new ArgumentNullException(nameof(DestinationDrive));
            }
            else if (string.IsNullOrEmpty(EfiDrive.Text))
            {
                throw new ArgumentNullException(nameof(EfiDrive));
            }
            else if (string.IsNullOrEmpty(DiskNumber.Text))
            {
                throw new ArgumentNullException(nameof(SourceDrive));
            }

            MessageBox.Show($"""
                Destination drive: {DestinationDrive.Text}
                EFI drive: {EfiDrive.Text}
                Disk number: {DiskNumber.Text}
                Source drive: {SourceDrive.Text}
                """);
        }
    }
}
