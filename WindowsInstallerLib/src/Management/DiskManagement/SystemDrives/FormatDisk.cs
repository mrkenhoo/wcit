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
                        NewProcess.StartDiskPartProcess(DiskNumber, EfiDrive, DestinationDrive);
                        return NewProcess.ExitCode;

                    case false:
                        NewProcess.StartDiskPartProcess(DiskNumber, EfiDrive, DestinationDrive, RunAsAdministrator: true);
                        return NewProcess.ExitCode;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
