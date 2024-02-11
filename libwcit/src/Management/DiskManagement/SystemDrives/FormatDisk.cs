using libwcit.Management.ProcessManager;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace libwcit.Management.DiskManagement
{
    [SupportedOSPlatform("windows")]
    public partial class SystemDrives
    {
        public static int FormatDisk(int DiskNumber, string DestinationDrive, string EfiDrive)
        {
            try
            {
                using Process process = new();
                process.StartInfo.FileName = "diskpart.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.StandardInput.WriteLine($"select disk {DiskNumber}");
                Console.WriteLine($"\nWiping disk {DiskNumber}...");
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
                Worker.ExitCode = process.ExitCode;
                process.Dispose();
                switch (Worker.ExitCode)
                {
                    case 0:
                        Console.WriteLine($"\nDisk {DiskNumber} has been formatted successfully");
                        break;
                    case 1:
                        Console.Error.WriteLine($"\nFailed to format the disk {DiskNumber}");
                        break;
                }

                return Worker.ExitCode;
            }
            catch (ObjectDisposedException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Win32Exception)
            {
                throw;
            }
            catch (SystemException)
            {
                throw;
            }
        }
    }
}
