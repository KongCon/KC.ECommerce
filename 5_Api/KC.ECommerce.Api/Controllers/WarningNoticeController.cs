using KC.ECommerce.IApplication;
using Microsoft.AspNetCore.Mvc;

namespace KC.ECommerce.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WarningNoticeController : ControllerBase
    {
        private readonly IWarningNoticeApp _warningNoticeApp;

        public WarningNoticeController(IWarningNoticeApp warningNoticeApp)
        {
            _warningNoticeApp = warningNoticeApp;
        }

        [HttpGet]
        public IActionResult GetList([FromQuery]WarningNoticePO qc)
        {
            var response = _warningNoticeApp.GetList(qc);
            return Ok(response);
        }

        /// <summary>
        /// 修改处理状态
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ChangeHandleStatus(ChangeHandleStatusPO paras)
        {
            var response = _warningNoticeApp.ChangeHandleStatus(paras);
            return Ok(response);
        }
    }
}
