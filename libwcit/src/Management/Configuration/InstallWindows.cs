using System;
using libwcit.Management.DiskManagement;
using libwcit.Management.PrivilegesManager;
using libwcit.Utilities.Deployment;

namespace libwcit.Management.Installer
{
    public partial class Configuration
    {
        public static void InstallWindows(int DiskNumber, string DestinationDrive, string EfiDrive, string ImageFile, int WindowsEdition)
        {
            if (!GetPrivileges.IsUserAdmin())
            {
                throw new UnauthorizedAccessException("You must have Administrator privileges to make changes to the system.");
            }

            try
            {
                if (DiskNumber < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(DiskNumber), $"{nameof(DiskNumber)} cannot be set to less than 0.");
                }
                else if (WindowsEdition < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(WindowsEdition), $"{nameof(WindowsEdition)} cannot be null");
                }

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
