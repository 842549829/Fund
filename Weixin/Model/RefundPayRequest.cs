namespace Weixin.Model
{
    /// <summary>
    /// 退款通知
    /// </summary>
    public class RefundPayRequest
    {
        /// <summary>
        /// 微信订单号 (微信订单号 ,商户订单号 二选一)
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户订单号 (微信订单号 ,商户订单号 二选一)
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string OutRefundNo { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public int TotalFee { get; internal set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public int RefundFee { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        public string RefundDesc { get; set; }
    }
}