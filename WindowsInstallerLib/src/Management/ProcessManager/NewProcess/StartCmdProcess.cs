using System;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace WindowsInstallerLib.Management.ProcessManager
{
    [SupportedOSPlatform("windows")]
    public static partial class NewProcess
    {
        public static int StartCmdProcess(string fileName, string args, bool RunAsAdministrator = false)
        {
            try
            {
                Process process = new();
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = args;
                if (RunAsAdministrator)
                {
                    process.StartInfo.Verb = "runas";
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
                ExitCode = process.ExitCode;

                string output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);

                process.Close();
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }


            return ExitCode;
        }
    }
}
