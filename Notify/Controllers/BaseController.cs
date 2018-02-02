using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace Notify.Controllers
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        protected SortedDictionary<string, string> GetRequestPost()
        {
            var sArray = new SortedDictionary<string, string>();
            var coll = Request.Form;
            var requestItem = coll.AllKeys;
            foreach (string item in requestItem)
            {
                sArray.Add(item, Request.Form[item]);
            }
            return sArray;
        }

        /// <summary>
        /// 获取Post参数
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        protected string GetRequestStr()
        {
            string content;
            using (Stream stream = Request.InputStream)
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                content = Encoding.UTF8.GetString(buffer);
            }
            return content;
        }

        /// <summary>
        /// 获取处理结果
        /// </summary>
        /// <param name="isSuccessed">是否支付成功</param>
        /// <param name="message">返回消息</param>
        /// <returns>结果</returns>
        protected static string GetResult(bool isSuccessed, string message)
        {
            StringBuilder sbPay = new StringBuilder();
            if (isSuccessed)
            {
                sbPay.Append("<return_code><![CDATA[FAIL]]></return_code>");
                sbPay.Append("<return_msg><![CDATA[OK]]></return_msg>");
            }
            else
            {
                sbPay.Append("<return_code><![CDATA[SUCCESS]]></return_code>");
                sbPay.Append("<return_msg><![CDATA[" + message + "]]></return_msg>");
            }
            var return_string = $"<xml>{sbPay}</xml>";
            return return_string;
        }

        /// <summary>
        /// 生成退分润数据集
        /// </summary>
        /// <param name="resultDetails">结果详情</param>
        /// <returns>退分润结果集</returns>
        protected static List<dynamic> CreateRefundShareArray(string resultDetails)
        {
            /* result_details格式为：第一笔交易#第二笔交易#第三笔交易...#第N笔交易第 N笔交易格式为：交易退款数据集$收费退款数据集|分润退款数据集|分润退款数据集|...|分退款数据集$$退子交易 
             * 说明：交易退款数据集不可为空，其余数据皆可为空。 
             * 交易退款数据集格式为：原付款支付宝交易号^退款总金额^退款状态。 
             * 收费退款数据集格式：被收费人支付宝账号[交易时支付宝收取服务费的账户]^被收费人支付宝账号对应用户ID[2088开头16位纯数字]^退款金额^退费理由。 
             * 分润退款数据集格式为：转出人支付宝账号[原收到分润金额的账户]^转出人支付宝账号对应用户ID[2088开头16位纯数字]^转入人支付宝账号[原付出分润金额的账户]^转入人支付宝账号对应用户ID^退款金额^退款状态。 
             * 子交易退款数据集格式为：金额^子交易共补款金额（总补款金额减去总退款金额）^退款状态。 
             */

            List<dynamic> result = new List<dynamic>();

            //移除退子交易数据
            string[] strNoChildArray = resultDetails.Split(new[] { "$$" }, StringSplitOptions.RemoveEmptyEntries);
            if (strNoChildArray.Length <= 0)
            {
                return result;
            }

            //拆分退分润数据
            string[] strPayArray = strNoChildArray[0].Split('$');
            var strShareArray = strPayArray.Length >= 2 ? strNoChildArray[1].Split('|') : strNoChildArray[0].Split('|');
            if (strShareArray.Length <= 1)
            {
                return result;
            }

            /* 循环分润数据
             * {0:转出人支付宝账号}^{1:转出人支付宝账号对应用户ID}^{2:转入人支付宝账号}^{3:转入人支付宝账号对应用户ID}^{4:退款金额}^{5:退款状态}。 
            */
            for (int i = 1; i < strShareArray.Length; i++)
            {
                var strItem = strShareArray[i].Split('^');
                if (strItem.Length >= 6)
                {
                    dynamic refundShare = new { };
                    refundShare.RivalAccount = strItem[0];
                    refundShare.RivalAccountId = strItem[1];
                    refundShare.RefundPrice = Convert.ToDecimal(strItem[4]);
                    if (strItem[5].ToUpper() == "SUCCESS")
                    {
                        refundShare.RefundStatus = "成功";
                    }
                    else
                    {
                        refundShare.RefundStatus = "失败";
                        refundShare.Remark = strItem[5];
                    }
                    result.Add(refundShare);
                }
            }

            return result;
        }

        /// <summary>
        /// 生成退支付数据
        /// </summary>
        /// <param name="resultDetails">结果详情</param>
        /// <returns>退支付结果</returns>
        protected static dynamic CreateRefundPay(string resultDetails)
        {
            /* result_details格式为：第一笔交易#第二笔交易#第三笔交易...#第N笔交易第 N笔交易格式为：交易退款数据集$收费退款数据集|分润退款数据集|分润退款数据集|...|分退款数据集$$退子交易 
           * 说明：交易退款数据集不可为空，其余数据皆可为空。 
           * 交易退款数据集格式为：原付款支付宝交易号^退款总金额^退款状态。 
           * 收费退款数据集格式：被收费人支付宝账号[交易时支付宝收取服务费的账户]^被收费人支付宝账号对应用户ID[2088开头16位纯数字]^退款金额^退费理由。 
           * 分润退款数据集格式为：转出人支付宝账号[原收到分润金额的账户]^转出人支付宝账号对应用户ID[2088开头16位纯数字]^转入人支付宝账号[原付出分润金额的账户]^转入人支付宝账号对应用户ID^退款金额^退款状态。 
           * 子交易退款数据集格式为：金额^子交易共补款金额（总补款金额减去总退款金额）^退款状态。 
           */

            dynamic refundBuyer = new object();

            //拆分退采购数据
            var strArray = resultDetails.Split('$');
            var strItem = strArray[0].Split('^');
            refundBuyer.RefundPrice = Convert.ToDecimal(strItem[1]);
            if (strItem[2] == "SUCCESS")
            {
                //退子交易成功
                refundBuyer.RefundStatus = "成功";
            }
            else
            {
                //退子交易失败
                refundBuyer.RefundStatus = "失败";
                refundBuyer.Remark = strItem[2];
            }

            // 解析退回的第三方手续费
            if (strArray.Length == 1)
            {
                refundBuyer.Poundage = 0;
            }
            else
            {
                var strItemPoundage = strArray[1].Split('^');
                if (strItemPoundage.Length == 4)
                {
                    refundBuyer.Poundage = Convert.ToDecimal(strItemPoundage[2]);
                }
            }
            return refundBuyer;
        }
    }
}