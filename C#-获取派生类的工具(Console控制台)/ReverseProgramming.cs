using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203
{
    //正向编程：由抽象类、父类、接口开始，依次写出派生类，然后基于派生类做一些业务的操作。【原始开发者】

    /// <summary>
    /// 逆向编程
    /// <para>不清楚对象之间结构的情况下编程</para>
    /// </summary>
    public class ReverseProgramming
    {
        /// <summary>
        /// 引用程序集的路径
        /// </summary>
        public string ReferenceAssembly { get; set; }

        /// <summary>
        /// 查找的基础类型(必须是完全限定名，即：加上命名空间的类型)
        /// </summary>
        public string LookupType { get; set; }

        /// <summary>
        /// 派生类的集合信息
        /// </summary>
        public List<string> DeriveClass { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReverseProgramming()
        {
            DeriveClass = new List<string>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReverseProgramming(string referenceAssembly, string lookupType ):this()
        {
            this.ReferenceAssembly = referenceAssembly;
            this.LookupType = lookupType;
            ReflectionIniDeriveClass();
        }

        /// <summary>
        /// 反射初始化 程序集下某类型的派生类型
        /// </summary>
        public void ReflectionIniDeriveClass()
        {
            Assembly assembly = Assembly.LoadFrom(this.ReferenceAssembly);
            Type[] arrTypes = assembly.GetTypes();//所有的类型
            Type targetType = assembly.GetType(this.LookupType);//目标类型
            foreach (Type item in arrTypes)
            {
                //类型之间遵循 里氏替换原则
                if (targetType.IsAssignableFrom(item))
                {
                    DeriveClass.Add(item.FullName);//返回所有派生类的信息
                }
            }
            PrintDeriveClassInfo();
        }

        /// <summary>
        /// 打印输出派生类型的信息 以及类型
        /// </summary>
        public void PrintDeriveClassInfo()
        {
            Assembly assembly = Assembly.LoadFrom(this.ReferenceAssembly);
            
            foreach (var item in this.DeriveClass)
            {
                Type deriveType = assembly.GetType(item);//派生类型
                string resultStr = "";
                if (deriveType.IsAbstract)
                {
                    if (deriveType.IsInterface)
                    {
                        resultStr = string.Format("{0},{1}", item, BasicClassType.Interface);
                    }
                    else
                    {
                        resultStr = string.Format("{0},{1}", item, BasicClassType.Abstract);
                    }
                
                }
                else
                {
                    resultStr = string.Format("{0},{1}", item, BasicClassType.Class);
                }
                Console.WriteLine(resultStr);
            }
        }
    }

    /// <summary>
    /// 基础类的类型
    /// </summary>
    public enum BasicClassType
    {
        Interface = 0,
        Class = 1,
        Abstract = 2
    }
}
