namespace KC.ECommerce.IApplication
{
    public class PagedPO : BasePO
    {
        public PagedPO()
        {
            this.Page = 1;
            this.Limit = 15;
        }
        public int Page { get; set; }

        public int Limit { get; set; }
    }
}
