using KC.ECommerce.Domain;
using KC.ECommerce.IRepository;
using Microsoft.EntityFrameworkCore;

namespace KC.ECommerce.Repository
{
    /// <summary>
    /// Repository基类
    /// </summary>
    public class BaseRepository<T, Tkey> : DefaultRepositoryImpl<T, Tkey>, IBaseRepository<T,Tkey>
        where T : class
    {
        public BaseRepository(DbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
