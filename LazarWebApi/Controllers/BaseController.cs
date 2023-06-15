using lazarData.Models.Response.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;
using TMK.Utils.Utils;

namespace SysRM.Controllers
{
    /// <summary>
    /// Базовый контроллер
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// Текущий пользователь
        /// </summary>
        protected UserBaseModel CurrentUser
        {
            get
            {
                var resp = RepositoryHelper.GetUserFromCache(HttpContext.User.Identity.Name);
                if (resp.State != ResultState.Success)
                {
                    return null;
                }
                return resp.Result;
            }
        }
        /// <summary>
        /// Является ли пользователь администратором (ИА / технологическим / филиала)
        /// </summary>
        /// <returns></returns>
        public JsonResult IsUserAdmin()
        {
            var isAdmin = CurrentUser.Roles.Any(r => r == Guids.Role.AdministratorIA || r == Guids.Role.AdministratorFilial || r == Guids.Role.TechnologyAdministratorFilial);
            return Json(new BaseResponse(new Entities.Views.ViewModels.Base.BaseResponseModel(), new { UserIsAdmin = isAdmin }), JsonRequestBehavior.AllowGet);
        }
    }
}