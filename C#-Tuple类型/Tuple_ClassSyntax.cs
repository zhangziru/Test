using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203
{
    //参考链接：https://docs.microsoft.com/zh-cn/dotnet/api/system.tuple?redirectedfrom=MSDN&view=netframework-4.8

    /// <summary>
    /// Tuple类型的语法，元素元组：元素的组合 也是集合中最基本的单元,所以称为元组。（个人理解）
    /// <para>主要解决的问题：从方法返回多个值，而无需使用out参数【重点】 </para>
    /// <para> .NET Framework 直接支持具有 1 到 7 元素元组，超过7个使用Tuple元素元组嵌套</para>
    /// <para> 直接对元素封装成一个整体，泛型方法的类型指定，可以通过元素 推断出来</para>
    /// <para>应用：将多个值传递给通过单个参数的方法。eg：Thread.Start(Object)方法只有一个参数，可提供一个值，则该线程在启动时执行的方法。</para>
    /// </summary>
    public class Tuple_ClassSyntax
    {
        public Tuple_ClassSyntax()
        {

            this.SimpleDemo();//7个元素，为边界，7个以下用法相同
            this.ComplexDemo();//7个以上元素用法相同
        }

        /// <summary>
        /// 方法的返回值 刚好7个元素的元组。
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int, int, int, int, int> SimpleDemo()
        {
            var primes = Tuple.Create(2, 3, 5, 7, 11, 13, 17);
            Console.WriteLine("Prime numbers less than 18: " +
                              "{0}, {1}, {2}, {3}, {4}, {5}, {6}, and {7}",
                              primes.Item1, primes.Item2, primes.Item3,
                              primes.Item4, primes.Item5, primes.Item6,
                              primes.Item7);
            return primes;
        }

        /// <summary>
        /// 方法的返回值 超过7个元素的元组。
        /// <para>使用 Tuple（元素元组）的嵌套</para>
        /// </summary>
        /// <returns>,primes.Rest是超过7个元素的其余元素集合，取其中的值跟 7个以下的方式相同，通过 Item+索引 来取</returns>
        public Tuple<int, int, int, int, int, int, int, Tuple<int>> ComplexDemo()
        {
            var primes = Tuple.Create(2, 3, 5, 7, 11, 13, 17, 19);
            Console.WriteLine("Prime numbers less than 20: " +
                              "{0}, {1}, {2}, {3}, {4}, {5}, {6}, and {7}",
                              primes.Item1, primes.Item2, primes.Item3,
                              primes.Item4, primes.Item5, primes.Item6,
                              primes.Item7, primes.Rest.Item1);
            return primes;
        }
    }
}
