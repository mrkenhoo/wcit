using Microsoft.Dism;
using System;
using System.IO;
using System.Runtime.Versioning;
using WindowsInstallerLib.Management;

namespace WindowsInstallerLib.Utilities
{
    [SupportedOSPlatform("windows")]
    partial class DeploymentManager
    {
        /// <summary>
        /// Installs drivers to an offline Windows image.
        /// </summary>
        /// <param name="DestinationDrive"></param>
        /// <param name="DriversSource"></param>
        internal static void AddDrivers(ref InstallerParameters parameters, string DriversSource)
        {
            try
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(parameters.ImageFilePath, nameof(parameters.ImageFilePath));
                ArgumentException.ThrowIfNullOrWhiteSpace(DriversSource, nameof(DriversSource));
                ArgumentException.ThrowIfNullOrWhiteSpace(parameters.DestinationDrive, nameof(parameters.DestinationDrive));

                if (!Directory.Exists(parameters.DestinationDrive))
                {
                    throw new DirectoryNotFoundException($"Could not find the directory: {parameters.DestinationDrive}");
                }

                switch (PrivilegesManager.IsUserAdmin())
                {
                    case true:
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
                        catch (DismRebootRequiredException)
                        {
                            throw;
                        }
                        catch (DirectoryNotFoundException)
                        {
                            throw;
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
                        break;
                    case false:
                        throw new UnauthorizedAccessException("You do not have enough privileges to initialize the DISM API.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deploys an image of Windows to the specified <paramref name="DestinationDrive"/>.
        /// What gets installed is specified by <paramref name="SourceDrive"/> and the <paramref name="Index"/>.
        /// </summary>
        /// <param name="SourceDrive"></param>
        /// <param name="DestinationDrive"></param>
        /// <param name="Index"></param>
        /// <exception cref="ArgumentException"></exception>
        internal static int ApplyImage(ref InstallerParameters parameters)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(parameters.DestinationDrive, nameof(parameters.DestinationDrive));
            ArgumentException.ThrowIfNullOrWhiteSpace(parameters.ImageFilePath, nameof(parameters.ImageFilePath));

            ArgumentOutOfRangeException.ThrowIfEqual(parameters.DiskNumber, -1, nameof(parameters.DiskNumber));
            ArgumentOutOfRangeException.ThrowIfEqual(parameters.ImageIndex, -1, nameof(parameters.ImageIndex));

            try
            {
                if (!Directory.Exists($@"{parameters.DestinationDrive}\windows"))
                {
                    switch (PrivilegesManager.IsUserAdmin())
                    {
                        case true:
                            Console.WriteLine($"\n==> Deploying Windows to drive {parameters.DestinationDrive} in disk {parameters.DiskNumber}, please wait...");
                            ProcessManager.StartDismProcess(@$"/apply-image /imagefile:{parameters.ImageFilePath} /applydir:{parameters.DestinationDrive} /index:{parameters.ImageIndex} /verify");
                            return ProcessManager.ExitCode;
                        case false:
                            throw new UnauthorizedAccessException($"You do not have enough privileges to deploy Windows to {parameters.DestinationDrive}.");
                    }
                }
                else
                {
                    Console.Error.WriteLine("Windows seems to be already deployed, not overwriting it.");
                    return 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Searches for a valid image file and return it's full path.
        /// </summary>
        /// <param name="InstallationSettings"></param>
        /// <returns></returns>
        internal static string GetImageFile(ref InstallerParameters parameters)
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
        internal static void GetImageInfo(ref InstallerParameters parameters)
        {
            ArgumentException.ThrowIfNullOrEmpty(parameters.ImageFilePath, nameof(parameters.ImageFilePath));

            switch (PrivilegesManager.IsUserAdmin())
            {
                case true:
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
                    break;
                case false:
                    throw new UnauthorizedAccessException("You do not have enough privileges to initialize the DISM API.");
            }
        }

        /// <summary>
        /// Gets all Windows editions available using DISM, if any.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal static DismImageInfoCollection GetImageInfoT(ref InstallerParameters parameters)
        {
            try
            {
                if (string.IsNullOrEmpty(parameters.ImageFilePath))
                {
                    throw new FileNotFoundException("No image file was specified.", parameters.ImageFilePath);
                }

                switch (PrivilegesManager.IsUserAdmin())
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
        internal static int InstallBootloader(ref InstallerParameters parameters)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(parameters.DestinationDrive, nameof(parameters.DestinationDrive));
            ArgumentException.ThrowIfNullOrWhiteSpace(parameters.EfiDrive, nameof(parameters.EfiDrive));
            ArgumentException.ThrowIfNullOrWhiteSpace(parameters.FirmwareType, nameof(parameters.FirmwareType));

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

                    switch (PrivilegesManager.IsUserAdmin())
                    {
                        case true:
                            Console.WriteLine($"\n==> Installing bootloader to drive {parameters.EfiDrive} in disk {parameters.DiskNumber}");
                            ProcessManager.StartCmdProcess("bcdboot", @$"{parameters.DestinationDrive}\windows /s {parameters.EfiDrive} /f {parameters.FirmwareType}");
                            return ProcessManager.ExitCode;
                        case false:
                            throw new UnauthorizedAccessException($"You do not have enough privileges to install the bootloader to {parameters.EfiDrive}.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
