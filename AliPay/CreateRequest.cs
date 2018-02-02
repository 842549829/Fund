using System;
using System.Collections.Generic;
using System.Xml;
using AliPay.ErrorCodeTranslation;
using AliPay.Model;
using AliPay.SdkPay;
using AliPay.SdkPay.Domain;
using AliPay.SdkPay.Parser;
using AliPay.SdkPay.Request;
using AliPay.SdkPay.Response;
using Config = AliPay.Model.Config;

namespace AliPay
{
    /// <summary>
    /// CreateRequest
    /// 航旅版 :加密参数需要Pid Key 
    /// 蚂蚁金服 :全部
    /// </summary>	
    public class CreateRequest
    {
        /// <summary>
        /// 创建PC支付链接串(航旅版)
        /// </summary>
        /// <param name="directInfo">支付信息</param>
        /// <param name="config">config</param>
        /// <returns>请求连接</returns>
        public static string CreateDirectGetPcPayByUser(DirectInfo directInfo, Config config)
        {
            var partner = config.GetPid();
            Submit submit = new Submit(config);
            var sParaTemp = new SortedDictionary<string, string>
            {
                {"service", "create_direct_pay_by_user"},
                {"partner", partner},
                {"_input_charset", Config.InputCharset.ToLower()},
                {"payment_type", "1"},
                {"notify_url", directInfo.Notify},
                {"return_url", directInfo.Return},
                {"out_trade_no", directInfo.OutTradeNo},
                {"subject", directInfo.Subject},
                {"total_fee", directInfo.TotalFee.ToString("0.00")},
                {"show_url", directInfo.ShowUrl},
                {"seller_email", Config.SellerEmail},
            };
            string responseResult = submit.BuildRequest(sParaTemp);
            return responseResult;
        }

        /// <summary>
        /// 创建Wap支付链接串(航旅版)
        /// </summary>
        /// <param name="directInfo">支付信息</param>
        /// <param name="config">config</param>
        /// <returns>请求连接</returns>
        public static string CreateDirectWapPayByUser(DirectInfo directInfo, Config config)
        {
            var partner = config.GetPid();
            Submit submit = new Submit(config);
            var sParaTemp = new SortedDictionary<string, string>
            {
                {"partner", partner},
                {"seller_id", partner},
                {"_input_charset", Config.InputCharset.ToLower()},
                {"service", "alipay.wap.create.direct.pay.by.user"},
                {"payment_type", "1"},
                {"notify_url", directInfo.Notify},
                {"return_url", directInfo.Return},
                {"out_trade_no", directInfo.OutTradeNo},
                {"subject", directInfo.Subject},
                {"total_fee", directInfo.TotalFee.ToString("0.00")},
                {"show_url", directInfo.ShowUrl},
                {"body", directInfo.ExtraCommonParam}
            };
            string responseResult = submit.BuildRequest(sParaTemp, "post", "确认");
            return responseResult;
        }

        /// <summary>
        /// 创建Wap支付链接串(航旅版)
        /// </summary>
        /// <param name="directInfo">支付信息</param>
        /// <param name="config">config</param>
        /// <returns>请求连接</returns>
        public static string CreateDirectGetWapPayByUser(DirectInfo directInfo, Config config)
        {
            var partner = config.GetPid();
            Submit submit = new Submit(config);
            var sParaTemp = new SortedDictionary<string, string>
            {
                {"partner", partner},
                {"seller_id", partner},
                {"_input_charset", Config.InputCharset.ToLower()},
                {"service", "alipay.wap.create.direct.pay.by.user"},
                {"payment_type", "1"},
                {"notify_url", directInfo.Notify},
                {"return_url", directInfo.Return},
                {"out_trade_no", directInfo.OutTradeNo},
                {"subject", directInfo.Subject},
                {"total_fee", directInfo.TotalFee.ToString("0.00")},
                {"show_url", directInfo.ShowUrl},
                {"body", directInfo.ExtraCommonParam},
                {"app_pay","Y"} // 唤起支付宝客户端支付
            };
            string responseResult = submit.BuildRequest(sParaTemp);
            return responseResult;
        }

