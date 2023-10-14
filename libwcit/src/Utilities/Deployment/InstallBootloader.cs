using libwcit.Management.ProcessManager;
using System;
using System.IO;
using System.Runtime.Versioning;

namespace libwcit.Utilities.Deployment
{
    [SupportedOSPlatform("windows")]
    public static partial class NewDeploy
    {
        /// <summary>
        /// Installs the bootloader into the <paramref name="EfiDrive"/> at the
        /// <paramref name="DestinationDrive"/> where the newly deployed Windows installation is found.
        /// The <paramref name="FirmwareType"/> needs to be set to BIOS or UEFI.
        /// </summary>
        /// <param name="DestinationDrive"></param>
        /// <param name="EfiDrive"></param>
        /// <param name="FirmwareType"></param>
        public static void InstallBootloader(string DestinationDrive, string EfiDrive, string FirmwareType)
        {
            try
            {
                if (Directory.Exists(EfiDrive + "\\EFI\\Boot") || Directory.Exists(EfiDrive + "\\EFI\\Microsoft"))
                {
                    Console.Error.WriteLine("The drive letter " + EfiDrive + "is already in use.");
                }
                else if (!Directory.Exists($"{DestinationDrive}\\windows"))
                {
                    throw new DirectoryNotFoundException($"Could not find the following directory: {DestinationDrive}\\windows");
                }
                else
                {
                    Worker.StartCmdProcess("bcdboot", $"{DestinationDrive}\\windows /s {EfiDrive} /f {FirmwareType}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
