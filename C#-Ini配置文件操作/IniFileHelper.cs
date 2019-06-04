using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203
{
    #region ini配置文件的定义规范
    //ini配置文件的定义规范，参考链接：https://www.cnblogs.com/renyuan/p/4111695.html
    //1、INI文件由节、键、值组成。  
    //**节
    //      [section]
    //**参数（键=值） 
    //      name=value
    //**注解
    //      注解使用分号表示（;）。在分号后面的文字，直到该行结尾都全部为注解。
    //2、对于一个section没有明显的结束标志符，一个section的开始就是上一个section的结束，或者是end of the file（文件的结尾）。
    //3、注释：在分号和行结束符之间的所有内容都是被忽略的。
    //4、上述的经典的 ini文件 定义方式，随着发展，出现了很多ini的变种。
    #endregion

    #region ini配置文件操作 Demo
    //本篇ini配置文件操作的代码，参考链接：https://www.cnblogs.com/falcon-fei/p/9849868.html
    //kernel32.dll是Windows中非常重要的32位动态链接库文件，属于内核级文件。 
    #endregion

    #region 说明一个问题（如何引入 C#非托管资源）
    //原文参考链接：https://www.cnblogs.com/taowd/p/7061368.html
    //说明一个问题：
    //1、程序集通过添加引用的程序集，就算添加到了 .NetFramework的CLR的托管平台了。我们就可以正常的使用程序集中的类 来操作了。
    //2、程序集没有通过引用添加到程序集，即：没有添加.NetFramwork的CLR托管平台中。我们如何使用 其中的类型，其中的方法 呢？
    //2.1、非托管程序集中的方法使用，如本篇中的代码。【重点】
    //2.2、非托管程序集中的类型如何使用，等待进一步的探索。 这个好像不常用的，如果有这样的需求，直接将该程序集添加 托管程序集中 直接使用就好。
    //3、extern关键字，表示：该方法是在外部定义的。【重点】
    #endregion

    public class IniFileHelper
    {
        string strIniFilePath;  // ini配置文件路径

        // 返回0表示失败，非0为成功
        [DllImport("kernel32", CharSet = CharSet.Auto)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        // 返回取得字符串缓冲区的长度
        [DllImport("kernel32", CharSet = CharSet.Auto)]
        private static extern long GetPrivateProfileString(string section, string key, string strDefault, StringBuilder retVal, int size, string filePath);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetPrivateProfileInt(string section, string key, int nDefault, string filePath);

        /// <summary>
        /// 无参构造函数
        /// </summary>
        /// <returns></returns>
        public IniFileHelper()
        {
            this.strIniFilePath = Directory.GetCurrentDirectory() + "\\Properties\\sysconfig.ini";
            if (!Directory.Exists(Path.GetDirectoryName(this.strIniFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(this.strIniFilePath));
            }
            if (!File.Exists(this.strIniFilePath))
            {
                File.Create(this.strIniFilePath);
            }
        }


        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="strIniFilePath">ini配置文件路径</param>
        /// <returns></returns>
        public IniFileHelper(string strIniFilePath)
        {
            if (strIniFilePath != null)
            {
                this.strIniFilePath = strIniFilePath;
            }
            if (!Directory.Exists(Path.GetDirectoryName(this.strIniFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(this.strIniFilePath));
            }
            if (!File.Exists(this.strIniFilePath))
            {
                File.Create(this.strIniFilePath);
            }
        }


        /// <summary>
        /// 获取ini配置文件中的字符串
        /// </summary>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <param name="strDefault">默认值</param>
        /// <param name="retVal">结果缓冲区</param>
        /// <param name="size">结果缓冲区大小（以字节为单位，输出的是Unicode字符，Unicode是2个字节，所以这里的大小必须是2的倍数）</param>
        /// <returns>成功true,失败false</returns>
        public bool GetIniString(string section, string key, string strDefault, StringBuilder retVal, int size)
        {
            long liRet = GetPrivateProfileString(section, key, strDefault, retVal, size, strIniFilePath);
            return (liRet >= 1);
        }


        /// <summary>
        /// 获取ini配置文件中的整型值
        /// </summary>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <param name="nDefault">默认值</param>
        /// <returns></returns>
        public int GetIniInt(string section, string key, int nDefault)
        {
            return GetPrivateProfileInt(section, key, nDefault, strIniFilePath);
        }


        /// <summary>
        /// 往ini配置文件写入字符串
        /// </summary>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <param name="val">要写入的字符串</param>
        /// <returns>成功true,失败false</returns>
        public bool WriteIniString(string section, string key, string val)
        {
            long liRet = WritePrivateProfileString(section, key, val, strIniFilePath);
            return (liRet != 0);
        }


        /// <summary>
        /// 往ini配置文件写入整型数据
        /// </summary>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <param name="val">要写入的数据</param>
        /// <returns>成功true,失败false</returns>
        public bool WriteIniInt(string section, string key, int val)
        {
            return WriteIniString(section, key, val.ToString());
        }

        public void IniDemoTest()
        {
            IniFileHelper ini = new IniFileHelper();
            //写入字符串
            //ini.WriteIniString("Connection", "name", "ConStr");
            //ini.WriteIniString("Connection", "value", "192.168.10.147");
            //写入整形
            //ini.WriteIniInt("Const", "LuchNumber", 6);
            //ini.WriteIniInt("Const", "Port", 5000);
            //读取ini文件数据
            int result = ini.GetIniInt("Const", "Port", 0);
            StringBuilder sb = new StringBuilder();
            bool flag = ini.GetIniString("Connection", "name", "NULL", sb, 2048);
            string result1 = sb.ToString();
        }
    }
}
