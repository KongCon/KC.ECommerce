using Microsoft.EntityFrameworkCore;

namespace KC.ECommerce.Domain
{
    public class ECommerceDBContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ECommerceDBContext(DbContextOptions<ECommerceDBContext> options) : base(options)
        {
        }

        /// <summary>
        /// 数据库表映射配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
