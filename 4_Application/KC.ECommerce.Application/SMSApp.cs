using KC.ECommerce.Common;
using KC.ECommerce.IApplication;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace KC.ECommerce.Application
{
    public class SMSApp : BaseApp, ISMSApp
    {
        private readonly SMSSetting _smsSetting;
        private readonly IHttpApp _httpApp;

        public SMSApp(IOptions<SMSSetting> smsSettingsOption, IHttpApp httpApp)
        {
            _smsSetting = smsSettingsOption.Value;
            _httpApp = httpApp;
        }

        public ResponseResultBase SendSMS(string content, string mobile)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(_smsSetting.UserId), "\"userid\"");
            formData.Add(new StringContent(_smsSetting.Account), "\"account\"");
            formData.Add(new StringContent(_smsSetting.Password), "\"password\"");
            formData.Add(new StringContent(mobile), "\"mobile\"");
            formData.Add(new StringContent(content), "\"content\"");
            formData.Add(new StringContent(_smsSetting.SendAction), "\"action\"");
            string requestUrl = _smsSetting.BaseAddress;
            var response = _httpApp.PostAsync(formData, requestUrl);
            if (response.IsSuccess)
            {
                var result = (string)response.Data;
                var smsResult = XMLHelper.DeserializeToObject<returnsms>(result);
                if (smsResult.returnstatus != "Success")
                {
                    var message = smsResult.message;
                    response.SetFailed(message, ErrorCode.Failed);
                }
            }
            return response;
        }
    }
}
