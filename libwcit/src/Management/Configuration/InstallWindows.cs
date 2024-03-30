using System;
using libwcit.Management.DiskManagement;
using libwcit.Utilities.Deployment;

namespace libwcit.Management.Installer
{
    public partial class Configuration
    {
        public static void InstallWindows(int? DiskNumber, string? DestinationDrive, string? EfiDrive, string? ImageFile, int? WindowsEdition)
        {
            try
            {
                if (DiskNumber == null)
                {
                    throw new ArgumentNullException(nameof(DiskNumber), $"{nameof(DiskNumber)} cannot be null");
                }
                else if (DiskNumber < 0)
                {

                }
                ArgumentException.ThrowIfNullOrWhiteSpace(nameof(DestinationDrive));
                ArgumentException.ThrowIfNullOrWhiteSpace(nameof(EfiDrive));
                ArgumentException.ThrowIfNullOrWhiteSpace(nameof(ImageFile));
                ArgumentOutOfRangeException.ThrowIfLessThan(WindowsEdition, 0);

                SystemDrives.FormatDisk((int)DiskNumber, DestinationDrive, EfiDrive);

                Console.WriteLine($"\nImage file: {NewDeploy.ImageFile}");
                Console.WriteLine($"==> Deploying Windows to drive {DestinationDrive} in disk {DiskNumber}, please wait...");
                NewDeploy.ApplyImage(ImageFile, DestinationDrive, (int)WindowsEdition);

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
