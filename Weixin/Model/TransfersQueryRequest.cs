namespace Weixin.Model
{
    /// <summary>
    /// 微信零钱转账查询
    /// </summary>
    public class TransfersQueryRequest
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string PartnerTradeNo { get; set; }
    }
}