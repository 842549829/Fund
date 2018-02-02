﻿using System;
using System.Xml.Serialization;

namespace AliPay.SdkPay
{
    /// <summary>
    /// Aop请求响应
    /// </summary>	
    [Serializable]
    public abstract class AopResponse
    {
        /// <summary>
        /// 错误码
        /// </summary>
        private string code;

        /// <summary>
        /// 错误消息
        /// </summary>
        private string msg;

        /// <summary>
        ///子错误码
        /// </summary>
        private string subCode;

        /// <summary>
        /// 子消息
        /// </summary>
        private string subMsg;

        /// <summary>
        /// 响应原始内容
        /// </summary>
        private string body;

        /// <summary>
        /// 错误码
        /// 对应 ErrCode
        /// </summary>
        [XmlElement("code")]
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        /// <summary>
        /// 错误信息
        /// 对应 ErrMsg
        /// </summary>
        [XmlElement("msg")]
        public string Msg
        {
            get { return msg; }
            set { msg = value; }
        }

        /// <summary>
        /// 子错误码
        /// 对应 SubErrCode
        /// </summary>
        [XmlElement("sub_code")]
        public string SubCode
        {
            get { return subCode; }
            set { subCode = value; }
        }

        /// <summary>
        /// 子错误信息
        /// 对应 SubErrMsg
        /// </summary>
        [XmlElement("sub_msg")]
        public string SubMsg
        {
            get { return subMsg; }
            set { subMsg = value; }
        }

        /// <summary>
        /// 响应原始内容
        /// </summary>
        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        /// <summary>
        /// 响应结果是否错误
        /// </summary>
        public bool IsError => !string.IsNullOrEmpty(this.SubCode);
    }
}