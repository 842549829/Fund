using System.Collections.Generic;

namespace Weixin.Model
{
    /// <summary>
    /// 退款查询结果
    /// </summary>
    public class RefundQueryResult
    {
        /// <summary>
        /// 是否请求成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 退款详情
        /// </summary>
        public List<RefundQueryDetails> Details { get; set; }
    }
}