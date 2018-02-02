using System;

namespace Weixin.Model
{
    /// <summary>
    /// 转账结果
    /// </summary>
    public class TransfersResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public TransfersStatus IsSuccess { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string PartnerTradeNo { get; set; }

        /// <summary>
        /// 微信订单号
        /// </summary>
        public string TransactionId { get; set; }
     
        /// <summary>
        /// 微信支付成功时间
        /// </summary>
        public DateTime PaymentTime { get; set; }
    }
}