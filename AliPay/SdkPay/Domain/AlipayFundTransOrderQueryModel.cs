using System;
using System.Xml.Serialization;

namespace AliPay.SdkPay.Domain
{
    /// <summary>
    /// AlipayFundTransOrderQueryModel
    /// </summary>
    [Serializable]
    public class AlipayFundTransOrderQueryModel : AopObject
    {
        /// <summary>
        /// 商户转账唯一订单号。发起转账来源方定义的转账单据ID，用于将转账回执通知给来源方。 不同来源方给出的ID可以重复，同一个来源方必须保证其ID的唯一性。 只支持半角英文、数字，及“-”、“_”。
        /// </summary>
        [XmlElement("out_biz_no")]
        public string OutBizNo { get; set; }

        /// <summary>
        /// 支付宝交易号
        /// </summary>
        [XmlElement("order_id")]
        public string OrderId { get; set; }
    }
}