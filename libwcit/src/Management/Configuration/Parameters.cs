using System.Runtime.Versioning;

namespace libwcit.Management.Installer
{
    [SupportedOSPlatform("windows")]
    public partial class Configuration
    {
        public static string? DestinationDrive { get; set; }
        public static string? EfiDrive { get; set; }
        public static int DiskNumber { get; set; }
        public static string? SourceDrive { get; set; }
        public static int WindowsEdition { get; set; }
        public static bool InstallExtraDrivers { get; set; }
    }
}
