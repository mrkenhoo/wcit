using libwcit.Management.EFIManager;
using libwcit.Management.Installer;
using libwcit.Management.PrivilegesManager;
using System;
using System.Reflection;

namespace wcit
{
    internal class Program
    {
        [MTAThread]
        private static int Main(string[] args)
        {
            Console.Title = $"{Assembly.GetExecutingAssembly().GetName().FullName} v{Assembly.GetExecutingAssembly().GetName().Version}";

#if WINDOWS10_0_22621_0_OR_GREATER && NET8_0_OR_GREATER
            switch (GetPrivileges.IsUserAdmin())
            {
                case true:
                    try
                    {
                        if (!GetEFIInfo.IsEFI())
                        {
                            throw new PlatformNotSupportedException("An error has occurred: Your system does not support EFI.");
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
#else
            throw new NotSupportedException("This system is not compatible with this program.");
#endif
            return Environment.ExitCode;
        }
    }
}
