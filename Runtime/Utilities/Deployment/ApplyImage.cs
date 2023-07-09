using Runtime.Management.ProcessManager;

namespace Runtime.Utilities.Deployment
{
    public static partial class NewDeploy
    {
        public static void ApplyImage(string SourceDrive, string DestinationDrive, int Index)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
