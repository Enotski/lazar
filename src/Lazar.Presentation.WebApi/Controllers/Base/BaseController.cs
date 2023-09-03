using Lazar.Infrastructure.Mapper;
using Lazar.Srevices.Iterfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace Lazar.Presentation.WebApi.Controllers.Base {
    public class BaseController : ControllerBase {
        protected readonly IServiceManager _serviceManager;
        protected readonly IModelMapper _mapper;

        public string UserIdentityName {
            get {
                if (HttpContext == null) {
                    return null;
                }
                return HttpContext.User.Identity.Name;
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
        public BaseController(IServiceManager serviceManager, IModelMapper mapper) {
            _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
