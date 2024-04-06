using libwcit.Management.ProcessManager;
using Microsoft.Dism;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Versioning;

namespace libwcit.Utilities.Deployment
{
    [SupportedOSPlatform("windows")]
    public partial class NewDeploy
    {
        /// <summary>
        /// Gets all Windows editions available from the <paramref name="ImageFile"/> using DISM, if any.
        /// </summary>
        /// <param name="ImageFile"></param>
        public static void GetImageInfo(string ImageFile)
        {
            try
            {
                if (ImageFile != null)
                {
                    Worker.StartCmdProcess("dism.exe", @$"/get-imageinfo /imagefile:{ImageFile}");
                }
                else
                {
                    throw new FileNotFoundException("No image file was specified");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Tuple<int, string>> GetImageInfoT(string SourceDrive, string ImageFile)
        {
            if (string.IsNullOrWhiteSpace(SourceDrive))
            {
                throw new ArgumentException($"'{nameof(SourceDrive)}' cannot be null or whitespace.", nameof(SourceDrive));
            }
            if (string.IsNullOrWhiteSpace(ImageFile))
            {
                throw new ArgumentException($"'{nameof(ImageFile)}' cannot be null or whitespace.", nameof(ImageFile));
            }

            try
            {
                List<Tuple<int, string>> ImageList = [];

                DismApi.Initialize(DismLogLevel.LogErrors);

                DismApi.GetImageInfo(ImageFile);

                DismImageInfoCollection imageInfos = DismApi.GetImageInfo(GetImageFile(SourceDrive));

                foreach (DismImageInfo imageInfo in imageInfos)
                {
                    ImageList.ForEach(imageInfos =>
                    {
                        ImageList.Add(Tuple.Create(imageInfo.ImageIndex, imageInfo.ImageName));
                    });
                }

                DismApi.Shutdown();

                return ImageList;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
