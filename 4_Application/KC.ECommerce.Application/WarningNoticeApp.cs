using KC.ECommerce.Common;
using KC.ECommerce.Domain;
using KC.ECommerce.Domain.Keys;
using KC.ECommerce.IApplication;
using KC.ECommerce.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace KC.ECommerce.Application
{
    public class WarningNoticeApp : BaseApp, IWarningNoticeApp
    {
        private readonly IWarningNoticeRepository _warningNoticerRepository;
        private readonly IWarningNoticeConfigRepository _warningNoticeConfigRepository;
        private readonly IMailApp _mailApp;
        private readonly ISMSApp _smsApp;

        public WarningNoticeApp(IWarningNoticeRepository warningNoticerRepository, 
            IWarningNoticeConfigRepository warningNoticeConfigRepository,
            IMailApp mailApp,
            ISMSApp smsApp)
        {
            _warningNoticerRepository = warningNoticerRepository;
            _warningNoticeConfigRepository = warningNoticeConfigRepository;
            _mailApp = mailApp;
            _smsApp = smsApp;
        }

        public ResponseResultBase GetList(WarningNoticePO qc)
        {
            var response = new ResponseResultBase();
            #region 参数拼接
            Expression<Func<WarningNotice, bool>> filter = null;
            if (qc.HandleStatus != HandleStatus.All)
            {
                filter = filter.And(x => x.HandleStatus == qc.HandleStatus);
            }
            if (qc.Id > 0)
            {
                filter = filter.And(x => x.Id == qc.Id);
            }
            if (!string.IsNullOrEmpty(qc.SmsContent))
            {
                filter = filter.And(x => x.SmsContent.Contains(qc.SmsContent));
            }
            if (!string.IsNullOrEmpty(qc.EmailContent))
            {
                filter = filter.And(x => x.EmailContent.Contains(qc.EmailContent));
            }
            Expression<Func<WarningNotice, object>> orderBy = x => x.CreateTime;
            #endregion
            var warningNoticeList = _warningNoticerRepository.GetPageList(out int totalCount, qc.Page, qc.Limit, filter, orderBy, false);
            var handleStatusList = EnumHelper.EnumToList<HandleStatus>();
            var rows = warningNoticeList.Select(x => new WarningNoticeDTO
            {
                Id = x.Id,
                ConfigType = x.ConfigType,
                SmsContent = x.SmsContent,
                EmailContent = x.EmailContent,
                SendStatus = x.SendStatus ? "已发送" : "未发送",
                SendCount = x.SendCount,
                SendTime = x.SendTime.HasValue ? Convert.ToDateTime(x.SendTime).ToString("yy/MM/dd hh:mm:ss") : "",
                CreateTime = x.CreateTime.ToString("yy/MM/dd hh:mm:ss"),
                IsSendEmail = x.IsSendEmail ? "是" : "否",
                IsSendSms = x.IsSendSms ? "是" : "否",
                HandleStatus = handleStatusList.Where(t => t.EnumValue == (int)x.HandleStatus).FirstOrDefault().Discription
            }).ToList();
            var data = new ElementUIPagedList<WarningNoticeDTO>();
            data.Total = totalCount;
            data.Items = rows;
            response.Data = data;
            return response;
        }

        /// <summary>
        /// 处理状态更改
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public ResponseResultBase ChangeHandleStatus(ChangeHandleStatusPO po)
        {
            var response = new ResponseResultBase();
            var warningNoticeList = _warningNoticerRepository.GetList(x => x.HandleStatus == HandleStatus.UnHandled && po.Ids.Contains(x.Id)).ToList();
            warningNoticeList.ForEach(x => x.HandleStatus = HandleStatus.Handled);
            _warningNoticerRepository.UpdateList(warningNoticeList);
            var configTypeList = warningNoticeList.Select(x=>x.ConfigType).Distinct().ToList();
            var configList = _warningNoticeConfigRepository.GetList(x => configTypeList.Contains(x.Type));
            var mailList = new List<string>();
            var mobileList= new List<string>();
            foreach (var warningNotice in warningNoticeList)
            {
                var config = configList.Where(x => x.Type == warningNotice.ConfigType).FirstOrDefault();
                if (config != null)
                {
                    if (warningNotice.IsSendEmail)
                    {
                        var mailAccounts = config.EmailAccounts.Split(",").ToList();
                        mailAccounts.ForEach(x => mailList.Add(x));
                    }
                    if (warningNotice.IsSendSms)
                    {
                        var mobiles = config.SmsAccounts.Split(",").ToList();
                        mobiles.ForEach(x => mobileList.Add(x));
                    }
                }
            }
            #region 邮件发送
            mailList = mailList.Distinct().ToList();
            if (mailList != null && mailList.Count > 0 && !string.IsNullOrEmpty(po.EmailContent))
            {
                mailList.ForEach(x => _mailApp.SendEmailAsyn(x, "问题邮件处理", po.EmailContent));
            }
            #endregion

            #region 短信发送
            mobileList = mobileList.Distinct().ToList();
            string mobileString= string.Join(",", mobileList.ToArray());
            if (!string.IsNullOrEmpty(mobileString) && !string.IsNullOrEmpty(po.SMSContent))
            {
                _smsApp.SendSMS(po.SMSContent, mobileString);
            }
            #endregion

            return response;
        }
    }
}
