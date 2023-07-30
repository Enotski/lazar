﻿using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Domain.Interfaces.Repositories.Auth;
using Lazar.Domain.Interfaces.Repositories.Logging;

namespace Lazar.Domain.Interfaces.Repositories.Common {
    public interface IRepositoryManager {
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        ITokenRepository TokenRepository { get; }
        IAuthRepository AuthRepository { get; }
        ISystemLogRepository SystemLogRepository { get; }
    }
}