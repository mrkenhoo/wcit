using Microsoft.Win32;

namespace Runtime.Management.EFIManager
{
    static partial class GetEFIInfo
    {
        internal static bool IsEFI()
        {
            const string UefiRegistryKeyPath = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control";
            const string UefiRegistryValueName = "FirmwareEnvironmentVariable";

            object firmwareEnvironmentVariable = Registry.GetValue(UefiRegistryKeyPath, UefiRegistryValueName, null);

            return firmwareEnvironmentVariable != null;
        }
    }
}
