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
            //Linq_Join_Demo();//Linq-Join方法的Demo
            Linq_GroupJoin_Demo();//Linq-GroupJoin方法的Demo
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
            #region 测试数据
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
            #endregion

            #region 获取两个 数据集合 中相同的元素（Linq_Join应用1）           
            var innerJoin = strList1.Join(strList2,
                                  str1 => str1,
                                  str2 => str2,
                                  (str1, str2) => str1 + "-" + str2);
            #endregion

            #region 测试数据
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
            #endregion

            #region  Linq  方法语法

            #region 获取两个 对象集合 中 某个属性相同[单个属性] 的对象（Linq_Join应用2）

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
                                         }, new ComparetorCustormTest((x, y) => { return x.StandardID == y.StandardID && x.StudentName == y.StudentName; }));
            #endregion

            #endregion

            #endregion

            #region Linq  查询语法
            //单个属性写法
            var list = from outer in standardList
                       join inner in studentList
                       on outer.StandardID equals inner.StandardID
                       select new
                       {
                           StudentName = inner.StudentName,
                           StandardName = outer.StandardName
                       };
            //多个属性写法(多条件写法，也是通过对象)
            var list1 = from outer in standardList
                        join inner in studentList
                        on new { ID = outer.StandardID, Name = outer.StandardName } equals new { ID = inner.StandardID, Name = inner.StudentName }
                        select new
                        {
                            StudentName = inner.StudentName,
                            StandardName = outer.StandardName
                        };
            #endregion
        }

        /// <summary>
        /// Linq中GroupJoin方法 []使用案例
        /// GroupJoin和Join是一样的除了GroupJoin返回一个Group（根据特定的group key返回 组数据）
        /// GroupJoin根据key联合两个序列并根据key分组
        /// </summary>
        public void Linq_GroupJoin_Demo()
        {
            #region 测试数据
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", StandardID =1 },
                new Student() { StudentID = 2, StudentName = "Moin", StandardID =1 },
                new Student() { StudentID = 3, StudentName = "Bill", StandardID =2 },
                new Student() { StudentID = 4, StudentName = "Ram",  StandardID =2 },
                new Student() { StudentID = 5, StudentName = "Ron" },
                new Student() { StudentID = 6, StudentName = "Standard 2", StandardID = 2  }
            };

            IList<Standard> standardList = new List<Standard>() {
                new Standard(){ StandardID = 1, StandardName="Standard 1"},
                new Standard(){ StandardID = 2, StandardName="Standard 2"},
                new Standard(){ StandardID = 3, StandardName="Standard 3"}
            }; 
            #endregion

            #region Linq  方法语法

            #region【左连接】 按照对象的 单个属性 进行分组 筛选
            //outer列表中的对象(第一个集合)，全部显示，右边集合（第二个集合）中为空的不显示。
            var groupJoin = standardList.GroupJoin(studentList,  //inner sequence
                                                std => std.StandardID, //outerKeySelector 
                                                s => s.StandardID,     //innerKeySelector
                                                (std, studentsGroup) => new // resultSelector 
                                                {
                                                    Students = studentsGroup,
                                                    StandarFulldName = std.StandardName
                                                });
            //组对象 集合里 是 右边集合（inner集合） 根据关联的键 过滤出来的元素集合。
            foreach (var item in groupJoin)
            {
                Console.WriteLine(item.StandarFulldName);

                foreach (var stud in item.Students)
                    Console.WriteLine(stud.StudentName);
            }
            #endregion

            #region【左连接】 按照对象的 多个属性 进行分组 筛选【这个不用创建对象比较器】
            //【左连接】outer列表中的对象(第一个集合)，全部显示，右边集合（第二个集合）中为空的不显示。
            var groupJoin1 = standardList.GroupJoin(studentList,  //inner sequence
                                                std => new { StandardID = std.StandardID, Name = std.StandardName }, //outerKeySelector 
                                                s => new { StandardID = s.StandardID, Name = s.StudentName },     //innerKeySelector
                                                (std, studentsGroup) => new // resultSelector 
                                                {
                                                    Students = studentsGroup,
                                                    StandarFulldName = std.StandardName
                                                });
            //组对象 集合里 是 右边集合（inner集合） 根据关联的键 过滤出来的元素集合。
            foreach (var item in groupJoin1)
            {
                Console.WriteLine(item.StandarFulldName);

                foreach (var stud in item.Students)
                    Console.WriteLine(stud.StudentName);
            }
            #endregion

            #endregion

            #region Linq  查询语法
            //单个属性写法
            var list = from outer in standardList
                       join inner in studentList
                       on outer.StandardID equals inner.StandardID
                       into groupedCollection
                       select new
                       {
                           Students = groupedCollection,
                           StandarFulldName = outer.StandardName
                       };
            //多个属性写法(多条件写法，也是通过对象)
            var list1 = from outer in standardList
                        join inner in studentList
                        on new { ID = outer.StandardID, Name = outer.StandardName } equals new { ID = inner.StandardID, Name = inner.StudentName }
                        into groupedCollection
                        select new
                        {
                            Students = groupedCollection,
                            StandarFulldName = outer.StandardName
                        }; 
            #endregion
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
