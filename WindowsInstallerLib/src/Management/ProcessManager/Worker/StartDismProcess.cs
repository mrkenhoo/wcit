using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace WindowsInstallerLib.Management.ProcessManager
{
    [SupportedOSPlatform("windows")]
    public static partial class Worker
    {
        public static int StartDismProcess(string args, bool RunAsAdministrator = false)
        {
            Process process = new();

            try
            {
                process.StartInfo.FileName = "dism.exe";
                process.StartInfo.Arguments = args;
                if (RunAsAdministrator)
                {
                    process.StartInfo.Verb = "RunAs";
                    process.StartInfo.UseShellExecute = true;
                }
                else
                {
                    process.StartInfo.UseShellExecute = false;
                }
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.WaitForExit();
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Win32Exception)
            {
                throw;
            }
            catch (PlatformNotSupportedException)
            {
                throw;
            }
            finally
            {
                ExitCode = process.ExitCode;
                process.Close();
            }

            return ExitCode;
        }
    }
}
