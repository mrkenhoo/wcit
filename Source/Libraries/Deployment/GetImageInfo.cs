using System;
using wcit.Libraries.ProcessManager;

namespace wcit.Libraries.Deployment
{
    public static partial class NewDeploy
    {
        public static void GetImageInfo(string SourceDrive)
        {
            try
            {
                if (System.IO.File.Exists($"{SourceDrive}\\sources\\install.esd"))
                {
                    Worker.StartCmdProcess("dism", $"/get-imageinfo /imagefile:{SourceDrive}\\sources\\install.esd");
                }
                else if (System.IO.File.Exists($"{SourceDrive}\\sources\\install.wim"))
                {
                    Worker.StartCmdProcess("dism", $"/get-imageinfo /imagefile:{SourceDrive}\\sources\\install.wim");
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
    }
}
