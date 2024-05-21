using System;
using WindowsInstallerLib.Management.DiskManagement;
using WindowsInstallerLib.Management.EFIManager;
using WindowsInstallerLib.Utilities.Deployment;

namespace WindowsInstallerLib.Management.Installer
{
    public partial class NewInstallation
    {
        /// <summary>
        /// Deploys an image of Windows onto the specified drive.
        /// </summary>
        /// <param name="DiskNumber"></param>
        /// <param name="EfiDrive"></param>
        /// <param name="DestinationDrive"></param>
        /// <param name="ImageFilePath"></param>
        /// <param name="ImageIndex"></param>
        public static void InstallWindows(int DiskNumber, string EfiDrive, string DestinationDrive, string ImageFilePath, int ImageIndex, string? FirmwareType)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(nameof(DiskNumber));
                ArgumentNullException.ThrowIfNull(nameof(EfiDrive));
                ArgumentNullException.ThrowIfNull(nameof(DestinationDrive));
                ArgumentNullException.ThrowIfNull(nameof(ImageFilePath));
                ArgumentNullException.ThrowIfNull(nameof(ImageIndex));
                
                if (string.IsNullOrEmpty(FirmwareType))
                {
                    switch (GetEFIInfo.IsEFI())
                    {
                        case true:
                            FirmwareType = "UEFI";
                            break;
                        case false:
                            FirmwareType = "BIOS";
                            break;
                    }
                }

                Disks.FormatDisk(DiskNumber, EfiDrive, DestinationDrive);

                Console.WriteLine($"\n==> Deploying Windows to drive {DestinationDrive} in disk {DiskNumber}, please wait...");
                NewDeploy.ApplyImage(ImageFilePath, DestinationDrive, ImageIndex);

                Console.WriteLine($"\n==> Installing bootloader to drive {EfiDrive} in disk {DiskNumber}");
                NewDeploy.InstallBootloader(DestinationDrive, EfiDrive, FirmwareType);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
