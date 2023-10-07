using libwcit.Management.ProcessManager;
using System;
using System.IO;
using System.Runtime.Versioning;

namespace libwcit.Utilities.Deployment
{
    [SupportedOSPlatform("windows")]
    public partial class NewDeploy
    {
        public static void ApplyImage(string SourceDrive, string DestinationDrive, int Index)
        {
            if (SourceDrive == null)
            {
                throw new ArgumentNullException(nameof(SourceDrive));
            }
            else if (DestinationDrive == null)
            {
                throw new ArgumentNullException(nameof(DestinationDrive));
            }
            if (Index <= 0)
            {
                throw new ArgumentException("No Windows edition was chosen", nameof(Index));
            }

            try
            {
                if (Directory.Exists(DestinationDrive + "\\windows"))
                {
                    if (File.Exists($"{SourceDrive}\\sources\\install.esd"))
                    {
                        Worker.StartDismProcess($"/apply-image /imagefile:{SourceDrive}\\sources\\install.esd /applydir:{DestinationDrive}\\ /index:{Index} /verify");
                    }
                    else if (File.Exists($"{SourceDrive}\\sources\\install.wim"))
                    {
                        Worker.StartDismProcess($"/apply-image /imagefile:{SourceDrive}\\sources\\install.wim /applydir:{DestinationDrive}\\ /index:{Index} /verify");
                    }
                    else
                    {
                        throw new FileNotFoundException("Could not find a valid image");
                    }
                }
                else
                {
                    Console.Error.WriteLine("Windows seems to be already deployed, not overwriting it.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
