using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Notify.Controllers
{
    /// <summary>
    /// 支付宝通知
    /// </summary>
    public class AliPayNoticeController : BaseController
    {
        /// <summary>
        /// 支付通知
        /// </summary>
        /// <returns>返回结果</returns>
        public ContentResult PayNotice()
        {
            
            var sPara = GetRequestPost();
            if (sPara.Count > 0)
            {
                // 签名验证
                var flg = AliPay.Notify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"], new AliPay.Model.Config{ });
                if (flg)
                {
                    //交易状态
                    string trade_status = Request.Form["trade_status"];

                    //商户订单号
                    string out_trade_no = Request.Form["out_trade_no"];

                    //支付宝交易号
                    string trade_no = Request.Form["trade_no"];

                    //获取总金额
                    decimal total_fee = Convert.ToDecimal(Request.Form["total_fee"]);

                    //买家支付宝账号
                    //string accocunt = Request.Form["buyer_email"];

                    //买家支付宝账号ID
                    string accountID = Request.Form["buyer_id"];

                    if (trade_status == "TRADE_SUCCESS")
                    {
                        // 处理业务
                        return new ContentResult { Content = "success" };
                    }
                    else
                    {
                        // 支付失败的通知直接返回结果不再来通知
                        return new ContentResult { Content = "success" };
                    }
                }
                else//验证失败
                {
                    return new ContentResult { Content = "fail" };
                }
            }
            else
            {
                return new ContentResult { Content = "无通知参数" };
            }
        }

        /// <summary>
        /// 支付宝Sdk支付通知
        /// </summary>
        /// <returns>返回结果</returns>
        public ContentResult PaySdkNotice()
        {
            var sPara = GetRequestPost();
            if (sPara.Count > 0)
            {
                // 签名验证
                var flg = AliPay.Notify.VerifySdk(sPara, new AliPay.SdkPay.Config { });
                if (flg)
                {
                    //交易状态
                    string trade_status = Request.Form["trade_status"];

                    //商户订单号
                    string out_trade_no = Request.Form["out_trade_no"];

                    //支付宝交易号
                    string trade_no = Request.Form["trade_no"];

                    //获取总金额
                    decimal total_fee = Convert.ToDecimal(Request.Form["buyer_pay_amount"]);

                    // 订单金额（用于获取不到总金额的时候第二次验证）
                    decimal orderAmount = Convert.ToDecimal(Request.Form["total_amount"]);

                    //买家支付宝账号
                    string accocunt = Request.Form["buyer_logon_id"];

                    if (trade_status == "TRADE_SUCCESS")
                    {
                        try
                        {
                            if (total_fee <= 0)
                            {
                                total_fee = orderAmount;
                            }
                            // 处理业务
                            return new ContentResult { Content = "success" };
                        }
                        catch (Exception ex)
                        {
                            return new ContentResult { Content = "fail" };
                        }
                    }
                    else
                    {
                        // 支付失败的通知直接返回结果不再来通知
                        return new ContentResult { Content = "success" };
                    }
                }
                else//验证失败
                {
                    return new ContentResult { Content = "fail" };
                }
            }
            else
            {
                return new ContentResult { Content = "无通知参数" };
            }
        }

        /// <summary>
        /// 支付宝退分润通知
        /// </summary>
        /// <returns>返回结果</returns>
        public ContentResult RefundShareNotify()
        {
            SortedDictionary<string, string> sPara = this.GetRequestPost();
            if (sPara.Count > 0)//判断是否有带返回参数
            {
                //if (PayOrderRepository.VerifyByAlipay(sPara, Request.Form["notify_id"], Request.Form["sign"]))//验证成功
                if (true)
                {
                    //退款批次号
                    string batch_no = Request.Form["batch_no"];
                    //退款成功总数
                    string success_num = Request.Form["success_num"];
                    //解冻结果明细
                    string unfreezed_deta = Request.Form["unfreezed_deta"];
                    //格式：解冻结订单号^冻结订单号^解冻结金额^交易号^处理时间^状态^描述码


                    CreateRefundShareArray(Request.Form["result_details"]);
                    //回发到服务层处理
                    //string result = PayOrderRepository.RefundToShare(CreateRefundShareArray(Request.Form["result_details"]));

                    return new ContentResult { Content = "success" };
                }
                else//验证失败
                {
                    return new ContentResult { Content = "fail" };
                }
            }
            else
            {
                return new ContentResult { Content = "无通知参数" };
            }
        }

        /// <summary>
        /// 退支付异步通知
        /// </summary>
        /// <returns>结果</returns>
        public ContentResult RefundPayNotify()
        {
            SortedDictionary<string, string> sPara = this.GetRequestPost();
            if (sPara.Count > 0)//判断是否有带返回参数
            {
                //if (PayOrderRepository.VerifyByAlipay(sPara, Request.Form["notify_id"], Request.Form["sign"]))//验证成功
                if (true)
                {
                    //退款批次号
                    string batch_no = Request.Form["batch_no"];

                    CreateRefundPay(Request.Form["result_details"]);

                    //回发到服务层处理
                    //string result = PayOrderRepository.RefundToShare(CreateRefundPay(Request.Form["result_details"]));

                    return new ContentResult { Content = "success" };
                }
                else//验证失败
                {
                    return new ContentResult { Content = "fail" };
                }
            }
            else
            {
                return new ContentResult { Content = "无通知参数" };
            }
        }
    }
}