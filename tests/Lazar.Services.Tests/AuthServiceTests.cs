using Lazar.Domain.Core.Keys;
using Lazar.Infrastructure.JwtAuth.Models;
using Lazar.Infrastructure.JwtAuth.Models.Dto;
using Lazar.Infrastructure.JwtAuth.Services;
using Lazar.Services.Administration;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Tests.Mocks;
using Microsoft.Extensions.Options;

namespace Lazar.Services.Tests {
    public class AuthServiceTests {
        [Fact]
        public async void LoginTets() {
            var authRepoManagerMock = MockIAuthRepositoryManager.GetMock();
            var repoManagerMock = MockIRepositoryManager.GetMock();
            IOptions<AuthConfiguration> options = Options.Create(new AuthConfiguration("key", "issuer", "audience"));
            var authService = new AuthService(repoManagerMock.Object, authRepoManagerMock.Object, options);
            var user = TestData.Users.First();
            var result = await authService.LogInAsync(new Infrastructure.JwtAuth.Models.Dto.LogInRequestDto(user.Login, TestData.FakeUsersPass));

            Assert.Equal(user.Login, result.Login);
        }
        [Fact]
        public async void RegisterTest() {
            var authRepoManagerMock = MockIAuthRepositoryManager.GetMock();
            var repoManagerMock = MockIRepositoryManager.GetMock();
            IOptions<AuthConfiguration> options = Options.Create(new AuthConfiguration("key", "issuer", "audience"));
            var authService = new AuthService(repoManagerMock.Object, authRepoManagerMock.Object, options);
            var user = TestData.Users.First();
            string newlogin = user.Login + "new";
            string newEmail = user.Email + "new";
            var model = new UserRegisterRequestDto(newlogin, user.Password, newEmail);
            var newModel = await authService.RegisterAsync(model);

            Assert.NotNull(newModel);
            Assert.Equal(newlogin, newModel.Login);
        }
        [Fact]
        public async void LogOutTest() {
            var authRepoManagerMock = MockIAuthRepositoryManager.GetMock();
            var repoManagerMock = MockIRepositoryManager.GetMock();
            IOptions<AuthConfiguration> options = Options.Create(new AuthConfiguration("key", "issuer", "audience"));
            var authService = new AuthService(repoManagerMock.Object, authRepoManagerMock.Object, options);
            var user = TestData.Users.First();
            var model = new LogOutRequestDto(user.Login);
            await authService.LogOutAsync(model);

            var removed = TestData.AuthModels.FirstOrDefault(x => x.Login == user.Login);

            Assert.Null(removed);
        }
    }
}
