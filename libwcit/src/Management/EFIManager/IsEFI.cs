using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace libwcit.Management.EFIManager
{
    [SupportedOSPlatform("windows")]
    public static partial class GetEFIInfo
    {
        [DllImport("kernel32.dll",
            EntryPoint = "GetFirmwareEnvironmentVariableA",
            SetLastError = true,
            CharSet = CharSet.Unicode,
            ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int GetFirmwareType(string lpName, string lpGUID, IntPtr pBuffer, uint size);

        public static bool IsEFI()
        {
            // Call the function with a dummy variable name and a dummy variable namespace (function will fail because these don't exist.)
            GetFirmwareType("", "{00000000-0000-0000-0000-000000000000}", IntPtr.Zero, 0);

            if (Marshal.GetLastWin32Error() == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
