using Lazar.Infrastructure.Mapper;
using Lazar.Presentation.WebApi.Controllers.Base;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Models;
using Lazar.Srevices.Iterfaces.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers.Administration {
    /// <summary>
    /// Controller of roles
    /// </summary>
    [ApiController]
    [Route("api/roles")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RolesController : BaseController {
        public RolesController(IServiceManager serviceManager, IModelMapper mapper)
            : base(serviceManager, mapper) {
        }
        [HttpPost]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromBody] DataTableRequestDto search) {
            try {
                return Ok(await _serviceManager.RoleService.GetAsync(search));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("get-list")]
        public JsonResult GetRoles([FromBody] SelectRoleRequestDto args)
        {
            var data = roleRepository.GetRoles(args.SearchValue ?? "", args.SelectedUserId);
            return Json(data);
        }
        [HttpPost]
        [Route("create")]
        public JsonResult AddRole([FromBody] RoleDto model)
        {
            var data = roleRepository.AddRole(model);
            return Json(data);
        }
        [HttpPost]
        [Route("update")]
        public JsonResult UpdateRole([FromBody] RoleDto model) {
            var data = roleRepository.UpdateRole(model);
            return Json(data);
        }
        [HttpPost]
        [Route("delete")]
        public JsonResult DeleteRole([FromBody] RoleDto model) {
            var data = roleRepository.DeleteRole(model.Id);
            return Json(data);
        }
        [HttpGet]
        [Route("get/{id?}")]
        public JsonResult GetRoleModel(Guid? id = null)
        {
            var res = roleRepository.GetView(id);
            return Json(res);
        }
        [HttpPost]
        [Route("get-all")]
        public async Task<IActionResult> GetAllAsync([FromBody] AdvancedSearchOptionDto search) {
            try {
                return Ok(await _serviceManager.WindRegionService.GetRecordsAsync(search));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpGet]
        [Route("get-one")]
        public async Task<IActionResult> GetAsync(Guid id) {
            try {
                return Ok(await _serviceManager.WindRegionService.GetRecordAsync(id));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("get-column-records")]
        public async Task<IActionResult> GetRecordsBySelectorAsync([FromBody] SelectorSearchOptionDto search) {
            try {
                return Ok(await _serviceManager.WindRegionService.GetRecordsBySelectorAsync(search));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("get-key-names")]
        public async Task<IActionResult> GetKeyNameRecordsAsync([FromBody] TermSearchOptionDto search) {
            try {
                return Ok(await _serviceManager.WindRegionService.GetKeyNameRecordsAsync(search));
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("get-report")]
        public async Task<IActionResult> GetReportAsync([FromBody] ReportGenerationOptionDto options) {
            try {
                var file = await _serviceManager.WindRegionService.GetReportAsync(options, _hostingEnvironment.ContentRootPath);
                return File(file.Data, _mimeMappingService.Map(file.Extension), file.FullName);
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] WindRegionDto data) {
            try {
                await _serviceManager.WindRegionService.CreateAsync(data, UserIdentityName);
                return Ok(new SuccessResponseDto());
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] WindRegionDto data) {
            try {
                await _serviceManager.WindRegionService.UpdateAsync(data, UserIdentityName);
                return Ok(new SuccessResponseDto());
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] IEnumerable<Guid> ids) {
            try {
                await _serviceManager.WindRegionService.DeleteAsync(ids, UserIdentityName);
                return Ok(new SuccessResponseDto());
            } catch (Exception exp) {
                return Ok(new ErrorResponseDto(exp));
            }
        }
    }
}