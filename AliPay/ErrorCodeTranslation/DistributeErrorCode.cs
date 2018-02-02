using System.Collections.Generic;

namespace AliPay.ErrorCodeTranslation
{
    /// <summary>
    /// 支付宝多级分润错误码
    /// </summary>	
    public class DistributeErrorCode
    {
        /// <summary>
        /// 支付宝多级分润错误码
        /// </summary>
        private static readonly Dictionary<string, string> dicErrorCode = new Dictionary<string, string>()
        {
            {"NULL_TRADENO_OR_OUTBILLNO","交易号和商户网站唯一订单号同时为空或分润号为空"},

            {"TRADE_PASS_TIME_OR_NOT_SUCCESS","交易超过可分润的时间或者交易状态不合法"},

            {"ROYALTY_PARAM_ERROR","分润参数集格式不正确"},

            {"ROYALTY_TYPE_ERROR","分润类型参数为空或者不合法"},

            {"TRADE_CAN_NOT_FOUND","交易信息不存在"},

            {"TRADE_NO_NOT_MATCH","交易号和商户网站唯一订单号不匹配"},

            {"SELLER_NOT_FOUND","卖家信息不存在"},

            {"NOT_CREATE_BY_PARTNER","交易不是由当前partner创建"},

            {"DOUBLE_OUT_BILL_NO","分润号重复"},

            {"NAVIGATION_INCOME_OF_ROYALTY_ACCOUNT","分润付款方余额不足或违反入大于等于出的原则"},

            {"OUTBILLNO_TOO_LONG","分润号超过允许长度"},

            {"OUTBILLNO_UNSUPPORT_ENCODING_ERROR","分润号字符集格式不合法"},

            {"DIRECTIONAL_PAY_FORBIDDEN","操作受限，不满足定向支付规则"},

            {"TXN_RESULT_ACCOUNT_STATUS_NOT_VALID","账户状态无效"},

            {"ILLEGAL_SIGN","签名不正确"},

            {"ILLEGAL_DYN_MD5_KEY","动态密钥信息错误"},

            {"ILLEGAL_ENCRYPT","加密不正确"},

            {"ILLEGAL_ARGUMENT","参数不正确"},

            {"ILLEGAL_SERVICE","参数不正确"},

            {"ILLEGAL_PARTNER","合作伙伴ID不正确"},

            {"ILLEGAL_EXTERFACE","接口配置不正确"},

            {"ILLEGAL_PARTNER_EXTERFACE","合作伙伴接口信息不正确"},

            {"ILLEGAL_SECURITY_PROFILE","未找到匹配的密钥配置"},

            {"ILLEGAL_SIGN_TYPE","签名类型不正确"},

            {"ILLEGAL_CHARSET","字符集不合法"},

            {"ILLEGAL_CLIENT_IP","客户端IP地址无权访问服务"},

            {"HAS_NO_PRIVILEGE","无权访问"},

            {"ILLEGAL_ENCODING","不支持该编码类型"},

            {"ERROR_CERTIFY_LEVEL_LIMIT","分润方账户存在问题"}
        };

        /// <summary>
        /// 支付宝多级分润错误码
        /// </summary>
        public Dictionary<string, string> ErrorCode => dicErrorCode;
    }
}