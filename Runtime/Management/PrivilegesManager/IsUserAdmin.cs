using System.Security.Principal;

namespace Runtime.Management.PrivilegesManager
{
    public sealed class GetPrivileges
    {
        public static bool IsUserAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
