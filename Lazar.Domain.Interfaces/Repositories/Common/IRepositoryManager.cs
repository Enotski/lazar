using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Domain.Interfaces.Repositories.Auth;
using Lazar.Domain.Interfaces.Repositories.EventLog;

namespace Lazar.Domain.Interfaces.Repositories.Common {
    public interface IRepositoryManager {
        IRoleRepository RoleRepository { get; }
        IUsersRepository UsersRepository { get; }
        IAuthRepository AuthRepository { get; }
        ISystemLogRepository EventLogRepository { get; }
    }
}