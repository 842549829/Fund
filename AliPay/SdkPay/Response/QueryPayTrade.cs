using System;
using System.Xml.Serialization;

namespace AliPay.SdkPay.Response
{
    /// <summary>
    /// 支付查询结果
    /// </summary>
    [Serializable]
    public class QueryPayTrade : AopResponse
    {
        /// <summary>
        /// 买家帐号(必填)
        /// </summary>
        [XmlElement("buyer_logon_id")]
        public string buyer_logon_id { get; set; }

        /// <summary>
        /// 买家支付金额(选填)
        /// </summary>
        [XmlElement("buyer_pay_amount")]
        public string buyer_pay_amount { get; set; }

        /// <summary>
        /// 买家Id(必填)
        /// </summary>
        [XmlElement("buyer_user_id")]
        public string buyer_user_id { get; set; }

        /// <summary>
        /// 交易中用户支付的可开具发票的金额，单位为元，两位小数。该金额代表该笔交易中可以给用户开具发票的金额(选填)
        /// </summary>
        [XmlElement("invoice_amount")]
        public string invoice_amount { get; set; }

        /// <summary>
        /// 买家支付宝用户号，该字段将废弃，不要使用(选填)
        /// </summary>
        [XmlElement("open_id")]
        [Obsolete]
        public string open_id { get; set; }

        /// <summary>
        /// 商家订单号(必填)
        /// </summary>
        [XmlElement("out_trade_no")]
        public string out_trade_no { get; set; }

        /// <summary>
        /// 积分支付的金额，单位为元，两位小数。该金额代表该笔交易中用户使用积分支付的金额，比如集分宝或者支付宝实时优惠等(选填)
        /// </summary>
        [XmlElement("point_amount")]
        public string point_amount { get; set; }

        /// <summary>
        /// 实收金额，单位为元，两位小数。该金额为本笔交易，商户账户能够实际收到的金额(必填)
        /// </summary>
        [XmlElement("receipt_amount")]
        public string receipt_amount { get; set; }

        /// <summary>
        /// 本次交易打款给卖家的时间(必填)
        /// </summary>
        [XmlElement("send_pay_date")]
        public string send_pay_date { get; set; }

        /// <summary>
        /// 交易的订单金额，单位为元，两位小数。该参数的值为支付时传入的total_amount(必填)
        /// </summary>
        [XmlElement("total_amount")]
        public string total_amount { get; set; }

        /// <summary>
        /// 支付宝交易号(必填)
        /// </summary>
        [XmlElement("trade_no")]
        public string trade_no { get; set; }

        /// <summary>
        /// 交易状态：WAIT_BUYER_PAY（交易创建，等待买家付款）、TRADE_CLOSED（未付款交易超时关闭，或支付完成后全额退款）、TRADE_SUCCESS（交易支付成功）、TRADE_FINISHED（交易结束，不可退款）	TRADE_CLOSED (必填	)
        /// </summary>
        [XmlElement("trade_status")]
        public string trade_status { get; set; }

        ///// <summary>
        ///// 交易支付使用的资金渠道
        ///// </summary>
        //[XmlElement("fund_bill_list")]
        //public List<fund_bill_list> fund_bill_list { get; set; }
    }

    /// <summary>
    /// amount
    /// </summary>
    [Serializable]
    public class fund_bill_list
    {
        /// <summary>
        /// trade_status
        /// </summary>
        [XmlElement("trade_status")]
        public string trade_status { get; set; }

        /// <summary>
        /// fund_channel
        /// </summary>
        [XmlElement("fund_channel")]
        public string fund_channel { get; set; }
    }
}