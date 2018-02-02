using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using AliPay.Model;
using AliPay.SdkPay.Util;

namespace AliPay
{
    /// <summary>
    /// Notify
    /// </summary>	
    public class Notify
    {
        /// <summary>
        ///  验证消息是否是支付宝发出的合法消息(航旅版)
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="notify_id">通知验证ID</param>
        /// <param name="sign">支付宝生成的签名结果</param>
        /// <param name="config">config</param>
        /// <returns>验证结果</returns>
        public static bool Verify(SortedDictionary<string, string> inputPara, string notify_id, string sign, Config config)
        {
            //获取返回时的签名验证结果
            bool isSign = GetSignVeryfy(inputPara, sign, config);
            //获取是否是支付宝服务器发来的请求的验证结果
            string responseTxt = "true";
            if (!string.IsNullOrEmpty(notify_id))
            {
                responseTxt = GetResponseTxt(notify_id, config);
            }
            //判断responsetTxt是否为true，isSign是否为true
            //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
            //isSign不是true，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
            return responseTxt == "true" && isSign;
        }

        /// <summary>
        ///  验证消息是否是支付宝发出的合法消息(蚂蚁金服)
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="config">config</param>
        /// <returns>验证结果</returns>
        public static bool VerifySdk(IDictionary<string, string> inputPara, SdkPay.Config config)
        {
            const string signType = SdkPay.Config.SignType;
            string publicKeyPemAliPay = config.GetPublicKeyPemAliPay();
            const string charset = SdkPay.Config.Charset;
            return AlipaySignature.RSACheckV1(inputPara, publicKeyPemAliPay, charset, signType, false);
        }

        /// <summary>
        /// 获取返回时的签名验证结果
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="sign">对比的签名结果</param>
        /// <param name="config">config</param>
        /// <returns>签名验证结果</returns>
        private static bool GetSignVeryfy(SortedDictionary<string, string> inputPara, string sign, Config config)
        {
            //过滤空值、sign与sign_type参数
            var sPara = Core.FilterPara(inputPara);
            //获取待签名字符串
            string preSignStr = Core.CreateLinkString(sPara);
            //获得签名验证结果
            bool isSgin = Md5.Verify(preSignStr, sign, config.GetKey(), Config.InputCharset);
            return isSgin;
        }

        /// <summary>
        /// 获取是否是支付宝服务器发来的请求的验证结果
        /// </summary>
        /// <param name="notify_id">通知验证ID</param>
        /// <param name="config">config</param>
        /// <returns>验证结果</returns>
        private static string GetResponseTxt(string notify_id, Config config)
        {
            string veryfy_url = Config.HttpsVeryfyUrl + "partner=" + config.GetPid() + "&notify_id=" + notify_id;
            //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            string responseTxt = GetHttp(veryfy_url, 120000);
            return responseTxt;
        }

        /// <summary>
        /// 获取远程服务器ATN结果
        /// </summary>
        /// <param name="strUrl">指定URL路径地址</param>
        /// <param name="timeout">超时时间设置</param>
        /// <returns>服务器ATN结果</returns>
        private static string GetHttp(string strUrl, int timeout)
        {
            string strResult = string.Empty;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(strUrl);
                myReq.Timeout = timeout;
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                if (myStream != null)
                {
                    StreamReader sr = new StreamReader(myStream, Encoding.Default);
                    StringBuilder strBuilder = new StringBuilder();
                    while (-1 != sr.Peek())
                    {
                        strBuilder.Append(sr.ReadLine());
                    }
                    strResult = strBuilder.ToString();
                }
            }
            catch (Exception exp)
            {
                strResult = "错误：" + exp.Message;
            }
            return strResult;
        }
    }
}