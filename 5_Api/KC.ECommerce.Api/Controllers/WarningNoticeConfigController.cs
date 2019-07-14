using KC.ECommerce.IApplication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace KC.ECommerce.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WarningNoticeConfigController : ControllerBase
    {
        private readonly IWarningNoticeConfigApp _warningNoticeConfigApp;

        public WarningNoticeConfigController(IWarningNoticeConfigApp warningNoticeConfigApp)
        {
            _warningNoticeConfigApp = warningNoticeConfigApp;
        }

        [HttpGet]
        public IActionResult GetList([FromQuery]WarningNoticeConfigPO po)
        {
            var response = _warningNoticeConfigApp.GetList(po);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateOrUpdate(WarningNoticeConfigVO vo)
        {
            var response = _warningNoticeConfigApp.CreateOrUpdate(vo);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Delete(List<int> ids)
        {
            var response = _warningNoticeConfigApp.Delete(ids);
            return Ok(response);
        }
    }
}
