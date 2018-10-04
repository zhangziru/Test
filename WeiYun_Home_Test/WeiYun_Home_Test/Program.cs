using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WeiYun_Home_Test;

namespace WeiYun_Home_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 委托BeginInvoke参数测试
            //new 委托BeginInvoke参数测试.DelegateTest().TestStart4();
            #endregion

            #region 判断两个变量是否为同一个对象（是不是使用同一块内存）
            //Equals和==比较的栈中的值，如果是引用类型，栈的值里面的存的堆地址一样。ReferenceEquals比较的是堆中的地址。
            //值类型不一样，每一个变量都是一块单独的内存。Equals和==相同，ReferenceEquals一定不同。
            //引用类型
            //Person p1 = new Person();
            //p1.Name = "张三";
            //Person p2 = new Person();
            //p2.Name = "张三";
            //Person p3 = p1;

            //bool equalResult = p3.Equals(p1);
            //Console.WriteLine("引用类型变量Equals的结果{0}",equalResult);
            //bool equalResult1 = p3==p1;
            //Console.WriteLine("引用类型变量==的结果{0}", equalResult1);
            //bool equalResult2 = object.ReferenceEquals(p3, p1);
            //Console.WriteLine("引用类型变量ReferenceEquals的结果{0}", equalResult2);
            //值类型
            //int a = 1;
            //int b = 2;
            //int c = 1;
            //bool equalResult3 = a.Equals(c);
            //Console.WriteLine("值类型变量Equals的结果{0}", equalResult3);
            //bool equalResult4 = a == c;
            //Console.WriteLine("值类型变量==的结果{0}", equalResult4);
            //bool equalResult5 = object.ReferenceEquals(a, c);
            //Console.WriteLine("值类型变量ReferenceEquals的结果{0}", equalResult5);
            #endregion


            #region Hash表的应用
            //Hashtable hs = new Hashtable();
            //hs.Add("Cyrus", "塞勒斯");
            //foreach (object item in hs.Keys)
            //{
            //    Console.WriteLine("键：{0}，值：{1}", item, hs[item]);
            //}

            //foreach (DictionaryEntry item in hs)
            //{
            //    Console.WriteLine("键：{0}，值：{1}", item.Key, item.Value);
            //}

            #endregion

            #region 判断输入的字符是不是字母
            //string c = Console.ReadLine();
            //while (c != "quit")
            //{
            //    if (char.IsLetter(c[0]))
            //    {
            //        Console.WriteLine("是字母");
            //    }
            //    else
            //    {
            //        Console.WriteLine("不是字母");
            //    }
            //    c = Console.ReadLine();

            //} 
            #endregion


            #region 递归调用
            //Say(); 
            #endregion

            #region 获取所有语言的编码方式
            //if (File.Exists(@"encodings.txt"))
            //{
            //    File.Delete(@"encodings.txt");
            //}
            //EncodingInfo[] infos = Encoding.GetEncodings();
            //foreach (var item in infos)
            //{
            //    File.AppendAllText(@"encodings.txt", string.Format("{0},{1},{2}"+Environment.NewLine, item.CodePage, item.DisplayName, item.Name));
            //}
            #endregion

            #region 获取电脑上的所有磁盘符
            //string[] drives = Directory.GetLogicalDrives();//返回所有磁盘符名称(包含DVD驱动器)
            //foreach (var item in drives)
            //{
            //    Console.WriteLine(item);
            //    DriveInfo drive = new DriveInfo(item);//根据磁盘符名称，创建驱动盘对象
            //    if (drive.DriveType == DriveType.Fixed)//平常我们需要的是固定的盘符（驱动）
            //    {
            //        Console.WriteLine("{0}的类型：{1},文件系统名称：{2}", drive.Name, drive.DriveType, drive.DriveFormat);
            //    }
            //    else
            //    {
            //        Console.WriteLine("{0}的类型：{1},文件系统名称：{2}", drive.Name, drive.DriveType, "驱动盘");
            //    }

            //}
            #endregion

            #region 通过文件流实现大文件拷贝
            // BigFileCopy(@"d:\a.rar", @"e:\abc.rar");
            //加密
            //JiaMi(@"d:\a.rar", @"e:\abc.rar");
            //解密(文件的路径位置调换一下就好了)
            //JiaMi( @"e:\abc.rar", @"e:\ABCD.rar");
            #endregion

            #region 统计字符串中字符串出现的次数
            //string content = "青葡萄，紫葡萄，青葡萄没紫葡萄紫，紫葡萄不吐葡萄皮，不吃葡萄倒吐葡萄皮。";
            //Console.WriteLine(content);
            //string statisticsString = "葡萄";
            //int count = StatisticsStringOccourCount(content, statisticsString);
            //Console.WriteLine("上述内容出现了{0}次{1}", count,statisticsString);
            #endregion

            #region 字符串利用正则来提取内容
            //string msg = "xioahu@qq.com,gogogo@qq.com,zzaaa@163.com,jjjjj@163.com,hhhh@outlook.com";
            ////从字符串中提取字符串
            ////如果想要对已经匹配的字符串再进行分组提取，就用到了“提取组”的功能
            ////通过添加()能实现提取组。
            ////在正则表达式中只要出现了()就表示进行了分组。
            ////小括号既有改变优先级的作用又具有提取组的功能。
            //MatchCollection matchs = Regex.Matches(msg, @"([-a-zA-Z_0-9]+)@([-a-zA-Z_0-9]+(\.[a-zA-Z]+)+)");
            //foreach (Match item in matchs)
            //{
            //    //Console.WriteLine(item.Value);//item.Value表示本次提取到的字符串
            //    //item.Groups集合中存储的就是所有的分组信息
            //    Console.WriteLine("第0组：{0}", item.Groups[0].Value);//item.Groups[0].Value与item.Value是等价的，都表示本次提取到的完整的字符串，表示整个邮箱字符串，而item.Groups[1].Value则表示第一组的字符串。
            //    Console.WriteLine("第1组：{0}", item.Groups[1].Value);
            //    Console.WriteLine("第2组：{0}", item.Groups[2].Value);
            //    Console.WriteLine("第3组：{0}", item.Groups[3].Value);
            //}
            #endregion

            #region 字符串提取，前者是对一个匹配项进行了分组，后者是对一个字符串提取了多个匹配项
            ////方法一
            //string date = " June   26, 1951 ";
            //Match match = Regex.Match(date, @"([a-zA-Z]+)\s*([0-9]{2})\s*,\s*([0-9]{4})\s*");
            //for (int i = 0; i < match.Groups.Count; i++)
            //{
            //    Console.WriteLine(match.Groups[i].Value);
            //}
            //Console.WriteLine("-----------------------------------------------------------");
            ////方法二

            //MatchCollection matches = Regex.Matches(date, @"[a-zA-Z0-9]+");
            //foreach (Match item in matches)
            //{
            //    Console.WriteLine(item.Value);
            //}
            ////这两种写法有什么区别，首先思路是完全不同的。前者是对一个匹配项进行了分组，后者是对一个字符串提取了多个匹配项

            #endregion

            #region 正则表达式的“贪婪模式”和“终止贪婪模式”
            ////贪婪模式,按照尽可能多的来匹配
            //string msg = "1111。11。111。1。1。";
            //Match match = Regex.Match(msg,".+。");
            //Console.WriteLine(match.Value);
            ////终止贪婪模式,它就会尽可能按照少的进行匹配
            ////终止贪婪模式，在表达式后面的限定符后面加上一个"?"就表示【终止贪婪模式】
            //Console.WriteLine("终止贪婪模式");
            //Match match1 = Regex.Match(msg, ".+?。");
            //Console.WriteLine(match1.Value);
            #endregion

            #region 通过WebClient类对象下载网页字符串
            //WebClient webclient = new WebClient();
            //string html = webclient.DownloadString("http://www.baidu.com");//自己找一个有邮箱的地址进行测试
            ////从网页字符串中进行字符串的提取邮箱
            //MatchCollection matches = Regex.Matches(html, @"[a-zA-Z0-9_.]+@[a-zA-Z0-9]+(\.[a-zA-Z]){1,2}");
            //foreach (Match item in matches)
            //{
            //    Console.WriteLine(item.Value);
            //}
            //Console.WriteLine("共{0}个邮箱地址。",matches.Count);
            #endregion

            #region 通过WebClient提取网页中的图片
            ////1、下载Html
            //WebClient client = new WebClient();
            //string html = client.DownloadString("http://www.27270.com/ent/meinvtupian/");

            ////2、提取Html中的img标签
            //MatchCollection matches = Regex.Matches(html, @"<img.+?src=""(.+?)"".+?>",RegexOptions.IgnoreCase);
            //foreach (Match item in matches)
            //{
            //    //3、通过“提取组”获取img的src属性
            //    //Console.WriteLine(item.Value+"\t"+item.Groups[1].Value);
            //    //Console.WriteLine(item.Groups[1].Value);
            //    string img_path = item.Groups[1].Value;
            //    //4、下载图片（如果是相对相对地址，通过拼接路径下载图片。远程在线的图片则直接下载）
            //    //5、下载图片还要用WebClient对象
            //    client.DownloadFile(img_path,@"d:\" + System.DateTime.Now.ToString("yyyyMMddhhmmss")+".jpg");
            //}

            //Console.WriteLine("下载完毕，ok!");
            #endregion

            Console.Read();
        }
        /// <summary>
        /// 递归调用
        /// </summary>
        static void Say()
        {
            Console.WriteLine("山上有座庙");
            Console.WriteLine("庙里有个老和尚和小和尚");
            Console.WriteLine("有一天，老和尚给小和尚讲故事：");
            Console.WriteLine("讲的是：");
            Say();//调用自己，递归调用。
        }
        /// <summary>
        /// 通过文件流实现将source中的文件拷贝到target
        /// </summary>
        /// <param name="source">原文件路径</param>
        /// <param name="target">目标文件路径</param>
        static void BigFileCopy(string source , string target)
        {
            //1、创建一个读取源文件的文件流
            using (FileStream fsRead = new FileStream(source, FileMode.Open, FileAccess.Read))
            {
                //2、创建一个写入新文件的文件流
                using (FileStream fsWrite = new FileStream(target, FileMode.Create, FileAccess.Write))
                {
                    //缓冲区的选择，选多大合适呢？缓冲区大了，保护硬盘不伤硬盘什么的。缓冲区小了，eg：1kb，那么每读1kb都要向磁盘中写。缓冲区小了，那么频率就大了。缓冲区大了，读半天写一次，对硬盘操作就少了，这样来说就是保护硬盘了。
                    byte[] bytes = new byte[1024 * 1024 * 5];//缓冲区的设置
                    int r = fsRead.Read(bytes, 0, bytes.Length);//返回值，表示本次实际读取到字节个数
                                                                //上边已经读了一次，判断读到内容是不是空
                    while (r > 0)
                    {
                        //将读取到的内容写入到新文件中
                        fsWrite.Write(bytes, 0, r);//第三个参数应该是实际读取到的字节数，而不是数组的长度
                        //在这里，我们每次写入完毕以后，可以显示拷贝了百分之几，显示一个拷贝进度
                        //Console.WriteLine(".");//这里先暂时显示一个点，可以做成进度条，从这里入手。显示百分比怎么来做呢？就是用已经写入的文件流除以总的文件长度就可以了。
                        double d = (fsWrite.Position / (double)fsRead.Length) * 100;
                        Console.WriteLine("{0}%",d);
                        Thread.Sleep(1000);
                        r = fsRead.Read(bytes, 0, bytes.Length);//返回值，表示本次实际读取到字节个数
                                                                    //上边的文件读取文件流，每次读取的时候，他怎么知道本次从哪个位置开始读取。它有一个属性：fsWrite.Postion.表示获取或者设置此流的当前位置，默认值是自动的移到你上次读取或者写位置。当然你也可以手动设置一下，这样你每次读的东西都是一样的
                    }//此处可以用do-while循环
                }
            }
        }

        /// <summary>
        /// 文件加密拷贝，文件打开为损坏，再解密一下就好了
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        static void JiaMi(string source, string target)
        {
            //1、创建一个读取源文件的文件流
            using (FileStream fsRead = new FileStream(source, FileMode.Open, FileAccess.Read))
            {
                //2、创建一个写入新文件的文件流
                using (FileStream fsWrite = new FileStream(target, FileMode.Create, FileAccess.Write))
                {
                    byte[] bytes = new byte[1024 * 1024 * 5];//缓冲区的设置
                    int count = 0;//返回值，表示本次实际读取到字节个数
                    //while的这种写法，可以说是do-while的新版本
                    while ((count = fsRead.Read(bytes, 0, bytes.Length)) > 0)
                    {
                        //加密，加密其实就是先把Bbyte[]数组中的字节内容先改变一下，然后在执行写入操作
                        for (int i = 0; i < count; i++)
                        {
                            bytes[i] = (byte)(byte.MaxValue - bytes[i]);
                        }
                        //拷贝，直接写拷贝代码
                        fsWrite.Write(bytes, 0, count);

                    }//此处可以用do-while循环
                }
            }
        }
        /// <summary>
        /// 统计字符串中字符串出现的次数
        /// </summary>
        /// <param name="content"></param>
        /// <param name="statisticsString"></param>
        /// <returns></returns>
        static int StatisticsStringOccourCount(string content ,string statisticsString)
        {
            int count = 0;//声明一个统计变量
            int index = 0;//声明一个索引变量,相当于游标
            while ((index = content.IndexOf(statisticsString, index)) > -1)
            {
                count++;
                index = index + statisticsString.Length;//索引位置的移动
            }
            return count;
        }
    }
}
