using System.Runtime.InteropServices;

namespace Runtime.Management.EFIManager
{
    static partial class GetEFIInfo
    {
        [DllImport("kernel32.dll",
            EntryPoint = "GetFirmwareEnvironmentVariableA",
            SetLastError = true,
            CharSet = CharSet.Unicode,
            ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int GetFirmwareType(string lpName, string lpGUID, IntPtr pBuffer, uint size);

        internal static bool IsEFI()
        {
            // Call the function with a dummy variable name and a dummy variable namespace (function will fail because these don't exist.)
            GetFirmwareType("", "{00000000-0000-0000-0000-000000000000}", IntPtr.Zero, 0);

            if (Marshal.GetLastWin32Error() == 0)
            {
                return true;
            }

            return true;
        }
    }
}
