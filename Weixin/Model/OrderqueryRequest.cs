namespace Weixin.Model
{
    /// <summary>
    /// 订单查询
    /// </summary>
    public class OrderQueryRequest
    {
        /// <summary>
        /// 微信订单号 (微信订单号 ,商户订单号 二选一)
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户订单号 (微信订单号 ,商户订单号 二选一)
        /// </summary>
        public string OutTradeNo { get; set; }
    }
}