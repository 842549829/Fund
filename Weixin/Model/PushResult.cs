namespace Weixin.Model
{
    /// <summary>
    /// 推送消息响应结果
    /// </summary>
    public class PushResult : Result
    {
        /// <summary>
        /// 第三方平台appid
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// component_verify_ticket
        /// </summary>
        public string InfoType { get; set; }

        /// <summary>
        /// Ticket内容
        /// </summary>
        public string ComponentVerifyTicket { get; set; }
    }
}