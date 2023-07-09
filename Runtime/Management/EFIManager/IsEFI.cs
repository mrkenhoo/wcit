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

        internal static int IsEFI()
        {
            return GetFirmwareType("", "{00000000-0000-0000-0000-000000000000}", IntPtr.Zero, 0);
        }
    }
}
