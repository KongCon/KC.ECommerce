using KC.ECommerce.Domain;
using KC.ECommerce.Domain.Entities;
using KC.ECommerce.IRepository;

namespace KC.ECommerce.Repository
{
    public class ProductRepository : BaseRepository<Product, int>, IProductRepository
    {
        public ProductRepository(ECommerceDBContext dbContext)
            : base(dbContext)
        {

        }
    }
}
