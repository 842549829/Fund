namespace Weixin.Model
{
    /// <summary>
    /// token 信息
    /// </summary>
    public class TokenInfo
    {
        /// <summary>
        /// appId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        /// 跳转连接
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccese { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public string Message { get; set; }
    }
}