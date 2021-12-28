using System;
using System.Windows.Forms;

namespace wcit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string version = "v20211228";
            Console.Title = $"Windows CLI Installer Tool - version {version}";
            if (GetCurrentRole.IsUserAdmin() == true)
            {
                Console.Clear();

                //DiskManager.GetPhysicalDisks();

                //Console.WriteLine("Please type the disk number of the device you want to format (e.g. 0):");
                //string diskNumber = Console.ReadLine();

                //Console.WriteLine("");

                //DiskManager.FormatDrive(diskNumber);

                Console.WriteLine("");

                //Console.WriteLine("Type the letter where the ISO is mounted at (e.g. D:):");
                //string source_drive = Console.ReadLine();

                Console.WriteLine("");

                //DeployManager.GetImageInfo(source_drive);

                //Console.WriteLine("Type the index number of the Windows edition to install (e.g. 1): ");
                //string windows_edition = Console.ReadLine();

                //Console.WriteLine("");
                //Console.WriteLine($"Deploying Windows from the drive {source_drive} to Disk {diskNumber}, please wait...");
                //DeployManager.DeployWindows(source_drive, windows_edition);

                Console.WriteLine("");
                Console.WriteLine("Installing bootloader...");
                DeployManager.InstallBootloader();

                Console.WriteLine("");

                Console.WriteLine("Windows has been deployed and it's ready to use");
                Console.WriteLine("Press ENTER to close the window");
                Console.ReadLine();
            }
            else
            {
                var Message = "This program needs administator privileges to work.";
                var Caption = "Insufficient privileges";
                var ButtonLayout = MessageBoxButtons.OK;
                var Icon = MessageBoxIcon.Error;
                MessageBox.Show(Message, Caption, ButtonLayout, Icon);
            }
        }
    }
}
