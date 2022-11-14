using System;
using System.Diagnostics;

namespace wcit.Libraries.DiskManagement
{
    public static partial class SystemDrives
    {
        public static void FormatDrive(string DiskNumber, string DestinationDrive, string EfiDrive)
        {
            try
            {
                Process process = new();
                process.StartInfo.FileName = "diskpart.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.StandardInput.WriteLine($"select disk {DiskNumber}");
                Console.WriteLine($"Wiping disk {DiskNumber}...");
                process.StandardInput.WriteLine("clean");
                Console.WriteLine($"Converting disk {DiskNumber} to GPT...");
                process.StandardInput.WriteLine("convert gpt");
                Console.WriteLine($"Creating EFI partition from disk {DiskNumber}...");
                process.StandardInput.WriteLine("create partition efi size=100");
                Console.WriteLine($"Formatting EFI partition from disk {DiskNumber}...");
                process.StandardInput.WriteLine("format fs=fat32 quick");
                Console.WriteLine($"Mounting EFI partition to {EfiDrive} from disk {DiskNumber}...");
                process.StandardInput.WriteLine($"assign letter {EfiDrive}");
                Console.WriteLine($"Creating MSR partition in disk {DiskNumber}...");
                process.StandardInput.WriteLine("create partition msr size=16");
                Console.WriteLine($"Creating primary partition in disk {DiskNumber}...");
                process.StandardInput.WriteLine("create partition primary");
                Console.WriteLine($"Formatting primary partition in disk {DiskNumber}...");
                process.StandardInput.WriteLine("format fs=ntfs quick");
                Console.WriteLine($"Mounting primary partition to {DestinationDrive} from disk {DiskNumber}...");
                process.StandardInput.WriteLine($"assign letter {DestinationDrive}");
                process.StandardInput.WriteLine("exit");
                process.WaitForExit();
                process.Dispose();
                if (Environment.ExitCode == 0)
                {
                    Console.WriteLine($"\nDisk {DiskNumber} has been formatted successfully");
                }
                else
                {
                    Console.Error.WriteLine("\nAn error has occurred.\n\nPress ENTER to close the program");
                    Console.ReadLine();
                    Environment.Exit(1);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
