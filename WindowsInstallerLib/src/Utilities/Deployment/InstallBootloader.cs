using System;
using System.IO;
using System.Runtime.Versioning;
using WindowsInstallerLib.Management.PrivilegesManager;
using WindowsInstallerLib.Management.ProcessManager;

namespace WindowsInstallerLib.Utilities.Deployment
{
    [SupportedOSPlatform("windows")]
    public partial class NewDeploy
    {
        /// <summary>
        /// Installs the bootloader to the EFI drive of a new Windows installation.
        /// </summary>
        /// <param name="InstallationSettings"/>
        /// <returns></returns>
        public static int InstallBootloader(string DestinationDrive, string EfiDrive, string FirmwareType)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(DestinationDrive, nameof(DestinationDrive));
            ArgumentException.ThrowIfNullOrWhiteSpace(EfiDrive, nameof(EfiDrive));
            ArgumentException.ThrowIfNullOrWhiteSpace(FirmwareType, nameof(FirmwareType));

            try
            {
                if (Directory.Exists(@$"{EfiDrive}\EFI\Boot") || Directory.Exists($@"{EfiDrive}\EFI\Microsoft"))
                {
                    throw new IOException($"The drive letter {EfiDrive} is already in use.");
                }
                else
                {
                    if (!Directory.Exists(@$"{DestinationDrive}windows"))
                    {
                        throw new DirectoryNotFoundException(@$"The directory {DestinationDrive}windows does not exist!");
                    }

                    switch (GetPrivileges.IsUserAdmin())
                    {
                        case true:
                            Worker.StartCmdProcess("bcdboot", @$"{DestinationDrive}\windows /s {EfiDrive} /f {FirmwareType}");
                            return Worker.ExitCode;
                        case false:
                            Worker.StartCmdProcess("bcdboot", @$"{DestinationDrive}\windows /s {EfiDrive} /f {FirmwareType}", true);
                            return Worker.ExitCode;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
