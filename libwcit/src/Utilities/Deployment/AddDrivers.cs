using libwcit.Management.ProcessManager;
using System;
using System.Runtime.Versioning;

namespace libwcit.Utilities.Deployment
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
        public static void AddDrivers(string ImageFile, string DriversSource)
        {
            try
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(ImageFile, nameof(ImageFile));
                ArgumentException.ThrowIfNullOrWhiteSpace(DriversSource, nameof(DriversSource));

                Worker.StartDismProcess($"/Image:{ImageFile} /Add-Driver /Drive:{DriversSource} /recurse");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
