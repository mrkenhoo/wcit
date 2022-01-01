using System;
using System.Diagnostics;

namespace wcit
{
    internal class Worker
    {
        public static void StartProcess(string fileName, string args)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = args;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                process.Start();
                process.WaitForExit();
                process.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void StartCmdProcess(string fileName, string args)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = args;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.WaitForExit();
                string output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
                process.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
