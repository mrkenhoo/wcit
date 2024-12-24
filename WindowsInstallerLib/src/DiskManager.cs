using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.Versioning;

namespace WindowsInstallerLib
{
    /// <summary>
    /// Manages the disks on the system.
    /// </summary>
    [SupportedOSPlatform("windows")]
    internal class DiskManager
    {
        /// <summary>
        /// Formats the disk with the specified parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal static int FormatDisk(ref Parameters parameters)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(parameters.EfiDrive);
                ArgumentException.ThrowIfNullOrEmpty(parameters.DestinationDrive);

                switch (PrivilegesManager.IsAdmin())
                {
                    case true:
                        ProcessManager.StartDiskPartProcess(parameters.DiskNumber, parameters.EfiDrive, parameters.DestinationDrive);
                        return ProcessManager.ExitCode;

                    case false:
                        throw new UnauthorizedAccessException($"You do not have enough privileges to format the disk {parameters.DiskNumber}.");
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Lists all the disks on the system.
        /// </summary>
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

        /// <summary>
        /// Lists all disk on the system using DriveInfo.
        /// </summary>
        /// <returns></returns>
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
