using Newtonsoft.Json;

namespace Weixin.Model
{
    /// <summary>
    /// IsFollow
    /// </summary>
    public class IsFollow
    {
        /// <summary>
        /// 是否关注 0-未关注，1-已关注
        /// </summary>
        [JsonProperty(PropertyName = "subscribe")]
        public int Subscribe { get; set; }

        //更多参数参考 https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140839
    }
}