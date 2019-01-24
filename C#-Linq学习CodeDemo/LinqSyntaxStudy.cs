using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203
{
    /// <summary>
    /// Linq语法学习，参考链接：
    /// </summary>
    public class LinqSyntaxStudy
    {
        #region 构造函数
        public LinqSyntaxStudy()
        {
            Linq_Join_Demo();
        }
        #endregion

        #region Join 和 GroupJoin 的Demo

        #region 说明
        //参考链接 https://www.cnblogs.com/lanpingwang/p/6602708.html
        //Join操作两个集合，inner collection 和 outer collection 
        #endregion

        /// <summary>
        /// Linq中Join方法 [内联接]使用案例
        /// 将两个序列连接并返回结果集
        /// </summary>
        public void Linq_Join_Demo()
        {
            #region 获取两个 数据集合 中相同的元素（Linq_Join应用1）
            IList<string> strList1 = new List<string>() {
                "One",
                "Two",
                "Three",
                "Four"
            };

            IList<string> strList2 = new List<string>() {
                "One",
                "Two",
                "Five",
                "Six"
            };
            var innerJoin = strList1.Join(strList2,
                                  str1 => str1,
                                  str2 => str2,
                                  (str1, str2) => str1 + "-" + str2);
            #endregion

            #region 获取两个 对象集合 中 某个属性相同[单个属性] 的对象（Linq_Join应用2）

            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", StandardID =1 },
                new Student() { StudentID = 2, StudentName = "Moin", StandardID =1 },
                new Student() { StudentID = 3, StudentName = "Bill", StandardID =2 },
                new Student() { StudentID = 4, StudentName = "Ram" , StandardID =2 },
                new Student() { StudentID = 5, StudentName = "Ron"  },
                new Student() { StudentID = 6, StudentName = "Standard 2", StandardID = 2  }
            };

            IList<Standard> standardList = new List<Standard>() {
                new Standard(){ StandardID = 1, StandardName="Standard 1"},
                new Standard(){ StandardID = 2, StandardName="Standard 2"},
                new Standard(){ StandardID = 3, StandardName="Standard 3"}
            };

            #region 操作
            var innerJoin1 = studentList.Join(// outer sequence 
                                          standardList,  // inner sequence 
                                          student => student.StandardID,    // outerKeySelector
                                          standard => standard.StandardID,  // innerKeySelector
                                          (student, standard) => new  // result selector
                                          {
                                              StudentName = student.StudentName,
                                              StandardName = standard.StandardName
                                          });
            #endregion

            #endregion

            #region 获取两个 对象集合 中 某个属性相同[多个属性] 的对象（Linq_Join应用3）-需要用到比较器
            //这种操作 需要自己定义 对象之间的比较器（对象之间比较的，比较器）
            #region 操作
            var innerJoin2 = studentList.Join(// outer sequence 
                                         standardList,  // inner sequence 
                                         student => new CommonCompareObj { StandardID = student.StandardID, StudentName = student.StudentName },    // outerKeySelector
                                         standard => new CommonCompareObj { StandardID = standard.StandardID, StudentName = standard.StandardName },  // innerKeySelector
                                         (student, standard) => new  // result selector
                                         {
                                             StudentName = student.StudentName,
                                             StandardName = standard.StandardName
                                         },new ComparetorCustormTest((x,y)=> { return x.StandardID == y.StandardID && x.StudentName == y.StudentName; }));
            #endregion

            #endregion


        }

        /// <summary>
        /// Linq中GroupJoin方法 []使用案例
        /// 
        /// </summary>
        public void Linq_GroupJoin_Demo()
        {

        }

        #endregion
    }

    #region 当前测试对象 辅助类
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int StandardID { get; set; }
    }

    public class Standard
    {
        public int StandardID { get; set; }
        public string StandardName { get; set; }
    }

    #region 自定义对象比较器
    //参考链接：https://q.cnblogs.com/q/55678/

    #region 相同对象之间的比较
    /// <summary>
    /// 自定义对象比较器【相同对象之间的比较器】
    /// 相同对象之间的比较 直接使用该比较器。
    /// 如果对象的类型不同，需要多一步，来的构建不同对象 共同的属性 对象
    /// </summary>
    public class ComparetorCustormTest : IEqualityComparer<CommonCompareObj>
    {
        Func<CommonCompareObj, CommonCompareObj, bool> pre;
        public ComparetorCustormTest(Func<CommonCompareObj, CommonCompareObj, bool> pre)
        {
            this.pre = pre;
        }
        public bool Equals(CommonCompareObj x, CommonCompareObj y)
        {
            return pre(x, y);
        }


        /// <summary>
        /// 【注意】如果IEqualityComparer获取的HashCode不相等，那它的Equal方法都不执行。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(CommonCompareObj obj)
        {
            return obj.StandardID;
        }
    }
    #endregion

    //不同对象之间 通过个别的属性进行 比较，来判断是否相等。解决方案：
    //1、创建 比较的对象（里面包含要比较的共同属性）
    //2、创建 比较对象的比较器 （是针对比较对象的）
    
    /// <summary>
    /// 比较器 针对不同对象 共同的对象（比较对象）
    /// </summary>
    public class CommonCompareObj
    {
        public int StandardID { get; set; }

        public string StudentName { get; set; }
    }
    #endregion

    #endregion
}
