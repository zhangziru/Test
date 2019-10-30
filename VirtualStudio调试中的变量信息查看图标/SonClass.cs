using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203
{
    public class SonClass : BaseClass
    {
        private int Son_Field0 = 0;

        private int Son_Property0_Private { get; set; }
        protected int Son_Property1_Protected { get; set; }
        public int Son_Property2_Public { get; set; }
    }
}
