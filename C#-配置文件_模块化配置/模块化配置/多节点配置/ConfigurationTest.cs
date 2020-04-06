using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTest.模块化配置.多节点配置
{
    class ConfigurationTest
    {
        /// <summary>
        /// 测试 单节点配置信息 获取
        /// </summary>
        public static void TestConfigInfo()
        {
            #region 单级配置节点测试
            Console.WriteLine("---------------------多级配置节点测试-----------------");
            Console.WriteLine("enabled:" + CustomerMultiConfig.GetConfig().CustomerElementConfig.Enabled);
            Console.WriteLine("connectionString:" + CustomerMultiConfig.GetConfig().CustomerElementConfig.ConnectionString);
            #endregion
        }
    }
}
