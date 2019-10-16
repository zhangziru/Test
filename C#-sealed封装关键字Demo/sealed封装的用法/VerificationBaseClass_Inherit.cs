using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203.sealed封装的用法
{
    /// <summary>
    /// 验证基类可以继承
    /// </summary>
    public class VerificationBaseClass_Inherit : BaseClass_Inherit_MemberNoInherit
    {
        /// <summary>
        /// 该方法已被密封，后续子类仍可以继承该成员，当前类的实例可以正常访问该成员。但是后续的子类无法再继续重写该方法。【重点】
        /// <para>结论：sealed用于类，可以实现当前类被其他的类继承。sealed用于方法，可以实现被重写的方法不被后续子类重写，在对应的子类实例中，该方法都可以正常的访问。</para>
        /// <para>结论：方法中只有应用了virtual关键字，该方法才可以被子类进行重写。在方法中应用sealed关键字，就相当于清除了virtual的功能。</para>
        /// <para>正常继承，正常访问,子类不能重写（不能重新定义）</para>
        /// </summary>
        public sealed override void SayHiByVirtual()
        {
            base.SayHiByVirtual();
        }
    }
}
