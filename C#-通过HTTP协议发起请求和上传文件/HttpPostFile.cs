using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace GoldenWingMau.WMS.Utility
{
    public class HttpPostFile
    {
        public void Post(string urlString,string FilePath,string saveName)
        {
            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);

       

            //时间戳
            string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");

            //请求头部信息
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(strBoundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append("file");
            sb.Append("\"; filename=\"");
            sb.Append(saveName);
            sb.Append("\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append("application/octet-stream");
            sb.Append("\r\n");
            sb.Append("\r\n");

            string strPostHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strPostHeader);

            // 根据uri创建HttpWebRequest对象
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(urlString));
            httpReq.Method = "POST";

            //对发送的数据不使用缓存【重要、关键】
            httpReq.AllowWriteStreamBuffering = false;

            //设置获得响应的超时时间（300秒）
            httpReq.Timeout = 300000;
            httpReq.ContentType = "multipart/form-data; boundary=" + strBoundary;
            long length = fs.Length + postHeaderBytes.Length + boundaryBytes.Length;
            long fileLength = fs.Length;
            httpReq.ContentLength = length;
            try
            {

                //每次上传4k
                int bufferLength = 4096;
                byte[] buffer = new byte[bufferLength];

                //已上传的字节数
                long offset = 0;

                //开始上传时间
                DateTime startTime = DateTime.Now;
                int size = r.Read(buffer, 0, bufferLength);
                Stream postStream = httpReq.GetRequestStream();

                //发送请求头部消息
                postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                while (size > 0)
                {
                    postStream.Write(buffer, 0, size);
                    offset += size;
                    size = r.Read(buffer, 0, bufferLength);
                }
                //添加尾部的时间戳
                postStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                postStream.Close();

                //获取服务器端的响应
                WebResponse webRespon = httpReq.GetResponse();
                Stream s = webRespon.GetResponseStream();
                StreamReader sr = new StreamReader(s);

            }
            catch (Exception ex)
            {
                throw new Exception("发送POST数据时发生错误！", ex);
            }
            finally
            {
                fs.Close();
                r.Close();
            }
        }
		
		//服务器端处理的Demo
		
		//public void ServerHandle()
        //{
        //    HttpFileCollection files = Request.Files;
        //    string filePath = Server.MapPath("~/UploadFiles/");
        //    if (files.Count != 0)
        //    {
        //        string fileName = files[0].FileName;
        //        files[0].SaveAs(Path.Combine(filePath, fileName));
        //        Response.Write("<p>上传成功</p>");
        //    }
        //    else
        //    {
        //        Response.Write("<p>未获取到Files:" + files.Count.ToString() + "</p>");
        //    }
        //}		
    }
}
