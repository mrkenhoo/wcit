﻿using Runtime.Management.DiskManagement;
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
            ConsoleColor foregroundDefault = Console.ForegroundColor;
            Console.Title = $"Windows CLI Installer Tool - version {Assembly.GetExecutingAssembly().GetName().Version}";
            if (GetPrivileges.IsUserAdmin())
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

                    if (Parameters.DiskNumber != null &&
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
                        Parameters.WindowsEdition != null)
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
            }
            else
            {
                throw new UnauthorizedAccessException("This program needs administrator privileges to work");
            }

            return 0;
        }
    }
}
