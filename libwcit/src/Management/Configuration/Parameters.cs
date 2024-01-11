using System.Runtime.Versioning;

namespace libwcit.Management.Installer
{
    [SupportedOSPlatform("windows")]
    public partial class Configuration
    {
        public static string? DestinationDrive { get; private set; }
        public static string? EfiDrive { get; private set; }
        public static int DiskNumber { get; private set; }
        public static string? SourceDrive { get; private set; }
        public static int WindowsEdition { get; private set; }
        public static bool InstallExtraDrivers { get; private set; }
    }
}
