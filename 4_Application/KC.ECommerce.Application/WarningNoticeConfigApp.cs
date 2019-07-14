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
    public class WarningNoticeConfigApp : BaseApp, IWarningNoticeConfigApp
    {
        private readonly IWarningNoticeConfigRepository _warningNoticeConfigRepository;

        public WarningNoticeConfigApp(IWarningNoticeConfigRepository warningNoticeConfigRepository)
        {
            _warningNoticeConfigRepository = warningNoticeConfigRepository;
        }

        public ResponseResultBase GetList(WarningNoticeConfigPO po)
        {
            var response = new ResponseResultBase();
            #region 参数拼接
            Expression<Func<WarningNoticeConfig, bool>> filter = null;
            if (!string.IsNullOrEmpty(po.Type))
            {
                filter = filter.And(x => x.Type.Contains(po.Type));
            }
            if (!string.IsNullOrEmpty(po.TypeDescription))
            {
                filter = filter.And(x => x.TypeDescription.Contains(po.TypeDescription));
            }
            if (!string.IsNullOrEmpty(po.SmsAccounts))
            {
                filter = filter.And(x => x.SmsAccounts.Contains(po.SmsAccounts));
            }
            if (!string.IsNullOrEmpty(po.EmailAccounts))
            {
                filter = filter.And(x => x.EmailAccounts.Contains(po.EmailAccounts));
            }
            Expression<Func<WarningNoticeConfig, object>> orderBy = x => x.CreateTime;
            #endregion
            var warningNoticeConfigList = _warningNoticeConfigRepository.GetPageList(out int totalCount, po.Page, po.Limit, filter, orderBy, false);
            var handleStatusList = EnumHelper.EnumToList<HandleStatus>();
            var rows = warningNoticeConfigList.Select(x => new WarningNoticeConfigDTO
            {
                Id = x.Id,
                Type = x.Type,
                TypeDescription=x.TypeDescription,
                DefaultSmsContent=x.DefaultSmsContent,
                DefaultEmailContent=x.DefaultEmailContent,
                SmsAccounts=x.SmsAccounts,
                EmailAccounts=x.EmailAccounts,
                CreateTime= x.CreateTime.HasValue ? Convert.ToDateTime(x.CreateTime).ToString("yy/MM/dd hh:mm:ss") : "",
                SqlScript=x.SqlScript
            }).ToList();
            var data = new ElementUIPagedList<WarningNoticeConfigDTO>();
            data.Total = totalCount;
            data.Items = rows;
            response.Data = data;
            return response;
        }

        public ResponseResultBase CreateOrUpdate(WarningNoticeConfigVO vo)
        {
            var response = new ResponseResultBase();
            var entity = _warningNoticeConfigRepository.GetById(vo.Id) ?? new WarningNoticeConfig();
            entity.Id = vo.Id;
            entity.Type = vo.Type;
            entity.TypeDescription = vo.TypeDescription;
            entity.DefaultSmsContent = vo.DefaultSmsContent;
            entity.DefaultEmailContent = vo.DefaultEmailContent;
            entity.SmsAccounts = vo.SmsAccounts;
            entity.EmailAccounts = vo.EmailAccounts;
            entity.SqlScript = vo.SqlScript;
            entity.CreateTime = entity.CreateTime ?? DateTime.Now;
            if (entity.Id > 0)
            {
                _warningNoticeConfigRepository.Update(entity);
            }
            else
            {
                _warningNoticeConfigRepository.Save(entity);
            }
            return response;
        }

        public ResponseResultBase Delete(List<int> ids)
        {
            var response = new ResponseResultBase();
            var entityList = _warningNoticeConfigRepository.GetList(x => ids.Contains(x.Id));
            if (entityList != null&& entityList.Count>0)
            {
                _warningNoticeConfigRepository.DeleteList(entityList);
            }
            else
            {
                response.SetFailed("数据不存在，请刷新重试",ErrorCode.Failed);
            }
            return response;
        }
    }
}
