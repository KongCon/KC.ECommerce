using KC.ECommerce.Common;

namespace KC.ECommerce.IApplication
{
    public interface IUserApp: IBaseApp
    {
        ResponseResultBase GetUserInfo();
    }
}
