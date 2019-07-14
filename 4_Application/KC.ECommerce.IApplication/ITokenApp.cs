using KC.ECommerce.Common;

namespace KC.ECommerce.IApplication
{
    public interface ITokenApp : IBaseApp
    {
        ResponseResultBase GetToken(TokenGetPO qc);
    }
}
