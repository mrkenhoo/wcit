using System;
using wcit.Management.ProcessManager;

namespace wcit.Utilities.Deployment
{
    public static partial class NewDeploy
    {
        public static void InstallBootloader(string DestinationDrive, string EfiDrive, string FirmwareType)
        {
            try
            {
                Worker.StartCmdProcess("bcdboot", $"{DestinationDrive}\\windows /s {EfiDrive} /f {FirmwareType}");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
