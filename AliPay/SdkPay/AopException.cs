using System;
using System.Runtime.Serialization;

namespace AliPay.SdkPay
{
    /// <summary>
    /// AOP客户端异常。
    /// </summary>
    public class AopException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AopException()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        public AopException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="context">内容</param>
        protected AopException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="innerException">异常</param>
        public AopException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="errorMsg">错误信息</param>
        public AopException(string errorCode, string errorMsg)
            : base(errorCode + ":" + errorMsg)
        {
            this.ErrorCode = errorCode;
            this.ErrorMsg = errorMsg;
        }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; }
    }
}