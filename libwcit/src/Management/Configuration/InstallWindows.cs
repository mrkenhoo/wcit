using libwcit.Management.DiskManagement;
using libwcit.Management.ProcessManager;
using libwcit.Utilities.Deployment;
using System;

namespace libwcit.Management.Installer
{
    public partial class Configuration
    {
        public static int InstallWindows()
        {
            if (DiskNumber != -1 && DestinationDrive != null && EfiDrive != null)
            {
                SystemDrives.FormatDisk(DiskNumber, DestinationDrive, EfiDrive);
            }

            if (NewDeploy.ImageFile != null && DestinationDrive != null && WindowsEdition >= 0)
            {
                Console.WriteLine($"\nImage file: {NewDeploy.ImageFile}");
                Console.WriteLine($"\n==> Deploying Windows to drive {DestinationDrive} in disk {DiskNumber}, please wait...");
                NewDeploy.ApplyImage(NewDeploy.ImageFile, DestinationDrive, WindowsEdition);
            }

            if (DestinationDrive != null && EfiDrive != null)
            {
                Console.WriteLine($"\n==> Installing bootloader to drive {EfiDrive} in disk {DiskNumber}");
                NewDeploy.InstallBootloader(DestinationDrive, EfiDrive, "UEFI");
            }

            if (Worker.ExitCode == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
