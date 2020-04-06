using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTest.模块化配置.自定义新增节点配置
{
    public class TestConfigInfo : ConfigurationSection
    {
        [ConfigurationProperty("trackers", IsDefaultCollection = false)]
        public trackers Trackers { get { return (trackers)base["trackers"]; } }
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public static TestConfigInfo GetConfig()
        {
            return GetConfig("TestConfigInfo");
        }
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="sectionName">xml节点名称</param>
        /// <returns></returns>
        public static TestConfigInfo GetConfig(string sectionName)
        {
            TestConfigInfo section = (TestConfigInfo)ConfigurationManager.GetSection(sectionName);
            if (section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");
            return section;
        }
        [ConfigurationProperty("TestName", IsRequired = false)]
        public string TestName
        {
            get { return (string)base["TestName"]; }
            set { base["TestName"] = value; }
        }
        [ConfigurationProperty("TestID", IsRequired = false)]
        public string TestID
        {
            get { return (string)base["TestID"]; }
            set { base["TestID"] = value; }
        }
    }
}
