using System;
using System.Security.Cryptography;
using System.Text;

namespace Weixin.Util
{
    /// <summary>
    /// 安全算法
    /// </summary>
    public class Security
    {
        /// <summary>
        /// MD5加密32
        /// </summary>
        /// <param name="source">数据</param>
        /// <returns>密文</returns>
        public static string Encrypt32(string source)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.UTF8.GetBytes(source));
            StringBuilder sb = new StringBuilder(32);
            foreach (var item in t)
            {
                sb.Append(item.ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString().ToUpper();
        }

        /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="str">加密串</param>
        /// <param name="key">密钥</param>
        /// <returns>结果</returns>
        public static string AesEncrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="str">密串</param>
        /// <param name="key">密钥</param>
        /// <returns>解密串</returns>
        public static string AesDecrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            byte[] toEncryptArray = Convert.FromBase64String(str);
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = rm.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="value">val</param>
        /// <returns>结果</returns>
        public static string SHA1Encrypt(string value)
        {
            byte[] strRes = Encoding.UTF8.GetBytes(value);
            HashAlgorithm iSha = new SHA1CryptoServiceProvider();
            strRes = iSha.ComputeHash(strRes);
            StringBuilder enText = new StringBuilder();
            foreach (byte iByte in strRes)
            {
                enText.AppendFormat("{0:x2}", iByte);
            }
            return enText.ToString();
        }
    }
}