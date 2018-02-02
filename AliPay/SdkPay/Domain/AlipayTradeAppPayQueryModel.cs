using System;
using System.Xml.Serialization;

namespace AliPay.SdkPay.Domain
{
    /// <summary>
    /// 支付宝交易查询
    /// </summary>
    [Serializable]
    public class AlipayTradeAppPayQueryModel : AopObject
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        [XmlElement("out_trade_no")]
        public string out_trade_no { get; set; }

        /// <summary>
        /// 第三方交易号
        /// </summary>
        [XmlElement("trade_no")]
        public string trade_no { get; set; }
    }
}