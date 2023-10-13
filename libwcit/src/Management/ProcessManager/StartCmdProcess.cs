﻿using System;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace libwcit.Management.ProcessManager
{
    [SupportedOSPlatform("windows")]
    public sealed partial class Worker
    {
        public static void StartCmdProcess(string fileName, string args)
        {
            try
            {
                Process process = new();
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = args;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.WaitForExit();
                string output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
                process.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}