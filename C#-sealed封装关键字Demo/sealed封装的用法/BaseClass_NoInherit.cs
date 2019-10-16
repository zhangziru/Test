using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203.sealed封装的用法
{
    /// <summary>
    /// 实例的成员可以正常的访问，但是不能够被继承的基类
    /// <para>类不能继承</para>
    /// </summary>
    public sealed class BaseClass_NoInherit
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }

        public void SayHi()
        {
            Console.WriteLine("BaseClass_NoInherit");
        }
    }
}
