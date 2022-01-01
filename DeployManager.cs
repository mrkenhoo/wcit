using System;
using System.Diagnostics;

namespace wcit
{
    internal class DeployManager
    {
        public static void GetImageInfo(string source_drive)
        {
            try
            {
                if (System.IO.File.Exists($"{source_drive}\\sources\\install.esd"))
                {
                    Worker.StartCmdProcess("dism", $"/get-imageinfo /imagefile:{source_drive}\\sources\\install.esd");
                }
                else if (System.IO.File.Exists($"{source_drive}\\sources\\install.wim"))
                {
                    Worker.StartCmdProcess("dism", $"/get-imageinfo /imagefile:{source_drive}\\sources\\install.wim");
                }
                else
                {
                    Console.Error.WriteLine("No valid image found.");
                    Environment.Exit(1);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DeployWindows(string source_drive, string windows_edition)
        {
            try
            {
                if (System.IO.File.Exists($"{source_drive}\\sources\\install.esd"))
                {
                    Worker.StartDismProcess($"/apply-image /imagefile:{source_drive}\\sources\\install.esd /applydir:j:\\ /index:{windows_edition} /verify");
                }
                else if (System.IO.File.Exists($"{source_drive}\\sources\\install.wim"))
                {
                    Worker.StartDismProcess($"/apply-image /imagefile:{source_drive}\\sources\\install.wim /applydir:j:\\ /index:{windows_edition} /verify");
                }
                else
                {
                    Console.Error.WriteLine("No valid image found.");
                    Environment.Exit(1);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void InstallBootloader()
        {
            try
            {
                Worker.StartCmdProcess("bcdboot", "j:\\windows /s i: /f UEFI");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
