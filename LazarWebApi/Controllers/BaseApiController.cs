using lazarData.Models.Administration;
using Microsoft.AspNetCore.Mvc;

namespace LazarWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseApiController : Controller
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