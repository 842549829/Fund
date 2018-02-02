using System.Collections.Generic;

namespace AliPay.Model
{
    /// <summary>
    /// 分润信息
    /// </summary>	
    public class BillInfo
    {
        /// <summary>
        /// 分润批次号
        /// </summary>
        public string OutBillNo { get; set; }

        /// <summary>
        /// 第三方支付交易号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 分润数据集
        /// 分润数据集格式:支付宝收款帐号A^收款金额^备注|支付宝收款帐号B^收款金额^备注   
        /// 注:分润数据集合最多分10个支付宝帐号
        /// 收款金额格式:0.00
        /// 建议备注内容填写: 机票总订单号
        /// </summary>
        public List<RoyaltyParameters> RoyaltyParameters { get; set; }
    }

    /// <summary>
    /// 分润数据集
    /// </summary>
    public class RoyaltyParameters
    {
        /// <summary>
        /// 支付宝收款帐号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 分润金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}