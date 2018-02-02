namespace AliPay.Model
{
    /// <summary>
    /// 支付宝查询结果
    /// </summary>
    public class AlipayTradeQueryResult
    {
        /// <summary>
        /// 是否支付成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 支付买家帐号
        /// </summary>
        public string Buyer { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        /// 第三方交易号
        /// </summary>
        public string PanyInterfaceNo { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string TradeNo { get; set; }
    }
}