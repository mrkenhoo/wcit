using Microsoft.Dism;
using System;
using System.IO;
using System.Runtime.Versioning;
using WindowsInstallerLib.Management;

namespace WindowsInstallerLib.Utilities
{
    [SupportedOSPlatform("windows")]
    partial class NewDeploy
    {
        /// <summary>
        /// Installs drivers to an offline Windows image.
        /// </summary>
        /// <param name="DestinationDrive"></param>
        /// <param name="DriversSource"></param>
        internal static void AddDrivers(string DestinationDrive, string ImageFile, string DriversSource)
        {
            try
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(ImageFile, nameof(ImageFile));
                ArgumentException.ThrowIfNullOrWhiteSpace(DriversSource, nameof(DriversSource));
                ArgumentException.ThrowIfNullOrWhiteSpace(DestinationDrive, nameof(DestinationDrive));

                if (!Directory.Exists(DestinationDrive))
                {
                    throw new DirectoryNotFoundException($"Could not find the directory: {DestinationDrive}");
                }

                switch (GetPrivileges.IsUserAdmin())
                {
                    case true:
                        try
                        {
                            DismApi.Initialize(DismLogLevel.LogErrorsWarningsInfo);

                            DismSession session = DismApi.OpenOfflineSession(DestinationDrive);

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
                    switch (GetPrivileges.IsUserAdmin())
                    {
                        case true:
                            Console.WriteLine($"\n==> Deploying Windows to drive {parameters.DestinationDrive} in disk {parameters.DiskNumber}, please wait...");
                            NewProcess.StartDismProcess(@$"/apply-image /imagefile:{parameters.ImageFilePath} /applydir:{parameters.DestinationDrive} /index:{parameters.ImageIndex} /verify");
                            return NewProcess.ExitCode;
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

            switch (GetPrivileges.IsUserAdmin())
            {
                case true:
                    try
                    {
                        DismApi.Initialize(DismLogLevel.LogErrorsWarnings);

                        DismImageInfoCollection images = DismApi.GetImageInfo(parameters.ImageFilePath);

                        Console.WriteLine($"\nFound {images.Count} image(s) in {parameters.ImageFilePath}", ConsoleColor.Yellow);

                        foreach (DismImageInfo image in images)
                        {
                            Console.WriteLine($"Image name: {image.ImageName}, image index: {image.ImageIndex}, ");
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

                switch (GetPrivileges.IsUserAdmin())
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

                    switch (GetPrivileges.IsUserAdmin())
                    {
                        case true:
                            Console.WriteLine($"\n==> Installing bootloader to drive {parameters.EfiDrive} in disk {parameters.DiskNumber}");
                            NewProcess.StartCmdProcess("bcdboot", @$"{parameters.DestinationDrive}\windows /s {parameters.EfiDrive} /f {parameters.FirmwareType}");
                            return NewProcess.ExitCode;
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
