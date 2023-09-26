using Runtime.Management.ProcessManager;
using System;

namespace Runtime.Utilities.Deployment
{
    public static partial class NewDeploy
    {
        public static void AddDriver(string DestinationDrive, string DriversSource)
        {
            try
            {
                switch(DestinationDrive == null)
                {
                    case true:
                        throw new ArgumentNullException(nameof(DestinationDrive));
                }

                switch(DriversSource == null)
                {
                    case true:
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
