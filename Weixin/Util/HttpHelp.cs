using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Weixin.Util
{
    /// <summary>
    /// HttpHelp
    /// </summary>
    public class HttpHelp
    {
        /// <summary>
        /// POST数据到指定接口并返回数据
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="postData">post参数</param>
        public static string PostXmlToUrl(string url, string postData)
        {
            string returnmsg;
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                returnmsg = wc.UploadString(url, "POST", postData);
            }
            return returnmsg;
        }

        /// <summary>
        /// 验证服务器证书
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="certificate">certificate</param>
        /// <param name="chain">chain</param>
        /// <param name="errors">errors</param>
        /// <returns>rel</returns>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// POST数据到指定接口并返回数据
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="postData">数据</param>
        /// <param name="cert">证书地址</param>
        /// <param name="password">证书密码</param>
        /// <returns>结果</returns>
        public static string PostXmlToUrl(string url, string postData, string cert, string password)
        {
            string result = null;
            StreamReader sr = null;
            HttpWebResponse wr = null;
            try
            {
                var hp = (HttpWebRequest)WebRequest.Create(url);
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                try
                {
                    hp.ClientCertificates.Add(new X509Certificate2(cert, password, X509KeyStorageFlags.MachineKeySet));
                }
                catch (Exception)
                {
                    X509Store store = new X509Store("My", StoreLocation.LocalMachine);
                    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                    X509Certificate2 certFile = store.Certificates.Find(X509FindType.FindBySubjectName, password, false)[0];
                    hp.ClientCertificates.Add(certFile);
                }
                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] data = encoding.GetBytes(postData);
                hp.Method = "POST";
                hp.ContentType = "application/x-www-form-urlencoded";
                hp.ContentLength = data.Length;
                Stream ws = hp.GetRequestStream();
                ws.Write(data, 0, data.Length);
                ws.Close();
                wr = (HttpWebResponse)hp.GetResponse();
                var stream = wr.GetResponseStream();
                if (stream != null)
                {
                    sr = new StreamReader(stream, encoding);
                    result = sr.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                sr?.Close();
                wr?.Close();
            }
            return result;
        }

        /// <summary>
        /// GET数据到指定接口并返回数据
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="getData">get参数</param>
        public static string GetUrl(string url, string getData)
        {
            string returnmsg;
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                returnmsg = wc.UploadString(url, "GET", getData);
            }
            return returnmsg;
        }

        /// <summary>
        /// GET数据到指定接口并返回数据
        /// </summary>
        /// <param name="url">url</param>
        public static string GetUrl(string url)
        {
            string returnmsg;
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                returnmsg = wc.UploadString(url, "GET");
            }
            return returnmsg;
        }
    }
}