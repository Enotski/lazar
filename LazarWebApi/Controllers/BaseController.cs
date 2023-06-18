using lazarData.Models.Administration;
using lazarData.Models.Response.ViewModels;
using lazarData.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;
using TMK.Utils.Utils;

namespace LazarWebApi.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Текущий пользователь
        /// </summary>
        protected User CurrentUser
        {
            get;
            //{
            //    //var resp = RepositoryHelper.GetUserFromCache(HttpContext.User.Identity.Name);
            //    if (resp.State != ResultState.Success)
            //    {
            //        return null;
            //    }
            //    return resp.Result;
            //}
            set;
        }
    }
}