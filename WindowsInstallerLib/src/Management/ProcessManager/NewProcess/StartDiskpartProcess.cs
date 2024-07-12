using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace WindowsInstallerLib.Management.ProcessManager
{
    static partial class NewProcess
    {
        internal static int StartDiskPartProcess(int DiskNumber, string EfiDrive, string DestinationDrive, bool RunAsAdministrator = false)
        {
            Process process = new();

            try
            {
                process.StartInfo.FileName = "diskpart.exe";
                if (RunAsAdministrator && Environment.OSVersion.Version.Major >= 6)
                {
                    process.StartInfo.Verb = "runas";
                    process.StartInfo.UseShellExecute = true;
                }
                else
                {
                    process.StartInfo.UseShellExecute = false;
                }
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();

                Console.WriteLine($"Formatting disk {DiskNumber}, please wait...");

                process.StandardInput.WriteLine($"select disk {DiskNumber}");
                process.StandardInput.WriteLine("clean");
                process.StandardInput.WriteLine("convert gpt");
                process.StandardInput.WriteLine("create partition efi size=100");
                process.StandardInput.WriteLine("format fs=fat32 quick");
                process.StandardInput.WriteLine($"assign letter {EfiDrive}");
                process.StandardInput.WriteLine("create partition msr size=16");
                process.StandardInput.WriteLine("create partition primary");
                process.StandardInput.WriteLine("format fs=ntfs quick");
                process.StandardInput.WriteLine($"assign letter {DestinationDrive}");
                process.StandardInput.WriteLine("exit");

                process.WaitForExit();
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
            catch(Exception)
            {
                throw;
            }

            finally
            {
                ExitCode = process.ExitCode;

                switch (ExitCode)
                {
                    case 0:
                        Console.WriteLine($"\nDisk {DiskNumber} has been formatted successfully.");
                        break;
                    case 1:
                        Console.Error.WriteLine($"\nFailed to format the disk {DiskNumber}.");
                        break;
                }
                process.Close();

            }

            return ExitCode;
        }
    }
}
