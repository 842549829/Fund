using System.Text;

namespace AliPay.Model
{
    /// <summary>
    /// 退支付
    /// </summary>
    public class RefundPayRequest : RefundRequest
    {
        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// 退款备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 生成退款详情
        /// </summary>
        /// <returns>退支付详情</returns>
        public override string ToString()
        {
            StringBuilder detailData = new StringBuilder();
            detailData.AppendFormat("{0}^{1:0.00}^{2}", this.PayInterfaceId, this.RefundAmount, this.Remark);
            return detailData.ToString();
        }
    }
}