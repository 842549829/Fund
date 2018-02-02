namespace AliPay.Model
{
    /// <summary>
    /// 退款结果
    /// </summary>
    public class RefundResult : OriginalDetails
    {
        /// <summary>
        /// 申请标识 T 成功 F 失败 P 处理中
        /// </summary>
        public string IsSuccess { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string Message { get; set; }
    }
}