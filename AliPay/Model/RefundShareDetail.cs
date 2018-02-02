namespace AliPay.Model
{
    /// <summary>
    /// 退款详情
    /// </summary>
    public class RefundShareDetail
    {
        /// <summary>
        /// 转出支付宝账号
        /// </summary>
        public string RivalAccount { get; set; }

        /// <summary>
        /// 转入支付宝账号
        /// </summary>
        public string PloatAccount { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// 退款备注长度256
        /// </summary>
        public string Remark { get; set; }
    }
}