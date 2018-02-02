using System;
using System.Collections.Generic;

namespace AliPay.Model
{
    /// <summary>
    /// 退款明细结果
    /// </summary>
    public class RefundResultDetails
    {
        /*
         * 退款结果明细以批量批次中的单笔明细为单位，多个明细之间采用“#”号分隔； 单笔明细中按“退交易结果$退收费结果|退分润结果$$退子交易结果”格式返回，多条退分润结果之间使用“|”分隔； 
         * 退交易处理结果格式：批次号^原付款交易号^退交易金额^处理结果码^是否充退^充退处理结果； 
         * 其中是否充退的可选值：true-有充退，false-无充退； 
         * 充退处理结果的可选值：S（成功）、F（失败）、P（处理中），无充退时
         * 充退处理结果显示为 null 字符串；退收费处理结果格式：转入人Email^转入人 userId^退款金额^处理结果码；  
         * 退分润处理结果格式：转出人Email^转出人userId^转入人Email^转入人userId^退款金额^处理结果代码； 
         * 退子交易处理结果格式：退子交易金额^子交易可退款金额^处理结果码。 
         * 样例: 201012300001^2010123016346858^0.02^SUCCESS|zen_gwen@hotmail.com^2088102210397302^alipay-test03@alipay.com^2088101568345155^0.01^SUCCESS$$10.00^10.00^SUCCESS 
         */

        /// <summary>
        /// 退款详情字符串
        /// </summary>
        private readonly string resultDetails;

        /// <summary>
        /// 退款构造函数
        /// </summary>
        /// <param name="resultDetails">退款详情字符串</param>
        public RefundResultDetails(string resultDetails)
        {
            this.resultDetails = resultDetails;
        }

        /// <summary>
        /// 退采购结果
        /// </summary>
        public Lazy<RefundPay> RefundPayResult => this.CreateRefundPay();

        /// <summary>
        /// 退分润结果
        /// </summary>
        public Lazy<List<RefundShare>> RefundShareResult => this.CreateRefundShare();

        /// <summary>
        /// 退支付信息
        /// </summary>
        /// <returns>退支付结果</returns>
        public Lazy<RefundPay> CreateRefundPay()
        {
            Lazy<RefundPay> result = new Lazy<RefundPay>(() =>
            {
                //拆分退采购数据
                RefundPay refundPay = new RefundPay();
                var strArray = resultDetails.Split('#')[0].Split('$');
                var strItem = strArray[0].Split('^');
                refundPay.BatchNo = strItem[0];
                refundPay.TradeNo = strItem[1];
                refundPay.RefundAmount = Convert.ToDecimal(strItem[2]);
                refundPay.ResultCode = strItem[3];
                if (strArray.Length >= 2)
                {
                    var poundageItem = strArray[1].Split('^');
                    refundPay.PoundageAmount = Convert.ToDecimal(poundageItem[2]);
                }
                return refundPay;
            });
            return result;
        }

        /// <summary>
        /// 退分润信息
        /// </summary>
        /// <returns>退分润信息</returns>
        public Lazy<List<RefundShare>> CreateRefundShare()
        {
            Lazy<List<RefundShare>> result = new Lazy<List<RefundShare>>(() =>
            {
                List<RefundShare> refundshars = new List<RefundShare>();
                var stringItem = this.resultDetails.Split('#');

                //移除退子交易数据
                var strNoChildArray = stringItem.Length >= 2 ? stringItem[1].Split(new[] { "$$" }, StringSplitOptions.RemoveEmptyEntries) : stringItem[0].Split(new[] { "$$" }, StringSplitOptions.RemoveEmptyEntries);
                if (strNoChildArray.Length <= 0)
                {
                    return refundshars;
                }

                //拆分退分润数据
                string[] strPayArray = strNoChildArray[0].Split('$');
                var strShareArray = strPayArray.Length >= 2 ? strPayArray[1].Split('|') : strPayArray[0].Split('|');
                if (strShareArray.Length <= 1)
                {
                    return refundshars;
                }
                string[] pays = strNoChildArray[0].Split('^');
                for (int i = 1; i < strShareArray.Length; i++)
                {
                    var strItem = strShareArray[i].Split('^');
                    if (strItem.Length >= 6)
                    {
                        RefundShare refundShare = new RefundShare
                        {
                            RivalAccount = strItem[0],
                            RivalAccountId = strItem[1],
                            PloatAccount = strItem[2],
                            PloatAccountId = strItem[3],
                            RefundAmount = Convert.ToDecimal(strItem[4]),
                            ResultCode = strItem[5].ToUpper(),
                            BatchNo = pays[0],
                            TradeNo = pays[1]
                        };
                        refundshars.Add(refundShare);
                    }
                }
                return refundshars;
            });
            return result;
        }

        /// <summary>
        /// 退支付信息
        /// </summary>
        public class RefundPay
        {
            /// <summary>
            /// 退款金额
            /// </summary>
            public decimal RefundAmount { get; set; }

            /// <summary>
            /// 退还手续费
            /// </summary>
            public decimal PoundageAmount { get; set; }

            /// <summary>
            /// 批次号
            /// </summary>
            public string BatchNo { get; set; }

            /// <summary>
            /// 原付款交易号
            /// </summary>
            public string TradeNo { get; set; }

            /// <summary>
            /// 处理结果代码
            /// </summary>
            public string ResultCode { get; set; }

            /// <summary>
            /// 是否退款成功
            /// </summary>
            public bool IsSuccess => this.ResultCode.ToUpper() == "SUCCESS";
        }

        /// <summary>
        /// 退分润信息
        /// </summary>
        public class RefundShare
        {
            /// <summary>
            /// 批次号
            /// </summary>
            public string BatchNo { get; set; }

            /// <summary>
            /// 原付款交易号
            /// </summary>
            public string TradeNo { get; set; }

            /// <summary>
            /// 转出人Email
            /// </summary>
            public string RivalAccount { get; set; }

            /// <summary>
            /// 转出人userId
            /// </summary>
            public string RivalAccountId { get; set; }

            /// <summary>
            /// 转入人Email
            /// </summary>
            public string PloatAccount { get; set; }

            /// <summary>
            /// 转入人userId
            /// </summary>
            public string PloatAccountId { get; set; }

            /// <summary>
            /// 退款金额
            /// </summary>
            public decimal RefundAmount { get; set; }

            /// <summary>
            /// 处理结果码
            /// </summary>
            public string ResultCode { get; set; }

            /// <summary>
            /// 是否退款成功
            /// </summary>
            public bool IsSuccess => this.ResultCode.ToUpper() == "SUCCESS";
        }
    }
}