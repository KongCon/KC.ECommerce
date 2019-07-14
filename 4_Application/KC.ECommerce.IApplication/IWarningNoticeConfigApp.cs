using KC.ECommerce.Common;
using System.Collections.Generic;

namespace KC.ECommerce.IApplication
{
    public interface IWarningNoticeConfigApp : IBaseApp
    {
        ResponseResultBase GetList(WarningNoticeConfigPO po);

        ResponseResultBase CreateOrUpdate(WarningNoticeConfigVO vo);

        ResponseResultBase Delete(List<int> ids);
    }
}
