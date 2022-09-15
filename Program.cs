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
                Console.WriteLine("Welcome to the Windows CLI Installer Tool!\nCreated by Ken Hoo (mrkenhoo)\n\n==> Available disks on your system:");

                DiskManager.GetPhysicalDisks();

                Console.WriteLine("\n==> Please type the disk number to format (e.g. 0):");
                string diskNumber = Console.ReadLine();

                if (string.IsNullOrEmpty(diskNumber))
                {
                    Console.WriteLine("No disk specified for formatting. \n\nPress ENTER to quit the program.");
                    Console.ReadLine();
                    System.Environment.Exit(1);
                }

                DiskManager.FormatDrive(diskNumber);

                Console.WriteLine("\n==> Type the letter where the ISO is mounted at below (e.g. D:).");
                string source_drive = Console.ReadLine();

                if (string.IsNullOrEmpty(source_drive))
                {
                    Console.WriteLine("No source drive was specified. \n\nPress ENTER to quit the program.");
                    Console.ReadLine();
                    System.Environment.Exit(1);
                }


                DeployManager.GetImageInfo(source_drive);

                Console.WriteLine("==> Type the index number of the Windows edition you wish to install below (e.g. 1).");
                string windows_edition = Console.ReadLine();

                if (string.IsNullOrEmpty(windows_edition))
                {
                    Console.WriteLine("No Windows edition was specified. \n\nPress ENTER to quit the program.");
                    Console.ReadLine();
                    System.Environment.Exit(1);
                }


                Console.WriteLine($"\n==> Deploying Windows to disk {diskNumber}, please wait...");
                DeployManager.DeployWindows(source_drive, windows_edition);

                Console.WriteLine("\n==> Installing bootloader...");
                DeployManager.InstallBootloader();

                Console.WriteLine("Windows has been deployed and it's ready to use\n\nPress ENTER to close the window");
                Console.ReadLine();
            }
            else
            {
                Console.Error.WriteLine("This program needs administrator privileges to work.");
                Console.ReadLine();
                Environment.Exit(1);
            }
#else
            Console.Error.WriteLine("This program is only compatible with Windows 7 or greater.");
#endif
        }
    }
}
