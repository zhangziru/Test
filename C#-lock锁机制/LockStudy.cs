using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp测试项目20181203
{
    #region 锁的描述
    //1、各个线程同时运行，其间不相互依赖和等待,但当不同的线程都需要访问某个资源的时候，就需要同步机制了 
    //2、lock是C#中最常用的同步方式，格式为lock(objectA){codeB} 。
    #region 锁的理接
    //lock(objectA){codeB}
    //看似简单，实际上有三个意思，这对于适当地使用它至关重要：
    //1. objectA被lock了吗？没有则由我来lock，否则一直等待，直至objectA被释放。
    //2. lock以后在执行codeB的期间其他线程不能调用codeB，也不能使用objectA。
    //3. 执行完codeB之后释放objectA，并且codeB可以被其他线程访问。
    #endregion   
    #endregion
    //原文链接：https://www.cnblogs.com/gsk99/p/4964063.html
    //C# this表示引用该类的当前实例
    #region 问题
    //问题：C# 类调用静态成员，会调用该类的构造函数吗？ 
    //不会。

    #endregion

    /// <summary>
    /// C#中锁的理解
    /// </summary>
    public class LockStudy
    {
        private bool deadlocked = true;
        private object locker = new object();
        #region 默认构造函数
        public LockStudy()
        {
            //调用构造函数
            Console.WriteLine("调用LockStudy()构造函数");
        } 
        #endregion

        #region 锁机制的测试入口
        //这个方法用到了lock，我们希望lock的代码在同一时刻只能由一个线程访问
        /// <summary>
        /// 锁机制的测试入口
        /// </summary>
        public static void TestMain()
        {
            Thread.CurrentThread.Name = "主线程";
            Console.WriteLine("进入{0}", Thread.CurrentThread.Name);
            LockStudy c1 = new LockStudy();
            //在t1线程中调用LockMe，并将deadlock设为true（将出现死锁）
            Thread t1 = new Thread(c1.LockMe) { Name = "子线程1" };
            t1.Start(true);
            Thread.Sleep(100);
            //在主线程中lock c1
            lock (c1)
            {
                //调用没有被lock的方法
                c1.DoNotLockMe();
                //调用被lock的方法，并试图将deadlock解除
                c1.LockMe(false);
            }
            #region 结果说明
            //结果的说明：在t1线程（子线程1）中，LockMe调用了lock(this), 也就是TestMain函数中的c1对象，这时候在主线程中调用lock(c1)时，必须要等待t1（子线程1）中的lock块执行完毕之后才能访问c1对象，即所有c1对象相关的操作都无法完成，于是我们看到连主线程中c1.DoNotLockMe()都没有执行。 
            #endregion
            #region 代码更改后，结果说明
            //代码更改后，结果的说明：这次我们使用一个私有成员作为锁定变量(locker)，在LockMe中仅仅锁定这个私有locker，而不是整个对象。这时候重新运行程序，可以看到虽然t1出现了死锁，DoNotLockMe()仍然可以由主线程访问；LockMe()依然不能访问，原因是其中锁定的locker还没有被t1释放。 
            #endregion
        }
        #endregion

        #region 加锁的方法
        /// <summary>
        /// 加锁的方法
        /// </summary>
        /// <param name="o"></param>
        public void LockMe(object o)
        {           
            lock (locker)
            {
                while (deadlocked)
                {
                    Console.Write(Thread.CurrentThread.Name + "=>");

                    deadlocked = (bool)o;
                    Console.WriteLine("Foo: I am locked :(");
                    Thread.Sleep(500);
                }
            }
        } 
        #endregion

        #region 所有线程都可以同时访问的方法
        /// <summary>
        /// 所有线程都可以同时访问的方法
        /// </summary>
        public void DoNotLockMe()
        {
            Console.Write(Thread.CurrentThread.Name + "=>");
            Console.WriteLine("I am not locked :)");
        }  
        #endregion
    }
}
