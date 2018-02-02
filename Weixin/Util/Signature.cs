using System.Collections.Generic;
using System.Text;

namespace Weixin.Util
{
    /// <summary>
    /// 签名
    /// </summary>
    public class Signature
    {
        /// <summary>
        /// 获取微信签名
        /// </summary>
        /// <param name="sParams">签名参数</param>
        /// <param name="key">key</param>
        /// <returns>签名</returns>
        public static string Getsign(SortedDictionary<string, string> sParams, string key)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in sParams)
            {
                if (string.IsNullOrEmpty(temp.Value) || temp.Key.ToLower() == "sign")
                {
                    continue;
                }
                sb.Append(temp.Key.Trim() + "=" + temp.Value.Trim() + "&");
            }
            sb.Append("key=" + key.Trim() + "");
            var signkey = sb.ToString();
            var sign = Security.Encrypt32(signkey);
            return sign;
        }

        /// <summary>
        /// 获取微信签名
        /// </summary>
        /// <param name="sParams">签名参数</param>
        /// <param name="sign">签名</param>
        /// <param name="key">key</param>
        /// <returns>签名</returns>
        public static bool VerifySign(SortedDictionary<string, string> sParams, string sign, string key)
        {
            return sign == Getsign(sParams, key);
        }
    }
}