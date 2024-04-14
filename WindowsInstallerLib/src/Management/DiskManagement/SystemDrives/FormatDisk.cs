using System.Runtime.Versioning;
using WindowsInstallerLib.Management.ProcessManager;

namespace WindowsInstallerLib.Management.DiskManagement
{
    [SupportedOSPlatform("windows")]
    public partial class SystemDrives
    {
        public static int FormatDisk(int DiskNumber, string DestinationDrive, string EfiDrive)
        {
            try
            {
                Worker.StartDiskpartProcess(DiskNumber, EfiDrive, DestinationDrive);

                return Worker.ExitCode;
            }
            catch
            {
                throw;
            }
        }
    }
}
