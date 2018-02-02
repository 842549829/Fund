using System;
using System.Collections.Generic;

namespace AliPay.Model
{
    /// <summary>
    /// 接口请求配置信息
    /// </summary>
    public abstract class ConfigRequest
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        public Config Config { get; set; }
    }
}