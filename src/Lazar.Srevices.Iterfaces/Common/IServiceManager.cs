using Lazar.Srevices.Iterfaces.Administration;
using Lazar.Srevices.Iterfaces.Logging;

namespace Lazar.Srevices.Iterfaces.Common {
    /// <summary>
    /// Manager of services
    /// </summary>
    public interface IServiceManager {
        /// <summary>
        /// Service of roles
        /// </summary>
        IRolesService RoleService { get; }
        /// <summary>
        /// Service of users
        /// </summary>
        IUsersService UsersService { get; }
        /// <summary>
        /// Service of system events logging
        /// </summary>
        ISystemLogService LoggingService { get; }
    }
}
