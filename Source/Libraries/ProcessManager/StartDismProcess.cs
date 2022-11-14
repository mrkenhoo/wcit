using System;
using System.Diagnostics;

namespace wcit.Libraries.ProcessManager
{
    public partial class Worker
    {
        public static void StartDismProcess(string args)
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
                process.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
