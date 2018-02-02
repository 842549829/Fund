namespace Weixin.Model
{
    /// <summary>
    /// 平台component_access_token
    /// </summary>
    public class Api_component_token_result
    {
        /// <summary>
        /// 平台component_access_token
        /// </summary>
        public string component_access_token { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public int expires_in { get; set; }
    }

    /// <summary>
    /// Api_component_token
    /// </summary>
    public class Api_component_token
    {
        /// <summary>
        /// 第三方平台appid
        /// </summary>
        public string component_appid { get; set; }

        /// <summary>
        /// 第三方平台appsecret
        /// </summary>
        public string component_appsecret { get; set; }

        /// <summary>
        /// component_verify_ticket
        /// </summary>
        public string component_verify_ticket { get; set; }
    }
}