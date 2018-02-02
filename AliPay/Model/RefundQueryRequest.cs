namespace AliPay.Model
{
    /// <summary>
    /// 退款查询
    /// </summary>
    public class RefundQueryRequest : ConfigRequest
    {
        /// <summary>
        /// 退款批次号{批次号和交易同时传以批次号为准}{注:批次号和交易号至少传入一个}
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 第三方支付交易号{批次号和交易同时传以批次号为准}{注:批次号和交易号至少传入一个}
        /// </summary>
        public string TradeNo { get; set; }
    }
}