using System;
using System.Xml.Linq;
using Weixin.Model;
using Weixin.Util;

namespace Weixin
{
    /// <summary>
    /// 通知
    /// </summary>
    public class Notify
    {
        /// <summary>
        /// 支付通知结果解析
        /// </summary>
        /// <param name="xml">xml</param>
        /// <param name="config">config</param>
        /// <returns>支付通知结果</returns>
        public static PayNotifyResult PayNotify(string xml, Config config)
        {
            var result = TenpayUtil.ConvertToPayNotifyRresult(xml);
            if (result.PayNotifyRresult.IsSuccess)
            {
                if (Signature.VerifySign(result.SignParanmeters, result.SignParanmeters["sign"], config.GetKey()))
                {
                    return result.PayNotifyRresult;
                }
                result.PayNotifyRresult.IsSuccess = false;
                result.PayNotifyRresult.Message = "签名错误";
                return result.PayNotifyRresult;
            }
            return result.PayNotifyRresult;
        }

        /// <summary>
        /// 退款通知结果解析
        /// </summary>
        /// <param name="xml">xml</param>
        /// <param name="config">config</param>
        /// <returns>退款结果</returns>
        public static RefundPayNotifyResult RefundPayNotify(string xml, Config config)
        {
            RefundPayNotifyResult refundPayNotifyResult = new RefundPayNotifyResult();
            var reqInfo = TenpayUtil.GetRefunPayNotifyReqInfo(xml);
            if (reqInfo.Key != null)
            {
                var stringA = Security.Encrypt32(config.GetKey()).ToLower();
                var reqInfoXml = Security.AesDecrypt(reqInfo.Key, stringA);
                refundPayNotifyResult = TenpayUtil.ConvertToRefundPayNotifyResult(reqInfoXml);
            }
            else
            {
                refundPayNotifyResult.IsSuccess = "CHANGE";
                refundPayNotifyResult.Message = reqInfo.Value;
            }
            return refundPayNotifyResult;
        }

        /// <summary>
        /// 接收推送消息
        /// </summary>
        /// <param name="xml">请求消息</param>
        /// <param name="configData">configData</param>
        /// <returns>处理结果</returns>
        public static PushResult ReceivePushMessage(string xml, ConfigData configData)
        {
            PushRequest pushRequest = new PushRequest
            {
                Xml = xml,
                EncodingAESKey = configData.EncodingAESKey,
                ComponentVerifyTicket = configData.component_verify_ticket_key,
                ValidityTime = DateTime.Now.AddDays(1)
            };
            XElement doc = XElement.Parse(pushRequest.Xml);
            var encry = doc.Element("Encrypt").GetString();
            string encodingAESKey = pushRequest.EncodingAESKey;
            string appid = string.Empty;
            var xmlContent = Cryptography.AES_decrypt(encry, encodingAESKey, ref appid);
            PushResult pushResult = TenpayUtil.ConvertToPushRequest(xmlContent);
            if (pushResult.InfoType == "component_verify_ticket")
            {
                // 写入缓存
                //Redis.SetRedis(pushRequest.ComponentVerifyTicket, pushResult.ComponentVerifyTicket, pushRequest.ValidityTime);
                pushResult.IsSucceed = !string.IsNullOrEmpty(pushRequest.ComponentVerifyTicket);
            }
            else
            {
                pushResult.IsSucceed = false;
                pushResult.Message = $"暂不处理类型{pushResult.InfoType}";
            }
            return pushResult;
        }
    }
}