namespace Weixin.Model
{
    /// <summary>
    /// 配置数据
    /// </summary>
    public class ConfigData
    {
        /// <summary>
        /// 获取第三方平台component_access_token
        /// 第三方平台通过自己的component_appid（即在微信开放平台管理中心的第三方平台详情页中的AppID和AppSecret）和component_appsecret，以及component_verify_ticket（每10分钟推送一次的安全ticket）来获取自己的接口调用凭据（component_access_token）
        /// </summary>
        public const string component_access_token_url = "https://api.weixin.qq.com/cgi-bin/component/api_component_token";

        /// <summary>
        /// 获取预授权码 pre_auth_code
        /// 第三方平台通过自己的接口调用凭据（component_access_token）来获取用于授权流程准备的预授权码（pre_auth_code）
        /// </summary>
        public const string pre_auth_code_url = "https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode";

        /// <summary>
        /// 引入用户进入授权页
        /// 第三方平台方可以在自己的网站:中放置“微信公众号授权”或者“小程序授权”的入口，引导公众号和小程序管理员进入授权页。授权页网址
        /// </summary>
        public const string authorization_code_url = "https://mp.weixin.qq.com/cgi-bin/componentloginpage";

        /// <summary>
        /// 使用授权码换取公众号或小程序的接口调用凭据和授权信息
        /// 该API用于使用授权码换取授权公众号或小程序的授权信息，并换取authorizer_access_token和authorizer_refresh_token。 授权码的获取，需要在用户在第三方平台授权页中完成授权流程后，在回调URI中通过URL参数提供给第三方平台方。请注意，由于现在公众号或小程序可以自定义选择部分权限授权给第三方平台，因此第三方平台开发者需要通过该接口来获取公众号或小程序具体授权了哪些权限，而不是简单地认为自己声明的权限就是公众号或小程序授权的权限。
        /// </summary>
        public const string authorization_code_redirect_url = "https://api.weixin.qq.com/cgi-bin/component/api_query_auth";

        /// <summary>
        /// 获取（刷新）授权公众号或小程序的接口调用凭据（令牌）
        /// 该API用于在授权方令牌（authorizer_access_token）失效时，可用刷新令牌（authorizer_refresh_token）获取新的令牌。请注意，此处token是2小时刷新一次，开发者需要自行进行token的缓存，避免token的获取次数达到每日的限定额度。缓存方法可以参考：http://mp.weixin.qq.com/wiki/2/88b2bf1265a707c031e51f26ca5e6512.html
        /// </summary>
        public const string authorization_access_token_refresh_url = "https://api.weixin.qq.com/cgi-bin/component/api_authorizer_token?";

        /// <summary>
        /// 获取授权方的帐号基本信息
        /// 该API用于获取授权方的基本信息，包括头像、昵称、帐号类型、认证类型、微信号、原始ID和二维码图片URL。需要特别记录授权方的帐号类型，在消息及事件推送时，对于不具备客服接口的公众号，需要在5秒内立即响应；而若有客服接口，则可以选择暂时不响应，而选择后续通过客服接口来发送消息触达粉丝。
        /// </summary>
        public const string get_authorizer_info_url = "https://api.weixin.qq.com/cgi-bin/component/api_get_authorizer_info";

        /// <summary>
        /// api_set_industry_url
        /// </summary>
        public const string api_set_industry_url = "https://api.weixin.qq.com/cgi-bin/template/api_set_industry";

        /// <summary>
        /// api_add_template_url
        /// </summary>
        public const string api_add_template_url = "https://api.weixin.qq.com/cgi-bin/template/api_add_template";

        /// <summary>
        /// template_send_mesg_url
        /// </summary>
        public const string template_send_mesg_url = "https://api.weixin.qq.com/cgi-bin/message/template/send";

        /// <summary>
        /// 第三方平台appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 第三方平台appsecret
        /// </summary>
        public string appsecret { get; set; }

        /// <summary>
        /// EncodingAESKey解密key
        /// </summary>
        public string EncodingAESKey { get; set; }

        /// <summary>
        /// 推送component_verify_ticket(rediseKey)
        /// </summary>
        public string component_verify_ticket_key { get; set; }

        /// <summary>
        /// 第三方平台component_access_token (rediseKey)
        /// </summary>
        public string component_access_token_key { get; set; }

        /// <summary>
        /// 预授权码pre_auth_code(rediseKey)
        /// </summary>
        public string pre_auth_code_key { get; set; }

        /// <summary>
        /// authorizer_access_token
        /// </summary>
        public string authorizer_access_token_key { get; set; }

        /// <summary>
        /// 刷新access_token的token,重要,此值一旦丢失则需要公众号重新授权才能拿到
        /// </summary>
        public string authorizer_refresh_token_key { get; set; }
    }
}