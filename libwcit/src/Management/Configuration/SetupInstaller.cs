using System;
using System.Globalization;
using System.IO;
using System.Runtime.Versioning;
using libwcit.Management.DiskManagement;
using libwcit.Utilities.Deployment;

namespace libwcit.Management.Installer
{
    [SupportedOSPlatform("windows")]
    public partial class Configuration
    {
        /// <summary>
        /// Configures the installer to get the deployment ready.
        /// </summary>
        /// <param name="InstallExtraDrivers"/>
        /// <exception cref="InvalidDataException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetupInstaller(bool InstallExtraDrivers = false)
        {
            if (DestinationDrive == null)
            {
                Console.Write("\n==> Type the mountpoint to use for deploying Windows (e.g. Z:): ");
                DestinationDrive = Console.ReadLine();

                if (string.IsNullOrEmpty(DestinationDrive))
                {
                    Console.Write("No destination drive was specified.\n\nPress ENTER to quit the program: ");
                    Console.ReadLine();
                    Environment.Exit(1);
                }
                else if (DestinationDrive.StartsWith(':'))
                {
                    throw new InvalidDataException(@$"Invalid source drive {SourceDrive}, it must have a colon at the end not at the beginning. For example: 'H:'.");
                }
                else if (!DestinationDrive.Contains(':'))
                {
                    throw new ArgumentException($"Invalid source drive {SourceDrive}, it must have a colon. For example: 'H:'.");
                }
            }

            if (EfiDrive == null)
            {
                Console.Write("\n==> Type a mountpoint for installing the bootloader at (e.g. K:): ");
                EfiDrive = Console.ReadLine();

                if (string.IsNullOrEmpty(EfiDrive))
                {
                    throw new ArgumentException("No EFI drive was specified");
                }
                else if (EfiDrive.StartsWith(':'))
                {
                    throw new InvalidDataException(@$"Invalid source drive {EfiDrive}, it must have a colon at the end not at the beginning. For example: 'H:'.");
                }
                else if (!EfiDrive.Contains(':'))
                {
                    throw new ArgumentException($"Invalid EFI drive {EfiDrive}, it must have a colon. For example: 'H:'.");
                }
            }

            if (DiskNumber == -1 || DiskNumber !>= 0)
            {
                Console.WriteLine("\n==> These are the disks available on your system:");
                SystemDrives.GetDisks();

                Console.Write("\n==> Please type the disk number to format (e.g. 0): ");
                string? SelectedDisk = Console.ReadLine();

                if (!string.IsNullOrEmpty(SelectedDisk))
                {
                    DiskNumber = Convert.ToInt32(SelectedDisk, CultureInfo.CurrentCulture);
                }
                else
                {
                    throw new ArgumentException("No disk was chosen to formatting.");
                }
            }

            if (SourceDrive == null)
            {
                Console.Write("\n==> Type the letter where the ISO is mounted at (e.g. D:): ");
                SourceDrive = Console.ReadLine();

                if (string.IsNullOrEmpty(SourceDrive))
                {
                    throw new ArgumentException("No source drive was specified.\n\nPress ENTER to quit the program.");
                }
                else if (SourceDrive.StartsWith(':'))
                {
                    throw new InvalidDataException(@$"Invalid source drive {SourceDrive}, it must have a colon at the end not at the beginning. For example: 'H:'.");
                }
                else if (!SourceDrive.Contains(':'))
                {
                   throw new ArgumentException($"Invalid source drive {SourceDrive}, it must end with a colon. For example: 'H:'.");
                }

                NewDeploy.ImageFile = NewDeploy.GetImageFile(SourceDrive);
                NewDeploy.GetImageInfo(NewDeploy.ImageFile);
            }
            else
            {
                NewDeploy.ImageFile = NewDeploy.GetImageFile(SourceDrive);
                NewDeploy.GetImageInfo(SourceDrive);
            }

            if (WindowsEdition !>= 0)
            {
                NewDeploy.GetImageInfo(NewDeploy.ImageFile);

                Console.Write("==> Type the index number of the Windows edition you wish to install (e.g. 1): ");
                string? SelectedIndex = Console.ReadLine();

                if (!string.IsNullOrEmpty(SelectedIndex))
                {
                    WindowsEdition = Convert.ToInt32(SelectedIndex, CultureInfo.CurrentCulture);
                }
                else
                {
                    throw new ArgumentException("No Windows edition was specified.");
                }
            }

            if (!InstallExtraDrivers)
            {
                Console.Write("==> Do you want to add any extra drivers to Windows before using it?: ");
                string? UserWantsDrivers = Console.ReadLine();

                if (UserWantsDrivers != null)
                {
                    if (!string.IsNullOrWhiteSpace(UserWantsDrivers) && UserWantsDrivers.Contains("yes") || UserWantsDrivers.Contains('y'))
                    {
                        Console.Write("==> Type a drive letter or directory where to look drivers for: ");
                        string? DriversSource = Console.ReadLine();
                        if (!string.IsNullOrEmpty(DriversSource))
                        {
                            NewDeploy.AddDrivers(DestinationDrive, DriversSource);
                        }
                    }
                }
                else
                {
                    throw new ArgumentException(UserWantsDrivers);
                }
            }

            Console.Write($"""
                Destination drive is set to '{DestinationDrive}'
                EFI drive is set to '{EfiDrive}'
                Disk number is set to '{DiskNumber}'
                Source drive is set to '{SourceDrive}'
                Windows edition (Index) is set to '{WindowsEdition}'
                """);

            Console.WriteLine($"\nIf this is correct, press any key to continue...");
            Console.ReadKey();
        }
    }
}
