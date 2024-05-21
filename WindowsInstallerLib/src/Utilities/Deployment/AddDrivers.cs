using System;
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
        public static int AddDrivers(string ImageFile, string DriversSource)
        {
            try
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(ImageFile, nameof(ImageFile));
                ArgumentException.ThrowIfNullOrWhiteSpace(DriversSource, nameof(DriversSource));

                switch (GetPrivileges.IsUserAdmin())
                {
                    case true:
                        Worker.StartDismProcess($"/Image:{ImageFile} /Add-Driver /Drive:{DriversSource} /recurse");
                        break;
                    case false:
                        Worker.StartDismProcess($"/Image:{ImageFile} /Add-Driver /Drive:{DriversSource} /recurse", true);
                        break;
                }

                return Worker.ExitCode;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
