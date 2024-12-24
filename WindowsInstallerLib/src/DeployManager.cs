using System;
using System.IO;
using System.Runtime.Versioning;
using Microsoft.Dism;

namespace WindowsInstallerLib
{
    /// <summary>
    /// Manages the deployment of Windows to a new drive.
    /// </summary>
    [SupportedOSPlatform("windows")]
    internal static class DeployManager
    {
        /// <summary>
        /// Adds drivers to the Windows image.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="DriversSource"></param>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        internal static void AddDrivers(ref Parameters parameters, string DriversSource)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(parameters.ImageFilePath, nameof(parameters.ImageFilePath));
            ArgumentException.ThrowIfNullOrWhiteSpace(DriversSource, nameof(DriversSource));
            ArgumentException.ThrowIfNullOrWhiteSpace(parameters.DestinationDrive, nameof(parameters.DestinationDrive));

            if (!Directory.Exists(parameters.DestinationDrive))
            {
                throw new DirectoryNotFoundException($"Could not find the directory: {parameters.DestinationDrive}");
            }

            if (!PrivilegesManager.IsAdmin())
            {
                throw new UnauthorizedAccessException("You do not have enough privileges to initialize the DISM API.");
            }

            try
            {
                DismApi.Initialize(DismLogLevel.LogErrorsWarningsInfo);
                DismSession session = DismApi.OpenOfflineSession(parameters.DestinationDrive);

                if (DriversSource.GetType().IsArray)
                {
                    DismApi.AddDriversEx(session, DriversSource, forceUnsigned: false, recursive: true);
                }
                else
                {
                    DismApi.AddDriver(session, DriversSource, forceUnsigned: false);
                }
            }
            finally
            {
                DismApi.Shutdown();
            }
        }

        /// <summary>
        /// Deploys an image of Windows to the specified <paramref name="DestinationDrive"/>.
        /// What gets installed is specified by <paramref name="SourceDrive"/> and the <paramref name="Index"/>.
        /// </summary>
        /// <param name="parameters"></param>
        internal static int ApplyImage(ref Parameters parameters)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(parameters.DestinationDrive, nameof(parameters.DestinationDrive));
            ArgumentException.ThrowIfNullOrWhiteSpace(parameters.ImageFilePath, nameof(parameters.ImageFilePath));
            ArgumentOutOfRangeException.ThrowIfEqual(parameters.DiskNumber, -1, nameof(parameters.DiskNumber));
            ArgumentOutOfRangeException.ThrowIfEqual(parameters.ImageIndex, -1, nameof(parameters.ImageIndex));

            if (Directory.Exists($@"{parameters.DestinationDrive}\windows"))
            {
                Console.Error.WriteLine("Windows seems to be already deployed, not overwriting it.");
                return 1;
            }

            if (!PrivilegesManager.IsAdmin())
            {
                throw new UnauthorizedAccessException($"You do not have enough privileges to deploy Windows to {parameters.DestinationDrive}.");
            }

