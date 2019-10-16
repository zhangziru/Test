using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203.sealed封装的用法
{
    /// <summary>
    ///实例的成员可以正常的访问,基类可以被继承，其中的某个成员不能够被继承。
    ///<para>类可以继承，成员不能继承</para>
    /// </summary>
    public class BaseClass_Inherit_MemberNoInherit
    {
        /// <summary>
        /// 正常继承，正常访问
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 正常继承，正常访问
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 正常继承，正常访问
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 只需要修改访问修饰符(为private)，就可以实现基类可以被继承，其中的某个成员不能被继承。但是该实例也不能访问。
        /// <para>不能继承，不能访问</para>
        /// </summary>
        private void SayHi()
        {
            Console.WriteLine("BaseClass_Inherit_MemberNoInherit");
        }

        /// <summary>
        /// 被Virtual修饰的访问，必须是公开的，由public来修饰
        /// <para>子类继承后，密封，后续的子类就无法继承该成员了</para>
        /// <para>正常继承，正常访问,子类可以重写（重新定义）</para>
        /// </summary>
        public virtual void SayHiByVirtual()
        {
            Console.WriteLine("BaseClass_Inherit_MemberNoInherit------ByVirtual");
        }
    }
}
