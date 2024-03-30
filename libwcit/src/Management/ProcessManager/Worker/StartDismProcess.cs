using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace libwcit.Management.ProcessManager
{
    [SupportedOSPlatform("windows")]
    public static partial class Worker
    {
        public static int StartDismProcess(string args)
        {
            try
            {
                Process process = new();
                process.StartInfo.FileName = "dism.exe";
                process.StartInfo.Arguments = args;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = false;
                process.Start();
                process.WaitForExit();
                ExitCode = process.ExitCode;
                process.Close();
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

            return ExitCode;
        }
    }
}
