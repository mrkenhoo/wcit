using System;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace WindowsInstallerLib.Management.ProcessManager
{
    [SupportedOSPlatform("windows")]
    public static partial class Worker
    {
        public static int StartCmdProcess(string fileName, string args, bool RunAsAdministrator)
        {
            try
            {
                Process process = new();
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = args;
                if (RunAsAdministrator)
                {
                    process.StartInfo.Verb = "RunAs";
                }
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.WaitForExit();
                ExitCode = process.ExitCode;

                string output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);

                process.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return ExitCode;
        }
    }
}