        /// <summary>
        /// 创建Sdk支付链接串(蚂蚁金服)
        /// </summary>
        /// <param name="directInfo">支付信息</param>
        /// <param name="config">config</param>
        /// <returns>请求连接</returns>
        public static string CreateDirectSdkPayByUser(DirectInfo directInfo, SdkPay.Config config)
        {
            const string url = SdkPay.Config.ServerUrl;
            string appId = config.GetAppId();
            string privateKeyPem = config.GetPrivateKeyPem();
            const string format = SdkPay.Config.Format;
            const string version = SdkPay.Config.Version;
            const string signType = SdkPay.Config.SignType;
            string publicKeyPem = config.GetPublicKeyPem();
            const string charset = SdkPay.Config.Charset;
            const string productCode = SdkPay.Config.ProductCode;
            string pid = config.GetPid();
            IAopClient client = new DefaultAopClient(url, appId, privateKeyPem, format, version, signType, publicKeyPem, charset, false);
            AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest();
            if (!string.IsNullOrWhiteSpace(directInfo.Notify))
            {
                request.SetNotifyUrl(directInfo.Notify);
            }
            if (!string.IsNullOrWhiteSpace(directInfo.Return))
            {
                request.SetReturnUrl(directInfo.Return);
            }
            AlipayTradeAppPayModel model = new AlipayTradeAppPayModel
            {
                Body = directInfo.ExtraCommonParam,
                Subject = directInfo.Subject,
                TotalAmount = directInfo.TotalFee.ToString("0.00"),
                ProductCode = productCode,
                OutTradeNo = directInfo.OutTradeNo,
                TimeoutExpress = "30m",
                SellerId = pid
            };
            request.SetBizModel(model);
            AlipayTradeAppPayResponse response = client.SdkExecute(request);
            var body = response.Body;
            return body;
        }

        /// <summary>
        /// 分润请求(航旅版)
        /// </summary>
        /// <param name="billInfo">分信息</param>
        /// <param name="requestStr">支付请求原始信息</param>
        /// <param name="responseStr">支付返回原始信息</param>
        /// <param name="config">config</param>
        /// <returns>成功?success:其他错误信息</returns>
        public static string CreateDistributeRoyalty(BillInfo billInfo, out string requestStr, out string responseStr, Config config)
        {
            Submit submit = new Submit(config);
            var royaltyParameters = Core.ToRoyaltyParameters(billInfo.RoyaltyParameters);
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>
            {
                {"partner", config.GetPid()},
                {"_input_charset", Config.InputCharset.ToLower()},
                {"service", "distribute_royalty"},
                {"out_bill_no", billInfo.OutBillNo},
                {"out_trade_no", string.Empty},
                {"trade_no", billInfo.TradeNo},
                {"royalty_parameters", royaltyParameters},
                {"royalty_type", "10"}
            };
            responseStr = submit.BuildRequest(sParaTemp, out requestStr);
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(responseStr);
                var selectSingleNode = xmlDoc.SelectSingleNode("/alipay/is_success");
                if (selectSingleNode != null)
                {
                    string strXmlResponse = selectSingleNode.InnerText;
                    if (strXmlResponse == "T")
                    {
                        return "success";
                    }
                    else
                    {
                        var singleNode = xmlDoc.SelectSingleNode("/alipay/error");
                        if (singleNode != null)
                        {
                            return singleNode.InnerText.TranslationZhCn<DistributeErrorCode>();
                        }
                        else
                        {
                            return "/alipay/error节点为空";
                        }
                    }
                }
                else
                {
                    return "/alipay/is_success节点为空";
                }
            }
            catch (Exception exp)
            {
                return exp.Message;
            }
        }

