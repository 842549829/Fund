namespace Weixin.Model
{
    /// <summary>
    /// 支付通知
    /// </summary>
    public class PayNotifyResult
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
        /// 设备号
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public string TradeType { get; set; }

        /// <summary>
        /// 支付银行
        /// </summary>
        public string BankType { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public int TotalFee { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 支付完成时间
        /// </summary>
        public string TimeEnd { get; set; }
    }
}