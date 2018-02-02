using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Weixin.Model;

namespace Weixin.Util
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class TenpayUtil
    {
        /// <summary>
        /// 微信统一下单接口xml参数整理
        /// </summary>
        /// <param name="request">微信支付参数实例</param>
        /// <param name="config">config</param>
        /// <returns>xml</returns>
        public static string GetUnifiedOrderXml(UnifiedOrderRequest request, Config config)
        {
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>
            {
                {"appid", config.GetAppId()},
                {"mch_id", config.GetPid()},
                {"nonce_str", Config.GetNoncestr()},
                {"body", request.Body},
                {"out_trade_no", request.OutTradeNo},
                {"total_fee", request.TotalFee.ToString()},
                {"spbill_create_ip", request.SpbillCreateIp},
                {"notify_url", request.NotifyUrl},
                {"trade_type", "APP"}
            };
            if (!string.IsNullOrEmpty(request.Detail))
            {
                sParams.Add("detail", request.Detail);
            }
            var sign = Signature.Getsign(sParams, config.GetKey());
            sParams.Add("sign", sign);

            //拼接成XML请求数据
            StringBuilder sbPay = new StringBuilder();
            foreach (KeyValuePair<string, string> k in sParams)
            {
                if (k.Key == "attach" || k.Key == "body" || k.Key == "sign")
                {
                    sbPay.Append("<" + k.Key + "><![CDATA[" + k.Value + "]]></" + k.Key + ">");
                }
                else
                {
                    sbPay.Append("<" + k.Key + ">" + k.Value + "</" + k.Key + ">");
                }
            }
            var return_string = $"<xml>{sbPay}</xml>";
            return return_string;
        }

        /// <summary>
        /// 微信统一下单接口xml参数整理
        /// </summary>
        /// <param name="request">微信支付参数实例</param>
        /// <param name="config">config</param>
        /// <returns>xml</returns>
        public static string GetUnifiedWapOrderXml(WapUnifiedOrderRequest request, Config config)
        {
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>
            {
                {"appid", config.GetAppId()},
                {"mch_id", config.GetPid()},
                {"nonce_str", Config.GetNoncestr()},
                {"body", request.Body},
                {"out_trade_no", request.OutTradeNo},
                {"total_fee", request.TotalFee.ToString()},
                {"spbill_create_ip", request.SpbillCreateIp},
                {"notify_url", request.NotifyUrl},
                {"trade_type", request.TradeType}
            };
            if (!string.IsNullOrWhiteSpace(request.SeneInfo))
            {
                sParams.Add("scene_info", request.SeneInfo);
            }
            if (!string.IsNullOrWhiteSpace(request.OpnenId))
            {
                sParams.Add("openid", request.OpnenId);
            }
            if (!string.IsNullOrEmpty(request.Detail))
            {
                sParams.Add("detail", request.Detail);
            }
            var sign = Signature.Getsign(sParams, config.GetKey());
            sParams.Add("sign", sign);

            //拼接成XML请求数据
            StringBuilder sbPay = new StringBuilder();
            foreach (KeyValuePair<string, string> k in sParams)
            {
                if (k.Key == "attach" || k.Key == "body" || k.Key == "sign")
                {
                    sbPay.Append("<" + k.Key + "><![CDATA[" + k.Value + "]]></" + k.Key + ">");
                }
                else
                {
                    sbPay.Append("<" + k.Key + ">" + k.Value + "</" + k.Key + ">");
                }
            }
            var return_string = $"<xml>{sbPay}</xml>";
            return return_string;
        }

        /// <summary>
        /// Model转化
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns>推送结果</returns>
        public static PushResult ConvertToPushRequest(string xml)
        {
            PushResult pushResult = new PushResult();
            XElement doc = XElement.Parse(xml);
            pushResult.AppId = doc.Element("AppId").GetString();
            pushResult.CreateTime = doc.Element("CreateTime").GetString();
            pushResult.InfoType = doc.Element("InfoType").GetString();
            pushResult.ComponentVerifyTicket = doc.Element("ComponentVerifyTicket").GetString();
            return pushResult;
        }

        /// <summary>
        /// 获取掉起支付签名
        /// </summary>
        /// <param name="result">统一下单结果</param>
        /// <param name="config">配置</param>
        /// <returns>参数</returns>
        public static SortedDictionary<string, string> GetPaySign(UnifiedOrderResult result, Config config)
        {
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>
            {
                {"appid", config.GetAppId()},
                {"partnerid", config.GetPid()},
                {"prepayid", result.PrepayId},
                {"package", "Sign=WXPay"},
                {"noncestr", Config.GetNoncestr()},
                {"timestamp", Config.GetTimestamp()},
            };
            var sign = Signature.Getsign(sParams, config.GetKey());
            sParams.Add("sign", sign);
            return sParams;
        }

        /// <summary>
        /// 获取掉起支付签名
        /// </summary>
        /// <param name="result">统一下单结果</param>
        /// <param name="config">配置</param>
        /// <returns>参数</returns>
        public static SortedDictionary<string, string> GetWapPaySign(WapUnifiedOrderResult result, Config config)
        {
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>
            {
                {"appId", config.GetAppId()},
                {"timeStamp", Config.GetTimestamp()},
                {"nonceStr", Config.GetNoncestr()},
                {"package", result.Package},
                {"signType", "MD5"}
            };
            var sign = Signature.Getsign(sParams, config.GetKey());
            sParams.Add("sign", sign);
            return sParams;
        }

        /// <summary>
        /// 创建微信支付参数
        /// </summary>
        /// <param name="parameters">签名参数</param>
        /// <param name="unifiedOrderResult">统一下单参数</param>
        /// <returns>结果</returns>
        public static UnifiedOrderResult CreatePayParameters(SortedDictionary<string, string> parameters, UnifiedOrderResult unifiedOrderResult)
        {
            UnifiedOrderResult result = new UnifiedOrderResult
            {
                Appid = parameters["appid"],
                DeviceInfo = unifiedOrderResult.DeviceInfo,
                IsSuccess = unifiedOrderResult.IsSuccess,
                Message = unifiedOrderResult.Message,
                MchId = parameters["partnerid"],
                PrepayId = parameters["prepayid"],
                NonceStr = parameters["noncestr"],
                Timestamp = parameters["timestamp"],
                Sign = parameters["sign"],
                TradeType = unifiedOrderResult.TradeType
            };
            return result;
        }

        /// <summary>
        /// 统一下单xml结果转化实体
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns>统一下单结果</returns>
        public static UnifiedOrderResult ConvertToUnifiedOrderResult(string xml)
        {
            UnifiedOrderResult unifiedOrderResult = new UnifiedOrderResult();
            XElement doc = XElement.Parse(xml);
            if (doc.Element("return_code").GetString() == "SUCCESS")
            {
                if (doc.Element("result_code").GetString() == "SUCCESS")
                {
                    unifiedOrderResult.IsSuccess = true;
                    unifiedOrderResult.Appid = doc.Element("appid").GetString();
                    unifiedOrderResult.MchId = doc.Element("mch_id").GetString();
                    unifiedOrderResult.DeviceInfo = doc.Element("device_info").GetString();
                    unifiedOrderResult.NonceStr = doc.Element("nonce_str").GetString();
                    unifiedOrderResult.Sign = doc.Element("sign").GetString();
                    unifiedOrderResult.TradeType = doc.Element("trade_type").GetString();
                    unifiedOrderResult.PrepayId = doc.Element("prepay_id").GetString();
                }
                else
                {
                    unifiedOrderResult.IsSuccess = false;
                    unifiedOrderResult.Message = doc.Element("err_code_des").GetString();
                }
            }
            else
            {
                unifiedOrderResult.IsSuccess = false;
                unifiedOrderResult.Message = doc.Element("return_msg").GetString();
            }
            return unifiedOrderResult;
        }

        /// <summary>
        /// Wap统一下单xml结果转化实体
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns>统一下单结果</returns>
        public static WapUnifiedOrderResult ConvertToWapUnifiedOrderResult(string xml)
        {
            WapUnifiedOrderResult unifiedOrderResult = new WapUnifiedOrderResult();
            XElement doc = XElement.Parse(xml);
            if (doc.Element("return_code").GetString() == "SUCCESS")
            {
                if (doc.Element("result_code").GetString() == "SUCCESS")
                {
                    unifiedOrderResult.IsSuccess = true;
                    unifiedOrderResult.Appid = doc.Element("appid").GetString();
                    unifiedOrderResult.NonceStr = doc.Element("nonce_str").GetString();
                    unifiedOrderResult.Sign = doc.Element("sign").GetString();
                    unifiedOrderResult.Package = "prepay_id=" + doc.Element("prepay_id").GetString();
                }
                else
                {
                    unifiedOrderResult.IsSuccess = false;
                    unifiedOrderResult.Message = doc.Element("err_code_des").GetString();
                }
            }
            else
            {
                unifiedOrderResult.IsSuccess = false;
                unifiedOrderResult.Message = doc.Element("return_msg").GetString();
            }
            return unifiedOrderResult;
        }

        /// <summary>
        /// 创建微信Wap支付参数
        /// </summary>
        /// <param name="parameters">签名参数</param>
        /// <param name="unifiedOrderResult">统一下单参数</param>
        /// <returns>结果</returns>
        public static WapUnifiedOrderResult CreatePayParameters(SortedDictionary<string, string> parameters, WapUnifiedOrderResult unifiedOrderResult)
        {
            WapUnifiedOrderResult result = new WapUnifiedOrderResult
            {
                IsSuccess = unifiedOrderResult.IsSuccess,
                Message = unifiedOrderResult.Message,
                Appid = parameters["appId"],
                NonceStr = parameters["nonceStr"],
                Timestamp = parameters["timeStamp"],
                Sign = parameters["sign"],
                Package = unifiedOrderResult.Package,
                SignType = "MD5"
            };
            return result;
        }

        /// <summary>
        /// 支付通知
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns>结果</returns>
        public static PayNotifyResponse ConvertToPayNotifyRresult(string xml)
        {
            PayNotifyResponse payNotifyResponse = new PayNotifyResponse();
            PayNotifyResult payNotifyRresult = new PayNotifyResult();
            SortedDictionary<string, string> signParanmeters = new SortedDictionary<string, string>();
            xml = xml.Replace("coupon_id_$n", "coupon_id_xmln").Replace("coupon_fee_$n", "coupon_fee_xmln");
            XElement doc = XElement.Parse(xml);
            if (doc.Element("return_code").GetString() == "SUCCESS")
            {
                if (doc.Element("result_code").GetString() == "SUCCESS")
                {
                    payNotifyRresult.IsSuccess = true;
                    payNotifyRresult.Appid = doc.Element("appid").GetString();
                    payNotifyRresult.MchId = doc.Element("mch_id").GetString();
                    payNotifyRresult.DeviceInfo = doc.Element("device_info").GetString();
                    payNotifyRresult.NonceStr = doc.Element("nonce_str").GetString();
                    payNotifyRresult.Sign = doc.Element("sign").GetString();
                    payNotifyRresult.TradeType = doc.Element("trade_type").GetString();
                    payNotifyRresult.BankType = doc.Element("bank_type").GetString();
                    payNotifyRresult.TotalFee = doc.Element("total_fee").GetInt();
                    payNotifyRresult.TransactionId = doc.Element("transaction_id").GetString();
                    payNotifyRresult.OutTradeNo = doc.Element("out_trade_no").GetString();
                    payNotifyRresult.TimeEnd = doc.Element("time_end").GetString();

                    signParanmeters.Add("return_code", doc.Element("result_code").GetString());
                    signParanmeters.Add("appid", doc.Element("appid").GetString());
                    signParanmeters.Add("mch_id", doc.Element("mch_id").GetString());
                    if (!string.IsNullOrEmpty(doc.Element("device_info").GetString()))
                    {
                        signParanmeters.Add("device_info", doc.Element("device_info").GetString());
                    }
                    signParanmeters.Add("nonce_str", doc.Element("nonce_str").GetString());
                    signParanmeters.Add("sign", doc.Element("sign").GetString());
                    signParanmeters.Add("result_code", doc.Element("result_code").GetString());
                    signParanmeters.Add("openid", doc.Element("openid").GetString());
                    if (!string.IsNullOrEmpty(doc.Element("is_subscribe").GetString()))
                    {
                        signParanmeters.Add("is_subscribe", doc.Element("is_subscribe").GetString());
                    }
                    signParanmeters.Add("trade_type", doc.Element("trade_type").GetString());
                    signParanmeters.Add("bank_type", doc.Element("bank_type").GetString());
                    signParanmeters.Add("total_fee", doc.Element("total_fee").GetString());
                    if (!string.IsNullOrEmpty(doc.Element("fee_type").GetString()))
                    {
                        signParanmeters.Add("fee_type", doc.Element("fee_type").GetString());
                    }
                    signParanmeters.Add("cash_fee", doc.Element("cash_fee").GetString());
                    if (!string.IsNullOrEmpty(doc.Element("coupon_fee").GetString()))
                    {
                        signParanmeters.Add("coupon_fee", doc.Element("coupon_fee").GetString());
                    }
                    if (!string.IsNullOrEmpty(doc.Element("coupon_count").GetString()))
                    {
                        signParanmeters.Add("coupon_count", doc.Element("coupon_count").GetString());
                    }
                    if (!string.IsNullOrEmpty(doc.Element("coupon_id_xmln").GetString()))
                    {
                        signParanmeters.Add("coupon_id_$n", doc.Element("coupon_id_xmln").GetString());
                    }
                    if (!string.IsNullOrEmpty(doc.Element("coupon_fee_xmln").GetString()))
                    {
                        signParanmeters.Add("coupon_fee_$n", doc.Element("coupon_fee_xmln").GetString());
                    }
                    signParanmeters.Add("transaction_id", doc.Element("transaction_id").GetString());
                    signParanmeters.Add("out_trade_no", doc.Element("out_trade_no").GetString());
                    if (!string.IsNullOrEmpty(doc.Element("attach").GetString()))
                    {
                        signParanmeters.Add("attach", doc.Element("attach").GetString());
                    }
                    signParanmeters.Add("time_end", doc.Element("time_end").GetString());
                }
                else
                {
                    payNotifyRresult.IsSuccess = false;
                    payNotifyRresult.Message = doc.Element("err_code_des").GetString();
                }
            }
            else
            {
                payNotifyRresult.IsSuccess = false;
                payNotifyRresult.Message = doc.Element("return_msg").GetString();
            }
            payNotifyResponse.PayNotifyRresult = payNotifyRresult;
            payNotifyResponse.SignParanmeters = signParanmeters;
            return payNotifyResponse;
        }

        /// <summary>
        /// 微信订单查询接口xml参数整理
        /// </summary>
        /// <param name="request">查询信息</param>
        /// <param name="config">config</param>
        /// <returns>xml</returns>
        public static string GetOrderqueryRequestXml(OrderQueryRequest request, Config config)
        {
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>
            {
                {"appid", config.GetAppId()},
                {"mch_id", config.GetPid()},
                {"nonce_str", Config.GetNoncestr()}
            };
            if (!string.IsNullOrWhiteSpace(request.OutTradeNo))
            {
                sParams.Add("out_trade_no", request.OutTradeNo);
            }
            else
            {
                sParams.Add("transaction_id", request.TransactionId);
            }

            var sing = Signature.Getsign(sParams, config.GetKey());
            sParams.Add("sign", sing);

            //拼接成XML请求数据
            StringBuilder sbPay = new StringBuilder();
            foreach (KeyValuePair<string, string> k in sParams)
            {
                if (k.Key == "attach" || k.Key == "body" || k.Key == "sign")
                {
                    sbPay.Append("<" + k.Key + "><![CDATA[" + k.Value + "]]></" + k.Key + ">");
                }
                else
                {
                    sbPay.Append("<" + k.Key + ">" + k.Value + "</" + k.Key + ">");
                }
            }
            var return_string = $"<xml>{sbPay}</xml>";
            return return_string;
        }

        /// <summary>
        /// 微信订单查询结果
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns>微信订单查询结果</returns>
        public static OrderQueryResult ConvertToOrderqueryResult(string xml)
        {
            OrderQueryResult orderqueryResult = new OrderQueryResult();
            XElement doc = XElement.Parse(xml);
            if (doc.Element("return_code").GetString() == "SUCCESS")
            {
                if (doc.Element("result_code").GetString() == "SUCCESS")
                {
                    var tradeState = doc.Element("trade_state").GetString();
                    if (tradeState == "SUCCESS")
                    {
                        orderqueryResult.IsSuccess = true;
                    }
                    else
                    {
                        orderqueryResult.IsSuccess = false;
                    }
                    orderqueryResult.TradeState = tradeState;
                    orderqueryResult.Appid = doc.Element("appid").GetString();
                    orderqueryResult.MchId = doc.Element("mch_id").GetString();
                    orderqueryResult.BankType = doc.Element("bank_type").GetString();
                    orderqueryResult.TotalFee = doc.Element("total_fee").GetInt();
                    orderqueryResult.TimeEnd = doc.Element("time_end").GetString();
                    orderqueryResult.TransactionId = doc.Element("transaction_id").GetString();
                }
                else
                {
                    orderqueryResult.IsSuccess = false;
                    orderqueryResult.Message = doc.Element("err_code_des").GetString();
                }
            }
            else
            {
                orderqueryResult.IsSuccess = false;
                orderqueryResult.Message = doc.Element("return_msg").GetString();
            }
            return orderqueryResult;
        }

        /// <summary>
        /// 微信退支付接口
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="config">config</param>
        /// <returns>xml</returns>
        public static string GetRefundPayRequestXml(RefundPayRequest request, Config config)
        {
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>
            {
                {"appid", config.GetAppId()},
                {"mch_id", config.GetPid()},
                {"nonce_str", Config.GetNoncestr()},
                {"total_fee", request.TotalFee.ToString()},
                {"refund_fee", request.RefundFee.ToString()},
                {"out_refund_no", request.OutRefundNo}
            };
            if (!string.IsNullOrWhiteSpace(request.OutTradeNo))
            {
                sParams.Add("out_trade_no", request.OutTradeNo);
            }
            else
            {
                sParams.Add("transaction_id", request.TransactionId);
            }
            if (!string.IsNullOrWhiteSpace(request.RefundDesc))
            {
                sParams.Add("refund_desc", request.RefundDesc);
            }
            var sing = Signature.Getsign(sParams, config.GetKey());
            sParams.Add("sign", sing);

            //拼接成XML请求数据
            StringBuilder sbPay = new StringBuilder();
            foreach (KeyValuePair<string, string> k in sParams)
            {
                if (k.Key == "attach" || k.Key == "body" || k.Key == "sign")
                {
                    sbPay.Append("<" + k.Key + "><![CDATA[" + k.Value + "]]></" + k.Key + ">");
                }
                else
                {
                    sbPay.Append("<" + k.Key + ">" + k.Value + "</" + k.Key + ">");
                }
            }
            var return_string = $"<xml>{sbPay}</xml>";
            return return_string;
        }

        /// <summary>
        /// 微信退支付申请接口结果
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns>微信退支付申请接口结果</returns>
        public static RefundPayResult ConvertToRefundPayResult(string xml)
        {
            RefundPayResult refundPayResult = new RefundPayResult();
            XElement doc = XElement.Parse(xml);
            if (doc.Element("return_code").GetString() == "SUCCESS")
            {
                if (doc.Element("result_code").GetString() == "SUCCESS")
                {
                    refundPayResult.IsSuccess = true;
                    refundPayResult.TransactionId = doc.Element("transaction_id").GetString();
                    refundPayResult.OutTradeNo = doc.Element("out_trade_no").GetString();
                    refundPayResult.OutRefundNo = doc.Element("out_refund_no").GetString();
                    refundPayResult.RefundId = doc.Element("refund_id").GetString();
                    refundPayResult.RefundFee = doc.Element("refund_fee").GetInt();
                }
                else
                {
                    refundPayResult.IsSuccess = false;
                    refundPayResult.Message = doc.Element("err_code_des").GetString();
                }
            }
            else
            {
                refundPayResult.IsSuccess = false;
                refundPayResult.Message = doc.Element("return_msg").GetString();
            }
            return refundPayResult;
        }

        /// <summary>
        /// 微信转账
        /// </summary>
        /// <param name="request"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string GetTransfersRequestXml(TransfersRequest request, Config config)
        {
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>
            {
                {"mch_appid", config.GetAppId()},
                {"mchid", config.GetPid()},
                {"nonce_str", Config.GetNoncestr()},
                {"partner_trade_no", request.PartnerTradeNo},
                {"openid", request.OpenId},
                {"amount", request.Amount.ToString()},
                {"desc", request.Desc},
                {"spbill_create_ip", request.SpbillCreateIp}
            };
            if (!string.IsNullOrWhiteSpace(request.ReUserName))
            {
                sParams.Add("check_name", "FORCE_CHECK");
                sParams.Add("re_user_name", request.ReUserName);
            }
            else
            {
                sParams.Add("check_name", "NO_CHECK");
            }
            var sing = Signature.Getsign(sParams, config.GetKey());
            sParams.Add("sign", sing);
            StringBuilder sbPay = new StringBuilder();
            foreach (KeyValuePair<string, string> k in sParams)
            {
                if (k.Key == "desc" || k.Key == "re_user_name")
                {
                    sbPay.Append("<" + k.Key + "><![CDATA[" + k.Value + "]]></" + k.Key + ">");
                }
                else
                {
                    sbPay.Append("<" + k.Key + ">" + k.Value + "</" + k.Key + ">");
                }
            }
            var return_string = $"<xml>{sbPay}</xml>";
            return return_string;
        }

        /// <summary>
        /// 获取退款
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns>key:结果,value:错误描述</returns>
        public static KeyValuePair<string, string> GetRefunPayNotifyReqInfo(string xml)
        {
            XElement doc = XElement.Parse(xml);
            if (doc.Element("return_code").GetString() == "SUCCESS")
            {
                return new KeyValuePair<string, string>(doc.Element("req_info").GetString(), null);
            }
            else
            {
                return new KeyValuePair<string, string>(null, doc.Element("return_msg").GetString());
            }
        }

        /// <summary>
        /// 微信退支付解密结果
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns>微信退支付解密结果</returns>
        public static RefundPayNotifyResult ConvertToRefundPayNotifyResult(string xml)
        {
            RefundPayNotifyResult refundPayNotifyResult = new RefundPayNotifyResult();
            XElement doc = XElement.Parse(xml);
            var refundStatus = doc.Element("refund_status").GetString();
            if (refundStatus.Trim() == "SUCCESS")
            {
                refundPayNotifyResult.IsSuccess = refundStatus;
                refundPayNotifyResult.TransactionId = doc.Element("transaction_id").GetString();
                refundPayNotifyResult.OutTradeNo = doc.Element("out_trade_no").GetString();
                refundPayNotifyResult.RefundId = doc.Element("refund_id").GetString();
                refundPayNotifyResult.OutRefundNo = doc.Element("out_refund_no").GetString();
                refundPayNotifyResult.TotalFee = doc.Element("total_fee").GetInt();
                refundPayNotifyResult.RefundFee = doc.Element("refund_fee").GetInt();
                refundPayNotifyResult.SettlementRefundFee = doc.Element("settlement_refund_fee").GetInt();
                refundPayNotifyResult.SuccessTime = doc.Element("success_time").GetString();
                refundPayNotifyResult.RefundRecvAccout = doc.Element("refund_recv_accout").GetString();
                refundPayNotifyResult.RefundAccount = doc.Element("refund_account").GetString();
                refundPayNotifyResult.RefundRequestSource = doc.Element("refund_request_source").GetString();
            }
            else
            {
                refundPayNotifyResult.IsSuccess = refundStatus;
                refundPayNotifyResult.Message = doc.Element("refund_status").GetString();
            }
            return refundPayNotifyResult;
        }

        /// <summary>
        /// 微信退款订单查询接口xml参数整理
        /// </summary>
        /// <param name="request">查询信息</param>
        /// <param name="config">config</param>
        /// <returns>xml</returns>
        public static string GetRefundQueryRequestXml(RefundQueryRequest request, Config config)
        {
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>
            {
                {"appid", config.GetAppId()},
                {"mch_id", config.GetPid()},
                {"nonce_str", Config.GetNoncestr()}
            };
            if (!string.IsNullOrWhiteSpace(request.OutTradeNo))
            {
                sParams.Add("out_trade_no", request.OutTradeNo);
            }
            if (!string.IsNullOrWhiteSpace(request.OutRefundNo))
            {
                sParams.Add("out_refund_no", request.OutRefundNo);
            }
            if (!string.IsNullOrWhiteSpace(request.TransactionId))
            {
                sParams.Add("transaction_id", request.TransactionId);
            }
            if (!string.IsNullOrWhiteSpace(request.RefundId))
            {
                sParams.Add("refund_id", request.RefundId);
            }
            var sing = Signature.Getsign(sParams, config.GetKey());
            sParams.Add("sign", sing);
            StringBuilder sbPay = new StringBuilder();
            foreach (KeyValuePair<string, string> k in sParams)
            {
                if (k.Key == "attach" || k.Key == "body" || k.Key == "sign")
                {
                    sbPay.Append("<" + k.Key + "><![CDATA[" + k.Value + "]]></" + k.Key + ">");
                }
                else
                {
                    sbPay.Append("<" + k.Key + ">" + k.Value + "</" + k.Key + ">");
                }
            }
            var return_string = $"<xml>{sbPay}</xml>";
            return return_string;
        }

        /// <summary>
        /// 微信退款订单查询结果
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns>微信退款订单查询结果</returns>
        public static RefundQueryResult ConvertToRefundQueryResult(string xml)
        {
            RefundQueryResult refundQueryResult = new RefundQueryResult();
            xml = xml.Replace("$", "xml");
            XElement doc = XElement.Parse(xml);
            if (doc.Element("return_code").GetString() == "SUCCESS")
            {
                if (doc.Element("result_code").GetString() == "SUCCESS")
                {
                    refundQueryResult.IsSuccess = true;
                    var details = new List<RefundQueryDetails>();
                    for (int i = 0; i < doc.Element("refund_count").GetInt(); i++)
                    {
                        details.Add(new RefundQueryDetails
                        {
                            RefundStatus = doc.Element("refund_status_" + i).GetString(),
                            OutRefundNo = doc.Element("out_refund_no_" + i).GetString(),
                            RefundFee = doc.Element("refund_fee_" + i).GetInt(),
                            RefundId = doc.Element("refund_id_" + i).GetString(),
                        });
                    }
                    refundQueryResult.Details = details;
                }
                else
                {
                    refundQueryResult.IsSuccess = false;
                    refundQueryResult.Message = doc.Element("err_code_des").GetString();
                }
            }
            else
            {
                refundQueryResult.IsSuccess = false;
                refundQueryResult.Message = doc.Element("return_msg").GetString();
            }
            return refundQueryResult;
        }

        /// <summary>
        /// 请求CODE
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="config">基础配置</param>
        /// <returns>结果</returns>
        public static string GetOpenidAndAccessTokenUrl(OpenidAndAccessTokenUrl request, Config config)
        {
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>
            {
                {"appid", config.GetAppId()},
                {"redirect_uri", request.RedirectUri},
                {"response_type", "code"},
                {"scope", "snsapi_base"},
                {"state", request.State}
            };
            string buff = string.Empty;
            foreach (KeyValuePair<string, string> pair in sParams)
            {
                if (pair.Key != "sign" && pair.Value != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            string url = $"{Config.AuthorizeUrl}?{buff}";
            return url;
        }

        /// <summary>
        /// 通过code换取网页授权openid的请求URL
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="config">配置</param>
        /// <returns>URL</returns>
        public static string GetOpenidFromCodeUrl(OpenidFromCodeRequest request, Config config)
        {
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>
            {
                {"appid", config.GetAppId()},
                {"secret", config.GetAppSecret()},
                {"code", request.Code},
                {"grant_type", "authorization_code"}
            };
            string buff = string.Empty;
            foreach (KeyValuePair<string, string> pair in sParams)
            {
                if (pair.Key != "sign" && pair.Value != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            string url = $"{Config.AccessTokenUrl}?{buff}";
            return url;
        }

        /// <summary>
        /// 微信转账订单查询接口xml参数整理
        /// </summary>
        /// <param name="request">查询信息</param>
        /// <param name="config">config</param>
        /// <returns>xml</returns>
        public static string GetTransfersQueryRequestXml(TransfersQueryRequest request, Config config)
        {
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>
            {
                {"appid", config.GetAppId()},
                {"mch_id", config.GetPid()},
                {"nonce_str", Config.GetNoncestr()},
                {"partner_trade_no", request.PartnerTradeNo}
            };
            var sing = Signature.Getsign(sParams, config.GetKey());
            sParams.Add("sign", sing);
            StringBuilder sbPay = new StringBuilder();
            foreach (KeyValuePair<string, string> k in sParams)
            {
                sbPay.Append("<" + k.Key + "><![CDATA[" + k.Value + "]]></" + k.Key + ">");
            }
            var return_string = $"<xml>{sbPay}</xml>";
            return return_string;
        }

        /// <summary>
        /// 微信转账查询解密结果
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns>微信转账解密查询结果</returns>
        public static TransfersQueryResult ConvertToTransfersQueryResult(string xml)
        {
            TransfersQueryResult transfersQueryResult = new TransfersQueryResult();
            XElement doc = XElement.Parse(xml);
            if (doc.Element("return_code").GetString() == "SUCCESS")
            {
                if (doc.Element("result_code").GetString() == "SUCCESS")
                {
                    var status = doc.Element("status").GetString();
                    if (status == "SUCCESS")
                    {
                        transfersQueryResult.Status = TransfersStatus.Success;
                        transfersQueryResult.PartnerTradeNo = doc.Element("partner_trade_no").GetString();
                        transfersQueryResult.DetailId = doc.Element("detail_id").GetString();
                        transfersQueryResult.OpenId = doc.Element("openid").GetString();
                        transfersQueryResult.TransferName = doc.Element("transfer_name").GetString();
                        transfersQueryResult.PaymentAmount = doc.Element("payment_amount").GetInt();
                        transfersQueryResult.TransferTime = doc.Element("transfer_time").GetDateTime();
                        transfersQueryResult.Desc = doc.Element("desc").GetString();
                    }
                    else if (status == "PROCESSING")
                    {
                        transfersQueryResult.Status = TransfersStatus.Processing;
                    }
                    else
                    {
                        transfersQueryResult.Status = TransfersStatus.Falied;
                        transfersQueryResult.ReturnMsg = doc.Element("reason").GetString();
                    }
                }
                else
                {
                    transfersQueryResult.Status = TransfersStatus.Falied;
                    transfersQueryResult.ReturnMsg = doc.Element("err_code_des").GetString();
                }
            }
            else
            {
                transfersQueryResult.Status = TransfersStatus.Falied;
                transfersQueryResult.ReturnMsg = doc.Element("return_msg").GetString();
            }
            return transfersQueryResult;
        }

        /// <summary>
        /// 微信转账解密结果
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns>微信转账解密结果</returns>
        public static TransfersResult ConvertToTransfersResult(string xml)
        {
            TransfersResult transfersQueryResult = new TransfersResult();
            if (xml == null)
            {
                transfersQueryResult.IsSuccess = TransfersStatus.Processing;
                transfersQueryResult.Message = "注意1-当返回错误码为“SYSTEMERROR”时，一定要使用原单号重试，否则可能造成重复支付等资金风险。";
            }
            else
            {
                XElement doc = XElement.Parse(xml);
                var returnCode = doc.Element("return_code").GetString();
                if (returnCode == "SUCCESS")
                {
                    if (doc.Element("err_code").GetString() == "SYSTEMERROR")
                    {
                        transfersQueryResult.IsSuccess = TransfersStatus.Processing;
                        transfersQueryResult.Message = "注意1-当返回错误码为“SYSTEMERROR”时，一定要使用原单号重试，否则可能造成重复支付等资金风险。";
                    }
                    else if (doc.Element("result_code").GetString() == "SUCCESS")
                    {
                        transfersQueryResult.IsSuccess = TransfersStatus.Success;
                        transfersQueryResult.PartnerTradeNo = doc.Element("partner_trade_no").GetString();
                        transfersQueryResult.TransactionId = doc.Element("payment_no").GetString();
                        transfersQueryResult.PaymentTime = doc.Element("payment_time").GetDateTime();
                    }
                    else
                    {
                        transfersQueryResult.IsSuccess = TransfersStatus.Falied;
                        transfersQueryResult.Message = doc.Element("err_code_des").GetString();
                    }
                }
                else
                {
                    transfersQueryResult.IsSuccess = TransfersStatus.Falied;
                    transfersQueryResult.Message = doc.Element("return_msg").GetString();
                }
            }
            return transfersQueryResult;
        }

        /// <summary>
        /// 获取用户信息Url
        /// </summary>
        /// <param name="wechatUserRequest">配置数据</param>
        /// <param name="config">config</param>
        /// <param name="forcedToRefresh">强制刷新缓存</param>
        /// <returns>Url</returns>
        public static string GetUserUrl(WechatUserRequest wechatUserRequest, Config config, bool forcedToRefresh)
        {
            var url = Config.UserUrl + WebUtil.GetGeneralAccessToken(config, forcedToRefresh);
            if (!string.IsNullOrWhiteSpace(wechatUserRequest.NextOpenid))
            {
                url = url + "&next_openid=" + wechatUserRequest.NextOpenid;
            }
            return url;
        }

        /// <summary>
        /// 获取发送模版信息的URL
        /// </summary>
        /// <param name="configData">configData</param>
        /// <param name="forcedToRefresh">强制刷新缓存</param>
        /// <returns>发送模版信息的URL</returns>
        public static string GetTemplateUrl(Config configData, bool forcedToRefresh)
        {
            return Config.SendTemplateMsgUrl + WebUtil.GetGeneralAccessToken(configData, forcedToRefresh);
        }

        /// <summary>
        /// JsApiUrl
        /// </summary>
        /// <param name="config">config</param>
        /// <param name="forcedToRefresh">forcedToRefresh</param>
        /// <returns>url</returns>
        public static string GetJsapiTicketUrl(Config config, bool forcedToRefresh)
        {
            return string.Format(Config.JsApiUrl, WebUtil.GetGeneralAccessToken(config, forcedToRefresh));
        }

        /// <summary>
        ///  生成config信息
        /// </summary>
        /// <param name="ticketStr">ticketStr</param>
        /// <param name="url">url</param>
        /// <param name="config">config</param>
        /// <returns>TokenInfo</returns>
        public static TokenInfo GetConfigInfo(string ticketStr, string url, Config config)
        {
            string timestamp = Config.GetTimestamp();
            string noncestr = Config.GetNoncestr();
            StringBuilder stringBuilder = new StringBuilder();
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>
            {
                { "jsapi_ticket",ticketStr},
                { "noncestr",noncestr},
                { "timestamp",timestamp},
                { "url",url }
            };
            foreach (var item in dictionary)
            {
                stringBuilder.AppendFormat("{0}={1}&", item.Key, item.Value);
            }
            string geturl = stringBuilder.ToString().TrimEnd('&');
            var enText = Security.SHA1Encrypt(geturl);
            return new TokenInfo
            {
                AppId = config.GetAppId(),
                Signature = enText,
                NonceStr = noncestr,
                Timestamp = Convert.ToInt64(timestamp)
            };
        }
    }
}