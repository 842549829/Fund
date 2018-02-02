namespace AliPay.Model
{
    /// <summary>
    /// 收款PID
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 获取PID
        /// </summary>
        /// <returns>应用PID</returns>
        public string GetPid()
        {
            return "";
        }

        /// <summary>
        /// 获取Key
        /// </summary>
        /// <returns>Key</returns>
        public string GetKey()
        {
            return "";
        }

        /// <summary>
        /// 支付宝网关地址
        /// </summary>
        public const string Gateway = "https://mapi.alipay.com/gateway.do?";

        /// <summary>
        /// 支付宝消息验证地址
        /// </summary>
        public const string HttpsVeryfyUrl = "https://mapi.alipay.com/gateway.do?service=notify_verify&";

        /// <summary>
        /// 编码方式
        /// </summary>
        public const string InputCharset = "utf-8";

        /// <summary>
        /// 加密方式
        /// </summary>
        public const string SignType = "MD5";

        /// <summary>
        /// 收款账号
        /// </summary>
        public const string SellerEmail = "SCLXGL01@163.COM";
    }
}