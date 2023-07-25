using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Models.Response;
using Microsoft.AspNetCore.Mvc;
using TMK.Utils.Utils;

namespace LazarWebApi.Controllers.Common
{
    public class TypesController : BaseApiController
    {
        [HttpGet]
        public JsonResult GetSubsystemType()
        {
            var exts = EnumHelper.GetListParam<SubSystemType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
            return Json(exts);
        }
        [HttpGet]
        public JsonResult GetEventTypes()
        {
            var exts = EnumHelper.GetListParam<EventType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
            return Json(exts);
        }
    }
}