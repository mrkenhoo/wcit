using System.IO;
using System.Runtime.Versioning;

namespace WindowsInstallerLib.Management.DiskManagement
{
    [SupportedOSPlatform("windows")]
    public partial class Disks
    {
        /// <summary>
        /// Retrieves all disks available in the system and prints it out.
        /// </summary>
        public static DriveInfo[] GetDisksT()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            return drives;
        }
    }
}
