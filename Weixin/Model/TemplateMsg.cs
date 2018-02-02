using Newtonsoft.Json;

namespace Weixin.Model
{
    /// <summary>
    /// 模版消息
    /// </summary>
    public class TemplateMsg<T>
    {
        /// <summary>
        /// 【必填】接收者openid
        /// </summary>
        [JsonProperty(PropertyName = "touser")]
        public string ToUser { get; set; }

        /// <summary>
        /// 【必填】模板ID
        /// </summary>
        [JsonProperty(PropertyName = "template_id")]
        public string TemplateId { get; set; }

        /// <summary>
        /// 模板跳转链接
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// 跳小程序所需数据，不需跳小程序可不用传该数据
        /// </summary>
        [JsonProperty(PropertyName = "miniprogram")]
        public MiniProgram MiniProgram { get; set; }

        /// <summary>
        /// 模板数据
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
    }
}
