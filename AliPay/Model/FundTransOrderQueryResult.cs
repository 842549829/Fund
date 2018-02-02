using System;

namespace AliPay.Model
{
    /// <summary>
    /// 单笔转账查询结果
    /// </summary>
    public class FundTransOrderQueryResult
    {
        /// <summary>
        /// 支付宝订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime PayDate { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutBizNo { get; set; }
    }
}