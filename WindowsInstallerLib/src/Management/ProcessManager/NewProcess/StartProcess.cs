using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace WindowsInstallerLib.Management.ProcessManager
{
    [SupportedOSPlatform("windows")]
    public static partial class NewProcess
    {
        public static int StartProcess(string filename, string args)
        {
            Process process = new();

            try
            {
                process.StartInfo.FileName = filename;
                process.StartInfo.Arguments = args;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
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
            catch(PlatformNotSupportedException)
            {
                throw;
            }
            catch (Exception)
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
