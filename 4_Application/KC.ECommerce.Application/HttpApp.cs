using KC.ECommerce.Common;
using KC.ECommerce.IApplication;
using System;
using System.Net.Http;

namespace KC.ECommerce.Application
{
    public class HttpApp:BaseApp, IHttpApp
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpApp(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public ResponseResultBase PostAsync(HttpContent httpContent, string url)
        {
            var result = new ResponseResultBase();
            HttpClient client = _httpClientFactory.CreateClient();
            try
            {
                var response = client.PostAsync(url, httpContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result.Data = data;
                }
                else
                {
                    string message = "地址:" + url + "请求失败,原因：" + response.Content.ToString();
                    result.SetFailed(message, ErrorCode.InternalServerError);
                }
            }
            catch (Exception e)
            {
                string message = "接口异常：地址:" + url + "请求失败,原因：" + e.Message;
                result.SetFailed(message,ErrorCode.InternalServerError);
            }
            return result;
        }

    }
}
