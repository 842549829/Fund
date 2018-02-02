using System.Collections.Generic;
using System.Text;

namespace AliPay.Model
{
    /// <summary>
    /// 退分润
    /// </summary>
    public class RefundShareRequest : RefundRequest
    {
        /// <summary>
        /// 退款详情
        /// </summary>
        public List<RefundShareDetail> Detail { get; set; }

        /// <summary>
        /// 生成退款详情
        /// </summary>
        /// <returns>退分润详情</returns>
        public override string ToString()
        {
            StringBuilder detailData = new StringBuilder();
            detailData.AppendFormat("{0}^0^退分润", this.PayInterfaceId);
            foreach (var item in this.Detail)
            {
                detailData.AppendFormat("|{0}^{1}^{2}^{3}^{4:0.00}^{5}", item.RivalAccount, string.Empty, item.PloatAccount, string.Empty, item.RefundAmount, item.Remark);
            }
            return detailData.ToString();
        }
    }
}