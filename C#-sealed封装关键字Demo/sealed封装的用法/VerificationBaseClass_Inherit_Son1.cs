using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203.sealed封装的用法
{
    public class VerificationBaseClass_Inherit_Son1 : VerificationBaseClass_Inherit
    {
        //子类不能重写，上个基类 实现了封装sealed
        //public sealed override void SayHiByVirtual()
        //{
        //    base.SayHiByVirtual();
        //}
    }
}
