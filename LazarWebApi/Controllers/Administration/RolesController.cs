// using SysRM.Data.Repositories;
// using System;
// using System.Collections.Generic;
// using System.Web.Mvc;
// using SysRM.Entities.Views.Response;
// using TMK.Utils.AD;
// using TMK.Utils.Models.Result;
// using static SysRM.Entities.Views.ViewModels.OtherViewModels;

// namespace SysRM.Controllers.References
// {
// 	public class RolesController : BaseController {
// 		protected override string _pathToView => "~/Views/Administration/Roles.cshtml";

// 		RoleRepository roleRepository = new RoleRepository();
// 		ADSearcher aDSearcher = new ADSearcher();

// 		[HttpPost]
// 		public JsonResult GetRolesDataGrid(int skip, int take, DataGridSort[] sorts, DataGridFilter[] filters) {
// 			var data = roleRepository.GetRolesDataGrid(skip, take, sorts, filters);
// 			return Json(data, JsonRequestBehavior.AllowGet);
// 		}

// 		[HttpPost]
// 		public JsonResult GetAdGroupDataGrid(int skip, int take, DataGridSort[] sorts, DataGridFilter[] filters, Guid? IdRole) {
// 			var data = roleRepository.GetAdGroupDataGrid(skip, take, sorts, filters, IdRole);
// 			return Json(data, JsonRequestBehavior.AllowGet);
// 		}

// 		[HttpPost]
// 		public JsonResult GetUsersGroupDataGrid(int skip, int take, DataGridSort[] sorts, DataGridFilter[] filters, List<string> Groups) {
// 			var data = roleRepository.GetUsersGroupDataGrid(skip, take, sorts, filters, Groups);
// 			return Json(data, JsonRequestBehavior.AllowGet);
// 		}

// 		[HttpPost]
// 		public JsonResult GetAllGroupsAdDataGrid(int skip, int take, DataGridSort[] sorts, DataGridFilter[] filters) {
// 			var data = roleRepository.GetAllGroupsAdDataGrid(skip, take, sorts, filters);
// 			return Json(data, JsonRequestBehavior.AllowGet);
// 		}

// 		[HttpPost]
// 		public JsonResult CommonMethodsChangeRoles(Guid? IdRole, string NameRole, string GroupAD, Guid[] Rights) {
// 			var data = roleRepository.CommonMethodsChangeRoles(IdRole, NameRole, GroupAD, CurrentUser?.UserInfo, Rights);
// 			return Json(data, JsonRequestBehavior.AllowGet);
// 		}

// 		[HttpPost]
// 		public JsonResult SaveADGroup(Guid IdRole, string GroupAD) {
			
// 			var data = roleRepository.SaveADGroup(IdRole,  GroupAD, CurrentUser?.UserInfo);
// 			return Json(data, JsonRequestBehavior.AllowGet);
// 		}

// 		[HttpPost]
// 		public JsonResult DeleteRoles(List<Guid> ListIdRoles) {
// 			var data = roleRepository.DeleteRoles(ListIdRoles, CurrentUser.UserInfo);
// 			return Json(data, JsonRequestBehavior.AllowGet);
// 		}

// 		[HttpPost]
// 		public JsonResult DeleteGroupRoles(Guid IdRole,List<string> Groups) {
// 			var data = roleRepository.DeleteRoles(IdRole,Groups, CurrentUser.UserInfo);
// 			return Json(data, JsonRequestBehavior.AllowGet);
// 		}

// 		public JsonResult GetRoleModel(Guid? id) {
// 			var res = roleRepository.GetRoleWithRights(id);
// 			return Json(res);
// 		}

// 		[HttpPost]
// 		public JsonResult FindByGroup(List<string> Groups,Exception exception = null) {
// 			var data = aDSearcher.FindByGroup(out exception, Groups);
// 			return Json(data, JsonRequestBehavior.AllowGet);
// 		}

// 		[HttpPost]
// 		public JsonResult GetTreesAccessesRoles(Guid? roleId) {
// 			var data = roleRepository.GetTreeAccessRigths(roleId);
// 			return Json(data, JsonRequestBehavior.AllowGet);
// 		}
// 	}
// }