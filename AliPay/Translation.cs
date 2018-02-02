using System.Collections.Generic;
using System.Reflection;
using AliPay.ErrorCodeTranslation;

namespace AliPay
{
    /// <summary>
    /// 翻译类
    /// </summary>	
    public static class Translation
    {
        /// <summary>
        /// 错误码翻译
        /// </summary>
        /// <typeparam name="T">错误类型</typeparam>
        /// <param name="enErrorCode">错误码</param>
        /// <returns>结果</returns>
        public static string TranslationZhCn<T>(this string enErrorCode) where T : new()
        {
            try
            {
                T t = new T();
                PropertyInfo pro = typeof(T).GetProperty("ErrorCode");
                Dictionary<string, string> dic = pro.GetValue(t, null) as Dictionary<string, string>;
                if (dic != null && dic.ContainsKey(enErrorCode))
                {
                    //业务错误码
                    return $"原错误码：{enErrorCode}，中文解释：{dic[enErrorCode]}";
                }
                else if (SystemErrorCode.ErrorCode.ContainsKey(enErrorCode))
                {
                    //系统错误码
                    return $"原错误码：{enErrorCode}，中文解释：{SystemErrorCode.ErrorCode[enErrorCode]}";
                }
                else
                {
                    return $"原错误码：{enErrorCode}，中文解释：{"未找到匹配的翻译项"}";
                }
            }
            catch
            {
                return $"原错误码：{enErrorCode}，中文解释：{"未找到匹配的翻译项"}";
            }
        }
    }
}