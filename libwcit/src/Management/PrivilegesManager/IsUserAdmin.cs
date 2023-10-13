using System.Runtime.Versioning;
using System.Security.Principal;

namespace libwcit.Management.PrivilegesManager
{
    [SupportedOSPlatform("windows")]
    public sealed class GetPrivileges
    {
        /// <summary>
        /// Checks if the current user has Administrator privileges.
        /// </summary>
        /// <returns>true or false</returns>
        public static bool IsUserAdmin()
        {
            WindowsPrincipal principal = new(WindowsIdentity.GetCurrent());
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
