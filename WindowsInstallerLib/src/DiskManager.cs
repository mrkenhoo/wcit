using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.Versioning;

namespace WindowsInstallerLib.Management
{
    [SupportedOSPlatform("windows")]
    partial class GetDisks
    {
        internal static int FormatDisk(ref InstallerParameters parameters)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(parameters.EfiDrive);
                ArgumentException.ThrowIfNullOrEmpty(parameters.DestinationDrive);

                switch (GetPrivileges.IsUserAdmin())
                {
                    case true:
                        NewProcess.StartDiskPartProcess(parameters.DiskNumber, parameters.EfiDrive, parameters.DestinationDrive);
                        return NewProcess.ExitCode;

                    case false:
                        throw new UnauthorizedAccessException($"You do not have enough privileges to format the disk {parameters.DiskNumber}.");
                }
            }
            catch
            {
                throw;
            }
        }

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

        internal static DriveInfo[] GetDisksT()
        {
            try
            {
                DriveInfo[] drives = DriveInfo.GetDrives();

                return drives;
            }
            catch (IOException)
            {
                throw;
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
