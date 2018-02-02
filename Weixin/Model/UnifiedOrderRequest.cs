namespace Weixin.Model
{
    /// <summary>
    /// 统一下单接口
    /// </summary>
    public class UnifiedOrderRequest
    {
        /// <summary>
        /// 商品描述	body	是	String(128)	腾讯充值中心-QQ会员充值	商品描述交易字段格式根据不同的应用场景按照以下格式：APP——需传入应用市场上的APP名字-实际商品名称，天天爱消除-游戏充值。
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 商品详情	detail	否	String(8192)	 	商品详细描述，对于使用单品优惠的商户，改字段必须按照规范上传，详见
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// 商户订单号	out_trade_no	是	String(32)	20150806125346	商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 总金额	total_fee	是	Int	888	订单总金额，单位为分
        /// </summary>
        public int TotalFee { get; set; }

        /// <summary>
        /// 终端IP	spbill_create_ip	是	String(16)	123.12.12.123	用户端实际ip
        /// </summary>
        public string SpbillCreateIp { get; set; }

        /// <summary>
        /// 通知地址	notify_url	是	String(256)	http://www.weixin.qq.com/wxpay/pay.php	接收微信支付异步通知回调地址，通知url必须为直接可访问的url，不能携带参数。
        /// </summary>
        public string NotifyUrl { get; set; }
    }
}