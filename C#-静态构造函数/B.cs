using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203.静态构造函数
{
    #region 说明
    /*在调用B的代码之前：
     * 1、先执行：B的静态构造函数 
     *  1）静态构造函数先初始化类型的静态成员变量。
     *  2）再执行：静态构造函数体内的语句。
     *  3）因此，调用 new B();，先打印a1,再打a3。
     *2、再执行：B的实例构造函数
     *  1）实例构造函数先初始化类型的实例成员变量。
     *  2）再执行：实例构造函数体内的语句。
     *  3）因此，调用 new B();，后续打印a2,再打a4。
    */
    #endregion

    /// <summary>
    /// 业务类
    /// </summary>
    class B
    {
        /// <summary>
        /// 静态成员a1
        /// </summary>
        static A a1 = new A("a1");

        /// <summary>
        /// 实例成员a2
        /// </summary>
        A a2 = new A("a2");

        /// <summary>
        /// 静态构造函数
        /// <para>这个函数的特点：在类型第一次被使用之前,由运行时自动调用，而且保证只调用一次。</para>
        /// </summary>
        static B() {
            a1 = new A("a3");
        }

        /// <summary>
        /// 实例构造函数
        /// </summary>
        public B()
        {
            a2 = new A("a4");
        }
    }
}
