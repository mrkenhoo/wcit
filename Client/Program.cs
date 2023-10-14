using libwcit.Management.DiskManagement;
using libwcit.Management.EFIManager;
using libwcit.Management.Installer;
using libwcit.Management.PrivilegesManager;
using libwcit.Utilities.Deployment;
using System;
using System.Reflection;

namespace wcit
{
    internal class Program
    {
        [MTAThread]
        private static int Main(string[] args)
        {
            Console.Title = $"{Assembly.GetExecutingAssembly().GetName().Name} v{Assembly.GetExecutingAssembly().GetName().Version}";

#if WINDOWS10_0_22621_0_OR_GREATER && NET7_0_OR_GREATER
            switch (GetPrivileges.IsUserAdmin())
            {
                case true:
                    try
                    {
                        if (!GetEFIInfo.IsEFI())
                        {
                            throw new PlatformNotSupportedException("An error has occurred: Your system does not support EFI.");
                        }

                        Console.Clear();

                        Console.WriteLine("Welcome to the Windows CLI Installer Tool!\nCreated by Felipe González Martín");

                        Configuration.SetupInstaller();

                        Console.WriteLine(@$"Destination drive is set to '{Configuration.DestinationDrive}'
EFI drive is set to '{Configuration.EfiDrive}'
Disk number is set to '{Configuration.DiskNumber}'
Source drive is set to '{Configuration.SourceDrive}'
Windows edition (Index) is set to '{Configuration.WindowsEdition}'");

                        Console.WriteLine($"\nIf this is correct, press any key to continue...");
                        Console.ReadKey();

                        if (Configuration.DiskNumber != -1 &&
                            Configuration.DestinationDrive != null &&
                            Configuration.EfiDrive != null)
                        {
                            SystemDrives.FormatDrive(Configuration.DiskNumber,
                                                     Configuration.DestinationDrive,
                                                     Configuration.EfiDrive);
                        }

                        Console.WriteLine($"\n==> Deploying Windows to drive {Configuration.DestinationDrive} in disk {Configuration.DiskNumber}, please wait...");
                        if (Configuration.SourceDrive != null &&
                            Configuration.DestinationDrive != null &&
                            Configuration.DiskNumber != -1 &&
                            Configuration.WindowsEdition != 0)
                        {
                            NewDeploy.ApplyImage(Configuration.SourceDrive, Configuration.DestinationDrive, Configuration.WindowsEdition);
                        }

                        Console.WriteLine($"\n==> Installing bootloader to drive {Configuration.EfiDrive} in disk {Configuration.DiskNumber}");
                        if (Configuration.DestinationDrive != null &&
                            Configuration.EfiDrive != null)
                        {
                            NewDeploy.InstallBootloader(Configuration.DestinationDrive, Configuration.EfiDrive, "UEFI");
                        }

                        Console.WriteLine("Windows has been deployed and it's ready to use\n\nPress ENTER to close the window");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message);
                    }
                    break;
                case false:
                    throw new UnauthorizedAccessException("This program needs administrator privileges to work.");
            }
#else
            throw new NotSupportedException("This system is not compatible with this program.");
#endif
            return Environment.ExitCode;
        }
    }
}
