
using System.Collections.Generic;

namespace AliPay.ErrorCodeTranslation
{
    /// <summary>
    /// 支付宝公共系统错误码
    /// </summary>	
    public class SystemErrorCode
    {
        /// <summary>
        /// 支付宝公共系统错误码
        /// </summary>
        public static Dictionary<string, string> ErrorCode { get; } = new Dictionary<string, string>()
        {
            {"SYSTEM_ERROR","支付宝系统错误"},

            {"SESSION_TIMEOUT","session超时"},

            {"ILLEGAL_TARGET_SERVICE","错误的target_service"},

            {"ILLEGAL_ACCESS_SWITCH_SYSTEM","partner不允许访问该类型的系统"},

            {"ILLEGAL_SWITCH_SYSTEM","切换系统异常"},

            {"EXTERFACE_IS_CLOSED","接口已关闭"},

            {"SYSTEM_EXCEPTION","支付宝系统异常"},

            {"ILLEGAL_SIGN","签名不正确"},

            {"ILLEGAL_DYN_MD5_KEY","动态密钥信息错误"},

            {"ILLEGAL_ENCRYPT","加密不正确"},

            {"ILLEGAL_SERVICE","Service参数不正确"},

            {"ILLEGAL_PARTNER","合作伙伴ID不正确"},

            {"ILLEGAL_EXTERFACE","接口配置不正确"},

            {"ILLEGAL_PARTNER_EXTERFACE","合作伙伴接口信息不正确"},

            {"ILLEGAL_SECURITY_PROFILE","未找到匹配的密钥配置"},

            {"ILLEGAL_AGENT","代理ID不正确"},

            {"ILLEGAL_SIGN_TYPE","签名类型不正确"},

            {"ILLEGAL_CHARSET","字符集不合法"},

            {"ILLEGAL_CLIENT_IP","客户端IP地址无权访问服务"},

            {"HAS_NO_PRIVILEGE","无权访问"},

            {"ILLEGAL_DIGEST_TYPE","摘要类型不正确"},

            {"ILLEGAL_DIGEST","文件摘要不正确"},

            {"ILLEGAL_FILE_FORMAT","文件格式不正确"},

            {"ILLEGAL_ENCODING","不支持该编码类型"},

            {"ILLEGAL_REQUEST_REFERER","防钓鱼检查不支持该请求来源"},

            {"ILLEGAL_ANTI_PHISHING_KEY","防钓鱼检查非法时间戳参数"},

            {"ANTI_PHISHING_KEY_TIMEOUT","防钓鱼检查时间戳超时"},

            {"ILLEGAL_EXTER_INVOKE_IP","防钓鱼检查非法调用IP"}
        };
    }
}