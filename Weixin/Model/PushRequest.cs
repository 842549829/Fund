using System;

namespace Weixin.Model
{
    /// <summary>
    /// 推送请求
    /// </summary>
    public class PushRequest
    {
        /// <summary>
        /// 请求信息
        /// </summary>
        public string Xml { get; set; }

        /// <summary>
        /// 解密key
        /// </summary>
        public string EncodingAESKey { get; set; }

        /// <summary>
        /// 写入redis的key值
        /// </summary>
        public string ComponentVerifyTicket { get; set; }

        /// <summary>
        /// 写入redis的有效期
        /// </summary>
        public DateTime ValidityTime { get; set; }
    }
}