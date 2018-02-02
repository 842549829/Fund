namespace Weixin.Model
{
    /// <summary>
    /// OpenidAndAccessTokenUrl
    /// </summary>
    public class OpenidAndAccessTokenUrl
    {
        /// <summary>
        /// 回调URL
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// state
        /// </summary>
        public string State { get; set; }
    }
}