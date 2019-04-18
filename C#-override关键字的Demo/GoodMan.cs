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
        /// <summary>
        /// new关键字的使用，隐藏基类中的虚方法
        /// </summary>
        public new void SayHi()
        {
            Console.WriteLine("Hello I'm GoodMan!");
        }
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
