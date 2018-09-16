using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiYun_Home_Test.委托BeginInvoke参数测试
{
    //相关链接：http://blog.csdn.net/cj198761/article/details/52727198
    public class DelegateTest
    {
        //委托定义
        public Func<int, string> delega = null;
        //测试方法
        public string GetName(int number)
        {
            //转为16进制
            return number.ToString("x");
        }
        //委托测试1
        public void TestStart()
        {
            string outResult = null;
            bool outResult1 = false;
            delega = new Func<int, string>(GetName);
            delega.BeginInvoke(1, result =>
            {
                outResult = delega.EndInvoke(result);
            }, null);
            Console.WriteLine(outResult1.ToString());
        }
        //委托测试2
        //如果不调用EndInvoke方法，程序会立即退出，这是由于使用BeginInvoke创建的线程都是后台线程，这种线程一旦所有的前台线程都退出后（其中主线程就是一个前台线程），不管后台线程是否执行完毕，都会结束线程，并退出程序。
        public void TestStart2()
        {
            string outResult = null;
            delega = new Func<int, string>(GetName);
            IAsyncResult res = delega.BeginInvoke(2, null, null);
            outResult = delega.EndInvoke(res);
            Console.WriteLine(outResult.ToString());
        }
        //委托测试3  使用IAsyncResult 属性IsCompleted来判断异步调用是否完成
        public void TestStart3()
        {
            string outResult = null;
            delega = new Func<int, string>(GetName);
            IAsyncResult res = delega.BeginInvoke(2, null, null);
            while (!res.IsCompleted)
            {
                Console.WriteLine("正在执行！请稍等。。。");
            }
            Console.WriteLine("调用完成！");
            outResult = delega.EndInvoke(res);
            Console.WriteLine(outResult.ToString());
        }
        //委托测试4  使用WaitOne方法等待异步方法执行完成
        public void TestStart4()
        {
            string outResult = null;
            delega = new Func<int, string>(GetName);
            IAsyncResult res = delega.BeginInvoke(4, null, null);
            //WateOne 在指定的等待时间内调用仍未完成，返回false   第一个参数：0 表示不等待，-1表示永远等待，直到异步调用结束
            while (!res.AsyncWaitHandle.WaitOne(1, false))
            {
                Console.WriteLine("正在执行！请稍等。。。");
            }
            Console.WriteLine("调用完成！");
            outResult = delega.EndInvoke(res);
            Console.WriteLine(outResult.ToString());
        }
    }
}
