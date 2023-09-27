﻿using Runtime.Management.ProcessManager;
using System;
using System.IO;
using System.Runtime.Versioning;

namespace Runtime.Utilities.Deployment
{
    [SupportedOSPlatform("windows")]
    public static partial class NewDeploy
    {
        public static void GetImageInfo(string SourceDrive)
        {
            try
            {
                if (File.Exists($"{SourceDrive}\\sources\\install.esd"))
                {
                    Worker.StartCmdProcess("dism", $"/get-imageinfo /imagefile:{SourceDrive}\\sources\\install.esd");
                }
                else if (File.Exists($"{SourceDrive}\\sources\\install.wim"))
                {
                    Worker.StartCmdProcess("dism", $"/get-imageinfo /imagefile:{SourceDrive}\\sources\\install.wim");
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
