using Newtonsoft.Json;

namespace Weixin.Model
{
    /// <summary>
    ///  获取jsapi_ticket返回model
    /// </summary>
    public class JsapiTicket
    {
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty(PropertyName = "errcode")]
        public int Errcode { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        [JsonProperty(PropertyName = "errmsg")]
        public string Errmsg { get; set; }

        /// <summary>
        /// ticket
        /// </summary>
        [JsonProperty(PropertyName = "ticket")]
        public string Ticket { get; set; }

        /// <summary>
        /// 有效时间
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }
    }
}