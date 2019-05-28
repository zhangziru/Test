using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp测试项目20181203
{
    //参考链接：
    //1、从Windows cmd命令行打开共享文件夹:https://blog.csdn.net/gaofenglian/article/details/70170655
    //2、C#读写共享文件夹:https://www.cnblogs.com/sxw117886/p/5619744.html 【其中有个致命的错误："NET HELPMSG 2250"】
    //3、解决方案：proc.StandardInput.AutoFlush = true;添加这个属性。参考的连接： https://www.cnblogs.com/dotnet261010/p/7087290.html  
    public class ShareFile
    {
        /// <summary>
        /// 共享网络的连接状态
        /// </summary>
        public bool NetConnectedState { get; set; }

        public ShareFile()
        {
            //连接共享文件夹
            NetConnectedState = connectState(@"\\192.168.10.174", "test", "111111");
            if (NetConnectedState)
            {
                try
                {
                    //共享文件夹的目录
                    DirectoryInfo theFolder = new DirectoryInfo(@"\\192.168.10.174\dddd");
                    //相对共享文件夹的路径
                    string backupPath = theFolder + "/Backup";
                    //获取保存文件的路径
                    string sharePath = theFolder.ToString();

                    if (!Directory.Exists(backupPath))
                    {
                        Directory.CreateDirectory(backupPath);
                    }

                    //执行方法                
                    //Transport(@"D:\1.png", filename, "1.png");
                    Thread listenEBOM = new Thread(() =>
                    {
                        while (NetConnectedState)
                        {
                            try
                            {
                                string[] fiels = Directory.GetFiles(sharePath);

                                foreach (var item in fiels)
                                {
                                    File.Copy(item, Path.Combine(backupPath, Path.GetFileName(item)),true);//先备份，后下载，再删除
                                    Transport(item, AppDomain.CurrentDomain.BaseDirectory + "EBOM/", Path.GetFileName(item));
                                }

                                Thread.Sleep(5000);
                            }
                            catch (Exception ex)
                            {
                                //文件过大，可能导致线程资源被占用，停滞60秒后自动继续。
                                Thread.Sleep(1000 * 60);
                                continue;
                            }
                        }
                    });
                    listenEBOM.IsBackground = true;
                    listenEBOM.Start();


                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                //ListBox1.Items.Add("未能连接！");
            }
            Thread.Sleep(10000);
            BomOneProject();
            Console.ReadKey();
        }

        #region 说明
        //参考链接：https://www.cnblogs.com/dotnet261010/p/7087290.html 
        #endregion

        /// <summary>
        /// 连接远程共享文件夹
        /// </summary>
        /// <param name="path">远程共享文件夹的路径</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public bool connectState(string path, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                //当前共享 未连接，需要
                proc.StandardInput.WriteLine("net use " + path + " /del /y");
                string dosLine = "net use " + path + " " + passWord + " /user:" + userName;
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                proc.StandardInput.AutoFlush = true;
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                }
                else
                {
                    throw new Exception(errormsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }


        /// <summary>
        /// 向远程文件夹保存本地内容，或者从远程文件夹下载文件到本地
        /// </summary>
        /// <param name="src">要保存的文件的路径，如果保存文件到共享文件夹，这个路径就是本地文件路径如：@"D:\1.avi"</param>
        /// <param name="dst">保存文件的路径，不含名称及扩展名</param>
        /// <param name="fileName">保存文件的名称以及扩展名</param>
        public static void Transport(string src, string dst, string fileName)
        {
            try
            {
                FileStream inFileStream = new FileStream(src, FileMode.Open);
                if (!Directory.Exists(dst))
                {
                    Directory.CreateDirectory(dst);
                }
                dst = dst + fileName;
                FileStream outFileStream = new FileStream(dst, FileMode.OpenOrCreate);

                byte[] buf = new byte[inFileStream.Length];

                int byteCount;

                while ((byteCount = inFileStream.Read(buf, 0, buf.Length)) > 0)
                {
                    outFileStream.Write(buf, 0, byteCount);
                }

                inFileStream.Flush();

                inFileStream.Close();

                outFileStream.Flush();

                outFileStream.Close();
                //从服务器端下载完毕 就删除服务器端的文件
                File.Delete(src);
            }
            catch (Exception)
            {
                throw;
            }

        }

        #region BomOne解压缩
        /// <summary>
        /// 如果 本地不需要这些文件，压根就不用将文件 再写一遍，直接读取其中的信息即可。
        /// </summary>
        public void BomOneProject()
        {
            //解压完毕以后，将压缩文件删除掉
            string[] files =  Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "EBOM");
            foreach (var item in files)
            {
                string pathDecompression = AppDomain.CurrentDomain.BaseDirectory + "EBOM/";//解压路径
                string targetZip = item;//目标文件
                pathDecompression = Path.Combine(pathDecompression, Path.GetFileNameWithoutExtension(item));
                //解压路径不存在，就创建
                if (!Directory.Exists(pathDecompression))
                {
                    Directory.CreateDirectory(pathDecompression);
                }

                //保存临时文件
                Hashtable imagelist = new Hashtable();

                using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(targetZip)))
                {
                    ZipEntry ent = null;

                    while ((ent = zipStream.GetNextEntry()) != null)
                    {
                        if (!string.IsNullOrEmpty(ent.Name))
                        {
                            FileInfo fi = new FileInfo(ent.Name);

                            string fileName = Path.Combine(pathDecompression, ent.Name);
                            fileName = fileName.Replace('/', '\\');//change by Mr.HopeGi   

                            if (fileName.EndsWith("\\"))
                            {
                                Directory.CreateDirectory(fileName);
                                continue;
                            }

                            if (!Regex.IsMatch(ent.Name, @".+\.(xls|xlsx)$"))
                            {
                                imagelist.Add(ent.Name.Split('_')[1] + "_" + ent.Name.Split('_')[2], fileName);
                            }
                            //解压缩到项目目录
                            using (FileStream streamWriter = File.Create(fileName))
                            {
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = zipStream.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                File.Delete(item);
            }

           
        }
        #endregion
    }
}
