namespace Weixin.Model
{
    /// <summary>
    /// Pre_auth_code
    /// </summary>
    public class Pre_auth_code
    {
        /// <summary>
        /// 第三方appid
        /// </summary>
        public string component_appid { get; set; }

        /// <summary>
        /// 第三方access_token
        /// </summary>
        public string component_access_token { get; set; }
    }
}