            Console.WriteLine($"\n==> Deploying Windows to drive {parameters.DestinationDrive} in disk {parameters.DiskNumber}, please wait...");
            ProcessManager.StartDismProcess(@$"/apply-image /imagefile:{parameters.ImageFilePath} /applydir:{parameters.DestinationDrive} /index:{parameters.ImageIndex} /verify");
            return ProcessManager.ExitCode;
        }

        /// <summary>
        /// Gets the image file from the source drive.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal static string GetImageFile(ref Parameters parameters)
        {
            try
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(nameof(parameters.SourceDrive));
                ArgumentException.ThrowIfNullOrWhiteSpace(nameof(parameters.ImageFilePath));

                if (File.Exists(@$"{parameters.SourceDrive}\sources\install.esd"))
                {
                    parameters.ImageFilePath = @$"{parameters.SourceDrive}\sources\install.esd";
                }
                else if (File.Exists(@$"{parameters.SourceDrive}\sources\install.wim"))
                {
                    parameters.ImageFilePath = @$"{parameters.SourceDrive}\sources\install.wim";
                }
                else
                {
                    throw new FileNotFoundException($"Could not find a valid image file at {parameters.SourceDrive}.");
                }

                return parameters.ImageFilePath;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all Windows editions available using DISM, if any.
        /// </summary>
        /// <param name="parameters"></param>
        internal static void GetImageInfo(ref Parameters parameters)
        {
            ArgumentException.ThrowIfNullOrEmpty(parameters.ImageFilePath, nameof(parameters));

            if (!PrivilegesManager.IsAdmin())
            {
                throw new UnauthorizedAccessException("You do not have enough privileges to initialize the DISM API.");
            }

            try
            {
                DismApi.Initialize(DismLogLevel.LogErrorsWarnings);

                DismImageInfoCollection images = DismApi.GetImageInfo(parameters.ImageFilePath);

                switch (images.Count)
                {
                    case > 1:
                        Console.WriteLine($"\nFound {images.Count} images in {parameters.ImageFilePath}, shown below.\n", ConsoleColor.Yellow);
                        break;
                    case 1:
                        Console.WriteLine($"\nFound {images.Count} image in {parameters.ImageFilePath}, shown below.\n", ConsoleColor.Yellow);
                        break;
                    case 0:
                        Console.WriteLine($"\nNo images were found in {parameters.ImageFilePath}\n", ConsoleColor.Red);
                        throw new InvalidDataException($"images.Count is {images.Count}. This is considered to be invalid, the program cannot continue.");
                }

                foreach (DismImageInfo image in images)
                {
                    Console.WriteLine($"Index: {image.ImageIndex}");
                    Console.WriteLine($"Name: {image.ImageName}");
                    Console.WriteLine($"Size: {image.ImageSize}");
                    Console.WriteLine($"Arch: {image.Architecture}\n");
                }
            }
            catch (DismException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DismApi.Shutdown();
            }
        }

        /// <summary>
        /// Gets all Windows editions available using DISM, if any.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal static DismImageInfoCollection GetImageInfoT(ref Parameters parameters)
        {
            try
            {
                if (string.IsNullOrEmpty(parameters.ImageFilePath))
                {
                    throw new FileNotFoundException("No image file was specified.", parameters.ImageFilePath);
                }

                switch (PrivilegesManager.IsAdmin())
                {
                    case true:
                        DismApi.Initialize(DismLogLevel.LogErrorsWarnings);
                        break;
                    case false:
                        throw new UnauthorizedAccessException("You do not have enough privileges to initialize the DISM API.");
                }

                DismImageInfoCollection images = DismApi.GetImageInfo(parameters.ImageFilePath);

                return images;
            }
            catch (DismException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DismApi.Shutdown();
            }
        }

        /// <summary>
        /// Installs the bootloader to the EFI drive of a new Windows installation.
        /// </summary>
        /// <param name="InstallationSettings"/>
        /// <returns></returns>
        internal static int InstallBootloader(ref Parameters parameters)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(parameters.DestinationDrive, nameof(parameters.DestinationDrive));
            ArgumentException.ThrowIfNullOrWhiteSpace(parameters.EfiDrive, nameof(parameters.EfiDrive));
            ArgumentException.ThrowIfNullOrWhiteSpace(parameters.FirmwareType, nameof(parameters.FirmwareType));

            if (!PrivilegesManager.IsAdmin())
            {
                throw new UnauthorizedAccessException($"You do not have enough privileges to install the bootloader to {parameters.EfiDrive}");
            }

            try
            {
                if (Directory.Exists(@$"{parameters.EfiDrive}\EFI\Boot") || Directory.Exists($@"{parameters.EfiDrive}\EFI\Microsoft"))
                {
                    throw new IOException($"The drive letter {parameters.EfiDrive} is already in use.");
                }
                else
                {
                    if (!Directory.Exists(@$"{parameters.DestinationDrive}windows"))
                    {
                        throw new DirectoryNotFoundException(@$"The directory {parameters.DestinationDrive}windows does not exist!");
                    }

                    Console.WriteLine($"Firmware type is set to: {parameters.FirmwareType}");
                    Console.WriteLine($"\n==> Installing bootloader to drive {parameters.EfiDrive} in disk {parameters.DiskNumber}");
                    ProcessManager.StartCmdProcess("bcdboot", @$"{parameters.DestinationDrive}\windows /s {parameters.EfiDrive} /f {parameters.FirmwareType}");
                }
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            
            return ProcessManager.ExitCode;
        }
    }
}