        /// <summary>
        /// 支付宝支付查询(蚂蚁金服)
        /// </summary>
        /// <param name="alipayTradeQueryInfo">查询信息</param>
        /// <param name="config">config</param>
        /// <returns>结果</returns>
        public static string QueryPayTrade(AlipayTradeQueryInfo alipayTradeQueryInfo, SdkPay.Config config)
        {
            const string url = SdkPay.Config.ServerUrl;
            string appId = config.GetAppId();
            string rivateKeyPem = config.GetPrivateKeyPem();
            const string format = SdkPay.Config.Format;
            const string version = SdkPay.Config.Version;
            const string signType = SdkPay.Config.SignType;
            string publicKeyPem = config.GetPublicKeyPemAliPay();
            const string charset = SdkPay.Config.Charset;
            IAopClient client = new DefaultAopClient(url, appId, rivateKeyPem, format, version, signType, publicKeyPem, charset, false);
            AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
            AlipayTradeAppPayQueryModel model = new AlipayTradeAppPayQueryModel
            {
                out_trade_no = alipayTradeQueryInfo.OutTradeNo,
                trade_no = alipayTradeQueryInfo.TradeNo
            };
            request.SetBizModel(model);
            AlipayTradeQueryResponse response = client.Execute(request);
            var body = response.Body;
            return body;
        }

        /// <summary>
        /// 支付宝支付查询(蚂蚁金服)
        /// </summary>
        /// <param name="alipayTradeQueryInfo">查询信息</param>
        /// <param name="config">config</param>
        /// <returns>结果</returns>
        public static AlipayTradeQueryResult QueryPayTradeResult(AlipayTradeQueryInfo alipayTradeQueryInfo, SdkPay.Config config)
        {
            var body = QueryPayTrade(alipayTradeQueryInfo, config);
            var parser = new AopJsonParser<QueryPayTrade>();
            var queryPayTrade = parser.Parse(body, SdkPay.Config.Charset);
            var alipayTradeQueryResult = new AlipayTradeQueryResult
            {
                IsSuccess = queryPayTrade.Code == "10000" && (queryPayTrade.trade_status == "TRADE_SUCCESS" || queryPayTrade.trade_status == "TRADE_FINISHED")
            };
            if (alipayTradeQueryResult.IsSuccess)
            {
                alipayTradeQueryResult.Buyer = queryPayTrade.buyer_logon_id;
                alipayTradeQueryResult.PayAmount = Convert.ToDecimal(queryPayTrade.total_amount);
                alipayTradeQueryResult.PanyInterfaceNo = queryPayTrade.trade_no;
                alipayTradeQueryResult.TradeNo = queryPayTrade.out_trade_no;
            }
            return alipayTradeQueryResult;
        }

