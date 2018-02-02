namespace AliPay.SdkPay
{
    /// <summary>
    /// Config
    /// </summary>	
    public class Config
    {
        /// <summary>
        /// 获取应用ID
        /// </summary>
        /// <returns>应用ID</returns>
        public string GetAppId()
        {
            return "";
        }

        /// <summary>
        /// 获取PID
        /// </summary>
        /// <returns>应用PID</returns>
        public string GetPid()
        {
            return "";
        }

        /// <summary>
        /// 获取商户私钥
        /// </summary>
        /// <returns>商户私钥</returns>
        public string GetPrivateKeyPem()
        {
            return "";
        }

        /// <summary>
        /// 获取商户公钥
        /// </summary>
        /// <returns>商户公钥</returns>
        public string GetPublicKeyPem()
        {
            return "";
        }

        /// <summary>
        /// 获取支付宝公钥
        /// </summary>
        /// <returns>支付宝公钥</returns>
        public string GetPublicKeyPemAliPay()
        {
            return "";
        }

        /// <summary>
        /// 网关地址
        /// </summary>
        public const string ServerUrl = "https://openapi.alipay.com/gateway.do";

        /// <summary>
        /// 销售产品码，商家和支付宝签约的产品码
        /// </summary>
        public const string ProductCode = "QUICK_MSECURITY_PAY";

        /// <summary>
        /// 数据格式
        /// </summary>
        public const string Format = "json";

        /// <summary>
        /// 版本号
        /// </summary>
        public const string Version = "1.0";

        /// <summary>
        /// 签名加密方式
        /// </summary>
        public const string SignType = "RSA2";

        /// <summary>
        /// 编码方式
        /// </summary>
        public const string Charset = "utf-8";
    }
}