using System;
using Newtonsoft.Json;
using System.Linq;
using Weixin.Model;
using Weixin.Util;

namespace Weixin
{
    /// <summary>
    /// 创建请求信息
    /// </summary>
    public class CreateRequest
    {
        #region web微信公众号
        /// <summary>
        /// 创建支付信息(安卓 IOS)
        /// </summary>
        /// <param name="request">request</param>
        /// <param name="config">配置</param>
        /// <returns>结果</returns>
        public static UnifiedOrderResult CreateDirectPayByUser(UnifiedOrderRequest request, Config config)
        {
            // 统一下单
            var requestStr = TenpayUtil.GetUnifiedOrderXml(request, config);
            var response = HttpHelp.PostXmlToUrl(Config.UnifiedPayUrl, requestStr);
            var unifiedOrderResult = TenpayUtil.ConvertToUnifiedOrderResult(response);
            // 调起支付
            var parameters = TenpayUtil.GetPaySign(unifiedOrderResult, config);
            var result = TenpayUtil.CreatePayParameters(parameters, unifiedOrderResult);
            return result;
        }

        /// <summary>
        /// 创建支付信息(网页公众号)
        /// </summary>
        /// <param name="request">request</param>
        /// <param name="config">配置</param>
        /// <returns>结果</returns>
        public static WapUnifiedOrderResult CreateDirectWapPayByUser(WapUnifiedOrderRequest request, Config config)
        {
            // 统一下单
            var requestStr = TenpayUtil.GetUnifiedWapOrderXml(request, config);
            var response = HttpHelp.PostXmlToUrl(Config.UnifiedPayUrl, requestStr);
            var unifiedOrderResult = TenpayUtil.ConvertToWapUnifiedOrderResult(response);
            // 调起支付
            var parameters = TenpayUtil.GetWapPaySign(unifiedOrderResult, config);
            var result = TenpayUtil.CreatePayParameters(parameters, unifiedOrderResult);
            return result;
        }

        /// <summary>
        /// 订单查询接口
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <param name="config">配置</param>
        /// <returns>结果</returns>
        public static OrderQueryResult OrderQuery(OrderQueryRequest request, Config config)
        {
            var requestStr = TenpayUtil.GetOrderqueryRequestXml(request, config);
            var response = HttpHelp.PostXmlToUrl(Config.OrderQueryUrl, requestStr);
            var result = TenpayUtil.ConvertToOrderqueryResult(response);
            return result;
        }

