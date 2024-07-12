namespace WindowsInstallerLib.Management.Installer
{
    public partial class NewInstallation
    {
        public static string? DestinationDrive { get; set; }
        public static string? EfiDrive { get; set; }
        public static int DiskNumber = -1;
        public static string? SourceDrive { get; set; }
        public static int ImageIndex = -1;
        public static string? ImageFilePath { get; set; }
        public static bool InstallExtraDrivers;
        public static string? FirmwareType { get; set; }
    }
}
