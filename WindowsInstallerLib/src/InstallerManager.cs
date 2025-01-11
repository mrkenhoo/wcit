using System;
using System.Globalization;
using System.IO;
using System.Runtime.Versioning;

namespace WindowsInstallerLib
{
    /// <summary>
    /// Contains the parameters required for installing Windows.
    /// </summary>
    /// <param name="DestinationDrive"></param>
    /// <param name="EfiDrive"></param>
    /// <param name="DiskNumber"></param>
    /// <param name="SourceDrive"></param>
    /// <param name="ImageIndex"></param>
    /// <param name="ImageFilePath"></param>
    /// <param name="InstallExtraDrivers"></param>
    /// <param name="FirmwareType"></param>
    [SupportedOSPlatform("windows")]
    public struct Parameters(string DestinationDrive,
                                    string EfiDrive,
                                    int DiskNumber,
                                    string SourceDrive,
                                    int ImageIndex,
                                    string ImageFilePath,
                                    bool InstallExtraDrivers,
                                    string FirmwareType)
    {
        public string DestinationDrive { get; set; } = DestinationDrive;
        public string EfiDrive { get; set; } = EfiDrive;
        public int DiskNumber { get; set; } = DiskNumber;
        public string SourceDrive { get; set; } = SourceDrive;
        public int ImageIndex { get; set; } = ImageIndex;
        public string ImageFilePath { get; set; } = ImageFilePath;
        public bool InstallExtraDrivers { get; set; } = InstallExtraDrivers;
        public string FirmwareType { get; set; } = FirmwareType;
    }

    /// <summary>
    /// Manages the installation of Windows.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public sealed class InstallerManager
    {
        /// <summary>
        /// Sets up the environment correctly for deploying Windows.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static void Configure(ref Parameters parameters)
        {
            #region DestinationDrive
            if (string.IsNullOrEmpty(parameters.DestinationDrive) ||
                string.IsNullOrWhiteSpace(parameters.DestinationDrive))
            {
                string p_DestinationDrive;

                Console.Write("\n==> Type the mountpoint to use for deploying Windows (e.g. Z:): ");
                try
                {
                    p_DestinationDrive = Console.ReadLine() ?? throw new ArgumentNullException(nameof(parameters), "DestinationDrive is null!");
                }
                catch (IOException)
                {
                    throw;
                }
                catch (OutOfMemoryException)
                {
                    throw;
                }
                catch (ArgumentNullException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }

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
            }
            #endregion

            #region EfiDrive
            if (string.IsNullOrEmpty(parameters.EfiDrive) ||
                string.IsNullOrWhiteSpace(parameters.EfiDrive))
            {
                string p_EfiDrive;

                Console.Write("\n==> Type the mountpoint to use for the bootloader (e.g. Y:): ");

                try
                {
                    p_EfiDrive = Console.ReadLine() ?? throw new ArgumentNullException(nameof(parameters), "EfiDrive is null!"); ;
                }
                catch (IOException)
                {
                    throw;
                }
                catch (OutOfMemoryException)
                {
                    throw;
                }
                catch (ArgumentNullException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }

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
            }
            #endregion

            #region DiskNumber
            if (string.IsNullOrEmpty(parameters.DiskNumber.ToString()) ||
                string.IsNullOrWhiteSpace(parameters.DiskNumber.ToString()))
            {
                int p_DiskNumber;

                try
                {
                    Console.WriteLine("\n==> These are the disks available on your system:");
                    DiskManager.ListAll();
                }
                catch (Exception)
                {
                    throw;
                }

                Console.Write("\n==> Please type the disk number to format (e.g. 0): ");
                try
                {
                    p_DiskNumber = Convert.ToInt32(Console.ReadLine(), CultureInfo.CurrentCulture);
                }
                catch (FormatException)
                {
                    throw;
                }
                catch (OverflowException)
                {
                    throw;
                }
                catch (ArgumentNullException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }

                parameters.DiskNumber = p_DiskNumber;
            }
            #endregion

            #region SourceDrive
            if (string.IsNullOrEmpty(parameters.SourceDrive) ||
                string.IsNullOrWhiteSpace(parameters.SourceDrive))
            {
                string? p_SourceDrive;

                Console.Write("\n==> Specify the mountpount where the source are mounted at (e.g. X:): ");
                try
                {
                    p_SourceDrive = Console.ReadLine();
                }
                catch (IOException)
                {
                    throw;
                }
                catch (OutOfMemoryException)
                {
                    throw;
                }
                catch (ArgumentNullException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }

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
            }
            #endregion

            #region ImageFilePath
            if (string.IsNullOrEmpty(parameters.ImageFilePath) ||
                string.IsNullOrWhiteSpace(parameters.ImageFilePath))
            {
                string p_ImageFilePath = DeployManager.GetImageFile(ref parameters);

                Console.WriteLine($"\nImage file path has been set to {p_ImageFilePath}.");

                parameters.ImageFilePath = p_ImageFilePath;
            }
            #endregion

            #region ImageIndex
            if (string.IsNullOrEmpty(parameters.ImageIndex.ToString()) ||
                string.IsNullOrWhiteSpace(parameters.ImageIndex.ToString()))
            {
                DeployManager.GetImageInfo(ref parameters);

                Console.Write("\n==> Type the index number of the Windows edition you wish to install (e.g. 1): ");
                string? SelectedIndex = Console.ReadLine();

                if (string.IsNullOrEmpty(SelectedIndex) || string.IsNullOrWhiteSpace(SelectedIndex))
                {
                    throw new ArgumentException("No Windows edition was specified.");
                }

                parameters.ImageIndex = Convert.ToInt32(SelectedIndex, CultureInfo.CurrentCulture);
            }
            #endregion

            #region FirmwareType
            if (string.IsNullOrEmpty(parameters.FirmwareType) ||
                string.IsNullOrWhiteSpace(parameters.FirmwareType))
            {
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
                    default:
                        throw new InvalidDataException(nameof(parameters.FirmwareType));
                }
            }
            #endregion
        }

        /// <summary>
        /// Installs Windows on the specified disk.
        /// </summary>
        /// <param name="parameters"></param>
        [SupportedOSPlatform("windows")]
        public static void InstallWindows(ref Parameters parameters)
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
                DeployManager.ApplyImage(ref parameters);
                DeployManager.InstallBootloader(ref parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