        /// <summary>
        /// 交易明细查询(航旅版)
        /// </summary>
        /// <param name="outTradeNo">商户订单号</param>
        /// <param name="config">config</param>
        /// <returns>结果</returns>
        public static List<AccountPageQueryResut> QueryAccountPage(string outTradeNo, Config config)
        {
            Submit submit = new Submit(config);
            string requestStr;
            List<AccountPageQueryResut> queryList = new List<AccountPageQueryResut>();
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>
            {
                {"partner", config.GetPid()},
                {"_input_charset", Config.InputCharset},
                {"service", "account.page.query"},
                {"page_no", "1"},
                {"merchant_out_order_no", outTradeNo}
            };
            string sHtmlText = submit.BuildRequest(sParaTemp, out requestStr);
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(sHtmlText);
                XmlNodeList nodelist = xmlDoc.SelectNodes("/alipay/response/account_page_query_result/account_log_list/AccountQueryAccountLogVO");
                if (nodelist != null)
                {
                    foreach (XmlNode item in nodelist)
                    {
                        AccountPageQueryResut model = new AccountPageQueryResut();
                        model.balance = item.SelectSingleNode("balance")?.InnerText;
                        model.buyer_account = item.SelectSingleNode("buyer_account")?.InnerText;
                        model.deposit_bank_no = item.SelectSingleNode("deposit_bank_no")?.InnerText;
                        model.goods_title = item.SelectSingleNode("goods_title")?.InnerText;
                        model.income = item.SelectSingleNode("income")?.InnerText;
                        model.iw_account_log_id = item.SelectSingleNode("iw_account_log_id")?.InnerText;
                        model.memo = item.SelectSingleNode("memo")?.InnerText;
                        model.merchant_out_order_no = item.SelectSingleNode("merchant_out_order_no")?.InnerText;
                        model.outcome = item.SelectSingleNode("outcome")?.InnerText;
                        model.partner_id = item.SelectSingleNode("partner_id")?.InnerText;
                        model.rate = item.SelectSingleNode("rate")?.InnerText;
                        model.seller_account = item.SelectSingleNode("seller_account")?.InnerText;
                        model.seller_fullname = item.SelectSingleNode("seller_fullname")?.InnerText;
                        model.service_fee = item.SelectSingleNode("service_fee")?.InnerText;
                        model.service_fee_ratio = item.SelectSingleNode("service_fee_ratio")?.InnerText;
                        model.sign_product_name = item.SelectSingleNode("sign_product_name")?.InnerText;
                        model.sub_trans_code_msg = item.SelectSingleNode("sub_trans_code_msg")?.InnerText;
                        model.total_fee = item.SelectSingleNode("total_fee")?.InnerText;
                        model.trade_no = item.SelectSingleNode("trade_no")?.InnerText;
                        model.trade_refund_amount = item.SelectSingleNode("trade_refund_amount")?.InnerText;
                        model.trans_code_msg = item.SelectSingleNode("trans_code_msg")?.InnerText;
                        model.trans_date = item.SelectSingleNode("trans_date")?.InnerText;
                        queryList.Add(model);
                    }
                }
            }
            catch (Exception)
            {
                return new List<AccountPageQueryResut>();
            }
            return queryList;
        }

        /// <summary>
        /// 生成支付圈签署链接(航旅版)
        /// </summary>
        /// <param name="account">签署账户</param>
        /// <param name="config">config</param>
        /// <returns>结果</returns>
        public static string SignProtocolWithPartner(string account, Config config)
        {
            Submit submit = new Submit(config);
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>
            {
                {"partner", config.GetPid()},
                {"_input_charset", Config.InputCharset},
                {"service", "sign_protocol_with_partner"},
                {"email", account}
            };
            string responseResult = submit.BuildLinkUrl(sParaTemp);
            return responseResult;
        }

        /// <summary>
        /// 支付宝支付圈签约查询(航旅版)
        /// </summary>
        /// <param name="account">签署账户</param>
        /// <param name="config">config</param>
        /// <returns>分润处理结果</returns>
        public static string CreateQueryCustomerProtocol(string account, Config config)
        {
            Submit submit = new Submit(config);
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>
            {
                {"partner", config.GetPid()},
                {"_input_charset", Config.InputCharset},
                {"service", "query_customer_protocol"},
                {"biz_type", "10004"},
                {"user_email", account}
            };
            string requestStr;
            string sHtmlText = submit.BuildRequest(sParaTemp, out requestStr);
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(sHtmlText);
                var selectSingleNode = xmlDoc.SelectSingleNode("/alipay/is_success");
                if (selectSingleNode != null)
                {
                    string strXmlResponse = selectSingleNode.InnerText;
                    if (strXmlResponse == "T")
                    {
                        return "success";
                    }
                    else
                    {
                        var singleNode = xmlDoc.SelectSingleNode("/alipay/error");
                        if (singleNode != null)
                        {
                            return singleNode.InnerText;
                        }
                        else
                        {
                            return "fail";
                        }
                    }
                }
                else
                {
                    return "fail";
                }
            }
            catch (Exception exp)
            {
                return exp.Message;
            }
        }

        /// <summary>
        /// 支付宝单笔转账(蚂蚁金服)
        /// </summary>
        /// <param name="fundTransToaccountTransferInfo">转账信息</param>
        /// <param name="config">config</param>
        /// <returns>转账结果</returns>
        public static FundTransToaccountTransferInfoResult FundTransToAccountTransfer(FundTransToaccountTransferInfo fundTransToaccountTransferInfo, SdkPay.Config config)
        {
            const string url = SdkPay.Config.ServerUrl;
            string appId = config.GetAppId();
            string privateKeyPem = config.GetPrivateKeyPem();
            const string format = SdkPay.Config.Format;
            const string version = SdkPay.Config.Version;
            const string signType = SdkPay.Config.SignType;
            string publicKeyPem = config.GetPublicKeyPemAliPay();
            const string charset = SdkPay.Config.Charset;
            IAopClient client = new DefaultAopClient(url, appId, privateKeyPem, format, version, signType, publicKeyPem, charset, false);
            var request = new AlipayFundTransToaccountTransferRequest();
            AlipayFundTransToaccountTransferModel model = new AlipayFundTransToaccountTransferModel
            {
                OutBizNo = fundTransToaccountTransferInfo.OutBizNo,
                PayeeAccount = fundTransToaccountTransferInfo.PayeeAccount,
                Amount = fundTransToaccountTransferInfo.Amount,
                PayeeRealName = fundTransToaccountTransferInfo.PayeeRealName,
                Remark = fundTransToaccountTransferInfo.Remark
            };
            request.SetBizModel(model);
            AlipayFundTransToaccountTransferResponse response = client.Execute(request);
            FundTransToaccountTransferInfoResult result = new FundTransToaccountTransferInfoResult();
            result.Body = response.Body;
            if (response.Code == "10000")
            {
                result.IsSuccess = true;
                result.Message = response.Msg;
                result.OrderId = response.OrderId;
                result.PayDate = Convert.ToDateTime(response.PayDate);
            }
            else
            {
                result.IsSuccess = false;
                result.Message = $"{response.Msg}-{response.SubMsg}";
            }
            return result;
        }

        /// <summary>
        /// 支付宝单笔转账查询结果(蚂蚁金服)
        /// </summary>
        /// <param name="fundTransOrderQueryInfo">查询信息</param>
        /// <param name="config">config</param>
        /// <returns>结果</returns>
        public static FundTransOrderQueryResult FundTransOrderQuery(FundTransOrderQueryInfo fundTransOrderQueryInfo, SdkPay.Config config)
        {
            const string url = SdkPay.Config.ServerUrl;
            string appId = config.GetAppId();
            string privateKeyPem = config.GetPrivateKeyPem();
            const string format = SdkPay.Config.Format;
            const string version = SdkPay.Config.Version;
            const string signType = SdkPay.Config.SignType;
            string publicKeyPem = config.GetPublicKeyPemAliPay();
            const string charset = SdkPay.Config.Charset;
            IAopClient client = new DefaultAopClient(url, appId, privateKeyPem, format, version, signType, publicKeyPem, charset, false);
            AlipayFundTransOrderQueryRequest request = new AlipayFundTransOrderQueryRequest();
            AlipayFundTransOrderQueryModel model = new AlipayFundTransOrderQueryModel
            {
                OrderId = fundTransOrderQueryInfo.OrderId,
                OutBizNo = fundTransOrderQueryInfo.OutBizNo
            };
            request.SetBizModel(model);
            AlipayFundTransOrderQueryResponse response = client.Execute(request);
            FundTransOrderQueryResult result = new FundTransOrderQueryResult();
            if (response.Code == "10000")
            {
                if (response.Status.ToUpper() == "SUCCESS")
                {
                    result.IsSuccess = true;
                    result.Message = response.Status.ToUpper();
                    result.OrderId = response.OrderId;
                    result.PayDate = Convert.ToDateTime(response.PayDate);
                    result.OutBizNo = response.OutBizNo;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = response.Status.ToUpper();
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = response.SubMsg ?? response.Msg;
            }
            return result;
        }

        /// <summary>
        /// 创建请求code的URL(蚂蚁金服)
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="config">配置</param>
        /// <returns>结果</returns>
        public static string GetOpenidAndAccessTokenUrl(OpenidAndAccessTokenUrlInfo request, SdkPay.Config config)
        {
            string scope = "auth_base";
            return $"https://openauth.alipay.com/oauth2/publicAppAuthorize.htm?app_id={config.GetAppId()}&scope={scope}&redirect_uri={request.RedirectUri}&state={request.State}";
        }

        /// <summary>
        /// 获取openId(蚂蚁金服)
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="config">配置</param>
        /// <returns>结果</returns>
        public static string GetOpenidFromCode(OpenidFromCodeRequestInfo request, SdkPay.Config config)
        {
            const string url = SdkPay.Config.ServerUrl;
            string appId = config.GetAppId();
            string privateKeyPem = config.GetPrivateKeyPem();
            const string format = SdkPay.Config.Format;
            const string signType = SdkPay.Config.SignType;
            string publicKeyPem = config.GetPublicKeyPemAliPay();
            const string charset = SdkPay.Config.Charset;
            IAopClient client = new DefaultAopClient(url, appId, privateKeyPem, format, charset, signType, publicKeyPem);
            AlipaySystemOauthTokenRequest alipaySystemOauthTokenRequest = new AlipaySystemOauthTokenRequest
            {
                GrantType = "authorization_code",
                Code = request.Code
            };
            AlipaySystemOauthTokenResponse response = client.Execute(alipaySystemOauthTokenRequest);
            return response?.UserId;
        }

        /// <summary>
        /// 退款(航旅版)
        /// </summary>
        /// <param name="request">退款请求参数</param>
        /// <returns>退款申请结果</returns>
        public static RefundResult Refund(RefundRequest request)
        {
            RefundResult refundResult = new RefundResult();
            var config = request.Config;
            Submit submit = new Submit(config);
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>
            {
                {"partner", config.GetPid()},
                {"_input_charset", Config.InputCharset.ToLower()},
                {"service", "refund_fastpay_by_platform_nopwd"},
                {"notify_url", request.NotifyUrl},
                {"batch_no", request.BatchNo},
                {"refund_date", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")},
                {"batch_num", request.BatchNum.ToString()},
                {"detail_data", request.ToString()}
            };

            string requestStr;
            var responseStr = submit.BuildRequest(sParaTemp, out requestStr);
            refundResult.RequestStr = requestStr;
            refundResult.ResponseStr = responseStr;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(responseStr);
                var selectSingleNode = xmlDoc.SelectSingleNode("/alipay/is_success");
                if (selectSingleNode != null)
                {
                    string strXmlResponse = selectSingleNode.InnerText;
                    refundResult.IsSuccess = strXmlResponse;
                    if (strXmlResponse == "T")
                    {
                        return refundResult;
                    }
                    else
                    {
                        var singleNode = xmlDoc.SelectSingleNode("/alipay/error");
                        if (singleNode != null)
                        {
                            refundResult.Message = singleNode.InnerText;
                            return refundResult;
                        }
                        else
                        {
                            refundResult.Message = "/alipay/error节点为空";
                            return refundResult;
                        }
                    }
                }
                else
                {
                    refundResult.Message = "/alipay/is_success节点为空";
                    return refundResult;
                }
            }
            catch (Exception exp)
            {
                refundResult.Message = exp.Message;
                return refundResult;
            }
        }

        /// <summary>
        /// 退款查询(航旅版)
        /// </summary>
        /// <param name="request">请求查询</param>
        /// <returns>结果</returns>
        public static RefundQueryResult QueryRefund(RefundQueryRequest request)
        {
            RefundQueryResult refundQueryResult = new RefundQueryResult();
            var config = request.Config;
            Submit submit = new Submit(config);
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>
            {
                {"partner", config.GetPid()},
                {"_input_charset", Config.InputCharset.ToLower()},
                {"service", "refund_fastpay_query"}
            };
            if (!string.IsNullOrWhiteSpace(request.BatchNo))
            {
                sParaTemp.Add("batch_no", request.BatchNo);
            }
            else
            {
                sParaTemp.Add("trade_no", request.TradeNo);
            }
            string requestStr;
            var responseStr = submit.BuildRequest(sParaTemp, out requestStr);
            refundQueryResult.RequestStr = requestStr;
            refundQueryResult.ResponseStr = responseStr;

            var dic = Core.UrlToData(responseStr);
            try
            {
                if (dic.ContainsKey("is_success"))
                {
                    string isSuccess = dic["is_success"];
                    if (isSuccess == "T")
                    {
                        if (dic.ContainsKey("result_details"))
                        {
                            refundQueryResult.IsSuccess = true;
                            refundQueryResult.ResultDetails = new RefundResultDetails(dic["result_details"]);
                        }
                        else
                        {
                            refundQueryResult.Message = "result_details参数为空";
                        }
                        return refundQueryResult;
                    }
                    else
                    {
                        if (dic.ContainsKey("error"))
                        {
                            refundQueryResult.Message = dic["error"];
                            return refundQueryResult;
                        }
                        else
                        {
                            refundQueryResult.Message = "error参数为空";
                            return refundQueryResult;
                        }
                    }
                }
                else
                {
                    refundQueryResult.Message = "is_success参数为空";
                    return refundQueryResult;
                }
            }
            catch (Exception exp)
            {
                refundQueryResult.Message = exp.Message;
                return refundQueryResult;
            }
        }
    }
}