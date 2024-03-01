using libwcit.Management.EFIManager;
using libwcit.Management.Installer;
using libwcit.Management.PrivilegesManager;
using System;
using System.Reflection;

namespace cli_app
{
    internal class Program
    {
        [MTAThread]
        private static int Main(string[] args)
        {
            string? ProgramName = Assembly.GetExecutingAssembly().GetName().Name;
            Version? ProgramVersion = Assembly.GetExecutingAssembly().GetName().Version;
            Console.Title = $"{ProgramName} v{ProgramVersion}";

#if WINDOWS10_0_22621_0_OR_GREATER && NET8_0_OR_GREATER
            switch (GetPrivileges.IsUserAdmin())
            {
                case true:
                    try
                    {
                        if (!GetEFIInfo.IsEFI())
                        {
                            throw new PlatformNotSupportedException("Your system does not support EFI.");
                        }

                        Console.Clear();

                        Console.WriteLine("Welcome to the Windows CLI Installer Tool!\nCreated by Felipe González Martín");

                        Configuration.SetupInstaller();

                        Configuration.InstallWindows();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    break;
                case false:
                    throw new UnauthorizedAccessException("This program needs administrator privileges to work.");
            }

            return 0;
#else
            throw new NotSupportedException("This system is not compatible with this program.");
#endif
        }
    }
}
