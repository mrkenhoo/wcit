using System.Runtime.Versioning;

namespace Runtime.Management.Installer
{
    [SupportedOSPlatform("windows")]
    sealed partial class Configuration
    {
        public static string? DestinationDrive { get; set; }
        public static string? EfiDrive { get; set; }
        public static int DiskNumber = -1;
        public static string? SourceDrive { get; set; }
        public static int WindowsEdition = 0;
        public static bool AddDriversToWindows = false;
    }
}
