using System;
using System.Diagnostics;

namespace wcit.Libraries.ProcessManager
{
    public static partial class Worker
    {
        public static void StartProcess(string fileName, string args)
        {
            try
            {
                Process process = new();
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = args;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
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
