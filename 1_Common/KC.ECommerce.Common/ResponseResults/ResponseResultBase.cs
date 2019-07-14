namespace KC.ECommerce.Common
{
    /// <summary>
    /// 请求响应实体类
    /// </summary>
    public class ResponseResultBase
    {
        private bool _isSuccess;
        private int _errorCode;
        private string _message;

        public ResponseResultBase()
        {
            this._isSuccess = true;
        }

        public bool IsSuccess { get { return this._isSuccess; } }

        public int ErrorCode { get { return this._errorCode; } }

        public string Message { get { return this._message; } }

        public object Data { get; set; }

        public void SetFailed(string message, ErrorCode errorCode)
        {
            this._isSuccess = false;
            this._errorCode = (int)errorCode;
            this._message = message;
        }
    }

    /// <summary>
    /// 错误Code
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 无权限
        /// </summary>
        NoPermission = 401,

        /// <summary>
        /// 未知资源
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// 内部服务器错误
        /// </summary>
        InternalServerError = 500,

        /// <summary>
        /// 常规业务错误
        /// </summary>
        Failed = 999
    }
}
