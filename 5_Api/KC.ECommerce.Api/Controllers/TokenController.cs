using KC.ECommerce.IApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KC.ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : BaseController
    {
        private readonly ITokenApp _tokenApp;

        public TokenController(ITokenApp tokenApp)
        {
            this._tokenApp = tokenApp;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Post([FromQuery]TokenGetPO qc)
        {
            var response = _tokenApp.GetToken(qc);
            return Ok(response);
        }
    }
}