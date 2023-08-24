using Lazar.Srevices.Iterfaces.Administration;
using Lazar.Srevices.Iterfaces.Logging;

namespace Lazar.Srevices.Iterfaces.Common {
    public interface IServiceManager {
        IRoleService RoleService { get; }
        IUsersService UsersService { get; }
        ILoggingervice LoggingService { get; }
    }
}
