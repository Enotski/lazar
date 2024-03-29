﻿using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Domain.Interfaces.Repositories.Logging;

namespace Lazar.Domain.Interfaces.Repositories.Common {
    /// <summary>
    /// Main repository manager
    /// </summary>
    public interface IRepositoryManager {
        /// <summary>
        /// Roles repository
        /// </summary>
        IRolesRepository RoleRepository { get; }
        /// <summary>
        /// Users repository
        /// </summary>
        IUsersRepository UserRepository { get; }
        /// <summary>
        /// System logs repository
        /// </summary>
        ISystemLogRepository SystemLogRepository { get; }
    }
}