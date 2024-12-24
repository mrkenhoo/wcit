using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace WindowsInstallerLib
{
    [SupportedOSPlatform("windows")]
    internal static partial class SystemInfoManager
    {
        /// <summary>
        /// Retrieves the firmware type of the system.
        /// </summary>
        /// <param name="lpName"></param>
        /// <param name="lpGUID"></param>
        /// <param name="pBuffer"></param>
        /// <param name="size"></param>

        [LibraryImport("kernel32.dll", EntryPoint = "GetFirmwareEnvironmentVariableA", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
        [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
        private static partial uint GetFirmwareType(string lpName, string lpGUID, IntPtr pBuffer, uint size);

        /// <summary>
        /// Checks if the system is using EFI firmware.
        /// </summary>
        /// <returns></returns>
        internal static bool IsEFI()
        {
            // Call the function with a dummy variable name and a dummy variable namespace (function will fail because these don't exist.)
            GetFirmwareType("DummyVariableName", "{00000000-0000-0000-0000-000000000000}", IntPtr.Zero, 0);

            // Check the last Win32 error to determine if the system supports EFI
            return Marshal.GetLastWin32Error() != 1;
        }
    }
}
