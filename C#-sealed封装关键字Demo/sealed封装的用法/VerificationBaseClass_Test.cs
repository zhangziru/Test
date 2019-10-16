using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203.sealed封装的用法
{
    public class VerificationBaseClass_Test
    {
        public VerificationBaseClass_Test()
        {
            TestRegion();
        }
        /// <summary>
        /// 测试区域
        /// </summary>
        public void TestRegion()
        {
            #region 基类不能够被其他类继承，但是可以正常的访问
            BaseClass_NoInherit noInherit = new BaseClass_NoInherit();
            //密封类，不能够被继承，可以正常访问
            var id = noInherit.ID;//访问属性
            var name = noInherit.Name;//访问属性
            var age = noInherit.Age;//访问属性
            noInherit.SayHi();//访问方法 
            #endregion

            VerificationBaseClass_Inherit_Son1 inherit_Son1 = new VerificationBaseClass_Inherit_Son1();
            var id1 = inherit_Son1.ID;
            var name1 = inherit_Son1.Name;
            var age1 = inherit_Son1.Age;
        }
    }
}
