using System;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Principal;

namespace WindowsInstallerLib
{
    /// <summary>
    /// Manages the privileges of the current user.
    /// </summary>
    internal static class PrivilegesManager
    {
        /// <summary>
        /// Checks if the current user is an administrator.
        /// </summary>
        /// <returns></returns>
        [SupportedOSPlatform("windows")]
        internal static bool IsAdmin()
        {
            try
            {
                WindowsPrincipal principal = new(WindowsIdentity.GetCurrent());
                bool isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
                return isAdmin;
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
