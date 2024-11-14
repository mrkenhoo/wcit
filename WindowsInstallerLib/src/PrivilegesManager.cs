using System;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Principal;

namespace WindowsInstallerLib
{
    namespace Management
    {
        [SupportedOSPlatform("windows")]
        static class PrivilegesManager
        {
            /// <summary>
            /// Checks if the current user has Administrator privileges.
            /// </summary>
            /// <returns>true or false</returns>
            internal static bool IsUserAdmin()
            {
                try
                {
                    WindowsPrincipal CurrentUser = new(WindowsIdentity.GetCurrent());
                    bool CurrentUserIsAdministrator = CurrentUser.IsInRole(WindowsBuiltInRole.Administrator);
                    return CurrentUserIsAdministrator;
                }
                catch (SecurityException)
                {
                    throw;
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
