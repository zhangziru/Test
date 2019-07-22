using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestDemo;
using System.Collections.Generic;

namespace UnitTestDemoTest
{
    //单元测试，说明：
    //参考链接：https://www.cnblogs.com/lip-blog/p/7504990.html
    //1、方法面上面的特性[TestMethod]：这是必须的，告诉编译器这是一个测试法式。
    //  1.1、通过菜单“测试”->“窗口”->“测试资源管理器” 来打开  单元测试的一个管理界面。测试项目中编译以后就会有测试项出现在“测试资源管理”中。【重点】
    //2、这里说一下VS的强大功能。这个提示功能很好用。
    //  A、可以提示方法的引用数量，并快速定位。
    //  B、还可以提示单元测试的结果。
    //  C、还可以提示源代码版本管理器，提交及修改的情况。
    //3、快捷的方法建立测试项目：在要测试的方法行，右击。选择创建单元测试。可弹出建立单元测试对话框。
    //4、测试中涉及的一个 Assert类 的使用,[ə'sɝt] vt. 维护、评估。
    //  4.1、Assert类所在的命名空间为Microsoft.VisualStudio.TestTools.UnitTesting。
    //  4.2、Assert类可以对特定功能进行验证。
    //  4.3、单元测试方法：执行开发代码中的方法代码，但只有包含Assert语句时才能报告代码行为方面的内容。【验证方法的功能正确性，显示错误信息的报告】【重点，关键点，新点】
    //  4.4、在测试Demo中，被标记的测试方法中 可以调用任意数量的Assert类方法。
    //5、Assert相关类的介绍：
    //  5.1、Assert类是对单个值的验证、评估。
    //  5.2、CollectionAssert类是集合的比较，即对集合的验证、评估，也可以验证一个或多个集合的状态。
    //  5.3、StringAssert类是对字符串进行比较,即对字符串的验证、评估。此类包含各种有用的方法，eg：StringAssert.Contains、StringAssert.Matches和StringAssert.StartWith。
    //  5.4、AssertFailedException只要测试失败，就会引发AssertFailedException异常。
    //  5.5、AssertInconclusiveException,[inconclusive] American:[, ɪnkən'klusɪv] adj. 不确定的；非决定性的；无结果的
    //6、Assert类的主要静态成员：
    //  6.1、 AreEqual：方法被重载了N多次，主要功能是判断两个值是否相等；如果两个值不相等，则测试失败。
    //  6.2、 AreNotEqual：方法被重载了N多次，主要功能是判断两个值是否不相等；如果两个值相等，则测试失败。
    //  6.3、 AreNotSame：引用的对象是否不相同；如果两个输入内容引用相同的对象，则测试失败.
    //  6.4、 AreSame：引用的对象是否相同；如果两个输入内容引用不相同的对象，则测试失败
    //  6.5、 Fail：断言失败。
    //  6.6、 Inconclusive：表示无法证明为 true 或 false 的测试结果
    //  6.7、 IsFalse：指定的条件是否为 false；如果该条件为 true，则测试失败。
    //  6.8、 IsTrue：指定的条件是否为 true；如果该条件为 false，则测试失败
    //  6.9、 IsInstanceofType：测试指定的对象是否为所需类型的实例；如果所需的实例不在该对象的继承层次结构中，则测试失
    //  6.10、IsNotInstanceofType: 测试指定的对象是否为所需类型的实例；如果所需的实例在该对象的继承层次结构中，则测试失败
    //  6.11、IsNull：测试指定的对象是否为非空
    //  6.12、IsNotNull：测试指定的对象是否为非空
    //7、StringAssert类中的静态方法，就是对字符串的单独处理（其中的意思，看方法名就可以知道什么意思）。
    //8、CollectionAssert类中的静态方法，就是对集合的单独处理（其中的意思，看方法名就可以知道什么意思）。


    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void AddTest()
        {
            int a = 100;
            int b = 10;
            Assert.AreEqual(Program.Add(a, b), 110);
        }

        [TestMethod]
        public void AddTest2()
        {
            int a = 100;
            int b = 10;
            Assert.AreEqual(Program.Add(a, b), 50);

        }

        [TestMethod]
        public void ResponseTest()
        {
            Assert.AreEqual(Program.Response(), "ok");
        }

        [TestMethod]
        public void ResponseTest1()
        {
            StringAssert.Equals(Program.Response(), "ok");
            Assert.Fail("强制报错！");
        }
        [TestMethod]
        public void GetStatusTest()
        {
            List<string> list = new List<string>();
            list.Add("123");
            list.Add("3444");
            CollectionAssert.AllItemsAreUnique(list,"验证失败");
        }
    }
}
