using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Domain.Interfaces.Repositories.Logging;
using Lazar.Srevices.Iterfaces.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazar.Services.Tests.Mocks {
    internal class MockIRepositoryManager {
        public static Mock<IRepositoryManager> GetMock() {
            var mock = new Mock<IRepositoryManager>();
            var usersRepoMock = MockIUsersRepository.GetMock();
            var rolesRepoMock = MockIRolesRepository.GetMock();
            var systemLogRepoMock = MockISystemLogRepository.GetMock();

            mock.Setup(x => x.RoleRepository).Returns(() => rolesRepoMock.Object);
            mock.Setup(x => x.UserRepository).Returns(() => usersRepoMock.Object);
            mock.Setup(x => x.SystemLogRepository).Returns(() => systemLogRepoMock.Object);
            // Setup the mock
            return mock;
        }
    }
}
