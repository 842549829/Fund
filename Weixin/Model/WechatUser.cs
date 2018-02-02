using Newtonsoft.Json;

namespace Weixin.Model
{
    /// <summary>
    /// WechatUser
    /// </summary>
    public class WechatUser
    {
        /// <summary>
        /// errcode
        /// </summary>
        [JsonProperty(PropertyName = "errmsg")]
        public string Errmsg { get; set; }

        /// <summary>
        /// errcode
        /// </summary>
        [JsonProperty(PropertyName = "errcode")]
        public string Errcode { get; set; }

        /// <summary>
        /// total
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        /// <summary>
        /// count
        /// </summary>
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        /// <summary>
        /// data
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public Data Data { get; set; }

        /// <summary>
        /// next_openid
        /// </summary>
        [JsonProperty(PropertyName = "next_openid")]
        public string NextOpenid { get; set; }
    }

    /// <summary>
    /// Data
    /// </summary>
    public class Data
    {
        /// <summary>
        /// openid
        /// </summary>
        [JsonProperty(PropertyName = "openid")]
        public string[] Openid { get; set; }
    }
}