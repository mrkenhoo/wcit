using System;
using System.Reflection;
using wcit.Libraries.Deployment;
using wcit.Libraries.DiskManagement;
using wcit.Libraries.ParametersManager;
using wcit.Libraries.PrivilegesManager;
using wcit.Source.Libraries.EFIManager;

namespace wcit
{
    internal class Program
    {
        [MTAThread]
        static int Main(string[] args)
        {
            ConsoleColor foregroundDefault = Console.ForegroundColor;
            Console.Title = $"Windows CLI Installer Tool - version {Assembly.GetExecutingAssembly().GetName().Version}";
#if WINDOWS7_0_OR_GREATER && NET7_0
            if (Permissions.IsUserAdmin())
            {
                try
                {
                    if (!GetEFIInfo.IsEFI())
                    {
                        Console.Error.WriteLine("Only EFI systems are supported");
                        Environment.Exit(1);
                    }

                    Console.Clear();

                    Console.WriteLine("Welcome to the Windows CLI Installer Tool!\nCreated by Ken Hoo (mrkenhoo)");

                    Parameters.Setup();

                    Console.WriteLine(@$"Destination drive is set to '{Parameters.DestinationDrive}'
EFI drive is set to '{Parameters.EfiDrive}'
Disk number is set to '{Parameters.DiskNumber}'
Source drive is set to '{Parameters.SourceDrive}'
Windows edition (Index) is set to '{Parameters.WindowsEdition}'", Console.ForegroundColor = ConsoleColor.Green);

                    Console.WriteLine($"\nIf this is correct, press any key to continue...", Console.ForegroundColor = foregroundDefault);
                    Console.ReadLine();

                    SystemDrives.FormatDrive(Parameters.DiskNumber,
                                             Parameters.DestinationDrive,
                                             Parameters.EfiDrive);

                    Console.WriteLine($"\n==> Deploying Windows to drive {Parameters.DestinationDrive} in disk {Parameters.DiskNumber}, please wait...");
                    NewDeploy.ApplyImage(Parameters.SourceDrive, Parameters.DestinationDrive, Parameters.WindowsEdition);

                    Console.WriteLine($"\n==> Installing bootloader to drive {Parameters.EfiDrive} in disk {Parameters.DiskNumber}");
                    NewDeploy.InstallBootloader(Parameters.DestinationDrive, Parameters.EfiDrive, "UEFI");

                    Console.WriteLine("Windows has been deployed and it's ready to use\n\nPress ENTER to close the window");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.Error.WriteLine("ERROR: This program needs administrator privileges to work.", Console.ForegroundColor = ConsoleColor.Red);
                Console.ReadLine();
                Environment.Exit(1);
            }
#else
            Console.Error.WriteLine("This program is only compatible with Windows 7 or greater and .NET 7.0");
            Console.WriteLine("Press any key to close the program.");
            Console.ReadLine();
            Environment.Exit(1);
#endif

            return 0;
        }
    }
}