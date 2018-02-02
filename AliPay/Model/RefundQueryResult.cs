namespace AliPay.Model
{
    /// <summary>
    /// 退款查询结果
    /// </summary>
    public class RefundQueryResult : OriginalDetails
    {
        /// <summary>
        /// 是否查询成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 退款查询结果
        /// </summary>
        public RefundResultDetails ResultDetails { get; set; }
    }
}