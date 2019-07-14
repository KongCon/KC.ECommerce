using KC.ECommerce.Common;
using KC.ECommerce.IApplication.ParameterObject;

namespace KC.ECommerce.IApplication
{
    public interface IProductApp : IBaseApp
    {
        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="po"></param>
        /// <returns></returns>
        ResponseResultBase GetList(ProductListPO po);
    }
}
