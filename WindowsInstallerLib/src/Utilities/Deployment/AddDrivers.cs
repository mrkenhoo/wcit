using Microsoft.Dism;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Versioning;
using WindowsInstallerLib.Management.PrivilegesManager;
using WindowsInstallerLib.Management.ProcessManager;

namespace WindowsInstallerLib.Utilities.Deployment
{
    [SupportedOSPlatform("windows")]
    public partial class NewDeploy
    {
        /// <summary>
        /// Installs drivers to an offline Windows image.
        /// </summary>
        /// <param name="DestinationDrive"></param>
        /// <param name="DriversSource"></param>
        public static int AddDrivers(string DestinationDrive, string ImageFile, string DriversSource)
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
                        //NewProcess.StartDismProcess($"/Image:{ImageFile} /Add-Driver /Drive:{DriversSource} /recurse");
                        break;
                    case false:
                        throw new UnauthorizedAccessException();
                }

                return NewProcess.ExitCode;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
