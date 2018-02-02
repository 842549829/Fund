
namespace Weixin.Model
{
    /// <summary>
    /// 退款详情
    /// </summary>
    public class RefundQueryDetails
    {
        /// <summary>
        /// 退款状态
        /// SUCCESS—退款成功
        /// REFUNDCLOSE—退款关闭。
        /// PROCESSING—退款处理中
        /// CHANGE—退款异常，退款到银行发现用户的卡作废或者冻结了，导致原路退款银行卡失败，可前往商户平台（pay.weixin.qq.com）-交易中心，手动处理此笔退款。$n为下标，从0开始编号。
        /// REQUERY 查询失败请重新查询
        /// </summary>
        public string RefundStatus { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public int RefundFee { get; set; }

        /// <summary>
        /// 微信退款订单号
        /// </summary>
        public string RefundId { get; set; }

        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string OutRefundNo { get; set; }
    }
}