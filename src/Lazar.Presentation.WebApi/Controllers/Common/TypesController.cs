using CommonUtils.Utils;
using Lazar.Domain.Core.Enums;
using Lazar.Services.Contracts.Response.Base;
using Lazar.Services.Contracts.Response.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Common {
    [ApiController]
    [Route("api/types")/*, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)*/]
    public class TypesController : ControllerBase{
        [HttpGet]
        [Route("get-subsystem-types")]
        public async Task<IActionResult> GetSubsystemType() {
            try {
                var res = await Task.Run(() => EnumHelper.GetListParam<SubSystemType>().Select(x => new ListItemDto<int>(x.Key, x.Value)));
                return Ok(new EntitiesResponseDto<ListItemDto<int>>(res));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpGet]
        [Route("get-event-types")]
        public async Task<IActionResult> GetEventTypes() {
            try {
                var res = await Task.Run(() => EnumHelper.GetListParam<EventType>().Select(x => new ListItemDto<int>(x.Key, x.Value)));
                return Ok(new EntitiesResponseDto<ListItemDto<int>>(res));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
    }
}