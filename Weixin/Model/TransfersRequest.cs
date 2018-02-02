namespace Weixin.Model
{
    /// <summary>
    /// 转账请求
    /// </summary>
    public class TransfersRequest
    {
        /// <summary>
        /// 商户订单号	 
        /// </summary>
        public string PartnerTradeNo { get; set; }

        /// <summary>
        /// 收款人OpnedId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 收款用户姓名	
        /// </summary>
        public string ReUserName { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 企业付款描述信息	
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 调用接口的机器Ip地址
        /// </summary>
        public string SpbillCreateIp { get; set; }
    }
}