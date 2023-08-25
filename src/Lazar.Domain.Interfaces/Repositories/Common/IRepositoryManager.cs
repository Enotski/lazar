using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Domain.Interfaces.Repositories.Logging;

namespace Lazar.Domain.Interfaces.Repositories.Common {
    /// <summary>
    /// Main repository manager
    /// </summary>
    public interface IRepositoryManager {
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        ISystemLogRepository SystemLogRepository { get; }
    }
}