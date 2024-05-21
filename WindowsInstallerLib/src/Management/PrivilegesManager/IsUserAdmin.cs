using System;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Principal;

namespace WindowsInstallerLib.Management.PrivilegesManager
{
    [SupportedOSPlatform("windows")]
    public class GetPrivileges
    {
        /// <summary>
        /// Checks if the current user has Administrator privileges.
        /// </summary>
        /// <returns>true or false</returns>
        public static bool IsUserAdmin()
        {
            try
            {
                WindowsPrincipal CurrentUser = new(WindowsIdentity.GetCurrent());
                bool CurrentUserIsAdministrator = CurrentUser.IsInRole(WindowsBuiltInRole.Administrator);
                return CurrentUserIsAdministrator;
            }
            catch(SecurityException)
            {
                throw;
            }
            catch(ArgumentException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
