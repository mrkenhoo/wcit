using System;
using System.Runtime.Versioning;
using WindowsInstallerLib.Management.PrivilegesManager;
using WindowsInstallerLib.Management.ProcessManager;

namespace WindowsInstallerLib.Management.DiskManagement
{
    [SupportedOSPlatform("windows")]
    public partial class Disks
    {
        public static int FormatDisk(int DiskNumber, string EfiDrive, string DestinationDrive)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(EfiDrive);
                ArgumentException.ThrowIfNullOrEmpty(DestinationDrive);

                switch (GetPrivileges.IsUserAdmin())
                {
                    case true:
                        Worker.StartDiskPartProcess(DiskNumber, EfiDrive, DestinationDrive);
                        return Worker.ExitCode;

                    case false:
                        Worker.StartDiskPartProcess(DiskNumber, EfiDrive, DestinationDrive, RunAsAdministrator: true);
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
