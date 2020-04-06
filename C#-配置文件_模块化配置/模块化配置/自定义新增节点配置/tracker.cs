using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTest.模块化配置.自定义新增节点配置
{
    public class tracker : ConfigurationElement
    {
        #region 配置即设置，设定当中有不能识别的元素、属性时，使其不报错

        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            return base.OnDeserializeUnrecognizedAttribute(name, value);

        }

        protected override bool OnDeserializeUnrecognizedElement(string elementName, System.Xml.XmlReader reader)
        {
            return base.OnDeserializeUnrecognizedElement(elementName, reader);

        }
        #endregion

        [ConfigurationProperty("Host", DefaultValue = "localhost", IsRequired = true)]
        public string Host { get { return this["Host"].ToString(); } }

        [ConfigurationProperty("Port", DefaultValue = "22122", IsRequired = true)]
        public int Port { get { return (int)this["Port"]; } }

    }
}
