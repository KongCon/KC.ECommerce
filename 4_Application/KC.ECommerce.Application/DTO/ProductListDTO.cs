namespace KC.ECommerce.Application.DTO
{
    public class ProductListDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 产品简述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 产品状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdatedDate { get; set; }
    }
}
