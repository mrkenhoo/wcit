using System;
using System.IO;
using System.Runtime.Versioning;
using WindowsInstallerLib.Management.Installer;
using WindowsInstallerLib.Management.PrivilegesManager;
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
        public static int ApplyImage(string ImageFilePath, string DestinationDrive, int ImageIndex)
        {
            ArgumentException.ThrowIfNullOrEmpty(ImageFilePath, nameof(ImageFilePath));
            ArgumentException.ThrowIfNullOrWhiteSpace(DestinationDrive, nameof(DestinationDrive));

            ArgumentOutOfRangeException.ThrowIfEqual(0, ImageFilePath.Length);
            ArgumentOutOfRangeException.ThrowIfEqual(0, ImageIndex);

            try
            {
                if (!Directory.Exists($@"{DestinationDrive}\windows"))
                {
                    switch (GetPrivileges.IsUserAdmin())
                    {
                        case true:
                            Worker.StartDismProcess(@$"/apply-image /imagefile:{ImageFilePath} /applydir:{DestinationDrive} /index:{ImageIndex} /verify");
                            return Worker.ExitCode;
                        case false:
                            Worker.StartDismProcess(@$"/apply-image /imagefile:{ImageFilePath} /applydir:{DestinationDrive} /index:{ImageIndex} /verify", true);
                            return Worker.ExitCode;
                    }
                }
                else
                {
                    Console.Error.WriteLine("Windows seems to be already deployed, not overwriting it.");
                    return 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
