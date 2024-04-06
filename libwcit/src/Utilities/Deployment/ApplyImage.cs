using libwcit.Management.ProcessManager;
using System;
using System.IO;
using System.Runtime.Versioning;

namespace libwcit.Utilities.Deployment
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
            if (string.IsNullOrEmpty(ImageFile))
            {
                throw new ArgumentNullException(nameof(ImageFile), $"'{nameof(ImageFile)}' cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(DestinationDrive))
            {
                throw new ArgumentNullException(nameof(DestinationDrive), $"'{nameof(DestinationDrive)}' cannot be null or empty.");
            }

            if (ImageFile.Length <= 0)
            {
                throw new InvalidDataException(@$"Invalid {ImageFile}");
            }

            if (ImageIndex < 0)
            {
                throw new ArgumentException("No Windows edition was chosen", nameof(ImageIndex));
            }

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
                    Worker.ExitCode = 2;
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
