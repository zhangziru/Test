using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp测试项目20181203
{
    [Serializable]
    public class Person
    {
        #region 方法重写
        /// <summary>
        /// 这个 虚方法 用于 测试 子类中的 new关键字
        /// </summary>
        public virtual void SayHi()
        {
            Console.WriteLine("Hello I'm BaseClass!");
        }

        /// <summary>
        /// 这个 虚方法 用于 测试 子类的子类（KindHardMan） 重新父类（GoodMan）中 override 修饰的方法
        /// </summary>
        public virtual void SayBye()
        {
            Console.WriteLine("Good Bye!");
        }
        #endregion

        #region 构造函数
        public Person()
        {
            Console.WriteLine("Person对象初始化！");
        }
        #endregion

    }
}
