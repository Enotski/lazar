using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Domain.Interfaces.Repositories.Common;

namespace Lazar.Infrastructure.Data.Common
{
    public class RepositoryManager : IRepositoryManager
    {
        private Lazy<IRoleRepository> _lazyRoleRepository;
        public IRoleRepository RoleRepository => _lazyRoleRepository.Value;

        IUserProfileRepository UserProfileRepository => throw new NotImplementedException();

        IUsersRepository UsersRepository => throw new NotImplementedException();

        IAuthRepository AuthRepository => throw new NotImplementedException();

        IEventLogRepository EventLogRepository { get; };

        public RepositoryManager(Lazar) {
            _lazySystemLogRepository = new Lazy<SystemLogRepository>(() => new SystemLogRepository(dbContext));
        }
    }
}
