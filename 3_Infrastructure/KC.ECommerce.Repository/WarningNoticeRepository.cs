using KC.ECommerce.Domain;
using KC.ECommerce.IRepository;

namespace KC.ECommerce.Repository
{
    public class WarningNoticeRepository : BaseRepository<WarningNotice, int>, IWarningNoticeRepository
    {
        public WarningNoticeRepository(ECRMDBContext dbContext)
            : base(dbContext)
        {

        }
    }
}
