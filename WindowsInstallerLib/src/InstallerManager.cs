using System;
using System.Globalization;
using System.IO;
using System.Runtime.Versioning;
using WindowsInstallerLib.Management;
using WindowsInstallerLib.Utilities;

namespace WindowsInstallerLib
{
    [SupportedOSPlatform("windows")]
    public struct InstallerParameters
    {
        public string? DestinationDrive;
        public string? EfiDrive;
        public int DiskNumber = -1;
        public string? SourceDrive;
        public int ImageIndex = -1;
        public string? ImageFilePath;
        public bool InstallExtraDrivers;
        public string? FirmwareType;

        public InstallerParameters() { }
    }

    [SupportedOSPlatform("windows")]
    public partial class InstallerManager
    {
        /// <summary>
        /// Sets up the environment correctly for deploying Windows.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static void ConfigureInstaller(ref InstallerParameters parameters)
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

            parameters.DestinationDrive = p_DestinationDrive;
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

            parameters.EfiDrive = p_EfiDrive;
            #endregion

            #region DiskNumber
            Console.WriteLine("\n==> These are the disks available on your system:");
            DiskManager.ListAll();

            Console.Write("\n==> Please type the disk number to format (e.g. 0): ");
            int p_DiskNumber = Convert.ToInt32(Console.ReadLine(), CultureInfo.CurrentCulture);

            parameters.DiskNumber = p_DiskNumber;
            #endregion

            #region SourceDrive
            Console.Write("\n==> Specify the mountpount where the source are mounted at (e.g. X:): ");
            string? p_SourceDrive = Console.ReadLine();


            ArgumentException.ThrowIfNullOrWhiteSpace(p_SourceDrive);

            if (p_SourceDrive.StartsWith(':'))
            {
                throw new ArgumentException(@$"Invalid source drive {p_SourceDrive}, it must have a colon at the end not at the beginning. For example: 'H:'.");
            }
            else if (!p_SourceDrive.EndsWith(':'))
            {
                throw new ArgumentException($"Invalid source drive {p_SourceDrive}, it must have a colon. For example: 'H:'.");
            }

            parameters.SourceDrive = p_SourceDrive;
            #endregion

            #region ImageFilePath
            string p_ImageFilePath = DeploymentManager.GetImageFile(ref parameters);

            Console.WriteLine($"\nImage file path has been set to {p_ImageFilePath}.");

            parameters.ImageFilePath = p_ImageFilePath;
            #endregion

            #region ImageIndex
            if (parameters.ImageIndex == -1)
            {
                DeploymentManager.GetImageInfo(ref parameters);

                Console.Write("\n==> Type the index number of the Windows edition you wish to install (e.g. 1): ");
                string? SelectedIndex = Console.ReadLine();

                if (string.IsNullOrEmpty(SelectedIndex) ||string.IsNullOrWhiteSpace(SelectedIndex))
                {
                    throw new ArgumentException("No Windows edition was specified.");
                }

                parameters.ImageIndex = Convert.ToInt32(SelectedIndex, CultureInfo.CurrentCulture);
            }

            #endregion

            #region FirmwareType
            switch (SystemInfoManager.IsEFI())
            {
                case true:
                    parameters.FirmwareType = "UEFI";
                    Console.WriteLine($"\nThe installer has set the firmware type to {parameters.FirmwareType}.", ConsoleColor.Yellow);
                    break;
                case false:
                    parameters.FirmwareType = "BIOS";
                    Console.WriteLine($"\nThe installer has set the firmware type to {parameters.FirmwareType}.", ConsoleColor.Yellow);
                    break;
            }
            #endregion
        }

        /// <summary>
        /// Deploys an image of Windows onto the specified drive.
        /// </summary>
        public static void InstallWindows(ref InstallerParameters parameters)
        {
            try
            {
                if (parameters.DiskNumber.Equals(-1))
                {
                    throw new InvalidDataException("No disk number was specified, required to know where to install Windows at.");
                }

                if (string.IsNullOrWhiteSpace(parameters.EfiDrive))
                {
                    throw new InvalidDataException("No EFI drive was specified, required for the bootloader installation.");
                }

                ArgumentException.ThrowIfNullOrWhiteSpace(parameters.DestinationDrive);
                ArgumentException.ThrowIfNullOrWhiteSpace(parameters.ImageFilePath);
                ArgumentOutOfRangeException.ThrowIfEqual(parameters.ImageIndex, -1);
                ArgumentException.ThrowIfNullOrWhiteSpace(parameters.FirmwareType);

                switch (parameters.FirmwareType)
                {
                    case "UEFI":
                        break;
                    case "BIOS":
                        break;
                    default:
                        throw new InvalidDataException($"Invalid firmware type: {parameters.FirmwareType}");
                }

                DiskManager.FormatDisk(ref parameters);
                DeploymentManager.ApplyImage(ref parameters);
                DeploymentManager.InstallBootloader(ref parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
