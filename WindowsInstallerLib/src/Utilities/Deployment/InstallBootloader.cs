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
        /// Installs the bootloader into the <paramref name="EfiDrive"/> where the newly deployed Windows installation is found.
        /// The <paramref name="FirmwareType"/> needs to be set to BIOS or UEFI.
        /// </summary>
        /// <param name="DestinationDrive"></param>
        /// <param name="EfiDrive"></param>
        /// <param name="FirmwareType"></param>
        /// <exception cref="ArgumentException"/>
        public static int InstallBootloader(string DestinationDrive, string EfiDrive, string FirmwareType, bool RunAsAdministrator = false)
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
                    if (Directory.Exists(@$"{DestinationDrive}\windows"))
                    {
                        switch (GetPrivileges.IsUserAdmin())
                        {
                            case true:
                                Worker.StartCmdProcess("bcdboot", @$"{DestinationDrive}\windows /s {EfiDrive} /f {FirmwareType}");
                                return Worker.ExitCode;
                            case false:
                                Worker.StartCmdProcess("bcdboot", @$"{DestinationDrive}\windows /s {EfiDrive} /f {FirmwareType}", RunAsAdministrator = true);
                                return Worker.ExitCode;
                        }
                    }
                    else
                    {
                        throw new DirectoryNotFoundException(@$"Could not find the directory {DestinationDrive}\windows");
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
