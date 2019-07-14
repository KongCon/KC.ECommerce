using KC.ECommerce.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace KC.ECommerce.Api.Extensions
{
    public class GlobalException : IExceptionFilter
    {
        private readonly ILogger<GlobalException> _logger;

        private readonly IHostingEnvironment _env;
        public GlobalException(IHostingEnvironment env, ILogger<GlobalException> logger)
        {
            _env = env;
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            var result = new ResponseResultBase();
            //这里面是自定义的操作记录日志
            string message = context.Exception.Message;
            if (_env.IsDevelopment())
            {
                message = context.Exception.Message + context.Exception.StackTrace;//堆栈信息
            }
            result.SetFailed(message, ErrorCode.InternalServerError);
            context.Result = new BadRequestObjectResult(result);//返回异常数据
            //采用log4net 进行错误日志记录
            _logger.LogError(context.Exception, result.Message);
        }
    }
}
