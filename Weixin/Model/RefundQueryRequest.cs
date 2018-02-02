namespace Weixin.Model
{
    /// <summary>
    /// 退款查询（四选一）
    /// </summary>
    public class RefundQueryRequest
    {
        /// <summary>
        /// 商户退款订单号
        /// </summary>
        public string OutRefundNo { get; set; }

        /// <summary>
        /// 商户支付订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 微信退款订单号
        /// </summary>
        public string RefundId { get; set; }
    }
}