        /// <summary>
        /// 退款信息
        /// </summary>
        /// <param name="request">退款参数</param>
        /// <param name="config">配置</param>
        /// <returns>退款结果</returns>
        public static RefundPayResult RefundPay(RefundPayRequest request, Config config)
        {
            var result = new RefundPayResult();
            var orderqueryResult = OrderQuery(new OrderQueryRequest
            {
                OutTradeNo = request.OutTradeNo,
                TransactionId = request.TransactionId
            }, config);
            if (orderqueryResult.TradeState == "SUCCESS" || orderqueryResult.TradeState == "REFUND")
            {
                if (request.RefundFee > orderqueryResult.TotalFee)
                {
                    result.IsSuccess = false;
                    result.Message = "退款金额大于总订单金额";
                    return result;
                }
                else
                {
                    // 转入退款的订单先去查询退款金额够不够
                    if (orderqueryResult.TradeState == "REFUND")
                    {
                        var refundQueryResult = RefundPayQuery(new RefundQueryRequest
                        {
                            OutTradeNo = request.OutTradeNo
                        }, config);
                        if (refundQueryResult.IsSuccess)
                        {
                            // 已退款金额
                            var oldRefundAmount = refundQueryResult.Details.Sum(item => item.RefundFee);
                            if (request.RefundFee > orderqueryResult.TotalFee - oldRefundAmount)
                            {
                                result.IsSuccess = false;
                                result.Message = "退款金额大于总订单金额";
                                return result;
                            }

                            var refundOrder = refundQueryResult.Details.FirstOrDefault(item => item.OutRefundNo == request.OutRefundNo);
                            if (refundOrder != null)
                            {
                                if (refundOrder.RefundStatus == "SUCCESS" || refundOrder.RefundStatus == "PROCESSING")
                                {
                                    result.IsSuccess = true;
                                    result.TransactionId = orderqueryResult.TransactionId;
                                    result.OutTradeNo = request.OutTradeNo;
                                    result.OutRefundNo = request.OutRefundNo;
                                    result.RefundId = refundOrder.RefundId;
                                    result.RefundFee = refundOrder.RefundFee;
                                    return result;
                                }
                            }
                        }
                        else
                        {
                            result.Message = "退款查询失败";
                            result.IsSuccess = false;
                            return result;
                        }
                    }
                    request.TotalFee = orderqueryResult.TotalFee;
                    var requestStr = TenpayUtil.GetRefundPayRequestXml(request, config);
                    var response = HttpHelp.PostXmlToUrl(Config.RefundPayUrl, requestStr, config.GetRefundCert(), config.GetPid());
                    result = TenpayUtil.ConvertToRefundPayResult(response);
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = orderqueryResult.Message;
            }
            return result;
        }

        /// <summary>
        /// 退款查询
        /// </summary>
        /// <param name="request">查询信息</param>
        /// <param name="config">配置</param>
        /// <returns>结果</returns>
        public static RefundQueryResult RefundPayQuery(RefundQueryRequest request, Config config)
        {
            var requestStr = TenpayUtil.GetRefundQueryRequestXml(request, config);
            var response = HttpHelp.PostXmlToUrl(Config.RefundQueryUrl, requestStr);
            var result = TenpayUtil.ConvertToRefundQueryResult(response);
            return result;
        }

        /// <summary>
        /// 创建请求code的URL
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="config">配置</param>
        /// <returns>结果</returns>
        public static string GetOpenidAndAccessTokenUrl(OpenidAndAccessTokenUrl request, Config config)
        {
            return TenpayUtil.GetOpenidAndAccessTokenUrl(request, config);
        }

        /// <summary>
        /// 获取openId
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="config">配置</param>
        /// <returns>结果</returns>
        public static string GetOpenidFromCode(OpenidFromCodeRequest request, Config config)
        {
            var url = TenpayUtil.GetOpenidFromCodeUrl(request, config);
            var data = HttpHelp.GetUrl(url);
            return data;
        }

        /// <summary>
        /// 微信转账
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="config">配置</param>
        /// <returns>结果</returns>
        public static TransfersResult Transfers(TransfersRequest request, Config config)
        {
            TransfersResult result = new TransfersResult();
            TransfersQueryRequest transfersQueryRequest = new TransfersQueryRequest
            {
                PartnerTradeNo = request.PartnerTradeNo
            };
            TransfersQueryResult transfersQueryResult = TransfersQuery(transfersQueryRequest, config);
            if (transfersQueryResult.Status == TransfersStatus.Success)
            {
                result.IsSuccess = TransfersStatus.Success;
                return result;
            }
            if (transfersQueryResult.Status == TransfersStatus.Processing)
            {
                result.IsSuccess = TransfersStatus.Processing;
                return result;
            }

            var requestStr = TenpayUtil.GetTransfersRequestXml(request, config);
            var response = HttpHelp.PostXmlToUrl(Config.TransfersUrl, requestStr, config.GetTransfersCertPath(), config.GetPid());
            result = TenpayUtil.ConvertToTransfersResult(response);
            return result;
        }

        /// <summary>
        /// 微信零钱转账查询
        /// </summary>
        /// <param name="request">查询信息</param>
        /// <param name="config">配置</param>
        /// <returns>结果</returns>
        public static TransfersQueryResult TransfersQuery(TransfersQueryRequest request, Config config)
        {
            var requestStr = TenpayUtil.GetTransfersQueryRequestXml(request, config);
            var response = HttpHelp.PostXmlToUrl(Config.TransferinfoUrl, requestStr, config.GetTransfersQueryCertPath(), config.GetPid());
            var result = TenpayUtil.ConvertToTransfersQueryResult(response);
            return result;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="wechatUserRequest">wechatUserRequest</param>
        /// <param name="config">config</param>
        /// <returns>关注的openid</returns>
        public static WechatUser GetWechatUser(WechatUserRequest wechatUserRequest, Config config)
        {
            string url = TenpayUtil.GetUserUrl(wechatUserRequest, config, false);
            var strSendRes = HttpHelp.GetUrl(url);
            WechatUser wechatUser = JsonConvert.DeserializeObject<WechatUser>(strSendRes);
            if (wechatUser.Errcode == "40001")
            {
                string url1 = TenpayUtil.GetUserUrl(wechatUserRequest, config, true);
                var strSendRes1 = HttpHelp.GetUrl(url1);
                wechatUser = JsonConvert.DeserializeObject<WechatUser>(strSendRes1);
            }
            return wechatUser;
        }

        /// <summary>
        /// 发送模版消息
        /// </summary>
        /// <param name="templateMsg">发送消息内容</param>
        /// <param name="config">配置</param>
        /// <returns>结果</returns>
        public static Result SendTemplateMessage<T>(TemplateMsg<T> templateMsg, Config config)
        {
            Result res = new Result();
            var configData = config;
            string url = TenpayUtil.GetTemplateUrl(configData, false);
            var strSendRes = HttpHelp.PostXmlToUrl(url, JsonConvert.SerializeObject(templateMsg));
            TemplateMsgResult sendRes = JsonConvert.DeserializeObject<TemplateMsgResult>(strSendRes);
            res.IsSucceed = sendRes.ErrorMsg.Trim().ToLower() == "ok";
            res.Message = sendRes.ErrorMsg;
            if (sendRes.ErrorCode == 40001)
            {
                string url1 = TenpayUtil.GetTemplateUrl(configData, true);
                var strSendRes1 = HttpHelp.PostXmlToUrl(url1, JsonConvert.SerializeObject(templateMsg));
                TemplateMsgResult sendRes1 = JsonConvert.DeserializeObject<TemplateMsgResult>(strSendRes1);
                res.IsSucceed = sendRes1.ErrorMsg.Trim().ToLower() == "ok";
                res.Message = sendRes1.ErrorMsg;
            }
            return res;
        }

        /// <summary>
        /// 获取分享参数信息
        /// </summary>
        /// <param name="url">分销Url</param>
        /// <param name="config">配置信息</param>
        /// <returns>结果</returns>
        public static TokenInfo GetWeiXinShareConfig(string url, Config config)
        {
            var jtCache = string.Empty; //Redis.GetRedis<string>(Config.JsapiTicket);
            if (string.IsNullOrEmpty(jtCache))
            {
                var jsapiTicketUrl = TenpayUtil.GetJsapiTicketUrl(config, false);
                var jsapiTicket = JsonConvert.DeserializeObject<JsapiTicket>(HttpHelp.GetUrl(jsapiTicketUrl));
                if (jsapiTicket.Errcode == 0)
                {
                    jtCache = jsapiTicket.Ticket;
                    //Redis.SetRedis(Config.JsapiTicket, jsapiTicket.ticket, DateTime.Now.AddSeconds(jsapiTicket.expires_in - 2));
                }
                else
                {
                    var jsapiTicketUrl1 = TenpayUtil.GetJsapiTicketUrl(config, true);
                    var jsapiTicket1 = JsonConvert.DeserializeObject<JsapiTicket>(HttpHelp.GetUrl(jsapiTicketUrl1));
                    jtCache = jsapiTicket1.Ticket;
                }
            }
            var info = TenpayUtil.GetConfigInfo(jtCache, url, config);
            return info;
        }

        /// <summary>
        /// 获取用户是否关注
        /// </summary>
        /// <param name="openId">openId</param>
        /// <param name="config">配置信息</param>
        /// <returns>是否关注</returns>
        public static IsFollow GetUserSubscribeInfo(string openId, Config config)
        {
            string accessToken = WebUtil.GetGeneralAccessToken(config, false);
            var userInfoObj = HttpHelp.GetUrl(string.Format(Config.UserInfoUrl, accessToken, openId));
            IsFollow sbscribeInfo = new IsFollow { Subscribe = -1 };
            if (userInfoObj.Contains("subscribe"))
            {
                JsonConvert.DeserializeObject<IsFollow>(userInfoObj);
            }
            return sbscribeInfo;
        }
        #endregion

        #region 微信开放平台
        /// <summary>
        /// 拉取component_access_token
        /// </summary>
        /// <param name="request">request</param>
        /// <param name="configData">configData</param>
        /// <returns>结果</returns>
        public static Api_component_token_result component_access_token(Api_component_token request, ConfigData configData)
        {
            var requestJson = JsonConvert.SerializeObject(request);
            var resultJson = HttpHelp.PostXmlToUrl(ConfigData.component_access_token_url, requestJson);
            Api_component_token_result result = JsonConvert.DeserializeObject<Api_component_token_result>(resultJson);
            if (!string.IsNullOrEmpty(configData.component_access_token_key) && result != null)
            {
                //Redis.SetRedis(request.rediseKey, result.component_access_token, DateTime.Now.AddMilliseconds(result.expires_in - 60), Config.UserRedisConnectString);
            }
            return result;
        }

        /// <summary>
        /// 拉取pre_auth_code
        /// </summary>
        /// <param name="request">request</param>
        /// <param name="configData">configData</param>
        /// <returns>结果</returns>
        public static Pre_auth_code_result pre_auth_code(Pre_auth_code request, ConfigData configData)
        {
            var requestJson = JsonConvert.SerializeObject(request);
            var url = ConfigData.pre_auth_code_url + "?component_access_token=" + request.component_access_token;
            var resultJson = HttpHelp.PostXmlToUrl(url, requestJson);
            Pre_auth_code_result result = JsonConvert.DeserializeObject<Pre_auth_code_result>(resultJson);
            //Redis.SetRedis(request.rediseKey, result.pre_auth_code, DateTime.Now.AddMilliseconds(result.expires_in - 60), Config.UserRedisConnectString);
            return result;
        }

        /// <summary>
        /// 创建授权地址
        /// </summary>
        /// <param name="redirect_ur">回调地址</param>
        /// <param name="configData">configData</param>
        /// <returns>授权地址</returns>
        public static string create_authorization_code_redirect_uri(string redirect_ur, ConfigData configData)
        {
            var component_appid = configData.appid;
            var code = pre_auth_code(new Pre_auth_code { component_appid = component_appid, component_access_token = "从redis中取" }, configData);
            string pre_auth_code_val = code.pre_auth_code;
            return $"{ConfigData.authorization_code_url}?component_appid={component_appid}&pre_auth_code={pre_auth_code_val}&redirect_uri={redirect_ur}";
        }

        /// <summary>
        /// 使用授权码换取公众号或小程序的接口调用凭据和授权信息
        /// </summary>
        /// <param name="authorization_code">授权code,会在授权成功时返回给第三方平台，详见第三方平台授权流程说明</param>
        /// <param name="configData">configData</param>
        /// <returns>授权信息</returns>
        public static authorizer_access_token_result authorizer_access_token(string authorization_code, ConfigData configData)
        {
            var component_access_token_model = component_access_token(null, null);
            string url = $"{ConfigData.authorization_code_redirect_url}?component_access_token={component_access_token_model.component_access_token}";
            var data = new { component_appid = configData.appid, authorization_code };
            var jsonData = HttpHelp.PostXmlToUrl(url, JsonConvert.SerializeObject(data));
            var authorizer_access_token_result_model = JsonConvert.DeserializeObject<authorizer_access_token_result>(jsonData);
            //string redisKey = configData.authorizer_access_token_key + authorizer_access_token_result_model.authorization_info.authorizer_appid;
            //Redis.SetRedis(redisKey,
            //                authorizer_access_token_result_model.authorization_info.authorizer_access_token,
            //                DateTime.Now.AddMilliseconds(authorizer_access_token_result_model.authorization_info.expires_in - 60),
            //                Config.UserRedisConnectString);
            //string refreshRedisKey = ConfigData.authorizer_refresh_token + authorizer_access_token_result_model.authorization_info.authorizer_appid;
            //Redis.SetRedis(refreshRedisKey, authorizer_access_token_result_model.authorization_info.authorizer_refresh_token, Config.UserRedisConnectString);
            return authorizer_access_token_result_model;
        }

        /// <summary>
        /// 刷新AccessToken
        /// </summary>
        /// <param name="authAppId">授权方的appid</param>
        /// <param name="configData">configData</param>
        /// <returns>AccessToken</returns>
        private static string RefreshAuthorizerAccessToken(string authAppId, ConfigData configData)
        {
            string redisKey = configData.authorizer_access_token_key + authAppId;
            string redisRefeshKey = configData.authorizer_refresh_token_key + authAppId;
            string refreshToken = ""; //Redis.GetRedis<string>(redisRefeshKey, Config.UserRedisConnectString);
            var component_access_token_model = component_access_token(null, configData);
            string refreshUrl = $"{ConfigData.authorization_access_token_refresh_url}component_access_token={component_access_token_model.component_access_token}";
            var data = new
            {
                component_appid = configData.appid,
                authorizer_appid = authAppId,
                authorizer_refresh_token = refreshToken,
            };
            var jsonData = HttpHelp.PostXmlToUrl(refreshUrl, JsonConvert.SerializeObject(data));
            authorizer_access_token_refresh_result refreshRes = JsonConvert.DeserializeObject<authorizer_access_token_refresh_result>(jsonData);
            if (refreshRes != null)
            {
                //Redis.SetRedis(redisKey, refreshRes.authorizer_access_token ?? string.Empty, Config.UserRedisConnectString);
                //Redis.SetRedis(redisRefeshKey, refreshRes.authorizer_refresh_token ?? string.Empty, Config.UserRedisConnectString);
                return refreshRes.authorizer_access_token;
            }
            return null;
        }

        /// <summary>
        /// 获取授权方的AccessToken
        /// </summary>
        /// <param name="authAppId">授权方appid</param>
        /// <param name="configData">configData</param>
        /// <returns>授权方的AccessToken</returns>
        public static string GetAuthorizerAccessToken(string authAppId, ConfigData configData)
        {
            return RefreshAuthorizerAccessToken(authAppId, null);
        }

        /// <summary>
        /// 获取授权方的帐号基本信息
        /// </summary>
        /// <param name="authorizer_appid">授权方appid</param>
        /// <param name="configData"></param>
        /// <returns>授权方的帐号基本信息</returns>
        public static api_get_authorizer_info_result get_authorizer_info(string authorizer_appid, ConfigData configData)
        {
            var component_access_token_val = component_access_token(null, configData);
            string url = $"{ConfigData.get_authorizer_info_url}?component_access_token={component_access_token_val.component_access_token}";
            var param = new
            {
                component_appid = configData.appid,
                authorizer_appid
            };
            var jsonData = HttpHelp.PostXmlToUrl(url, JsonConvert.SerializeObject(param));
            var api_get_authorizer_info_result = JsonConvert.DeserializeObject<api_get_authorizer_info_result>(jsonData);
            return api_get_authorizer_info_result;
        }
        #endregion
    }
}