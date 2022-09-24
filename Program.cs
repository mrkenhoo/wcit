using System;
using System.Reflection;

namespace wcit
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
#if WINDOWS7_0_OR_GREATER && NET6_0
            Console.Title = $"Windows CLI Installer Tool - version {Assembly.GetExecutingAssembly().GetName().Version}";
            if (PrivilegesManager.IsUserAdmin())
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Windows CLI Installer Tool!\nCreated by Ken Hoo (mrkenhoo)");

                Console.WriteLine("\n==> Type the mountpoint to use for deploying Windows (e.g. Z:).");
                string destination_drive = Console.ReadLine();

                if (string.IsNullOrEmpty(destination_drive))
                {
                    Console.WriteLine("No destination drive was specified.\n\nPress ENTER to quit the program.");
                    Console.ReadLine();
                    Environment.Exit(1);
                }
                else if (!destination_drive.Contains(':'))
                {
                    Console.Error.WriteLine($"\nERROR: '{destination_drive}': Invalid destination drive, it must have a colon. For example: 'H:'.\n\nPress ENTER to quit the program.");
                    Console.ReadLine();
                    Environment.Exit(1);
                }

                Console.WriteLine("\n==> Type a mountpoint for installing the bootloader at (e.g. K).");
                string efi_drive = Console.ReadLine();

                if (string.IsNullOrEmpty(efi_drive))
                {
                    Console.WriteLine("No EFI drive was specified.\n\nPress ENTER to quit the program.");
                    Console.ReadLine();
                    Environment.Exit(1);
                }
                else if (!efi_drive.Contains(':'))
                {
                    Console.Error.WriteLine($"\nERROR: '{destination_drive}': Invalid EFI drive, it must have a colon. For example: 'H:'.\n\nPress ENTER to quit the program.");
                    Console.ReadLine();
                    Environment.Exit(1);
                }

                Console.WriteLine("\n==> These are the disks available on your system:");

                DiskManager.GetPhysicalDisks();

                Console.WriteLine("\n==> Please type the disk number to format (e.g. 0):");
                string diskNumber = Console.ReadLine();

                if (string.IsNullOrEmpty(diskNumber))
                {
                    Console.WriteLine("No disk specified for formatting.\n\nPress ENTER to quit the program.");
                    Console.ReadLine();
                    Environment.Exit(1);
                }

                DiskManager.FormatDrive(diskNumber, destination_drive, efi_drive);

                Console.WriteLine("\n==> Type the letter where the ISO is mounted at below (e.g. D:).");
                string source_drive = Console.ReadLine();

                if (string.IsNullOrEmpty(source_drive))
                {
                    Console.WriteLine("No source drive was specified.\n\nPress ENTER to quit the program.");
                    Console.ReadLine();
                    Environment.Exit(1);
                }

                DeployManager.GetImageInfo(source_drive);

                Console.WriteLine("==> Type the index number of the Windows edition you wish to install below (e.g. 1).");
                string windows_edition = Console.ReadLine();

                if (string.IsNullOrEmpty(windows_edition))
                {
                    Console.WriteLine("No Windows edition was specified.\n\nPress ENTER to quit the program.");
                    Console.ReadLine();
                    Environment.Exit(1);
                }

                Console.WriteLine($"\n==> Deploying Windows to drive {destination_drive} in disk {diskNumber}, please wait...");
                DeployManager.DeployWindows(source_drive, destination_drive, windows_edition);

                Console.WriteLine($"\n==> Installing bootloader to drive {efi_drive} in disk {diskNumber}");
                DeployManager.InstallBootloader(destination_drive, efi_drive);

                Console.WriteLine("Windows has been deployed and it's ready to use\n\nPress ENTER to close the window");
            }
            else
            {
                Console.Error.WriteLine("This program needs administrator privileges to work.");
                Console.ReadLine();
                Environment.Exit(1);
            }
#else
            Console.Error.WriteLine("This program is only compatible with Windows 7 or greater.");
            Console.ReadLine();
            Environment.Exit(1);
#endif
        }
    }
}
