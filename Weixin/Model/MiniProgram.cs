using Newtonsoft.Json;

namespace Weixin.Model
{
    /// <summary>
    /// 跳小程序所需数据，不需跳小程序可不用传该数据
    /// </summary>
    public class MiniProgram
    {
        /// <summary>
        /// 【必填】所需跳转到的小程序appid（该小程序appid必须与发模板消息的公众号是绑定关联关系）
        /// </summary>
        [JsonProperty(PropertyName = "appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 【必填】所需跳转到小程序的具体页面路径，支持带参数,（示例index?foo=bar）
        /// </summary>
        [JsonProperty(PropertyName = "pagepath")]
        public string PagePath { get; set; }
    }
}