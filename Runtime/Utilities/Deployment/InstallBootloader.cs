using Runtime.Management.ProcessManager;

namespace Runtime.Utilities.Deployment
{
    public static partial class NewDeploy
    {
        public static void InstallBootloader(string DestinationDrive, string EfiDrive, string FirmwareType)
        {
            try
            {
                if (Directory.Exists(EfiDrive))
                {
                    throw new Exception("The drive letter " + EfiDrive + "is already in use.");
                }
                else if (!Directory.Exists($"{DestinationDrive}\\windows"))
                {
                    throw new DirectoryNotFoundException($"Could not find the following directory: {DestinationDrive}\\windows");
                }
                else
                {
                    Worker.StartCmdProcess("bcdboot", $"{DestinationDrive}\\windows /s {EfiDrive} /f {FirmwareType}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
