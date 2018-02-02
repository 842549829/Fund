using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Weixin.Model;

namespace Notify.Controllers
{
    /// <summary>
    /// 腾讯通知
    /// </summary>
    public class TenpayNoticeController : BaseController
    {
        /// <summary>
        /// 微信Sdk支付通知
        /// </summary>
        /// <returns>ContentResult</returns>
        public ContentResult PaySdkNotice()
        {
            try
            {
                var requestStr = GetRequestStr();
                var flg = Weixin.Notify.PayNotify(requestStr, new Weixin.Model.Config { /*微信配置信息*/ });
                if (flg.IsSuccess)
                {
                    // 处理业务
                    return new ContentResult { Content = GetResult(true, "OK") };
                }
                else
                {
                    return new ContentResult { Content = GetResult(false, flg.Message) };
                }
            }
            catch (Exception ex)
            {
                return new ContentResult { Content = GetResult(false, "微信支付通知异常") };
            }
        }

        /// <summary>
        /// 退款通知(联盟圈)
        /// </summary>
        /// <returns>返回结果</returns>
        public ContentResult RefundWapNotice()
        {

            var requestStr = GetRequestStr();
            var flg = Weixin.Notify.RefundPayNotify(requestStr, new Weixin.Model.Config { /*微信配置信息*/ });
            try
            {
                if (flg.IsSuccess == "SUCCESS")
                {
                    // 处理业务逻辑
                    return new ContentResult { Content = GetResult(true, "OK") };
                }
                else
                {
                    return new ContentResult { Content = GetResult(false, flg.Message) };
                }
            }
            catch (Exception ex)
            {
                return new ContentResult { Content = GetResult(false, "微信退款通知验签异常") };
            }
        }

        /// <summary>
        /// 微信主动推送的通知,包括ticket定时推送通知,公众号授权、取消授权、更新授权通知
        /// </summary>
        /// <returns>结果</returns>
        public ContentResult TicketNotify()
        {
            try
            {
                var requestStr = GetRequestStr();
                PushResult res = Weixin.Notify.ReceivePushMessage(requestStr, new ConfigData());
                if (res.IsSucceed)
                {
                    return new ContentResult { Content = "success" };
                }
            }
            catch (Exception)
            {
                return new ContentResult { Content = "fail" };
            }
            return new ContentResult { Content = "success" };
        }
    }
}