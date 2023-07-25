using Lazar.Srevices.Iterfaces.Administration;
using Lazar.Srevices.Iterfaces.Auth;
using Lazar.Srevices.Iterfaces.EventLog;

namespace Lazar.Srevices.Iterfaces.Common {
    public interface IServiceManager {
        IRoleService RoleService { get; }
        IUsersService UsersService { get; }
        IAuthService AuthService { get; }
        IEventLogService EventLogService { get; }
    }
}
