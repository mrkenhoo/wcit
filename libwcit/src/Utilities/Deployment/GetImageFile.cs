using System.IO;

namespace libwcit.Utilities.Deployment
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
            }
            else if (File.Exists(@$"{SourceDrive}\sources\install.wim"))
            {
                ImageFile = @$"{SourceDrive}\sources\install.esd";
            }
            else
            {
                throw new FileNotFoundException(@$"Could not find an image file.");
            }

            return ImageFile;
        }
    }
}
