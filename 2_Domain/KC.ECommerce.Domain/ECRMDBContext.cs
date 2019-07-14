using Microsoft.EntityFrameworkCore;

namespace KC.ECommerce.Domain
{
    public class ECRMDBContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ECRMDBContext(DbContextOptions<ECRMDBContext> options) : base(options)
        {
        }

        //DbSet<WarningNotice> WarningNotice { get; set; }

        //DbSet<WarningNoticeConfig> WarningNoticeConfig { get; set; }

        /// <summary>
        /// 数据库表映射配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WarningNotice>(entity =>
            {
                entity.ToTable("WarningNotice");
            });

            modelBuilder.Entity<WarningNoticeConfig>(entity =>
            {
                entity.ToTable("WarningNoticeConfig");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
