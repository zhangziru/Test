using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203
{
    /// <summary>
    /// 压缩 与 解压缩 的大练兵
    /// </summary>
    public class GZipDemo
    {
        public GZipDemo()
        {
            CompressedFile(@"D:\公司信息.txt");
            DecompressionFile(AppDomain.CurrentDomain.BaseDirectory + "公司信息.zip");
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        public void CompressedFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("{0}:该文件不存在，请检查路径是否错误！", path);
                return;
            }
            using (FileStream fsRead = File.OpenRead(path))
            {
                //打开现有的文件或者创建一个压缩文件
                using (FileStream fsWrite = File.OpenWrite(Path.GetFileNameWithoutExtension(path) + ".zip"))
                {
                    using (GZipStream zipStream = new GZipStream(fsWrite, CompressionMode.Compress))
                    {
                        byte[] buffer = new byte[1024];
                        int len = 0;

                        do
                        {
                            len = fsRead.Read(buffer, 0, buffer.Length);
                            zipStream.Write(buffer, 0, len);
                        } while (len > 0);
                    }
                }
            }

        }

        //解压文件
        public void DecompressionFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("{0}:该文件不存在，请检查路径是否错误！", path);
                return;
            }
            using (FileStream fsRead = File.OpenRead(path))
            {
                using (GZipStream zipStream = new GZipStream(fsRead, CompressionMode.Decompress))
                {
                    using (FileStream fsWrite = File.OpenWrite(Path.GetFileNameWithoutExtension(path) + ".txt"))
                    {
                        byte[] buffer = new byte[1024];
                        int len = 0;
                        do
                        {
                            len = zipStream.Read(buffer, 0, buffer.Length);
                            fsWrite.Write(buffer, 0, len);
                        } while (len > 0);
                    }
                }
            }
        }
    }
}
