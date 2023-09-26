using System;
using System.Linq;
using System.Management;

namespace Runtime.Management.DiskManagement
{
    partial class SystemDrives
    {
        internal static void ListAll()
        {
            try
            {
                WqlObjectQuery DeviceTable = new("SELECT * FROM Win32_DiskDrive");
                ManagementObjectSearcher DeviceInfo = new(DeviceTable);
                foreach (ManagementObject o in DeviceInfo.Get().Cast<ManagementObject>())
                {
                    Console.WriteLine("Disk number = " + o["Index"]);
                    Console.WriteLine("Model = " + o["Model"]);
                    Console.WriteLine("DeviceID = " + o["DeviceID"]);
                    Console.WriteLine("");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
