using System;
using System.Reflection;
using System.Runtime.Versioning;
using WindowsInstallerLib.Management.Installer;

namespace cli_app
{
    [SupportedOSPlatform("windows")]
    internal class Program
    {
        [MTAThread]
        static int Main(string[] args)
        {
            try
            {
                string ProgramAuthor = "Ken Hoo";
                string ProgramName = Assembly.GetExecutingAssembly().GetName().Name ?? "Windows CLI Installer";

                Version? ProgramVersion = Assembly.GetExecutingAssembly().GetName().Version;
#if DEBUG
                AssemblyConfigurationAttribute? ConfigurationAttribute = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyConfigurationAttribute>();

                string? ConfigurationMode = ConfigurationAttribute?.Configuration;

                Console.Title = $"[{ConfigurationMode?.ToString()}] {ProgramName}";
#else
                string ProgramFullName = "Windows CLI Installer";

                Console.Title = $"{ProgramFullName}";
#endif

                Console.WriteLine($"Welcome to the {ProgramName} tool!\nCurrent version: {ProgramVersion}\nCreated by {ProgramAuthor}");

                NewInstallation.ConfigureInstaller();

                NewInstallation.InstallWindows(NewInstallation.DiskNumber,
                                               NewInstallation.EfiDrive,
                                               NewInstallation.DestinationDrive,
                                               NewInstallation.ImageFilePath,
                                               NewInstallation.ImageIndex,
                                               NewInstallation.FirmwareType);
            }
            catch (Exception)
            {
                throw;
            }

            return 0;
        }
    }
}
