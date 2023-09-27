using Lazar.Infrastructure.Mapper;
using Lazar.Services.Administration;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Logging;
using Lazar.Services.Tests.Mocks;

namespace Lazar.Services.Tests {
    public class SystemLogServiceTests {
        public IModelMapper GetMapper() {
            return new AutoModelMapper();
        }
        [Fact]
        public async void WhenGetAsyncByDataTableRequestDto_ThenLogsByRequestReturn() {
            var repoManagerMock = MockIRepositoryManager.GetMock();
            var mapper = GetMapper();
            var logService = new SystemLogService(repoManagerMock.Object, mapper);

            var op1 = new Contracts.Request.DataTable.Base.DataTableRequestDto() {
                Sorts = new List<SortOptionDto> {
                    new SortOptionDto(){ ColumnName = "EVENTTYPENAME", Type = Domain.Core.Enums.SortType.Descending }
                }
            };
            var op2 = new Contracts.Request.DataTable.Base.DataTableRequestDto() {
                Filters = new List<SearchOptionDto> {
                    new SearchOptionDto(){ ColumnName = "Description", Value = "role" }
                }
            };

            var res1 = await logService.GetRecordsAsync(op1);
            var res2 = await logService.GetRecordsAsync(op2);

            var exp1 = TestData.Logs.OrderByDescending(x => x.EventType);
            var exp2 = TestData.Logs.Where(x => x.Description.ToLower().Contains("role"));

            Assert.Equal(exp1.Select(x => x.Id), res1.data.Select(x => x.Id.Value));
            Assert.Equal(exp2.Select(x => x.Id), res2.data.Select(x => x.Id.Value));
        }
    }
}
