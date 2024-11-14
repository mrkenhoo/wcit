using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace WindowsInstallerLib
{
    namespace Management
    {
        [SupportedOSPlatform("windows")]
        static partial class SystemInfoManager
        {
            [LibraryImport("kernel32.dll", EntryPoint = "GetFirmwareEnvironmentVariableA", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
            [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
            private static partial void GetFirmwareType(string lpName, string lpGUID, IntPtr pBuffer, uint size);

            /// <summary>
            /// Checks if the system supports EFI.
            /// </summary>
            /// <returns>true or false</returns>
            internal static bool IsEFI()
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
}
