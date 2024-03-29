﻿using System;
using System.Linq;
using System.Management;
using System.Runtime.Versioning;

namespace libwcit.Management.DiskManagement
{
    [SupportedOSPlatform("windows")]
    public partial class SystemDrives
    {
        /// <summary>
        /// Retrieves all disks available in the system and prints it out.
        /// </summary>
        public static void GetDisks()
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
