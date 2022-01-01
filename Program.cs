using System;
using System.Windows.Forms;

namespace wcit
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            string version = "v0.0.1.1";
            Console.Title = $"Windows CLI Installer Tool - version {version}";
            if (GetCurrentRole.IsUserAdmin() == true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Windows CLI Installer Tool!\nCreated by Ken Hoo (mrkenhoo)\n\n==> Available disks on your system:");

                DiskManager.GetPhysicalDisks();

                Console.WriteLine("\n==> Please type the disk number to format (e.g. 0):");
                string diskNumber = Console.ReadLine();

                DiskManager.FormatDrive(diskNumber);

                Console.WriteLine("\n==> Type the letter where the ISO is mounted at below (e.g. D:).");
                string source_drive = Console.ReadLine();
                
                DeployManager.GetImageInfo(source_drive);

                Console.WriteLine("==> Type the index number of the Windows edition you wish to install below (e.g. 1).");
                string windows_edition = Console.ReadLine();

                Console.WriteLine($"\n==> Deploying Windows to disk {diskNumber}, please wait...");
                DeployManager.DeployWindows(source_drive, windows_edition);

                Console.WriteLine("\n==> Installing bootloader...");
                DeployManager.InstallBootloader();

                Console.WriteLine("Windows has been deployed and it's ready to use\n\nPress ENTER to close the window");
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
