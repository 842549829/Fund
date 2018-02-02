namespace AliPay.Model
{
    /// <summary>
    /// 转账信息
    /// </summary>
    public class FundTransToaccountTransferInfo
    {
        /// <summary>
        /// 商户转账唯一订单号。发起转账来源方定义的转账单据ID，用于将转账回执通知给来源方。 不同来源方给出的ID可以重复，同一个来源方必须保证其ID的唯一性。 只支持半角英文、数字，及“-”、“_”。
        /// </summary>
        public string OutBizNo { get; set; }

        /// <summary>
        /// 收款方账户。与payee_type配合使用。付款方和收款方不能是同一个账户
        /// </summary>
        public string PayeeAccount { get; set; }

        /// <summary>
        /// 转账金额，单位：元。 只支持2位小数，小数点前最大支持13位，金额必须大于等于0.1元。
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 收款方真实姓名（最长支持100个英文/50个汉字）。 如果本参数不为空，则会校验该账户在支付宝登记的实名是否与收款方真实姓名一致。
        /// </summary>
        public string PayeeRealName { get; set; }

        /// <summary>
        /// 转账备注（支持200个英文/100个汉字）。 
        /// 当付款方为企业账户，且转账金额达到（大于等于）50000元，remark不能为空。收款方可见，会展示在收款用户的收支详情中。
        /// </summary>
        public string Remark { get; set; }
    }
}