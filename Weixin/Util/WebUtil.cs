using Newtonsoft.Json;
using System;
using Weixin.Model;

namespace Weixin.Util
{
    /// <summary>
    /// WebUtil
    /// </summary>
    public class WebUtil
    {
        /// <summary>
        /// 获取普通AccessToken
        /// </summary>
        /// <param name="configData">configData</param>
        /// <param name="forcedToRefresh">forcedToRefresh</param>
        /// <returns>普通AccessToken</returns>
        public static string GetGeneralAccessToken(Config configData, bool forcedToRefresh)
        {
            GeneralAccessToken token = null; /*读取redise*/ //Redis.GetRedis<GeneralAccessToken>(configData.GeneralAccessTokenCookieKey);
            if (forcedToRefresh)
            {
                token = QueryGeneralAccessTokenApi(configData);
            }
            else if (token != null)
            {
                token = QueryGeneralAccessTokenApi(configData);
            }
            else
            {
                token = QueryGeneralAccessTokenApi(configData);
            }
            return token.AccessToken;
        }

        /// <summary>
        /// 查询并保存普通AccessToken
        /// </summary>
        /// <param name="configData">configData</param>
        /// <returns>AccessToken</returns>
        private static GeneralAccessToken QueryGeneralAccessTokenApi(Config configData)
        {
            var strToken = HttpHelp.GetUrl(string.Format(Config.GeneralAccessTokenUrl, configData.GetAppId(), configData.GetAppSecret()));
            GeneralAccessToken token = JsonConvert.DeserializeObject<GeneralAccessToken>(strToken);
            token.InvalidTime = DateTime.Now.AddSeconds(token.ExpiresIn);
            // 写入缓存
            //Redis.SetRedis(configData.GeneralAccessTokenCookieKey, token);
            return token;
        }
    }
}