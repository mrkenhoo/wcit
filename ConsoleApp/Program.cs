using System;
using System.Reflection;
using System.Runtime.Versioning;
using WindowsInstallerLib;

namespace ConsoleApp
{
    [SupportedOSPlatform("windows")]
    internal sealed class Program
    {
        [MTAThread]
        internal static int Main(string[] args)
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
                Console.WriteLine($"Welcome to the {ProgramName} tool!\nCurrent version: {ProgramVersion}-testing\nCreated by {ProgramAuthor}");
#else
                Console.Title = $"{ProgramName}";
                Console.WriteLine($"Welcome to the {ProgramName} tool!\nCurrent version: {ProgramVersion}\nCreated by {ProgramAuthor}");
#endif
                InstallerParameters parameters = new();

                InstallerManager.ConfigureInstaller(ref parameters);

                InstallerManager.InstallWindows(ref parameters);
            }
            catch (Exception)
            {
                throw;
            }

            return 0;
        }
    }
}
