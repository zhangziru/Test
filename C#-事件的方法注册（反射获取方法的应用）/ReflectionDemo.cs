using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203
{
    //需求：在构造函数中，通过反射的方式实现当前类型中的 具有特定前缀（SendTo）的方法 注册到 SendTo事件。
    //1、微软经验（源码）：方法的注册使用的不是事件，而是 MethodInfo 对象,然后去通过Invoke调用方法。【也是实现的方式的一种】
    //2、c#反射入门篇(Reflection)——MethodInfo 发现方法的属性 ，参考的链接：https://blog.csdn.net/okaiguo/article/details/91492108
    //3、本篇通过事件来 进行方法的注册。【重点】
    public class ReflectionDemo
    {
        public event Action SendTo;
        public event Action ReceiveFrom;

        public ReflectionDemo()
        {
            #region 反射 事件注册
            var methods = this.GetType().GetMethods();
            foreach (MethodInfo method in methods)
            {

                #region SendTo事件注册
                if (method.Name.StartsWith("SendTo"))
                {
                    if (!method.IsStatic)
                    {                        
                        //方法注册事件，方式一(只能注册实例的方法。非静态方法)
                        SendTo += method.CreateDelegate(typeof(Action), this) as Action;
                    }
                    else
                    {
                        //方法注册事件，方式二(只能注册静态的方法)。
                        #region 参数说明
                        //注意：
                        //第一个参数：委托的类型
                        //第二个参数：方法的信息对象
                        //最后一个参数：，无法绑定该方法时，是否抛出异常。默认值（true），即抛出异常。 
                        #endregion
                        Delegate localVar = Delegate.CreateDelegate(typeof(Action), method, false);
                        SendTo += (Action)localVar;
                    }
                }
                #endregion

                #region ReceiveMsg事件注册
                if (method.Name.StartsWith("ReceiveMsg"))
                {
                    if (!method.IsStatic)
                    {
                        //方法注册事件，方式一(只能注册实例的方法。非静态方法)
                        ReceiveFrom += method.CreateDelegate(typeof(Action), this) as Action;
                    }
                    else
                    {
                        //方法注册事件，方式二(只能注册静态的方法)。
                        #region 参数说明
                        //注意：
                        //第一个参数：委托的类型
                        //第二个参数：方法的信息对象
                        //最后一个参数：，无法绑定该方法时，是否抛出异常。默认值（true），即抛出异常。 
                        #endregion
                        Delegate localVar = Delegate.CreateDelegate(typeof(Action), method, false);
                        ReceiveFrom += (Action)localVar;
                    }
                } 
                #endregion
            }
            #endregion

            #region 事件的调用方式
            //方式一
            //SendTo?.Invoke();
            //方式二
            if (SendTo != null)
            {
                SendTo();
            }

            //熟悉以后随便用
            ReceiveFrom.Invoke();
            #endregion
        }


        public static void SendToGirl()
        {
            Console.WriteLine("Hello Girl!");
        }

        public void SendToBoy()
        {
            Console.WriteLine("Hello Boy!");
        }

        public void ReceiveMsgFromGirl()
        {
            Console.WriteLine("Hello Handsome guy!");
        }

        public void ReceiveMsgFromBoy()
        {
            Console.WriteLine("Hello Beauty!");
        }
    }
}
