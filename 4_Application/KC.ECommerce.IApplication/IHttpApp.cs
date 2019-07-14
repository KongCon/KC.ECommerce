using KC.ECommerce.Common;
using System.Net.Http;

namespace KC.ECommerce.IApplication
{
    public interface IHttpApp:IBaseApp
    {
        ResponseResultBase PostAsync(HttpContent httpContent, string url);
    }
}
