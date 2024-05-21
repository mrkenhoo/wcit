using System;
using System.IO;
using WindowsInstallerLib.Management.Installer;

namespace WindowsInstallerLib.Utilities.Deployment
{
    public partial class NewDeploy
    {
        /// <summary>
        /// Searches for a valid image file and return it's full path.
        /// </summary>
        /// <param name="InstallationSettings"></param>
        /// <returns></returns>
        public static string GetImageFile(string SourceDrive)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(nameof(SourceDrive));

                if (File.Exists(@$"{SourceDrive}\sources\install.esd"))
                {
                    NewInstallation.ImageFilePath = @$"{SourceDrive}\sources\install.esd";
                }
                else if (File.Exists(@$"{SourceDrive}\sources\install.wim"))
                {
                    NewInstallation.ImageFilePath = @$"{SourceDrive}\sources\install.wim";
                }
                else
                {
                    throw new FileNotFoundException(@$"Could not find a valid image file at {SourceDrive}.");
                }

                return NewInstallation.ImageFilePath;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
