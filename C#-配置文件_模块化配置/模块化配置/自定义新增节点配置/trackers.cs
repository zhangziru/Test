using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTest.模块化配置.自定义新增节点配置
{
    public class trackers : ConfigurationElementCollection
    {
        [ConfigurationProperty("TrackerName", IsRequired = false)]
        public string TrackerName
        {
            get { return (string)base["TrackerName"]; }
            set { base["TrackerName"] = value; }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new tracker();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((tracker)element).Host;
        }
    }
}
