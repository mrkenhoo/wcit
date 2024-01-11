using System;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace libwcit.Management.ProcessManager
{
    [SupportedOSPlatform("windows")]
    public static partial class Worker
    {
        public static int StartProcess(string fileName, string args)
        {
            try
            {
                Process process = new();
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = args;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                process.Start();
                process.WaitForExit();
                ExitCode = process.ExitCode;
                process.Close();

                return process.ExitCode;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
