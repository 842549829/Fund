using System.Collections.Generic;

namespace Weixin.Model
{
    /// <summary>
    /// 支付通知响应
    /// </summary>
    public class PayNotifyResponse
    {
        /// <summary>
        /// 签名参数
        /// </summary>
        public SortedDictionary<string, string> SignParanmeters { get; set; }

        /// <summary>
        /// 通知结果
        /// </summary>
        public PayNotifyResult PayNotifyRresult { get; set; }
    }
}