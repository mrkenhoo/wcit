using System;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security.Principal;

namespace WindowsInstallerLib.Management.PrivilegesManager
{
    [SupportedOSPlatform("windows")]
    public class GetPrivileges
    {
        /// <summary>
        /// Checks if the current user has Administrator privileges.
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsUserAdmin()
        {
            try
            {
                WindowsPrincipal CurrentUser = new(WindowsIdentity.GetCurrent());
                bool CurrentUserIsAdministrator = CurrentUser.IsInRole(WindowsBuiltInRole.Administrator);
                return CurrentUserIsAdministrator;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
