using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Domain.Interfaces.Repositories.Auth;
using Lazar.Domain.Interfaces.Repositories.EventLogs;

namespace Lazar.Domain.Interfaces.Repositories.Common {
    public interface IRepositoryManager {
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IAuthRepository AuthRepository { get; }
        ISystemLogRepository SystemLogRepository { get; }
    }
}