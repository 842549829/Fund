namespace Weixin.Model
{
    /// <summary>
    /// 通过code换取网页授权openid的返回数据
    /// </summary>
    public class OpenidFromCodeRequest
    {
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
    }
}