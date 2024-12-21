using System;
using System.Runtime.Versioning;
using WindowsInstallerLib;

namespace ConsoleApp
{
    [SupportedOSPlatform("windows")]
    internal sealed class Program
    {
        [MTAThread]
        internal static int Main()
        {
            InstallerParameters parameters = new();

            try
            {
                ProgramInfo.GetInformation();
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                InstallerManager.ConfigureInstaller(ref parameters);

                InstallerManager.InstallWindows(ref parameters);
            }
            catch (Exception)
            {
                throw;
            }

            return 0;
        }
    }
}
