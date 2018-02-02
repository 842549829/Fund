namespace Weixin.Model
{
    /// <summary>
    /// 退款结果
    /// </summary>
    public class RefundPayResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 微信订单号
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string OutRefundNo { get; set; }

        /// <summary>
        /// 微信退款单号	
        /// </summary>
        public string RefundId { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public int RefundFee { get; set; }
    }
}