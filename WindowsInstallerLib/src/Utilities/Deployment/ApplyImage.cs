using System;
using System.IO;
using System.Runtime.Versioning;
using WindowsInstallerLib.Management.ProcessManager;

namespace WindowsInstallerLib.Utilities.Deployment
{
    [SupportedOSPlatform("windows")]
    public partial class NewDeploy
    {
        /// <summary>
        /// Deploys an image of Windows to the specified <paramref name="DestinationDrive"/>.
        /// What gets installed is specified by <paramref name="SourceDrive"/> and the <paramref name="Index"/>.
        /// </summary>
        /// <param name="SourceDrive"></param>
        /// <param name="DestinationDrive"></param>
        /// <param name="Index"></param>
        /// <exception cref="ArgumentException"></exception>
        public static int ApplyImage(string ImageFile, string DestinationDrive, int ImageIndex)
        {
            ArgumentException.ThrowIfNullOrEmpty(ImageFile, nameof(ImageFile));
            ArgumentException.ThrowIfNullOrWhiteSpace(DestinationDrive, nameof(DestinationDrive));

            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(0, ImageFile.Length, ImageFile);
            ArgumentOutOfRangeException.ThrowIfLessThan(0, ImageIndex);

            try
            {
                if (!Directory.Exists($@"{DestinationDrive}\windows"))
                {
                    Worker.StartDismProcess(@$"/apply-image /imagefile:{ImageFile} /applydir:{DestinationDrive}\ /index:{ImageIndex} /verify");
                    return Worker.ExitCode;
                }
                else
                {
                    Console.Error.WriteLine("Windows seems to be already deployed, not overwriting it.");
                    return Worker.ExitCode;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
