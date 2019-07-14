using KC.ECommerce.Application.DTO;
using KC.ECommerce.Common;
using KC.ECommerce.Domain.Entities;
using KC.ECommerce.Domain.Keys;
using KC.ECommerce.IApplication;
using KC.ECommerce.IApplication.ParameterObject;
using KC.ECommerce.IRepository;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace KC.ECommerce.Application
{
    public class ProductApp : BaseApp, IProductApp
    {
        #region 字段
        private readonly IProductRepository _productRepository;
        #endregion

        #region 构造函数
        public ProductApp(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="po"></param>
        /// <returns></returns>
        public ResponseResultBase GetList(ProductListPO po)
        {
            var response = new ResponseResultBase();
            #region 参数拼接
            Expression<Func<Product, bool>> filter = null;
            if (!string.IsNullOrEmpty(po.Name))
            {
                filter = filter.And(x => x.Name.Contains(po.Name));
            }
            if (po.Status != ProductStatus.All)
            {
                filter = filter.And(x => x.Status == po.Status);
            }
            Expression<Func<Product, object>> orderBy = x => x.Sequence;
            #endregion
            var productList = _productRepository.GetPageList(out int totalCount, po.Page, po.Limit, filter, orderBy, false);
            var productStatusEnumList = EnumHelper.EnumToList<ProductStatus>();
            var rows = productList.Select(x => new ProductListDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Status = productStatusEnumList.Where(t => t.EnumValue == (int)x.Status).FirstOrDefault().Discription,
                Sequence = x.Sequence,
                CreatedDate = x.CreatedDate.ToString("yy/MM/dd hh:mm:ss"),
                UpdatedDate = x.UpdatedDate.ToString("yy/MM/dd hh:mm:ss")
            }).ToList();
            var data = new ElementUIPagedList<ProductListDTO>();
            data.Total = totalCount;
            data.Items = rows;
            response.Data = data;
            return response;
        }
        #endregion

    }
}
