using KC.ECommerce.Common;
using KC.ECommerce.IApplication;
using Microsoft.AspNetCore.Mvc;

namespace KC.ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserApp _userApp;
        //private readonly IWorkContext _workContext;

        public UserController(IUserApp userApp)
        {
            this._userApp = userApp;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _userApp.GetUserInfo();
            return Ok(response);
        }
    }
}