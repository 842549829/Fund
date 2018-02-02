using System;
using System.Collections.Generic;

namespace Weixin.Model
{
    [Serializable]
    public class api_get_authorizer_info_result
    {
        public authorizer_info authorizer_info { get; set; }

        public authorization_infos authorization_info { get; set; }
    }

    [Serializable]
    public class authorizer_info
    {
        public string nick_name { get; set; }

        public string head_img { get; set; }

        public info service_type_info { get; set; }

        public info verify_type_info { get; set; }

        public string user_name { get; set; }

        public string principal_name { get; set; }

        public business_info business_info { get; set; }

        public string alias { get; set; }

        public string qrcode_url { get; set; }

        public string signature { get; set; }
    }

    [Serializable]
    public class business_info
    {
        public int open_store { get; set; }

        public int open_scan { get; set; }

        public int open_pay { get; set; }

        public int open_card { get; set; }

        public int open_shake { get; set; }
    }

    [Serializable]
    public class info
    {
        public int id { get; set; }
    }

    [Serializable]
    public class authorization_infos
    {
        public string authorizer_appid { get; set; }

        public string authorizer_refresh_token { get; set; }

        public List<func_infos> func_info { get; set; }
    }

    [Serializable]
    public class func_infos
    {
        public info funcscope_category { get; set; }
    }
}