using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203
{
    public abstract class GoodMan : Person
    {
        #region 方法重写
        #region new 关键的验证
        /// <summary>
        /// new关键字的使用，隐藏基类中的虚方法
        /// </summary>
        public new void SayHi() 
        #endregion
        {
            Console.WriteLine("Hello I'm GoodMan!");
        }

        #region sealed 关键字的验证
        /// <summary>
        /// sealed 的使用，后面的派生类，将无法继续重写 基类的虚方法（到此为止了）
        /// <para>该方法，派生类可以继承来使用，但是无重写的权限。</para>
        /// <para>重写紧记一点：被重写以后，当前对象调用该方法都是重写后的方法。【重点，一定是当前对象，不管它存到了哪里，尤其注意“里氏替换原则”】</para>
        /// </summary>
        public override sealed void SayToGoodMan()
        {
            Console.WriteLine("GoodMan 独特的一面，后代不可替代！");
        } 
        #endregion

        /// <summary>
        /// 重写父类的基方法（可被子类override[重写]）
        /// </summary>
        public override void SayBye()
        {
            Console.WriteLine("GoodMan（好人），我们再会！");
        }

        /// <summary>
        /// 自身的虚方法（可被子类override[重写]）
        /// </summary>
        public virtual void DoSomethings()
        {
            Console.WriteLine("GoodMan(好人)，总是在默默无闻的做事情！");
        }

        /// <summary>
        /// 只有抽象类，才可以有抽象的成员
        /// <para>抽象方法成员（可被子类override[重写]）</para>
        /// </summary>
        public abstract void FuturityThings();
        #endregion


        #region 构造函数
        public GoodMan()
        {
            Console.WriteLine("GoodMan对象初始化！");
        } 
        #endregion
    }
}
