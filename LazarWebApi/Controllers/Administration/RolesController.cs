using SysRM.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SysRM.Entities.Views.Response;
using TMK.Utils.AD;
using TMK.Utils.Models.Result;
using static SysRM.Entities.Views.ViewModels.OtherViewModels;

namespace SysRM.Controllers.References {
    public class RolesController : BaseController {
        protected override string _pathToView => "~/Views/Administration/Roles.cshtml";

        RoleRepository roleRepository = new RoleRepository();

        [HttpPost]
        public JsonResult GetRolesDataGrid(int skip, int take, DataGridSort[] sorts, DataGridFilter[] filters) {
            var data = roleRepository.GetRolesDataGrid(skip, take, sorts, filters);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CommonMethodsChangeRoles(Guid? IdRole, string NameRole, string GroupAD, Guid[] Rights) {
            var data = roleRepository.CommonMethodsChangeRoles(IdRole, NameRole, GroupAD, CurrentUser?.UserInfo, Rights);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteRoles(List<Guid> ListIdRoles) {
            var data = roleRepository.DeleteRoles(ListIdRoles, CurrentUser.UserInfo);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRoleModel(Guid? id) {
            var res = roleRepository.GetRoleWithRights(id);
            return Json(res);
        }
    }
}