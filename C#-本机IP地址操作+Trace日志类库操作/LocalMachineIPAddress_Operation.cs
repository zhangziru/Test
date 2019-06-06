using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp测试项目20181203
{
    //
    //参考原文链接：https://www.cnblogs.com/lijianda/archive/2017/03/23/6604651.html
    //Dns类：提供简单的域名解析

    //在.NET平台，有很多优秀的日志类库，例如Log4Net。如果程序很小，我们可以自己通过C#的Trace类来实现一个基本的日志记录功能。
    //C# 使用Trace记录程序日志,
    //1、需要在配置文件中添加一段配置（必须项）【重点】
    //2、使用Trace可以直接记录日志的信息，当然 我们也可以做一些处理，让我们的日志看起来更有逻辑性。
    //3、即：记录的日志也有轻重缓急。提供了一个辅助类【可以尝试用一用】
    //4、参考的原文链接：https://www.cnblogs.com/yang-fei/p/4868205.html
    #region Trace 记录日志的配置
    //<system.diagnostics>
    //    <trace autoflush = "true" indentsize="0">
    //        <listeners>
    //            <add name = "LogListener" type="System.Diagnostics.TextWriterTraceListener" initializeData="LogConsoleApp.log" />
    //        </listeners>
    //    </trace>
    //</system.diagnostics> 
    #endregion

    #region Trace 记录日志的代码的配置
    //Trace.Listeners.Add(new TextWriterTraceListener("TraceLog.log"));//配置日志的文件名
    #endregion
    /// <summary>
    /// 本机IP地址操作
    /// </summary>
    public class LocalMachineIPAddress_Operation
    {
        public LocalMachineIPAddress_Operation()
        {
            Trace.Listeners.Add(new TextWriterTraceListener("TraceLog.log"));//配置日志的文件名
            GetCurrentUseIPv4Address();
        }

        /// <summary>
        /// 获取本机所有的IP地址
        /// <para>这些地址是包含所有网卡（虚拟网卡）的ipv4和ipv6地址。</para>
        /// </summary>
        /// <returns></returns>
        public IPAddress[] GetMachineAllIPAddress()
        {
            string name = Dns.GetHostName();
            IPAddress[] ipAddressList = Dns.GetHostAddresses(name);
            return ipAddressList;
        }

        /// <summary>
        /// 获取本机 IPv4的地址
        /// </summary>
        /// <returns></returns>
        public IPAddress[] GetIPv4Address()
        {
            List<IPAddress> result = new List<IPAddress>();
            IPAddress[] ipAddressList = this.GetMachineAllIPAddress();
            foreach (IPAddress ipa in ipAddressList)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)//IPv4地址的过滤
                    result.Add(ipa);
            }
            return result.ToArray();
        }

        /// <summary>
        /// 获取本机 IPv6的地址
        /// </summary>
        /// <returns></returns>
        public IPAddress[] GetIPv6Address()
        {
            List<IPAddress> result = new List<IPAddress>();
            IPAddress[] ipAddressList = this.GetMachineAllIPAddress();
            foreach (IPAddress ipa in ipAddressList)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetworkV6)//IPv6地址的过滤
                    result.Add(ipa);
            }
            return result.ToArray();
        }

        /// <summary>
        /// 获取本机正在使用的IPv4地址（查询本机路由表）
        /// <para>本机可能有多个ipv4的地址</para>
        /// <para>1、一个电脑有多个网卡，有线的、无线的、还有vmare虚拟的两个网卡。</para>
        /// <para>2.就算只有一个网卡，但是该网卡配置了N个IP地址.其中还包括ipv6地址。</para>
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUseIPv4Address()
        {
            string result = RunApp("route", "print", true);
            Match m = Regex.Match(result, @"0.0.0.0\s+0.0.0.0\s+(\d+.\d+.\d+.\d+)\s+(\d+.\d+.\d+.\d+)");
            if (m.Success)
            {
                return m.Groups[2].Value;
            }
            else
            {
                try
                {
                    System.Net.Sockets.TcpClient c = new System.Net.Sockets.TcpClient();
                    c.Connect("www.baidu.com", 80);
                    string ip = ((System.Net.IPEndPoint)c.Client.LocalEndPoint).Address.ToString();
                    c.Close();
                    return ip;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>  
        /// 获取本机主DNS  
        /// </summary>  
        /// <returns></returns>  
        public static string GetPrimaryDNS()
        {
            string result = RunApp("nslookup", "", true);
            Match m = Regex.Match(result, @"\d+\.\d+\.\d+\.\d+");
            if (m.Success)
            {
                return m.Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>  
        /// 运行一个控制台程序并返回其输出参数。  
        /// </summary>  
        /// <param name="filename">程序名</param>  
        /// <param name="arguments">输入参数</param>  
        /// <returns></returns>  
        public static string RunApp(string filename, string arguments, bool recordLog)
        {
            try
            {
                if (recordLog)
                {
                    Trace.WriteLine(filename + " " + arguments);
                }
                Process proc = new Process();
                proc.StartInfo.FileName = filename;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.Arguments = arguments;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();

                //程序调用 一个进程，获取该进程的返回信息：通过 流读取器 来获取相关的信息。
                using (System.IO.StreamReader sr = new System.IO.StreamReader(proc.StandardOutput.BaseStream, Encoding.Default))
                {
                    string txt = sr.ReadToEnd();
                    sr.Close();
                    if (recordLog)
                    {
                        Trace.WriteLine(txt);
                    }
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                    return txt;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return ex.Message;
            }
        }

    }
}
