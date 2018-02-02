using Newtonsoft.Json;

namespace Weixin.Model
{
    /// <summary>
    /// 模版消息api调用结果，用户是否接收到信息的结果由微信服务器推送
    /// </summary>
    public class TemplateMsgResult
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        [JsonProperty(PropertyName = "errcode")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty(PropertyName = "errmsg")]
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 消息id
        /// </summary>
        [JsonProperty(PropertyName = "msgid")]
        public long MsgId { get; set; }
    }
}