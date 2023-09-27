using Lazar.Domain.Core.Interfaces;
using Lazar.Infrastructure.Mapper;
using Lazar.Services.Administration;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Tests.Mocks;

namespace Lazar.Services.Tests {
    public class UsersSeviceTests {
        public IModelMapper GetMapper() {
            return new AutoModelMapper();
        }
        [Fact]
        public async void WhenGetAsyncById_ThenOneUserReturn() {
            var repoManagerMock = MockIRepositoryManager.GetMock();
            var mapper = GetMapper();
            var UserService = new UsersService(repoManagerMock.Object, mapper);

            var result = await UserService.GetAsync(TestData.Users[0].Id);

            Assert.NotNull(result);
            Assert.Equal(TestData.Users[0].Id, result.Data.Id);
        }
        [Fact]
        public async void WhenGetAsyncByDataTableRequestDto_ThenUsersByRequestReturn() {
            var repoManagerMock = MockIRepositoryManager.GetMock();
            var mapper = GetMapper();
            var UserService = new UsersService(repoManagerMock.Object, mapper);

            var op1 = new Contracts.Request.DataTable.Base.DataTableRequestDto() {
                Sorts = new List<SortOptionDto> {
                    new SortOptionDto(){ ColumnName = "Name", Type = Domain.Core.Enums.SortType.Descending }
                }
            };
            var op2 = new Contracts.Request.DataTable.Base.DataTableRequestDto() {
                Filters = new List<SearchOptionDto> {
                    new SearchOptionDto(){ ColumnName = "Name", Value = "admin" }
                }
            };
            var check1 = TestData.Users.OrderByDescending(x => x.Name);
            var check2 = TestData.Users.Where(x => x.Name.ToLower().Contains("admin"));

            var result1 = await UserService.GetAsync(op1);
            var result2 = await UserService.GetAsync(op2);

            Assert.Equal(check1.Count(), result1.data.Count());
            Assert.Equal(check1.Select(x => x.Id), result1.data.Select(x => x.Id.Value));

            Assert.Equal(check2.Count(), result2.data.Count());
            Assert.Equal(check2.Select(x => x.Id), result2.data.Select(x => x.Id.Value));
        }
        [Fact]
        public async void CrateUserTest() {
            var repoManagerMock = MockIRepositoryManager.GetMock();
            var mapper = GetMapper();
            var UserService = new UsersService(repoManagerMock.Object, mapper);
            var key = Guid.NewGuid();
            var changedBy = "CreateUserTestSys";
            var newUser = new Contracts.Administration.UserDto(key, "TestUser", "login", "pass", "Email", "", new List<Guid>() { TestData.Roles[0].Id }, changedBy, DateTime.UtcNow);
            await UserService.CreateAsync(newUser, changedBy);

            var addedUser = TestData.Users.FirstOrDefault(x => x.Login == newUser.Login);
            var logRecord = TestData.Logs.FirstOrDefault(x => x.ChangedBy == changedBy);

            Assert.NotNull(newUser);
            Assert.NotNull(logRecord);

            Assert.Equal(newUser.Name, addedUser.Name);
        }
        [Fact]
        public async void UpdateUserTest() {
            var repoManagerMock = MockIRepositoryManager.GetMock();
            var mapper = GetMapper();
            var UserService = new UsersService(repoManagerMock.Object, mapper);
            var oldUser = TestData.Users[0];
            var changedBy = "UpdateUserTestSys";
            var name = oldUser.Name + "TestUser";
            await UserService.UpdateAsync(new Contracts.Administration.UserDto(oldUser.Id, name, oldUser.Login + " updateLogin", oldUser.Password + "updatePass", oldUser.Email + " updateEmail", "", new List<Guid>() { TestData.Roles[0].Id }, changedBy, DateTime.UtcNow), changedBy);

            var updatedUser = TestData.Users.FirstOrDefault(x => x.Id == oldUser.Id);
            var logRecord = TestData.Logs.FirstOrDefault(x => x.ChangedBy == changedBy);

            Assert.NotNull(updatedUser);
            Assert.NotNull(logRecord);

            Assert.Equal(name, updatedUser.Name);
        }
        [Fact]
        public async void DeleteUserTest() {
            var repoManagerMock = MockIRepositoryManager.GetMock();
            var mapper = GetMapper();
            var userService = new UsersService(repoManagerMock.Object, mapper);
            var oldUser = TestData.Users[0];
            var oldUsersCount = TestData.Users.Count;
            var changedBy = "DeleteUserTestSys";

            await userService.DeleteAsync(new List<Guid>() { oldUser.Id }, changedBy);

            var deletedUser = TestData.Users.FirstOrDefault(x => x.Id == oldUser.Id);
            var logRecord = TestData.Logs.FirstOrDefault(x => x.ChangedBy == changedBy);

            Assert.Null(deletedUser);
            Assert.NotNull(logRecord);
            Assert.Equal(oldUsersCount - 1, TestData.Users.Count);
        }
    }
}
