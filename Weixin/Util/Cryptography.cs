using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Weixin.Util
{
    /// <summary>
    /// 微信开放平台加解算法
    /// </summary>
    public class Cryptography
    {
        /// <summary>
        /// HostToNetworkOrder
        /// </summary>
        /// <param name="inval">uint</param>
        /// <returns>uint</returns>
        public static uint HostToNetworkOrder(uint inval)
        {
            uint outval = 0;
            for (int i = 0; i < 4; i++)
            {
                outval = (outval << 8) + ((inval >> (i * 8)) & 255);
            }
            return outval;
        }

        /// <summary>
        /// HostToNetworkOrder
        /// </summary>
        /// <param name="inval">uint</param>
        /// <returns>uint</returns>
        public static int HostToNetworkOrder(int inval)
        {
            int outval = 0;
            for (int i = 0; i < 4; i++)
            {
                outval = (outval << 8) + ((inval >> (i * 8)) & 255);
            }
            return outval;
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="input">密文</param>
        /// <param name="encodingAESKey">EncodingAESKey</param>
        /// <param name="appid">appid</param>
        /// <returns>string</returns>
        public static string AES_decrypt(string input, string encodingAESKey, ref string appid)
        {
            if (appid == null)
            {
                throw new ArgumentNullException(nameof(appid));
            }
            var Key = Convert.FromBase64String(encodingAESKey + "=");
            byte[] Iv = new byte[16];
            Array.Copy(Key, Iv, 16);
            byte[] btmpMsg = AES_decrypt(input, Iv, Key);
            int len = BitConverter.ToInt32(btmpMsg, 16);
            len = IPAddress.NetworkToHostOrder(len);
            byte[] bMsg = new byte[len];
            byte[] bAppid = new byte[btmpMsg.Length - 20 - len];
            Array.Copy(btmpMsg, 20, bMsg, 0, len);
            Array.Copy(btmpMsg, 20 + len, bAppid, 0, btmpMsg.Length - 20 - len);
            string oriMsg = Encoding.UTF8.GetString(bMsg);
            appid = Encoding.UTF8.GetString(bAppid);
            return oriMsg;
        }

        /// <summary>
        /// 加密算法
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="encodingAESKey">encodingAESKey</param>
        /// <param name="appid">appid</param>
        /// <returns>string</returns>
        public static string AES_encrypt(string input, string encodingAESKey, string appid)
        {
            var Key = Convert.FromBase64String(encodingAESKey + "=");
            byte[] Iv = new byte[16];
            Array.Copy(Key, Iv, 16);
            string Randcode = CreateRandCode(16);
            byte[] bRand = Encoding.UTF8.GetBytes(Randcode);
            byte[] bAppid = Encoding.UTF8.GetBytes(appid);
            byte[] btmpMsg = Encoding.UTF8.GetBytes(input);
            byte[] bMsgLen = BitConverter.GetBytes(HostToNetworkOrder(btmpMsg.Length));
            byte[] bMsg = new byte[bRand.Length + bMsgLen.Length + bAppid.Length + btmpMsg.Length];

            Array.Copy(bRand, bMsg, bRand.Length);
            Array.Copy(bMsgLen, 0, bMsg, bRand.Length, bMsgLen.Length);
            Array.Copy(btmpMsg, 0, bMsg, bRand.Length + bMsgLen.Length, btmpMsg.Length);
            Array.Copy(bAppid, 0, bMsg, bRand.Length + bMsgLen.Length + btmpMsg.Length, bAppid.Length);

            return AES_encrypt(bMsg, Iv, Key);

        }

        /// <summary>
        /// CreateRandCode
        /// </summary>
        /// <param name="codeLen">codeLen</param>
        /// <returns>string</returns>
        private static string CreateRandCode(int codeLen)
        {
            string codeSerial = "2,3,4,5,6,7,a,c,d,e,f,h,i,j,k,m,n,p,r,s,t,A,C,D,E,F,G,H,J,K,M,N,P,Q,R,S,U,V,W,X,Y,Z";
            if (codeLen == 0)
            {
                codeLen = 16;
            }
            string[] arr = codeSerial.Split(',');
            string code = "";
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < codeLen; i++)
            {
                var randValue = rand.Next(0, arr.Length - 1);
                code += arr[randValue];
            }
            return code;
        }

        /// <summary>
        /// AES_encrypt
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="iv">iv</param>
        /// <param name="key">key</param>
        /// <returns>string</returns>
        private static string AES_encrypt(string input, byte[] iv, byte[] key)
        {
            var aes = new RijndaelManaged();
            //秘钥的大小，以位为单位
            aes.KeySize = 256;
            //支持的块大小
            aes.BlockSize = 128;
            //填充模式
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.Key = key;
            aes.IV = iv;
            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.UTF8.GetBytes(input);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = ms.ToArray();
            }
            string Output = Convert.ToBase64String(xBuff);
            return Output;
        }

        /// <summary>
        /// AES_encrypt
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="iv">iv</param>
        /// <param name="key">key</param>
        /// <returns>string</returns>
        private static string AES_encrypt(byte[] input, byte[] iv, byte[] key)
        {
            var aes = new RijndaelManaged();
            //秘钥的大小，以位为单位
            aes.KeySize = 256;
            //支持的块大小
            aes.BlockSize = 128;
            //填充模式
            //aes.Padding = PaddingMode.PKCS7;
            aes.Padding = PaddingMode.None;
            aes.Mode = CipherMode.CBC;
            aes.Key = key;
            aes.IV = iv;
            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;

            #region 自己进行PKCS7补位，用系统自己带的不行
            byte[] msg = new byte[input.Length + 32 - input.Length % 32];
            Array.Copy(input, msg, input.Length);
            byte[] pad = KCS7Encoder(input.Length);
            Array.Copy(pad, 0, msg, input.Length, pad.Length);
            #endregion

            #region 注释的也是一种方法，效果一样
            //ICryptoTransform transform = aes.CreateEncryptor();
            //byte[] xBuff = transform.TransformFinalBlock(msg, 0, msg.Length);
            #endregion

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    cs.Write(msg, 0, msg.Length);
                }
                xBuff = ms.ToArray();
            }

            string Output = Convert.ToBase64String(xBuff);
            return Output;
        }

        /// <summary>
        /// KCS7Encoder
        /// </summary>
        /// <param name="text_length">text_length</param>
        /// <returns>byte[]</returns>
        private static byte[] KCS7Encoder(int text_length)
        {
            int block_size = 32;
            // 计算需要填充的位数
            int amount_to_pad = block_size - (text_length % block_size);
            if (amount_to_pad == 0)
            {
                amount_to_pad = block_size;
            }
            // 获得补位所用的字符
            char pad_chr = Chr(amount_to_pad);
            string tmp = "";
            for (int index = 0; index < amount_to_pad; index++)
            {
                tmp += pad_chr;
            }
            return Encoding.UTF8.GetBytes(tmp);
        }

        /// <summary>
        /// 将数字转化成ASCII码对应的字符，用于对明文进行补码
        /// </summary>
        /// <param name="a">需要转化的数字</param>
        /// <returns>转化得到的字符</returns>
        private static char Chr(int a)
        {

            byte target = (byte)(a & 0xFF);
            return (char)target;
        }

        /// <summary>
        /// AES_decrypt
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="iv">iv</param>
        /// <param name="key">key</param>
        /// <returns>byte[]</returns>
        private static byte[] AES_decrypt(string input, byte[] iv, byte[] key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            aes.Key = key;
            aes.IV = iv;
            var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Convert.FromBase64String(input);
                    byte[] msg = new byte[xXml.Length + 32 - xXml.Length % 32];
                    Array.Copy(xXml, msg, xXml.Length);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = Decode2(ms.ToArray());
            }
            return xBuff;
        }

        /// <summary>
        /// Decode2
        /// </summary>
        /// <param name="decrypted">decrypted</param>
        /// <returns>byte[]</returns>
        private static byte[] Decode2(byte[] decrypted)
        {
            int pad = (int)decrypted[decrypted.Length - 1];
            if (pad < 1 || pad > 32)
            {
                pad = 0;
            }
            byte[] res = new byte[decrypted.Length - pad];
            Array.Copy(decrypted, 0, res, 0, decrypted.Length - pad);
            return res;
        }
    }
}