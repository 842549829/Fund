namespace AliPay.Model
{
    /// <summary>
    /// 账务明细查询 类
    /// </summary>
    public class AccountPageQueryResut
    {
        /// <summary>
        /// 交易后余额
        /// </summary>
        public string balance { get; set; }

        /// <summary>
        /// 买家支付宝人民币资金账户
        /// </summary>
        public string buyer_account { get; set; }

        /// <summary>
        /// 货币代码
        /// </summary>
        public string currency { get; set; }

        /// <summary>
        /// 充值网银流水号
        /// </summary>
        public string deposit_bank_no { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string goods_title { get; set; }

        /// <summary>
        /// 收入金额
        /// </summary>
        public string income { get; set; }

        /// <summary>
        /// 账务序列号
        /// </summary>
        public string iw_account_log_id { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string merchant_out_order_no { get; set; }

        /// <summary>
        /// 支出金额
        /// </summary>
        public string outcome { get; set; }

        /// <summary>
        /// 合作者身份ID
        /// </summary>
        public string partner_id { get; set; }

        /// <summary>
        /// 费率
        /// </summary>
        public string rate { get; set; }

        /// <summary>
        /// 卖家支付宝人民币资金账号
        /// </summary>
        public string seller_account { get; set; }

        /// <summary>
        /// 卖家姓名
        /// </summary>
        public string seller_fullname { get; set; }

        /// <summary>
        /// 交易服务费
        /// </summary>
        public string service_fee { get; set; }

        /// <summary>
        /// 交易服务费率
        /// </summary>
        public string service_fee_ratio { get; set; }

        /// <summary>
        /// 签约产品
        /// </summary>
        public string sign_product_name { get; set; }

        /// <summary>
        /// 子业务类型
        /// </summary>
        public string sub_trans_code_msg { get; set; }

        /// <summary>
        /// 交易总金额
        /// </summary>
        public string total_fee { get; set; }

        /// <summary>
        /// 支付宝交易号
        /// </summary>
        public string trade_no { get; set; }

        /// <summary>
        /// 累积退款金额
        /// </summary>
        public string trade_refund_amount { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string trans_code_msg { get; set; }

        /// <summary>
        /// 交易日期
        /// </summary>
        public string trans_date { get; set; }
    }
}