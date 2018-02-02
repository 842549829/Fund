using System.Collections.Generic;
using AliPay.SdkPay.Response;

namespace AliPay.SdkPay.Request
{
    /// <summary>
    /// AOP API: alipay.fund.trans.toaccount.transfer
    /// </summary>
    public class AlipayFundTransToaccountTransferRequest : IAopRequest<AlipayFundTransToaccountTransferResponse>
    {
        /// <summary>
        /// 单笔转账到支付宝账户接口
        /// </summary>
        public string BizContent { get; set; }

        #region IAopRequest Members

        /// <summary>
        /// 是否需要加密
        /// </summary>
        private bool needEncrypt;

        /// <summary>
        /// 版本
        /// </summary>
        private string apiVersion = "1.0";

        /// <summary>
        /// 终端类型
        /// </summary>
        private string terminalType;

        /// <summary>
        /// 终端信息
        /// </summary>
        private string terminalInfo;

        /// <summary>
        /// 代码
        /// </summary>
        private string prodCode;

        /// <summary>
        /// 通知地址
        /// </summary>
        private string notifyUrl;

        /// <summary>
        /// 返回地址
        /// </summary>
        private string returnUrl;

        /// <summary>
        /// bizModel
        /// </summary>
        private AopObject bizModel;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="needEncrypt">needEncrypt</param>
        public void SetNeedEncrypt(bool needEncrypt)
        {
            this.needEncrypt = needEncrypt;
        }

        /// <summary>
        /// GetNeedEncrypt
        /// </summary>
        /// <returns></returns>
        public bool GetNeedEncrypt()
        {
            return this.needEncrypt;
        }

        /// <summary>
        /// SetNotifyUrl
        /// </summary>
        /// <param name="notifyUrl">notifyUrl</param>
        public void SetNotifyUrl(string notifyUrl)
        {
            this.notifyUrl = notifyUrl;
        }

        /// <summary>
        /// GetNotifyUrl
        /// </summary>
        /// <returns>GetNotifyUrl</returns>
        public string GetNotifyUrl()
        {
            return this.notifyUrl;
        }

        /// <summary>
        /// SetReturnUrl
        /// </summary>
        /// <param name="returnUrl">returnUrl</param>
        public void SetReturnUrl(string returnUrl)
        {
            this.returnUrl = returnUrl;
        }

        /// <summary>
        /// GetReturnUrl
        /// </summary>
        /// <returns>returnUrl</returns>
        public string GetReturnUrl()
        {
            return this.returnUrl;
        }

        /// <summary>
        /// SetTerminalType
        /// </summary>
        /// <param name="terminalType">terminalType</param>
        public void SetTerminalType(string terminalType)
        {
            this.terminalType = terminalType;
        }

        /// <summary>
        /// GetTerminalType
        /// </summary>
        /// <returns>terminalType</returns>
        public string GetTerminalType()
        {
            return this.terminalType;
        }

        /// <summary>
        /// terminalInfo
        /// </summary>
        /// <param name="terminalInfo">terminalInfo</param>
        public void SetTerminalInfo(string terminalInfo)
        {
            this.terminalInfo = terminalInfo;
        }

        /// <summary>
        /// GetTerminalInfo
        /// </summary>
        /// <returns>terminalInfo</returns>
        public string GetTerminalInfo()
        {
            return this.terminalInfo;
        }

        /// <summary>
        /// SetProdCode
        /// </summary>
        /// <param name="prodCode">SetProdCode</param>
        public void SetProdCode(string prodCode)
        {
            this.prodCode = prodCode;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public string GetProdCode()
        {
            return this.prodCode;
        }

        /// <summary>
        /// 获取接口名称
        /// </summary>
        /// <returns>接口名称</returns>
        public string GetApiName()
        {
            return "alipay.fund.trans.toaccount.transfer";
        }

        /// <summary>
        /// SetApiVersion
        /// </summary>
        /// <param name="apiVersion">apiVersion</param>
        public void SetApiVersion(string apiVersion)
        {
            this.apiVersion = apiVersion;
        }

        /// <summary>
        /// GetApiVersion
        /// </summary>
        /// <returns>apiVersion</returns>
        public string GetApiVersion()
        {
            return this.apiVersion;
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <returns>GetParameters</returns>
        public IDictionary<string, string> GetParameters()
        {
            AopDictionary parameters = new AopDictionary { { "biz_content", this.BizContent } };
            return parameters;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns>BizModel</returns>
        public AopObject GetBizModel()
        {
            return this.bizModel;
        }

        /// <summary>
        /// 设置Model
        /// </summary>
        /// <param name="bizModel">bizModel</param>
        public void SetBizModel(AopObject bizModel)
        {
            this.bizModel = bizModel;
        }

        #endregion
    }
}