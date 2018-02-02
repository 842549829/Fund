using System;
using System.Security.Cryptography;
using Weixin.Util;

namespace Weixin.Model
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 统一支付接口
        /// </summary>
        public const string UnifiedPayUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";

        /// <summary>
        /// 网页授权接口
        /// </summary>
        public const string AccessTokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token";

        /// <summary>
        /// 微信订单查询接口
        /// </summary>
        public const string OrderQueryUrl = "https://api.mch.weixin.qq.com/pay/orderquery";

        /// <summary>
        /// 微信退支付接口
        /// </summary>
        public const string RefundPayUrl = "https://api.mch.weixin.qq.com/secapi/pay/refund";

        /// <summary>
        /// 微信退款查询接口
        /// </summary>
        public const string RefundQueryUrl = "https://api.mch.weixin.qq.com/pay/refundquery";

        /// <summary>
        /// Get Code Url
        /// </summary>
        public const string AuthorizeUrl = "https://open.weixin.qq.com/connect/oauth2/authorize";

        /// <summary>
        /// 微信转账接口
        /// </summary>
        public const string TransfersUrl = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";

        /// <summary>
        /// 转账查询接口
        /// </summary>
        public const string TransferinfoUrl = "https://api.mch.weixin.qq.com/mmpaymkttransfers/gettransferinfo";

        /// <summary>
        /// 普通AccessToken请求地址
        /// </summary>
        public static string GeneralAccessTokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        /// <summary>
        /// 发送模版消息url
        /// </summary>
        public static string SendTemplateMsgUrl = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=";

        /// <summary>
        /// 获取用户Url
        /// </summary>
        public static string UserUrl = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=";

        /// <summary>
        /// UserInfoUrl
        /// </summary>
        public static string UserInfoUrl = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN";

        /// <summary>
        /// jsapi Url
        /// </summary>
        public static string JsApiUrl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";

        /// <summary>
        /// 随机种子
        /// </summary>
        /// <returns>结构</returns>
        public static int CreateRandomSeed()
        {
            byte[] bytes = new byte[4];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// 随机串
        /// </summary>
        public static string GetNoncestr()
        {
            Random random = new Random(CreateRandomSeed());
            return Security.Encrypt32(random.Next(1000000, 9999999).ToString());
        }

        /// <summary>
        /// 时间截，自1970年以来的秒数
        /// </summary>
        public static string GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 获取应用ID
        /// </summary>
        /// <returns>应用ID</returns>
        public string GetAppId()
        {
            return "";
        }

        /// <summary>
        /// 获取PID
        /// </summary>
        /// <returns>应用PID</returns>
        public string GetPid()
        {
            return "";
        }

        /// <summary>
        /// 获取Key
        /// </summary>
        /// <returns>Key</returns>
        public string GetKey()
        {
            return "";
        }

        /// <summary>
        /// 获取应用ID
        /// </summary>
        /// <returns>应用ID</returns>
        public string GetAppSecret()
        {
            return "";
        }

        /// <summary>
        /// 获取退款证书地址
        /// </summary>
        /// <returns>证书地址</returns>
        public string GetRefundCert()
        {
            return "";
        }

        /// <summary>
        /// 获取转账证书路径
        /// </summary>
        /// <returns>证书路径</returns>
        public string GetTransfersCertPath()
        {
            return "";
        }

        /// <summary>
        /// 获取转账证书路径
        /// </summary>
        /// <returns>证书路径</returns>
        public string GetTransfersQueryCertPath()
        {
            return "";
        }
    }
}