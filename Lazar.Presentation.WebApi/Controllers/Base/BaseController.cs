using Lazar.Srevices.Iterfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace Lazar.Presentation.WebApi.Controllers.Base {
    public class BaseController : ControllerBase {
        protected readonly IServiceManager _serviceManager;
        public string UserIdentityName {
            get {
                if (HttpContext == null) {
                    return null;
                }
                return HttpContext.User.Identity.Name?.ToUpper();
            }
        }
        public string RemoteIpAddress {
            get {
                if (HttpContext == null) {
                    return null;
                }
                return HttpContext.Connection.RemoteIpAddress?.ToString();
            }
        }
        public BaseController(IServiceManager serviceManager) {
            _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
        }
    }
}
