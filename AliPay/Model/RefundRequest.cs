namespace AliPay.Model
{
    /// <summary>
    /// 退款请求
    /// </summary>
    public abstract class RefundRequest : ConfigRequest
    {
        /// <summary>
        /// 退款通知地址
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 退款批次号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 退款总比数量{默认写1就行了}
        /// </summary>
        public int BatchNum { get; set; } = 1;

        /// <summary>
        /// 第三方交易号
        /// </summary>
        public string PayInterfaceId { get; set; }
    }
}