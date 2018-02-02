namespace Weixin.Model
{
    /// <summary>
    /// 授权令牌刷新结果
    /// </summary>
    public class authorizer_access_token_refresh_result
    {
        /// <summary>
        /// 授权方令牌
        /// </summary>
        public string authorizer_access_token { get; set; }

        /// <summary>
        /// 有效期,一般为2小时
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 授权方的刷新令牌,当授权令牌过期后通过此刷新令牌进行刷新; 此刷新令牌需要妥善保存,若丢失则需要公众号重新授权才可获取
        /// </summary>
        public string authorizer_refresh_token { get; set; }
    }
}