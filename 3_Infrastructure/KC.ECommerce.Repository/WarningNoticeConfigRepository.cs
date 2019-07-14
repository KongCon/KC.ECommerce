using KC.ECommerce.Domain;
using KC.ECommerce.IRepository;

namespace KC.ECommerce.Repository
{
    public class WarningNoticeConfigRepository : BaseRepository<WarningNoticeConfig, int>, IWarningNoticeConfigRepository
    {
        public WarningNoticeConfigRepository(ECRMDBContext dbContext)
            : base(dbContext)
        {

        }
    }
}
