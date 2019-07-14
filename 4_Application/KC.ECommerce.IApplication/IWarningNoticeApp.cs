using KC.ECommerce.Common;

namespace KC.ECommerce.IApplication
{
    public interface IWarningNoticeApp : IBaseApp
    {
        ResponseResultBase GetList(WarningNoticePO qc);

        /// <summary>
        /// 处理状态更改
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        ResponseResultBase ChangeHandleStatus(ChangeHandleStatusPO paras);
    }
}
