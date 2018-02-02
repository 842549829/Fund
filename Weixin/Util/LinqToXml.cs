using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Weixin.Util
{
    /// <summary>
    /// LinqToXml
    /// </summary>
    public static class LinqToXml
    {
        /// <summary>
        /// 获取最终结果值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static string GetValue(string value)
        {
            if (value != null)
            {
                return Regex.Replace(value, @"<!\[CDATA\[([\s\S]+)\]\]>", "$1");
            }
            return null;
        }

        /// <summary>
        /// 获取xml节点值
        /// </summary>
        /// <param name="element">文档结构</param>
        /// <returns>值</returns>
        public static string GetString(this XElement element)
        {
            return element == null ? string.Empty : GetValue(element.Value);
        }

        /// <summary>
        /// 获取xml节点值
        /// </summary>
        /// <param name="element">文档结构</param>
        /// <returns>值</returns>
        public static Guid GetGuid(this XElement element)
        {
            return element == null ? Guid.Empty : Guid.Parse(GetValue(element.Value));
        }

        /// <summary>
        /// 获取xml节点值
        /// </summary>
        /// <param name="element">文档结构</param>
        /// <returns>值</returns>
        public static int GetInt(this XElement element)
        {
            return element == null ? 0 : Convert.ToInt32(GetValue(element.Value));
        }

        /// <summary>
        /// 获取xml节点值
        /// </summary>
        /// <param name="element">文档结构</param>
        /// <returns>值</returns>
        public static DateTime GetDateTime(this XElement element)
        {
            return element == null ? DateTime.MinValue : Convert.ToDateTime(GetValue(element.Value));
        }

        /// <summary>
        /// 获取xml节点值转换为时间列表
        /// </summary>
        /// <param name="element">文档结构</param>
        /// <returns>值</returns>
        public static List<DateTime> GetDateTimes(this XElement element)
        {
            List<DateTime> res = new List<DateTime>();
            if (element == null)
            {
                res.Add(DateTime.MinValue);
                return res;
            }
            string[] strDate = GetValue(element.Value).Split(',');
            res.AddRange(strDate.Select(Convert.ToDateTime));
            return res;
        }

        /// <summary>
        /// 获取xml节点值
        /// </summary>
        /// <param name="element">文档结构</param>
        /// <returns>值</returns>
        public static decimal GetDecimal(this XElement element)
        {
            return element == null ? 0M : Convert.ToDecimal(GetValue(element.Value));
        }

        /// <summary>
        /// 获取xml节点值
        /// </summary>
        /// <param name="element">文档结构</param>
        /// <param name="symbol">标识符号</param>
        /// <returns>值</returns>
        public static bool GetBoolean(this XElement element, string symbol = "")
        {
            if (element == null)
            {
                return false;
            }
            var val = GetValue(element.Value);
            if (string.IsNullOrEmpty(symbol))
            {
                return Convert.ToBoolean(symbol);
            }
            return string.Equals(val.Trim(), symbol, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
