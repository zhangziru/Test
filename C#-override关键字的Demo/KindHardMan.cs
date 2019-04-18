using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203
{
    #region override使用的Demo
    //说明：为了演示override(重写) ，主要有三种情况：
    //1、重写的基方法必须是 virtual
    //2、重写的基方法必须是 abstract
    //3、重写的基方法必须是 override,即父类中已经重新过的方法
    #endregion

    /// <summary>
    /// 善良的人，基类是好人
    /// </summary>
    public abstract class KindHardMan : GoodMan
    {
        /// <summary>
        /// 重新基类中virtual方法
        /// </summary>
        public override void DoSomethings()
        {
            Console.WriteLine("继承好人，我成为了善良的人！");
        }

        /// <summary>
        /// 基类中重写祖父的方法，可以被后代继续重写
        /// </summary>
        public override void SayBye()
        {
            Console.WriteLine("KindHardMan(善良的人)，我们再会！");
        }

        /// <summary>
        /// 重写基类中抽象（abstract）的方法
        /// </summary>
        public override void FuturityThings()
        {
            Console.WriteLine("人类普遍的坏习惯，未来善良的人不会有！");
        }
    }
}
