﻿using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Domain.Interfaces.Repositories.Logging;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Administration;
using Lazar.Infrastructure.Data.Ef.Repositories.Logging;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Common {
    /// <summary>
    /// Main repository manager
    /// </summary>
    public class RepositoryManager : IRepositoryManager {
        /// <summary>
        /// Roles repository
        /// </summary>
        public IRoleRepository RoleRepository => _lazyRoleRepository.Value;
        private Lazy<IRoleRepository> _lazyRoleRepository;
        /// <summary>
        /// Users repository
        /// </summary>
        public IUserRepository UserRepository => _lazyUsersRepository.Value;
        private Lazy<IUserRepository> _lazyUsersRepository;
        /// <summary>
        /// System logs repository
        /// </summary>
        public ISystemLogRepository SystemLogRepository => _lazyLoggingRepository.Value;
        private Lazy<ISystemLogRepository> _lazyLoggingRepository;

        public RepositoryManager(LazarContext context) {
            _lazyRoleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(context));
            _lazyUsersRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
            _lazyLoggingRepository = new Lazy<ISystemLogRepository>(() => new SystemLogRepository(context));
        }
    }
}
