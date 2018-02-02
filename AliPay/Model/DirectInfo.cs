namespace AliPay.Model
{
    /// <summary>
    /// 支付链接生成实体
    /// </summary>	
    public class DirectInfo
    {
        /// <summary>
        /// 支付异步通知地址
        /// </summary>
        public string Notify { get; set; }

        /// <summary>
        /// 支付成功同步返回地址
        /// </summary>
        public string Return { get; set; } = string.Empty;

        /// <summary>
        /// 支付交易号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 交易名称
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal TotalFee { get; set; }

        /// <summary>
        /// 商品显示
        /// </summary>
        public string ShowUrl { get; set; } = string.Empty;

        /// <summary>
        /// 扩展参数
        /// </summary>
        public string ExtraCommonParam { get; set; } = string.Empty;
    }
}