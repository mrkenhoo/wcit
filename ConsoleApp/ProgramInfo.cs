using System.Reflection;
using System.Runtime.Versioning;

namespace ConsoleApp
{
    [SupportedOSPlatform("windows")]
    internal static class ProgramInfo
    {
        private static string ProgramAuthor => "Felipe González Martín";
        private static string ProgramName => Assembly.GetExecutingAssembly().GetName().Name ?? "Windows Installer";
        private static string ProgramVersion => Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0";
        private static string ProgramConfigurationMode => Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration ?? "Debug";

        internal static string GetAuthor() => ProgramAuthor;
        internal static string GetName() => ProgramName;
        internal static string GetVersion() => ProgramVersion;
        internal static string GetConfigurationMode() => ProgramConfigurationMode;
    }
}
