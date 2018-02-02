using Newtonsoft.Json;
using System;

namespace Weixin.Model
{
    /// <summary>
    /// GeneralAccessToken
    /// </summary>
    public class GeneralAccessToken
    {
        /// <summary>
        /// access_token
        /// </summary>
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// expires_in
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime InvalidTime { get; set; }
    }
}