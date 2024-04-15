using System;
using System.Reflection;
using WindowsInstallerLib.Management.EFIManager;
using WindowsInstallerLib.Management.Installer;
using WindowsInstallerLib.Utilities.Deployment;

namespace cli_app
{
    internal class Program
    {
        [MTAThread]
        private static int Main(string[] args)
        {
            string? ProgramName = Assembly.GetExecutingAssembly().GetName().Name;
            Version? ProgramVersion = Assembly.GetExecutingAssembly().GetName().Version;
            Console.Title = $"{ProgramName} v{ProgramVersion}";

#if WINDOWS10_0_22621_0_OR_GREATER && NET8_0_OR_GREATER
            try
            {
                if (!GetEFIInfo.IsEFI())
                {
                    throw new PlatformNotSupportedException("Your system does not support EFI.");
                }

                Console.Clear();

                Console.WriteLine("Welcome to the Windows CLI Installer Tool!\nCreated by Felipe González Martín");

                Configuration.SetupInstaller();

                Configuration.InstallWindows(Configuration.DiskNumber, Configuration.DestinationDrive,
                                             Configuration.EfiDrive, NewDeploy.ImageFile, Configuration.WindowsEdition);
            }
            catch (Exception)
            {
                throw;
            }

            return 0;
#else
            throw new NotSupportedException("This system is not compatible with this program.");
#endif
        }
    }
}
