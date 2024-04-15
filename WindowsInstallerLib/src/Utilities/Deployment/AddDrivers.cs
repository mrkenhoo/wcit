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
        /// Installs drivers to an offline Windows image at
        /// <paramref name="DestinationDrive"/> from <paramref name="DestinationDrive"/>.
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
                        return Worker.ExitCode;
                    case false:
                        Worker.StartDismProcess($"/Image:{ImageFile} /Add-Driver /Drive:{DriversSource} /recurse", RunAsAdministrator: true);
                        return Worker.ExitCode;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
