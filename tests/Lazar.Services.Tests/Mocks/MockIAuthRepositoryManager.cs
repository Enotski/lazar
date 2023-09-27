using Lazar.Infrastructure.JwtAuth.Iterfaces.Auth;
using Moq;

namespace Lazar.Services.Tests.Mocks {
    internal class MockIAuthRepositoryManager {
        public static Mock<IAuthRepositoryManager> GetMock() {
            var mock = new Mock<IAuthRepositoryManager>();
            var authRepoMock = MockIAuthRepository.GetMock();
            var tokensRepoMock = MockITokenRepository.GetMock();

            mock.Setup(x => x.AuthRepository).Returns(() => authRepoMock.Object);
            mock.Setup(x => x.TokenRepository).Returns(() => tokensRepoMock.Object);

            return mock;
        }
    }
}
