using System;
using System.IO;
using WindowsInstallerLib.Management.DiskManagement;
using WindowsInstallerLib.Utilities.Deployment;

namespace WindowsInstallerLib.Management.Installer
{
    public partial class NewInstallation
    {
        /// <summary>
        /// Deploys an image of Windows onto the specified drive.
        /// </summary>
        public static void InstallWindows()
        {
            try
            {
                if (DiskNumber.Equals(-1))
                {
                    throw new InvalidDataException("No disk number was specified, required to know where to install Windows at.");
                }

                if (string.IsNullOrWhiteSpace(EfiDrive))
                {
                    throw new InvalidDataException("No EFI drive was specified, required for the bootloader installation.");
                }

                ArgumentException.ThrowIfNullOrWhiteSpace(nameof(DestinationDrive));
                ArgumentException.ThrowIfNullOrWhiteSpace(nameof(ImageFilePath));
                ArgumentException.ThrowIfNullOrWhiteSpace(nameof(ImageIndex));
                ArgumentException.ThrowIfNullOrWhiteSpace(nameof(FirmwareType));

                switch (FirmwareType)
                {
                    case "UEFI":
                        break;
                    case "BIOS":
                        break;
                    default:
                        throw new InvalidDataException($"Invalid firmware type: {FirmwareType}");
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
