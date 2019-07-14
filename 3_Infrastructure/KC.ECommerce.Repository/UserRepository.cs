using KC.ECommerce.Domain;
using KC.ECommerce.IRepository;

namespace KC.ECommerce.Repository
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(ECommerceDBContext dbContext)
            : base(dbContext)
        {

        }
    }
}
