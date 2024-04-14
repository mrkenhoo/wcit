using System;
using WindowsInstallerLib.Management.DiskManagement;
using WindowsInstallerLib.Management.PrivilegesManager;
using WindowsInstallerLib.Utilities.Deployment;

namespace WindowsInstallerLib.Management.Installer
{
    public partial class Configuration
    {
        /// <summary>
        /// Installs Windows onto the specified <paramref name="DestinationDrive"/>.
        /// </summary>
        /// <param name="DiskNumber"></param>
        /// <param name="DestinationDrive"></param>
        /// <param name="EfiDrive"></param>
        /// <param name="ImageFile"></param>
        /// <param name="WindowsEdition"></param>
        ///
        public static void InstallWindows(int DiskNumber, string DestinationDrive, string EfiDrive, string ImageFile, int WindowsEdition)
        {
            try
            {
                if (!GetPrivileges.IsUserAdmin())
                {
                    throw new UnauthorizedAccessException("You must have Administrator privileges to make changes to the system.");
                }

                ArgumentOutOfRangeException.ThrowIfLessThan(0, DiskNumber);
                ArgumentOutOfRangeException.ThrowIfLessThan(0, WindowsEdition);

                SystemDrives.FormatDisk(DiskNumber, DestinationDrive, EfiDrive);

                Console.WriteLine($"\nImage file: {NewDeploy.ImageFile}");
                Console.WriteLine($"==> Deploying Windows to drive {DestinationDrive} in disk {DiskNumber}, please wait...");
                NewDeploy.ApplyImage(ImageFile, DestinationDrive, WindowsEdition);

                Console.WriteLine($"\n==> Installing bootloader to drive {EfiDrive} in disk {DiskNumber}");
                NewDeploy.InstallBootloader(DestinationDrive, EfiDrive, "UEFI");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
