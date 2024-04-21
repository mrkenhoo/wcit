using System.IO;

namespace WindowsInstallerLib.Utilities.Deployment
{
    public partial class NewDeploy
    {
        /// <summary>
        /// Looks for the image file (install.esd or install.wim) at <paramref name="SourceDrive"/>.
        /// </summary>
        /// <param name="SourceDrive"></param>
        /// <returns>
        /// The path of the image file:
        /// <paramref name="SourceDrive"/>\sources\install.esd or <paramref name="SourceDrive"/>\sources\install.wim
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static string GetImageFile(string SourceDrive)
        {
            if (File.Exists(@$"{SourceDrive}\sources\install.esd"))
            {
                ImageFile = @$"{SourceDrive}\sources\install.esd";
                return ImageFile;
            }
            else if (File.Exists(@$"{SourceDrive}\sources\install.wim"))
            {
                ImageFile = @$"{SourceDrive}\sources\install.wim";
                return ImageFile;
            }
            else
            {
                throw new FileNotFoundException(@$"Could not find a valid image file at {SourceDrive}.");
            }
        }
    }
}
