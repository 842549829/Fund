using System;

namespace Weixin.Model
{
    /// <summary>
    /// 微信零钱转账查询结果
    /// </summary>
    public class TransfersQueryResult
    {
        /// <summary>
        /// 转账状态 SUCCESS:转账成功 FAILED:转账失败 PROCESSING:处理中
        /// </summary>
        public TransfersStatus Status { get; set; }

        /// <summary>
        /// 返回信息	
        /// </summary>
        public string ReturnMsg { get; set; }

        /// <summary>
        /// 商户单号
        /// </summary>
        public string PartnerTradeNo { get; set; }

        /// <summary>
        /// 微信商户号
        /// </summary>
        public string DetailId { get; set; }

        /// <summary>
        /// 收款用户openid
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 收款用户姓名	
        /// </summary>
        public string TransferName { get; set; }

        /// <summary>
        /// 付款金额(付款金额单位分)
        /// </summary>
        public int PaymentAmount { get; set; }

        /// <summary>
        /// 转账时间	
        /// </summary>
        public DateTime TransferTime { get; set; }

        /// <summary>
        /// 付款描述
        /// </summary>
        public string Desc { get; set; }
    }
}