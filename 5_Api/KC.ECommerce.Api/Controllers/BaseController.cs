using KC.ECommerce.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KC.ECommerce.Api.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}
