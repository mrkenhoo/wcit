using System;
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
            Parameters parameters = new();

            try
            {
#if DEBUG
                Console.Title = $"[{ProgramInfo.GetConfigurationMode()}] {ProgramInfo.GetName()}";
                Console.WriteLine($"Welcome to the {ProgramInfo.GetName()} tool!");
                Console.WriteLine($"Current version: {ProgramInfo.GetVersion()}-{ProgramInfo.GetConfigurationMode()}");
                Console.WriteLine($"Created by {ProgramInfo.GetAuthor()}");
#else
                Console.Title = $"{ProgramInfo.GetName()}";
                Console.WriteLine($"Welcome to the {ProgramInfo.GetName()} tool!");
                Console.WriteLine($"Current version: {ProgramInfo.GetVersion()}");
                Console.WriteLine($"Created by {ProgramInfo.GetAuthor()}");
#endif
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                ArgumentParser.ParseArgs(ref parameters, args);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                InstallerManager.Configure(ref parameters);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
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
