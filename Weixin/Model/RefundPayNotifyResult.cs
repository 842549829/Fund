namespace Weixin.Model
{
    /// <summary>
    /// 退款通知结果
    /// </summary>
    public class RefundPayNotifyResult
    {
        /// <summary>
        /// 退款结果
        /// SUCCESS-退款成功
        /// CHANGE-退款异常
        /// REFUNDCLOSE—退款关闭
        /// </summary>
        public string IsSuccess { get; set; }

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
        /// 订单金额
        /// </summary>
        public int TotalFee { get; set; }

        /// <summary>
        /// 申请退款金额
        /// </summary>
        public int RefundFee { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public int SettlementRefundFee { get; set; }

        /// <summary>
        /// 退款成功时间
        /// </summary>
        public string SuccessTime { get; set; }

        /// <summary>
        /// 退款入账账户
        /// </summary>
        public string RefundRecvAccout { get; set; }

        /// <summary>
        /// 退款资金来源
        /// </summary>
        public string RefundAccount { get; set; }

        /// <summary>
        /// 退款发起来源
        /// </summary>
        public string RefundRequestSource { get; set; }
    }
}