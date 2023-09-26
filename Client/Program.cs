using Runtime.Management.DiskManagement;
using Runtime.Management.EFIManager;
using Runtime.Management.ParametersManager;
using Runtime.Management.PrivilegesManager;
using Runtime.Utilities.Deployment;
using System.Reflection;

namespace wcit
{
    internal sealed class Program
    {
        [MTAThread]
        private static int Main(string[] args)
        {
            Console.Title = $"Windows CLI Installer Tool - version {Assembly.GetExecutingAssembly().GetName().Version}";

#if WINDOWS10_0_22621_0_OR_GREATER && NET7_0_OR_GREATER
            switch (GetPrivileges.IsUserAdmin())
            {
                case true:
                    try
                    {
                        if (!GetEFIInfo.IsEFI())
                        {
                            throw new PlatformNotSupportedException("An error has occurred: Your system does not support EFI");
                        }

                        Console.Clear();

                        Console.WriteLine("Welcome to the Windows CLI Installer Tool!\nCreated by Ken Hoo (mrkenhoo)");

                        Parameters.Setup();

                        Console.WriteLine(@$"Destination drive is set to '{Parameters.DestinationDrive}'
EFI drive is set to '{Parameters.EfiDrive}'
Disk number is set to '{Parameters.DiskNumber}'
Source drive is set to '{Parameters.SourceDrive}'
Windows edition (Index) is set to '{Parameters.WindowsEdition}'");

                        Console.WriteLine($"\nIf this is correct, press any key to continue...");
                        Console.ReadKey();

                        if (Parameters.DiskNumber != -1 &&
                            Parameters.DestinationDrive != null &&
                            Parameters.EfiDrive != null)
                        {
                            SystemDrives.FormatDrive(Parameters.DiskNumber,
                                                     Parameters.DestinationDrive,
                                                     Parameters.EfiDrive);
                        }

                        Console.WriteLine($"\n==> Deploying Windows to drive {Parameters.DestinationDrive} in disk {Parameters.DiskNumber}, please wait...");
                        if (Parameters.SourceDrive != null &&
                            Parameters.DestinationDrive != null &&
                            Parameters.DiskNumber != -1 &&
                            Parameters.WindowsEdition != 0)
                        {
                            NewDeploy.ApplyImage(Parameters.SourceDrive, Parameters.DestinationDrive, Parameters.WindowsEdition);
                        }

                        Console.WriteLine($"\n==> Installing bootloader to drive {Parameters.EfiDrive} in disk {Parameters.DiskNumber}");
                        if (Parameters.DestinationDrive != null &&
                            Parameters.EfiDrive != null)
                        {
                            NewDeploy.InstallBootloader(Parameters.DestinationDrive, Parameters.EfiDrive, "UEFI");
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
