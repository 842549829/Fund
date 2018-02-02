namespace Weixin.Model
{
    /// <summary>
    /// 订单查询结果
    /// </summary>
    public class OrderQueryResult
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
        /// 应用APPID
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        /// 商户号	
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 付款银行	
        /// </summary>
        public string BankType { get; set; }

        /// <summary>
        /// 总金额	
        /// </summary>
        public int TotalFee { get; set; }

        /// <summary>
        /// 支付完成时间
        /// </summary>
        public string TimeEnd { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// SUCCESS—支付成功
        /// REFUND—转入退款
        /// NOTPAY—未支付
        /// CLOSED—已关闭
        /// REVOKED—已撤销（刷卡支付）
        /// USERPAYING--用户支付中
        /// PAYERROR--支付失败
        /// </summary>
        public string TradeState { get; set; }
    }
}