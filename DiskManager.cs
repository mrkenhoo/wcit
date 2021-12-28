using System;
using System.Diagnostics;
using System.Management;

namespace wcit
{
    internal class DiskManager
    {
        public static void GetPhysicalDisks()
        {
            WqlObjectQuery DeviceTable = new WqlObjectQuery("SELECT * FROM Win32_DiskDrive");
            ManagementObjectSearcher DeviceInfo = new ManagementObjectSearcher(DeviceTable);
            foreach (ManagementObject o in DeviceInfo.Get())
            {
                Console.WriteLine("Device number = " + o["Index"]);
                Console.WriteLine("Model = " + o["Model"]);
                Console.WriteLine("DeviceID = " + o["DeviceID"]);
                Console.WriteLine("");
            }
        }
        public static void FormatDrive(string diskNumber)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "diskpart.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                Console.WriteLine($"Formatting disk {diskNumber}...");
                process.StandardInput.WriteLine($"select disk {diskNumber}");
                process.StandardInput.WriteLine("clean");
                process.StandardInput.WriteLine("convert gpt");
                process.StandardInput.WriteLine("create partition efi size=100");
                process.StandardInput.WriteLine("format fs=fat32 quick");
                process.StandardInput.WriteLine("assign letter i");
                process.StandardInput.WriteLine("create partition msr size=16");
                process.StandardInput.WriteLine("create partition primary");
                process.StandardInput.WriteLine("format fs=ntfs quick");
                process.StandardInput.WriteLine("assign letter j");
                process.StandardInput.WriteLine("exit");
                process.WaitForExit();
                process.Close();
                process.Dispose();
                Console.WriteLine($"Disk {diskNumber} has been formatted successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
