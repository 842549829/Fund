namespace AliPay.Model
{
    /// <summary>
    ///  支付宝交易查询 
    /// </summary>
    public class AlipayTradeQueryInfo
    {
        /// <summary>
        /// 第三方交易号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutTradeNo { get; set; }
    }
}