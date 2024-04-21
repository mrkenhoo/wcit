using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Versioning;
using WindowsInstallerLib.Management.ProcessManager;
using Microsoft.Dism;
using WindowsInstallerLib.Management.PrivilegesManager;

namespace WindowsInstallerLib.Utilities.Deployment
{
    [SupportedOSPlatform("windows")]
    public partial class NewDeploy
    {
        /// <summary>
        /// Gets all Windows editions available from the <paramref name="ImageFile"/> using DISM, if any.
        /// </summary>
        /// <param name="ImageFile"></param>
        public static int GetImageInfo(string ImageFile)
        {
            try
            {
                if (NewDeploy.ImageFile != null)
                {
                    switch (GetPrivileges.IsUserAdmin())
                    {
                        case true:
                            Worker.StartCmdProcess("dism.exe", @$"/get-imageinfo /imagefile:{NewDeploy.ImageFile}");
                            return Worker.ExitCode;

                        case false:
                            Worker.StartCmdProcess("dism.exe", @$"/get-imageinfo /imagefile:{NewDeploy.ImageFile}", RunAsAdministrator: true);
                            return Worker.ExitCode;
                    }
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
