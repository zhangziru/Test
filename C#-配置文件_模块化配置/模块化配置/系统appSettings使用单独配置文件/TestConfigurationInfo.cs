using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTest.模块化配置.系统appSettings使用单独配置文件
{
    class TestConfigurationInfo
    {
        /// <summary>
        /// 测试 单节点配置信息 获取
        /// </summary>
        public static void TestConfigInfo()
        {
            Console.WriteLine("---------------------系统自带appSettings配置文件-----------------");
            Console.WriteLine("logLevel:" + System.Configuration.ConfigurationManager.AppSettings["logLevel"]);
            Console.WriteLine("LogType:" + System.Configuration.ConfigurationManager.AppSettings["LogType"]);
        }
    }
}
