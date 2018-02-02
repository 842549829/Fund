using System.Security.Cryptography;
using System.Text;

namespace AliPay
{
    /// <summary>
    /// 支付宝Md5签名
    /// </summary>	
    public class Md5
    {
        /// <summary>
        /// 签名字符串
        /// </summary>
        /// <param name="prestr">需要签名的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <returns>签名结果</returns>
        public static string Sign(string prestr, string key, string inputCharset)
        {
            StringBuilder sb = new StringBuilder(32);
            prestr = prestr + key;
            MD5 md5 = new MD5CryptoServiceProvider();
            var t = md5.ComputeHash(Encoding.GetEncoding(inputCharset).GetBytes(prestr));
            foreach (byte item in t)
            {
                sb.Append(item.ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="prestr">需要签名的字符串</param>
        /// <param name="sign">签名结果</param>
        /// <param name="key">密钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <returns>验证结果</returns>
        public static bool Verify(string prestr, string sign, string key, string inputCharset)
        {
            string mysign = Sign(prestr, key, inputCharset);
            return mysign == sign;
        }
    }
}