using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace GoldenWingMau.WMS.Utility
{
    public class HttpPost
    {
        public CookieContainer cookieContainer { get; set; }

        public string Post(string urlString, Encoding encoding, string postDataString, string ContentType = "application/json")
        {
            if (cookieContainer == null)
            {
                cookieContainer = new CookieContainer();
            }

            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            Stream inputStream = null;
            Stream outputStream = null;
            StreamReader streamReader = null;
            string htmlString = string.Empty;

            byte[] postDataByte = encoding.GetBytes(postDataString); //转换POST数据  

            //建立页面请求 
            ServicePointManager.ServerCertificateValidationCallback =
new RemoteCertificateValidationCallback(CheckValidationResult);//SSl验证
            try
            {
                httpWebRequest = WebRequest.Create(urlString) as HttpWebRequest;
            }
            catch (Exception ex)
            {
                throw new Exception("建立页面请求时发生错误！", ex);
            }

            //指定请求处理方式  

            httpWebRequest.Method = "POST";
            httpWebRequest.KeepAlive = false;
            httpWebRequest.ContentType = ContentType;
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.ContentLength = postDataByte.Length;

            //向服务器传送数据  
            try
            {
                inputStream = httpWebRequest.GetRequestStream();
                inputStream.Write(postDataByte, 0, postDataByte.Length);
            }
            catch (Exception ex)
            {
                throw new Exception("发送POST数据时发生错误！", ex);
            }
            finally
            {
                inputStream.Close();
            }

            //接受服务器返回信息  

            try
            {

                httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                outputStream = httpWebResponse.GetResponseStream();
                streamReader = new StreamReader(outputStream, encoding);
                htmlString = streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception("接受服务器返回页面时发生错误！", ex);
            }
            finally
            {
                streamReader.Close();
            }

            //获取cookies
            foreach (Cookie cookie in httpWebResponse.Cookies)
            {
                cookieContainer.Add(cookie);
            }

            return htmlString;

        }

        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   // 总是接受  
            return true;
        }
    }

    internal class AcceptAllCertificatePolicy : ICertificatePolicy
    {
        public AcceptAllCertificatePolicy()
        {
        }

        public bool CheckValidationResult(ServicePoint sPoint, X509Certificate cert, WebRequest wRequest, int certProb)
        {
            return true;
        }
    }
}
