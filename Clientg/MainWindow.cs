using System;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace client_gui
{
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
                string diskNumber = o["Index"].ToString();
                string model = o["Model"].ToString();
                string deviceID = o["DeviceID"].ToString();

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

        }
    }
}
