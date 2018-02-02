using System;
namespace AliPay.Model
{
    /// <summary>
    /// 单笔转账查询信息
    /// </summary>
    public class FundTransOrderQueryInfo
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutBizNo { get; set; }

        /// <summary>
        /// 支付宝订单号
        /// </summary>
        public string OrderId { get; set; }
    }
}