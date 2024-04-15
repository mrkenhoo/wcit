using System.Runtime.Versioning;
using WindowsInstallerLib.Management.PrivilegesManager;
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
                switch (GetPrivileges.IsUserAdmin())
                {
                    case true:
                        Worker.StartDiskpartProcess(DiskNumber, EfiDrive, DestinationDrive);
                        return Worker.ExitCode;

                    case false:
                        Worker.StartDiskpartProcess(DiskNumber, EfiDrive, DestinationDrive, RunAsAdministrator: true);
                        return Worker.ExitCode;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
