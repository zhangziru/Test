using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTest.模块化配置.自定义新增节点配置
{
    class ConfigurationTest
    {
        /// <summary>
        /// 测试 单节点配置信息 获取
        /// </summary>
        public static void TestConfig()
        {
            Console.WriteLine("---------------------自定义新增节点测试-----------------");
            Console.WriteLine("TestID:" + TestConfigInfo.GetConfig().TestID);
            Console.WriteLine("TestName:" + TestConfigInfo.GetConfig().TestName);
            foreach (tracker item in TestConfigInfo.GetConfig().Trackers)
            {
                Console.WriteLine("Host:" + item.Host + " Port:" + item.Port);
            }
        }
    }
}
