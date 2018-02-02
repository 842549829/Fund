
using System;

namespace AliPay.Model
{
    /// <summary>
    /// 转账结果
    /// </summary>
    public class FundTransToaccountTransferInfoResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 转账消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 转账订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 成功时间
        /// </summary>
        public DateTime PayDate { get; set; }

        /// <summary>
        /// 支付宝返回的原始信息
        /// </summary>
        public string Body { get; set; }
    }
}