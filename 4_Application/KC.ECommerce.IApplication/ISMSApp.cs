using KC.ECommerce.Common;

namespace KC.ECommerce.IApplication
{
    public interface ISMSApp:IBaseApp
    {
        ResponseResultBase SendSMS(string content, string mobile);
    }
}
