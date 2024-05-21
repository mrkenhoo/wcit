using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Versioning;
using WindowsInstallerLib.Management.DiskManagement;
using WindowsInstallerLib.Management.EFIManager;
using WindowsInstallerLib.Utilities.Deployment;

namespace WindowsInstallerLib.Management.Installer
{
    [SupportedOSPlatform("windows")]
    public partial class NewInstallation
    {
        /// <summary>
        /// Sets up the environment correctly for deploying Windows.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static void ConfigureInstaller()
        {
            #region DestinationDrive
            Console.Write("\n==> Type the mountpoint to use for deploying Windows (e.g. Z:): ");
            string? p_DestinationDrive = Console.ReadLine();

            ArgumentException.ThrowIfNullOrWhiteSpace(p_DestinationDrive);

            if (p_DestinationDrive.StartsWith(':'))
            {
                throw new ArgumentException(@$"Invalid source drive {p_DestinationDrive}, it must have a colon at the end not at the beginning. For example: 'Z:'.");
            }
            else if (!p_DestinationDrive.EndsWith(':'))
            {
                throw new ArgumentException($"Invalid source drive {p_DestinationDrive}, it must have a colon. For example: 'Z:'.");
            }

            DestinationDrive = p_DestinationDrive;
            #endregion

            #region EfiDrive
            Console.Write("\n==> Type the mountpoint to use for the bootloader (e.g. Y:): ");
            string? p_EfiDrive = Console.ReadLine();

            ArgumentException.ThrowIfNullOrWhiteSpace(p_EfiDrive);

            if (p_EfiDrive.StartsWith(':'))
            {
                throw new ArgumentException(@$"Invalid EFI drive {p_EfiDrive}, it must have a colon at the end not at the beginning. For example: 'Y:'.");
            }
            else if (!p_EfiDrive.EndsWith(':'))
            {
                throw new ArgumentException($"Invalid EFI drive {p_EfiDrive}, it must have a colon. For example: 'Y:'.");
            }

            EfiDrive = p_EfiDrive;
            #endregion

            #region DiskNumber
            Console.WriteLine("\n==> These are the disks available on your system:");
            Disks.GetDisks();

            Console.Write("\n==> Please type the disk number to format (e.g. 0): ");
            int p_DiskNumber = Convert.ToInt32(Console.ReadLine());

            DiskNumber = p_DiskNumber;
            #endregion

            #region SourceDrive
            Console.Write("\n==> Specify the mountpount where the source are mounted at (e.g. X:): ");
            string? p_SourceDrive = Console.ReadLine();

            if (string.IsNullOrEmpty(p_SourceDrive))
            {
                throw new ArgumentNullException(nameof(p_SourceDrive), "Value cannot be null.");
            }
            else
            {
                if (p_SourceDrive.StartsWith(':'))
                {
                    throw new ArgumentException(@$"Invalid source drive {p_SourceDrive}, it must have a colon at the end not at the beginning. For example: 'H:'.");
                }
                else if (!p_SourceDrive.EndsWith(':'))
                {
                    throw new ArgumentException($"Invalid source drive {p_SourceDrive}, it must have a colon. For example: 'H:'.");
                }
            }

            SourceDrive = p_SourceDrive;
            #endregion

            #region ImageFilePath
            string p_ImageFilePath = NewDeploy.GetImageFile(p_SourceDrive);

            Console.WriteLine($"\nImage file path has been set to {p_ImageFilePath}.");

            ImageFilePath = p_ImageFilePath;
            #endregion

            #region ImageIndex
            int p_ImageIndex = -1;

            if (ImageIndex !>= 0)
            {
                NewDeploy.GetImageInfo(ImageFilePath);

                Console.Write("\n==> Type the index number of the Windows edition you wish to install (e.g. 1): ");
                string? SelectedIndex = Console.ReadLine();

                if (string.IsNullOrEmpty(SelectedIndex) ||string.IsNullOrWhiteSpace(SelectedIndex))
                {
                    throw new ArgumentException("No Windows edition was specified.");
                }

                p_ImageIndex = Convert.ToInt32(SelectedIndex, CultureInfo.CurrentCulture);
            }

            ImageIndex = p_ImageIndex;
            #endregion

            #region FirmwareType
            switch (GetEFIInfo.IsEFI())
            {
                case true:
                    FirmwareType = "UEFI";
                    Console.WriteLine($"\nThe installer has set the firmware type to {FirmwareType}.", ConsoleColor.Yellow);
                    break;
                case false:
                    FirmwareType = "BIOS";
                    Console.WriteLine($"\nThe installer has set the firmware type to {FirmwareType}.", ConsoleColor.Yellow);
                    break;
            }
            #endregion
        }
    }
}
