using libwcit.Management.ProcessManager;
using System;
using System.Runtime.Versioning;

namespace libwcit.Utilities.Deployment
{
    [SupportedOSPlatform("windows")]
    public static partial class NewDeploy
    {
        /// <summary>
        /// Installs drivers to an offline Windows image at
        /// <paramref name="DestinationDrive"/> from <paramref name="DestinationDrive"/>.
        /// </summary>
        /// <param name="DestinationDrive"></param>
        /// <param name="DriversSource"></param>
        public static void AddDriver(string DestinationDrive, string DriversSource)
        {
            try
            {
                switch(DestinationDrive)
                {
                    case null:
                        throw new ArgumentNullException(nameof(DestinationDrive));
                }

                switch(DriversSource)
                {
                    case null:
                        throw new ArgumentNullException(nameof(DriversSource));
                }
                Worker.StartDismProcess($"/Image:{DestinationDrive} /Add-Driver /Drive:{DriversSource} /recurse");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
