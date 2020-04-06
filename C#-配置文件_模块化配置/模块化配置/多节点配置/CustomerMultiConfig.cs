using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTest.模块化配置.多节点配置
{
    /// <summary>
    /// 多级配置文件自定义节点配置
    /// </summary>
    public class CustomerMultiConfig : ConfigurationSection
    {
        private const string CustomerConfigPropertyName = "CustomerElement";
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public static CustomerMultiConfig GetConfig()
        {
            return GetConfig("CustomerMultiConfig");
        }
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="sectionName">xml节点名称</param>
        /// <returns></returns>
        public static CustomerMultiConfig GetConfig(string sectionName)
        {
            CustomerMultiConfig section = (CustomerMultiConfig)ConfigurationManager.GetSection(sectionName);
            if (section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");
            return section;
        }
        [ConfigurationProperty(CustomerConfigPropertyName)]
        public CustomerElement CustomerElementConfig
        {
            get { return (CustomerElement)base[CustomerConfigPropertyName]; }
            set { base[CustomerConfigPropertyName] = value; }
        }
    }
}
