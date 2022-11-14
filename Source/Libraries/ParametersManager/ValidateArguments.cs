using System;
using wcit.Libraries.Deployment;
using wcit.Libraries.DiskManagement;

namespace wcit.Libraries.ParametersManager
{
    internal class Parameters
    {
        public static string? DestinationDrive { get; set; }
        public static string? EfiDrive { get; set; }
        public static string? DiskNumber { get; set; }
        public static string? SourceDrive { get; set; }
        public static string? WindowsEdition { get; set; }

        public static void Setup()
        {

            if (DestinationDrive == null)
            {
                Console.WriteLine("\n==> Type the mountpoint to use for deploying Windows (e.g. Z:).");
                DestinationDrive = Console.ReadLine();

                if (string.IsNullOrEmpty(DestinationDrive))
                {
                    Console.WriteLine("No destination drive was specified.\n\nPress ENTER to quit the program.",
                                      Console.ForegroundColor = ConsoleColor.Red);
                    Console.ReadLine();
                    Environment.Exit(1);
                }
                else if (!DestinationDrive.Contains(':'))
                {
                    Console.Error.WriteLine($"\nERROR: '{DestinationDrive}': Invalid destination drive, it must have a colon. For example: 'H:'.\n\nPress ENTER to quit the program.",
                                            Console.ForegroundColor = ConsoleColor.Red);
                    Console.ReadLine();
                    Environment.Exit(1);
                }
            }

            if (EfiDrive == null)
            {
                Console.WriteLine("\n==> Type a mountpoint for installing the bootloader at (e.g. K:).");
                EfiDrive = Console.ReadLine();

                if (string.IsNullOrEmpty(EfiDrive))
                {
                    Console.WriteLine("No EFI drive was specified.\n\nPress ENTER to quit the program.", Console.ForegroundColor = ConsoleColor.Red);
                    Console.ReadLine();
                    Environment.Exit(1);
                }
                else if (!EfiDrive.Contains(':'))
                {
                    Console.Error.WriteLine($"\nERROR: '{EfiDrive}': Invalid EFI drive, it must have a colon. For example: 'H:'.\n\nPress ENTER to quit the program.",
                                            Console.ForegroundColor = ConsoleColor.Red);
                    Console.ReadLine();
                    Environment.Exit(1);
                }
            }

            if (DiskNumber == null)
            {
                Console.WriteLine("\n==> These are the disks available on your system:");
                SystemDrives.ListAll();

                Console.WriteLine("\n==> Please type the disk number to format (e.g. 0):");
                DiskNumber = Console.ReadLine();

                if (string.IsNullOrEmpty(DiskNumber))
                {
                    Console.WriteLine("No disk specified for formatting.\n\nPress ENTER to quit the program.",
                                      Console.ForegroundColor = ConsoleColor.Red);
                    Console.ReadLine();
                    Environment.Exit(1);
                }
            }

            if (SourceDrive == null)
            {
                Console.WriteLine("\n==> Type the letter where the ISO is mounted at below (e.g. D:).");
                SourceDrive = Console.ReadLine();

                if (string.IsNullOrEmpty(SourceDrive))
                {
                    Console.WriteLine("No source drive was specified.\n\nPress ENTER to quit the program.",
                                      Console.ForegroundColor = ConsoleColor.Red);
                    Console.ReadLine();
                    Environment.Exit(1);
                }
            }

            if (WindowsEdition == null)
            {
                NewDeploy.GetImageInfo(SourceDrive);

                Console.WriteLine("==> Type the index number of the Windows edition you wish to install below (e.g. 1).");
                WindowsEdition = Console.ReadLine();

                if (string.IsNullOrEmpty(WindowsEdition))
                {
                    Console.WriteLine("No Windows edition was specified.\n\nPress ENTER to quit the program.",
                                      Console.ForegroundColor = ConsoleColor.Red);
                    Console.ReadLine();
                    Environment.Exit(1);
                }
            }
        }
    }
}