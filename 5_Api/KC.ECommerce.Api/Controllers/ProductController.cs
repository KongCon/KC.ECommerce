using KC.ECommerce.IApplication;
using KC.ECommerce.IApplication.ParameterObject;
using Microsoft.AspNetCore.Mvc;

namespace KC.ECommerce.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region 字段
        private readonly IProductApp _productApp;
        #endregion

        #region 构造函数
        public ProductController(IProductApp productApp)
        {
            _productApp = productApp;
        }
        #endregion


        [HttpGet]
        public IActionResult GetList([FromQuery]ProductListPO po)
        {
            var response = _productApp.GetList(po);
            return Ok(response);
        }
    }
}
