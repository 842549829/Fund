namespace Weixin.Model
{
    /// <summary>
    /// wap统一下单
    /// </summary>
    public class WapUnifiedOrderRequest : UnifiedOrderRequest
    {
        /// <summary>
        /// 场景信息
        ///  JSAPI
        /// {"store_info" : {
        /// "id": "SZTX001",
        /// "name": "腾大餐厅",
        ///  "area_code": "440305",
        ///  "address": "科技园中一路腾讯大厦" }
        /// }
        /// 
        /// MWEB:
        /// {"h5_info": //h5支付固定传"h5_info" 
        ///   {"type": "Wap",  //场景类型
        ///    "wap_url": "https://pay.qq.com",//WAP网站URL地址
        ///    "wap_name": "腾讯充值"  //WAP 网站名
        ///   }
        /// }
        /// </summary>
        public string SeneInfo { get; set; }

        /// <summary>
        /// MWEB NATIVE JSAPI APP
        /// </summary>
        public string TradeType { get; set; }

        /// <summary>
        /// OpnenId
        /// </summary>
        public string OpnenId { get; set; }
    }
}