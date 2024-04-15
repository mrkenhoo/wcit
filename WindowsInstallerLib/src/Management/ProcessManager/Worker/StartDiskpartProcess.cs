using System;
using System.ComponentModel;
using System.Diagnostics;

namespace WindowsInstallerLib.Management.ProcessManager
{
    static partial class Worker
    {
        internal static int StartDiskpartProcess(int DiskNumber, string EfiDrive, string DestinationDrive, bool RunAsAdministrator)
        {
            try
            {
                Process process = new();
                process.StartInfo.FileName = "diskpart.exe";
                if (RunAsAdministrator)
                {
                    process.StartInfo.Verb = "RunAs";
                }
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();

                Console.WriteLine($"Selecting disk {DiskNumber}...");
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
                ExitCode = process.ExitCode;
                process.Close();

                switch (ExitCode)
                {
                    case 0:
                        Console.WriteLine($"\nDisk {DiskNumber} has been formatted successfully.");
                        break;
                    case 1:
                        Console.Error.WriteLine($"\nFailed to format the disk {DiskNumber}.");
                        break;
                }
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

            return ExitCode;
        }
    }
}
