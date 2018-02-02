namespace Weixin.Model
{
    /// <summary>
    /// authorizer_access_token_result
    /// </summary>
    public class authorizer_access_token_result
    {
        /// <summary>
        /// authorization_info
        /// </summary>
        public authorization_info authorization_info { get; set; }

        /// <summary>
        /// func_info
        /// </summary>
        public func_info[] func_info { get; set; }
    }

    /// <summary>
    /// authorization_info
    /// </summary>
    public class authorization_info
    {
        /// <summary>
        /// authorizer_appid
        /// </summary>
        public string authorizer_appid { get; set; }

        /// <summary>
        /// authorizer_access_token
        /// </summary>
        public string authorizer_access_token { get; set; }

        /// <summary>
        /// expires_in
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// authorizer_refresh_token
        /// </summary>
        public string authorizer_refresh_token { get; set; }
    }

    /// <summary>
    /// func_info
    /// </summary>
    public class func_info
    {
        /// <summary>
        /// funcscope_category
        /// </summary>
        public funcscope_category funcscope_category { get; set; }
    }

    /// <summary>
    /// funcscope_category
    /// </summary>
    public class funcscope_category
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
    }
}