using System;
using System.Globalization;
using System.Runtime.Versioning;
using WindowsInstallerLib;

namespace ConsoleApp
{
    /// <summary>
    /// Manages everything related to the command line arguments.
    /// </summary>
    [SupportedOSPlatform("windows")]
    internal sealed class ArgumentParser
    {
        /// <summary>
        /// Validates and parses the command line arguments.
        /// </summary>
        /// <param name="args"></param>
        internal static void ParseArgs(ref Parameters parameters, string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }

            foreach (string arg in args)
            {
                switch (arg.ToLower(CultureInfo.CurrentCulture))
                {
                    case "/?" or "/h":
                        Console.WriteLine($"\nUsage: {ProgramInfo.GetName()} [options]");
                        Console.WriteLine("\nOptions:");
                        Console.WriteLine("  /DestinationDrive <drive>  Specifies the mountpoint to use for deploying Windows.");
                        Console.WriteLine("  /EfiDrive <drive>          Specifies the mountpoint to use for the EFI partition.");
                        Console.WriteLine("  /DiskNumber <number>       Specifies the disk number to use for deploying Windows.");
                        Console.WriteLine("  /SourceDrive <drive>       Specifies the mountpoint to use for the Windows image.");
                        Console.WriteLine("  /ImageIndex <number>       Specifies the index of the Windows image to deploy.");
                        Console.WriteLine("  /ImageFilePath <path>      Specifies the path to the Windows image to deploy.");
                        Console.WriteLine("  /InstallExtraDrivers       Installs additional drivers during the deployment.");
                        Console.WriteLine("  /FirmwareType <type>       Specifies the firmware type to use for the deployment.");
                        Console.WriteLine("  /?, /h                     Displays this help message.\n");
                        Environment.Exit(0);
                        return;
                    case "/destinationdrive":
                        parameters.DestinationDrive = args[Array.IndexOf(args, arg) + 1].ToUpperInvariant();
                        continue;
                    case "/efidrive":
                        parameters.EfiDrive = args[Array.IndexOf(args, arg) + 1].ToUpperInvariant();
                        continue;
                    case "/disknumber":
                        parameters.DiskNumber = Convert.ToInt32(args[Array.IndexOf(args, arg) + 1], CultureInfo.CurrentCulture);
                        continue;
                    case "/sourcedrive":
                        parameters.SourceDrive = args[Array.IndexOf(args, arg) + 1].ToUpperInvariant();
                        continue;
                    case "/imageindex":
                        parameters.ImageIndex = Convert.ToInt32(args[Array.IndexOf(args, arg) + 1], CultureInfo.CurrentCulture);
                        continue;
                    case "/imagefilepath":
                        parameters.ImageFilePath = args[Array.IndexOf(args, arg) + 1].ToLowerInvariant();
                        continue;
                    case "/installextradrivers":
                        parameters.InstallExtraDrivers = true;
                        continue;
                    case "/firmwaretype":
                        parameters.FirmwareType = args[Array.IndexOf(args, arg) + 1].ToUpperInvariant();
                        continue;
                }
            }
#if DEBUG
            Console.WriteLine("Parameters:");
            Console.WriteLine($"  Destination Drive: {parameters.DestinationDrive}");
            Console.WriteLine($"  EFI Drive: {parameters.EfiDrive}");
            Console.WriteLine($"  Disk Number: {parameters.DiskNumber}");
            Console.WriteLine($"  Source Drive: {parameters.SourceDrive}");
            Console.WriteLine($"  Image Index: {parameters.ImageIndex}");
            Console.WriteLine($"  Image File Path: {parameters.ImageFilePath}");
            Console.WriteLine($"  Install Extra Drivers: {parameters.InstallExtraDrivers}");
            Console.WriteLine($"  Firmware Type: {parameters.FirmwareType}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
#endif
        }
    }
}
