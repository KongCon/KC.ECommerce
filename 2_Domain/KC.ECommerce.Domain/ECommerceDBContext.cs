using KC.ECommerce.Domain.Entities;
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

        //public DbSet<User> User { get; set; }

        //public DbSet<Product> Product { get; set; }

        //public DbSet<Role> Role { get; set; }

        //public DbSet<Menu> Menu { get; set; }

        /// <summary>
        /// 数据库表映射配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasMany(x => x.UserRoleList)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasMany(x => x.UserRoleList)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();

                entity.HasMany(x => x.RoleMenuList)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();

            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.HasMany(x => x.RoleMenuList)
                .WithOne(x => x.Menu)
                .HasForeignKey(x => x.MenuId)
                .IsRequired();
            });


            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
