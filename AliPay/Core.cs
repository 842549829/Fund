using AliPay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AliPay
{
    /// <summary>
    /// 支付宝参数处理
    /// </summary>	
    public class Core
    {
        /// <summary>
        /// 除去数组中的空值和签名参数并以字母a到z的顺序排序
        /// </summary>
        /// <param name="dicArrayPre">过滤前的参数组</param>
        /// <returns>过滤后的参数组</returns>
        public static Dictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            return dicArrayPre.Where(temp => temp.Key.ToLower() != "sign" && temp.Key.ToLower() != "sign_type" && !string.IsNullOrEmpty(temp.Value)).ToDictionary(temp => temp.Key, temp => temp.Value);
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        /// </summary>
        /// <param name="dicArray">需要拼接的数组</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            var prestr = new StringBuilder();
            foreach (var temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }
            // 去掉最後一個&字符
            var nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);
            return prestr.ToString();
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串，并对参数值做urlencode
        /// </summary>
        /// <param name="dicArray">需要拼接的数组</param>
        /// <param name="code">字符编码</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkStringUrlencode(Dictionary<string, string> dicArray, Encoding code)
        {
            var prestr = new StringBuilder();
            foreach (var temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + HttpUtility.UrlEncode(temp.Value, code) + "&");
            }
            // 去掉最後一個&字符
            var nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);
            return prestr.ToString();
        }

        /// <summary>
        /// 分润参数集转化
        /// </summary>
        /// <param name="royaltyParameters">分润参数集</param>
        /// <returns>结果</returns>
        public static string ToRoyaltyParameters(List<RoyaltyParameters> royaltyParameters)
        {
            if (royaltyParameters == null)
            {
                throw new ArgumentNullException(nameof(royaltyParameters));
            }
            if (royaltyParameters.Count > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(royaltyParameters));
            }
            StringBuilder result = new StringBuilder();
            foreach (var item in royaltyParameters)
            {
                result.AppendFormat("{0}^{1}^{2}|", item.Account, item.Amount.ToString("0.00"), item.Remark);
            }
            return result.ToString().Trim('|');
        }

        /// <summary>
        /// 解码url
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>参数字典</returns>
        public static Dictionary<string, string> UrlToData(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }
            try
            {
                url = url.Trim();
                var split = url.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                var results = new Dictionary<string, string>();
                foreach (var item in split)
                {
                    var idx = item.IndexOf('=');

                    results.Add(Uri.UnescapeDataString(item.Substring(0, idx)), Uri.UnescapeDataString(item.Substring(idx + 1)));
                }
                return results;
            }
            catch (Exception ex)
            {
                throw new FormatException("URL格式错误", ex);
            }
        }
    }
}