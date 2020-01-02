using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203.VS中调试技巧之查看内存
{
    public class DebuggerTest
    {
        public DebuggerTest()
        {
            //在即时窗口中进行测试
            //1、&varableName:获取变量的栈中地址。
            //2、*&varableName:获取变量在栈中存储的值。
            /*总结：
             *      1、如果是值类型，栈中的地址不相同，值相同
             *      2、如果是引用类型，栈中的地址不行同，栈中的值相同。因为变量是存储在栈上的，而数据的存储空间是堆，所以他们的存储空间的地址是一样的。
             *          任何一个变量中的值改变了，都会影响所有引用该堆地址的变量。【重点】
            */
            MemoryCache_ReferenceTypeAddressTest();//引用类型，内存地址测试
            MemoryCache_ValueTypeAddressTest();//值类型，内存地址测试
        }

        #region 引用类型的内存地址测试
        /// <summary>
        /// 引用类型的内存地址测试
        /// </summary>
        public void MemoryCache_ReferenceTypeAddressTest()
        {
            HappyFuture future1;
            HappyFuture future = new VS中调试技巧之查看内存.HappyFuture();
            future.Name = "焦裕禄";
            //在此处查看 future的内存地址
            DataComplete(future);
        }

        /// <summary>
        /// 数据完善
        /// </summary>
        /// <param name="model"></param>
        public void DataComplete(HappyFuture model)
        {
            //在此处查看model的内存地址
            model.HappinessIndex = -100;
            model.Memo = "母亲称他为“禄子”";
            model.Age = 10;
        }
        #endregion

        /// <summary>
        /// 值类型的内存地址的测试
        /// </summary>
        public void MemoryCache_ValueTypeAddressTest()
        {
            int luckNumber = 20200102;
            //在此处查看luckNumber的内存地址
            DataReset(luckNumber);
        }

        public void DataReset(int temp)
        {
            //在此处查看temp的内存地址
            temp = 2222;
        }
    }
}
