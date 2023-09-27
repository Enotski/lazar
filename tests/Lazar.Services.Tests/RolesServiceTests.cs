using Lazar.Domain.Core.Keys;
using Lazar.Infrastructure.Mapper;
using Lazar.Services.Administration;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Tests.Mocks;

namespace Lazar.Services.Tests {
    public class RolesServiceTests {
        public IModelMapper GetMapper() {
            return new AutoModelMapper();
        }
        [Fact]
        public async void WhenGetAsyncById_ThenOneRoleReturn() {
            var repoManagerMock = MockIRepositoryManager.GetMock();
            var mapper = GetMapper();
            var roleService = new RolesService(repoManagerMock.Object, mapper);
            var keys = new List<Guid> { RoleKeys.User, RoleKeys.Moderator };

            var result = await roleService.GetAsync(keys[0]);

            Assert.NotNull(result);
            Assert.Equal(RoleKeys.User, result.Data.Id);
        }
        [Fact]
        public async void WhenGetAsyncByDataTableRequestDto_ThenRolesByRequestReturn() {
            var repoManagerMock = MockIRepositoryManager.GetMock();
            var mapper = GetMapper();
            var roleService = new RolesService(repoManagerMock.Object, mapper);

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
            var check1 = TestData.Roles.OrderByDescending(x => x.Name);
            var check2 = TestData.Roles.Where(x => x.Name.ToLower().Contains("admin"));

            var result1 = await roleService.GetAsync(op1);
            var result2 = await roleService.GetAsync(op2);

            Assert.Equal(check1.Count(), result1.data.Count());
            Assert.Equal(check1.Select(x => x.Id), result1.data.Select(x => x.Id.Value));

            Assert.Equal(check2.Count(), result2.data.Count());
            Assert.Equal(check2.Select(x => x.Id), result2.data.Select(x => x.Id.Value));
        }
        [Fact]
        public async void CrateRoleTest() {
            var repoManagerMock = MockIRepositoryManager.GetMock();
            var mapper = GetMapper();
            var roleService = new RolesService(repoManagerMock.Object, mapper);
            var key = Guid.NewGuid();
            var changedBy = "CreateRoleTestSys";
            var name = "TestRole";

            await roleService.CreateAsync(new Contracts.Administration.RoleDto(key, name, changedBy, DateTime.UtcNow), changedBy);

            var newRole = TestData.Roles.FirstOrDefault(x => x.Name == name);
            var logRecord = TestData.Logs.FirstOrDefault(x => x.ChangedBy == changedBy);

            Assert.NotNull(newRole);
            Assert.NotNull(logRecord);

            Assert.Equal(name, newRole.Name);
        }
        [Fact]
        public async void UpdateRoleTest() {
            var repoManagerMock = MockIRepositoryManager.GetMock();
            var mapper = GetMapper();
            var roleService = new RolesService(repoManagerMock.Object, mapper);
            var oldRole = TestData.Roles[0];
            var changedBy = "UpdateRoleTestSys";
            var name = oldRole.Name + "TestRole";

            await roleService.UpdateAsync(new Contracts.Administration.RoleDto(oldRole.Id, name, changedBy, DateTime.UtcNow), changedBy);

            var updatedRole = TestData.Roles.FirstOrDefault(x => x.Id == oldRole.Id);
            var logRecord = TestData.Logs.FirstOrDefault(x => x.ChangedBy == changedBy);

            Assert.NotNull(updatedRole);
            Assert.NotNull(logRecord);

            Assert.Equal(name, updatedRole.Name);
        }
        [Fact]
        public async void DeleteRoleTest() {
            var repoManagerMock = MockIRepositoryManager.GetMock();
            var mapper = GetMapper();
            var roleService = new RolesService(repoManagerMock.Object, mapper);
            var oldRole = TestData.Roles[0];
            var oldRolesCount = TestData.Roles.Count;
            var changedBy = "DeleteRoleTestSys";

            await roleService.DeleteAsync(new List<Guid>() { oldRole.Id }, changedBy);

            var deletedRole = TestData.Roles.FirstOrDefault(x => x.Id == oldRole.Id);
            var logRecord = TestData.Logs.FirstOrDefault(x => x.ChangedBy == changedBy);

            Assert.Null(deletedRole);
            Assert.NotNull(logRecord);
            Assert.Equal(oldRolesCount - 1, TestData.Roles.Count);
        }
    }
}
