using Lazar.Infrastructure.Mapper;
using Lazar.Presentation.WebApi.Controllers.Base;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Base;
using Lazar.Srevices.Iterfaces.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lazar.Presentation.WebApi.Controllers.Logging {
    [ApiController]
    [Route("api/system-log")/*, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)*/]
    public class SystemLogController : BaseController {
        public SystemLogController(IServiceManager serviceManager, IModelMapper mapper)
            : base(serviceManager, mapper) {
        }
        [HttpPost]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromBody] DataTableRequestDto args) {
            try {
                return Ok(await _serviceManager.LoggingService.GetRecordsAsync(args));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] IEnumerable<Guid> ids) {
            try {
                await _serviceManager.LoggingService.DeleteRecordsAsync(ids, UserIdentityName);
                return Ok(new SuccessResponseDto());
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("clear")]
        public async Task<IActionResult> Clear() {
            try {
                await _serviceManager.LoggingService.ClearLogAsync(UserIdentityName);
                return Ok(new SuccessResponseDto());
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("remove-by-period")]
        public async Task<IActionResult> RemoveByPeriod([FromBody] PeriodDto period) {
            try {
                await _serviceManager.LoggingService.RemoveByPeriodAsync(period, UserIdentityName);
                return Ok(new SuccessResponseDto());
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
    }
}
