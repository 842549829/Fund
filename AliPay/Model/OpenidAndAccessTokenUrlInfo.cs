namespace AliPay.Model
{
    /// <summary>
    /// 授权Url
    /// </summary>
    public class OpenidAndAccessTokenUrlInfo
    {
        /// <summary>
        /// 回调URL
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// 商户自定义参数
        /// </summary>
        public string State { get; set; }
    }
}