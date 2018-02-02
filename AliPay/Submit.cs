using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using AliPay.Model;

namespace AliPay
{
    /// <summary>
    /// 生成提交数据
    /// </summary>	
    public class Submit
    {
        /// <summary>
        /// config
        /// </summary>
        private readonly Config config;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config">config</param>
        public Submit(Config config)
        {
            this.config = config;
        }

        /// <summary>
        /// 生成请求时的签名
        /// </summary>
        /// <param name="sPara">请求给支付宝的参数数组</param>
        /// <returns>签名结果</returns>
        private string BuildRequestMysign(Dictionary<string, string> sPara)
        {
            //把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            string prestr = Core.CreateLinkString(sPara);
            string mysign = Md5.Sign(prestr, this.config.GetKey(), Config.InputCharset);
            return mysign;
        }

        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <returns>要请求的参数数组</returns>
        private Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
        {
            //过滤签名参数数组
            var sPara = Core.FilterPara(sParaTemp);
            //获得签名结果
            var mysign = BuildRequestMysign(sPara);
            //签名结果与签名方式加入请求提交参数组中
            sPara.Add("sign", mysign);
            sPara.Add("sign_type", Config.SignType);
            return sPara;
        }

        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <param name="code">字符编码</param>
        /// <returns>要请求的参数数组字符串</returns>
        private string BuildRequestParaToString(SortedDictionary<string, string> sParaTemp, Encoding code)
        {
            //待签名请求参数数组
            var sPara = BuildRequestPara(sParaTemp);
            //把参数组中所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串，并对参数值做urlencode
            string strRequestData = Core.CreateLinkStringUrlencode(sPara, code);
            return strRequestData;
        }

        /// <summary>
        /// 建立请求，以表单HTML形式构造（默认）
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="strMethod">提交方式。两个值可选：post、get</param>
        /// <param name="strButtonValue">确认按钮显示文字</param>
        /// <returns>提交表单HTML文本</returns>
        public string BuildRequest(SortedDictionary<string, string> sParaTemp, string strMethod, string strButtonValue)
        {
            var dicPara = BuildRequestPara(sParaTemp);
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.AppendFormat("<form id='alipaysubmit' name='alipaysubmit' action='{0}_input_charset={1}' method='{2}'>", Config.Gateway, Config.InputCharset, strMethod.ToLower().Trim());
            foreach (var temp in dicPara)
            {
                sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }
            sbHtml.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");
            sbHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>");
            return sbHtml.ToString();
        }

        /// <summary>
        ///  建立请求，以表GET构造
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <returns>提交URL文本</returns>
        public string BuildRequest(SortedDictionary<string, string> sParaTemp)
        {
            var dicPara = BuildRequestPara(sParaTemp);
            StringBuilder sbUrl = new StringBuilder(Config.Gateway);
            foreach (var temp in dicPara)
            {
                sbUrl.AppendFormat("{0}={1}&", temp.Key, temp.Value);
            }
            return sbUrl.ToString().TrimEnd('&');
        }

        /// <summary>
        /// 生成Url链接
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <returns>提交表单HTML文本</returns>
        public string BuildLinkUrl(SortedDictionary<string, string> sParaTemp)
        {
            var dicPara = BuildRequestPara(sParaTemp);
            StringBuilder strUrl = new StringBuilder();
            strUrl.Append(Config.Gateway);
            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                strUrl.AppendFormat("{0}={1}&", temp.Key, temp.Value);
            }
            return strUrl.ToString().TrimEnd('&');
        }

        /// <summary>
        /// 建立请求，以模拟远程HTTP的POST请求方式构造并获取支付宝的处理结果
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="requestStr">请求支付宝参数</param>
        /// <returns>支付宝处理结果</returns>
        public string BuildRequest(SortedDictionary<string, string> sParaTemp, out string requestStr)
        {
            Encoding code = Encoding.GetEncoding(Config.InputCharset);
            string strRequestData = BuildRequestParaToString(sParaTemp, code);
            requestStr = strRequestData;

            //把数组转换成流中所需字节数组类型
            var bytesRequestData = code.GetBytes(strRequestData);

            //构造请求地址
            string strUrl = Config.Gateway + "_input_charset=" + Config.InputCharset;

            //请求远程HTTP
            string strResult = "";
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(strUrl);
                myReq.Method = "post";
                myReq.ContentType = "application/x-www-form-urlencoded";
                myReq.ContentLength = bytesRequestData.Length;
                Stream requestStream = myReq.GetRequestStream();
                requestStream.Write(bytesRequestData, 0, bytesRequestData.Length);
                requestStream.Close();
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                if (myStream != null)
                {
                    StreamReader reader = new StreamReader(myStream, code);
                    StringBuilder responseData = new StringBuilder();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        responseData.Append(line);
                    }
                    myStream.Close();
                    strResult = responseData.ToString();
                }
            }
            catch (Exception exp)
            {
                strResult = "报错：" + exp.Message;
            }
            return strResult;
        }
    }
}