using System;
using System.Collections;
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
            //Linq_GroupJoin_Demo();//Linq-GroupJoin方法的Demo
            //Linq_GroupBy_Demo();//Linq-Join方法的Demo
            //Linq_ToLookup_Demo();//Linq_ToLookup方法的Demo
            //Linq_OrderBy_Demo();//Linq_OrderBy方法的Demo
            //Linq_OrderByDescending_Demo();//Linq_OrderByDescending方法的Demo
            //Linq_OfType_Demo();//Linq_OfType方法的Demo
            //Linq_Where_Demo();//Linq_Where方法的Demo
            //Linq_SelectMany_Demo();//Linq_SelectMany方法的Demo
            //Linq_All_Demo();//Linq_All方法的Demo
            //Linq_Any_Demo();//Linq_Any方法的Demo
            //Linq_Contain_Demo();//Linq_Contain方法的Demo

            //聚合函数
            //Linq_Aggregate_Demo();//Linq_Aggregate方法的Demo
            //Linq_Average_Demo();//Linq_Average方法的Demo
            //Linq_Count_Demo();//Linq_Count方法的Demo
            //Linq_Max_Demo();//Linq_Max方法的Demo
            //Linq_Sum_Demo();//Linq_Sum方法的Demo

            //查询操作（ElementAt, ElementAtOrDefault）
            //Linq_ElementAt_Demo();//Linq_ElementAt方法的Demo
            //Linq_ElementAtOrDefault_Demo();//Linq_ElementAtOrDefault方法的Demo
            //Linq_First_Demo();//Linq_First方法的Demo【Linq_Last同理】
            //Linq_FirstOrDefault_Demo();//Linq_FirstOrDefault方法的Demo【Linq_LastOrDefault同理】
            //Linq_Single_Demo();//Linq_Single方法的Demo
            //Linq_SingleOrDefault_Demo();//Linq_SingleOrDefault方法的Demo

            //判断集合相等(SequenceEqual)
            //Linq_SequenceEqual_Demo();//Linq_SequenceEqual方法的Demo

            //同类型元素集合的连接（Concat）
            //Linq_Concat_Demo();//Linq_Concat方法的Demo

            //集合为空的处理方式(DefaultIfEmpty)
            //Linq_DefaultIfEmpty_Demo();//Linq_DefaultIfEmpty方法的Demo

            // Enumerable 静态方法的使用  Empty、Range、Repeat
            //Linq_Empty_Demo();//Linq_Empty方法的Demo【快速构建一个 指定类型的空集合】
            //Linq_Range_Demo();//Linq_Range方法的Demo【快速构建一个 自增值的整形集合】
            Linq_Repeat_Demo();//Linq_Repeat方法的Demo【快速构建一个 重复值的整形集合】
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

        #region GroupBy 和 ToLookup 的Demo
        #region 说明
        //参考链接 https://www.cnblogs.com/lanpingwang/p/6602449.html
        //Join对集合进行分组
        #endregion


        /// <summary>
        /// Linq中GroupBy方法 [分组]使用案例
        /// GroupBy操作返回根据一些键值进行分组，每组代表IGrouping<TKey,TElement>对象
        /// foreach遍历group，每个Group包含一个key和内部的集合
        /// </summary>
        public void Linq_GroupBy_Demo()
        {
            #region 测试数据
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Abram" , Age = 21 }
            };
            #endregion

            #region Linq 方法语法【根据单个属性，进行分组】
            //与GroupJoin的用法极其的类似，不同之处是，该方法不需要将组元素的集合，输出出来就可以使用
            var groupedResult = studentList.GroupBy(s => s.Age);

            foreach (var ageGroup in groupedResult)
            {
                Console.WriteLine("Age Group: {0}", ageGroup.Key);  //Each group has a key 
                var list = ageGroup.ToList();//可以获取分组后的对象列表
                foreach (Student s in ageGroup)  //Each group has a inner collection  
                    Console.WriteLine("Student Name: {0}", s.StudentName);
            }

            #endregion

            #region Linq 查询语法【根据单个属性，进行分组】

            var groupedResult1 = from s in studentList
                                 group s by s.Age;

            //iterate each group        
            foreach (var ageGroup in groupedResult1)
            {
                Console.WriteLine("Age Group: {0}", ageGroup.Key); //Each group has a key 

                foreach (Student s in ageGroup) // Each group has inner collection
                    Console.WriteLine("Student Name: {0}", s.StudentName);
            }
            #endregion

            #region Linq 方法语法【根据多个属性，进行分组】
            //与GroupJoin的用法极其的类似，不同之处是，该方法不需要将组元素的集合，输出出来就可以使用
            var groupedResult2 = studentList.GroupBy(s => new { Name = s.StudentName, Age = s.Age });

            foreach (var ageGroup in groupedResult2)
            {
                Console.WriteLine("StudentName & Age Group: Name={0},Age={1}", ageGroup.Key.Name, ageGroup.Key.Age);  //Each group has a key 

                foreach (Student s in ageGroup)  //Each group has a inner collection  
                    Console.WriteLine("Student Name: {0}", s.StudentName);
            }

            #endregion

            #region Linq 查询语法【根据多个属性，进行分组】

            var groupedResult3 = from s in studentList
                                 group s by new { Name = s.StudentName, Age = s.Age };

            //iterate each group        
            foreach (var ageGroup in groupedResult3)
            {
                Console.WriteLine("StudentName & Age Group: Name={0},Age={1}", ageGroup.Key.Name, ageGroup.Key.Age);  //Each group has a key 

                foreach (Student s in ageGroup) // Each group has inner collection
                    Console.WriteLine("Student Name: {0}", s.StudentName);
            }
            #endregion
        }

        /// <summary>
        ///  Linq中ToLookup方法 [分组]使用案例
        ///  ToLookup与GroupBy相同;唯一的区别是GroupBy的执行被延迟，而ToLookup立即执行。
        /// </summary>
        public void Linq_ToLookup_Demo()
        {
            #region 测试数据
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Abram" , Age = 21 }
            };
            #endregion

            #region Linq 方法语法

            var lookupResult = studentList.ToLookup(s => s.Age);

            foreach (var group in lookupResult)
            {
                Console.WriteLine("Age Group: {0}", group.Key);  //Each group has a key 

                foreach (Student s in group)  //Each group has a inner collection  
                    Console.WriteLine("Student Name: {0}", s.StudentName);
            }

            #endregion

            #region Linq 查询语法

            //没有对应的查询语法

            #endregion
        }

        #endregion

        #region OrderBy 和 OrderByDescending 的Demo
        #region 说明
        //参考链接 https://www.cnblogs.com/lanpingwang/p/6602258.html
        //OrderBy 和 OrderByDescending对集合进行排序
        //1、LINQ包含五种排序操作:OrderBy、OrderByDescending、ThenBy、ThenByDescending、Reverse
        //2、查询语言不支持OrderByDescending、ThenBy、ThenByDescending、Reverse，它仅支持Order By从句后面跟ascending、descending
        //3、查询语法支持多字段排序
        #endregion

        /// <summary>
        /// Linq-OrderBy 语法 使用案例
        /// </summary>
        public void Linq_OrderBy_Demo()
        {
            #region 测试数据
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }
            };
            #endregion

            #region Linq 方法语法

            var studentsInAscOrder = studentList.OrderBy(s => s.StudentName);

            foreach (var item in studentsInAscOrder)
            {
                Console.WriteLine("姓名排序：{0}", item.StudentName);
            }

            #endregion
            Console.WriteLine("-------------------------");
            #region Linq 查询语法

            var orderByResult = from s in studentList
                                orderby s.StudentName
                                select s;
            foreach (var item in orderByResult)
            {
                Console.WriteLine("姓名排序：{0}", item.StudentName);
            }
            #endregion
        }

        /// <summary>
        /// Linq-OrderByDescending 语法 使用案例
        /// </summary>
        public void Linq_OrderByDescending_Demo()
        {
            #region 测试数据
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }
            };
            #endregion

            #region Linq 方法语法

            var studentsInDescOrder = studentList.OrderByDescending(s => s.StudentName);
            foreach (var item in studentsInDescOrder)
            {
                Console.WriteLine("姓名排序：{0}", item.StudentName);
            }
            #endregion
            Console.WriteLine("-------------------------");
            #region Linq 查询语法

            var orderByDescendingResult = from s in studentList
                                          orderby s.StudentName descending
                                          select s;
            foreach (var item in orderByDescendingResult)
            {
                Console.WriteLine("姓名排序：{0}", item.StudentName);
            }
            #endregion
        }
        #endregion

        #region OfType 的Demo

        #region 说明
        //参考链接 https://www.cnblogs.com/lanpingwang/p/6602145.html
        //OfType操作根据集合中的元素是否是给定的类型进行筛选
        #endregion

        /// <summary>
        /// Linq-OfType 语法 使用案例
        /// OfType操作根据集合中的元素是否是给定的类型进行筛选
        /// </summary>
        public void Linq_OfType_Demo()
        {
            #region 测试数据
            IList mixedList = new ArrayList();
            mixedList.Add(0);
            mixedList.Add("One");
            mixedList.Add("Two");
            mixedList.Add(3);
            mixedList.Add(new Student() { StudentID = 1, StudentName = "Bill" });
            #endregion

            #region Linq 方法语法

            var stringResult = mixedList.OfType<string>();

            #endregion

            #region Linq 查询语法

            var stringResult1 = from s in mixedList.OfType<string>()
                                select s;

            var intResult = from s in mixedList.OfType<int>()
                            select s;
            #endregion
        }
        #endregion

        #region Where 的Demo

        #region 说明
        //参考链接 https://www.cnblogs.com/lanpingwang/p/6602122.html
        //需要记住的几点：
        //1.Where根据特定条件来筛选集合元素
        //2.where扩展方法有2个重载，使用第二个重载方法可以知道当前元素在集合中的索引位置
        //3.方法语法需要整个lambda表达式，而查询语法仅仅需要表达式主体
        //4.在单一的LINQ查询中可以使用多个where从句
        #endregion


        /// <summary>
        /// Linq-Where 语法 使用案例
        /// 使用很简单
        /// where的第二个扩展方法包含集合的index索引【值得一看】
        /// </summary>
        public void Linq_Where_Demo()
        {
            #region 测试数据
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13} ,
                new Student() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20} ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 15 }
            };
            #endregion

            #region Linq 方法语法
            //使用第二扩展方法（第一个方法过于简单，自己尝试）【值得一看】，如果要在 查询语言众写，需要自己定义 委托变量或者创建一个方法来实现。总体来说 不利于阅读。但是可以实现是真的。
            var filteredResult = studentList.Where((s, i) =>
            {
                if (i % 2 == 0) // if it is even element
                    return true;

                return false;
            });

            foreach (var std in filteredResult)
                Console.WriteLine(std.StudentName);

            //第一个扩展方法，使用多个Where
            var filteredResult2 = studentList.Where(s => s.Age > 12).Where(s => s.Age < 20);

            #endregion

            #region Linq 查询语法

            //使用多个Where
            var filteredResult1 = from s in studentList
                                  where s.Age > 12
                                  where s.Age < 20
                                  select s;

            #endregion
        }
        #endregion

        #region SelectMany 的Demo

        #region 说明
        //参考链接 https://www.cnblogs.com/lanpingwang/p/6602906.html 
        //SelectMany 是  GroupBy 的一个逆向操作（将分好组的数据，重新还原为一个完整的集合）
        //一般结合Where来使用
        #endregion

        /// <summary>
        /// Linq-SelectMany 使用案例
        /// Select 可以输出一个对象
        /// SelectMany 可以输出多个对象
        /// 一般结合Where来使用
        /// </summary>
        public void Linq_SelectMany_Demo()
        {
            #region 测试数据
            List<Teacher> teachers = new List<Teacher>
            {
                new Teacher("a",new List<Student_01>{ new Student_01(100),new Student_01(90),new Student_01(30) }),
                new Teacher("b",new List<Student_01>{ new Student_01(100),new Student_01(90),new Student_01(60) }),
                new Teacher("c",new List<Student_01>{ new Student_01(100),new Student_01(90),new Student_01(40) }),
                new Teacher("d",new List<Student_01>{ new Student_01(100),new Student_01(90),new Student_01(60) }),
                new Teacher("e",new List<Student_01>{ new Student_01(100),new Student_01(90),new Student_01(50) }),
                new Teacher("f",new List<Student_01>{ new Student_01(100),new Student_01(90),new Student_01(60) }),
                new Teacher("g",new List<Student_01>{ new Student_01(100),new Student_01(90),new Student_01(60) })
            };
            #endregion

            #region Linq 方法语法

            var list2 = teachers.SelectMany(t => t.Students);

            var list3 = teachers.SelectMany(
            t => t.Students,
            (t, s) => new { t.Name, s.Score });

            #endregion

            #region Linq 查询语法



            #endregion
        }
        #endregion

        #region All 、 Any 和 Contain （量词）的Demo

        #region 说明
        //参考链接 https://www.cnblogs.com/lanpingwang/p/6602969.html
        //All 判断所有的元素是否满足条件
        //Any 判断存在一个元素满足条件
        //Contain 判断是否包含元素
        #endregion

        /// <summary>
        /// Linq-All 的使用案例
        /// 判断所有的元素是否满足条件
        /// </summary>
        public void Linq_All_Demo()
        {
            #region 测试数据
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }
            };
            #endregion

            #region Linq 方法语法
            //All量词的应用
            bool areAllStudentsTeenAger = studentList.All(s => s.Age > 12 && s.Age < 20);

            #endregion

            #region Linq 查询语法
            //没有对应的 查询方法

            #endregion
        }

        /// <summary>
        /// Linq-Any 的使用案例
        /// 判断存在一个元素满足条件
        /// </summary>
        public void Linq_Any_Demo()
        {
            #region 测试数据
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }
            };
            #endregion

            #region Linq 方法语法
            //Any量词的应用
            bool isAnyStudentTeenAger = studentList.Any(s => s.Age > 12 && s.Age < 20);

            #endregion

            #region Linq 查询语法
            //没有对应的 查询方法

            #endregion
        }

        /// <summary>
        /// Linq-Contain 的使用案例
        /// 判断是否包含元素，一般在值集合中 使用比较 普遍。在对象集合列表中使用，需要自己定制 对象比较器。
        /// </summary>
        public void Linq_Contain_Demo()
        {
            #region 测试数据
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }
            };
            #endregion

            #region Linq 方法语法
            //Contain量词的应用
            Student std = new Student() { StudentID = 3, StudentName = "Bill" };
            bool result = studentList.Contains(std, new Student_01Comparer()); //returns true

            #endregion

            #region Linq 查询语法
            //没有对应的 查询方法

            #endregion
        }
        #endregion

        #region 聚合函数（Aggregate）Demo

        #region 说明
        //参考链接 https://www.cnblogs.com/lanpingwang/p/6603101.html
        //Aggregate 在集合上执行自定义聚集操作
        //Average 求平均数
        //Count 求集合的总数
        //LongCount 求集合的总数
        //Max 最大值
        //Min 最小值
        //Sum 总数
        #endregion

        /// <summary>
        /// Linq-Aggregate 使用案例
        /// 在集合上执行自定义聚集操作【操作原理：“斐波那契数列”的思想，第三个值是前两个值的一个操作结果..循环】
        /// </summary>
        public void Linq_Aggregate_Demo()
        {
            #region 测试数据
            //数值集合
            IList<string> strList = new List<string>() { "One", "Tow", "Three", "Four", "Five" };

            //对象列表
            // Student collection
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 } ,
                new Student() { StudentID = 2, StudentName = "Moin", Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18 } ,
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron", Age = 15 }
            };
            string commaSeparatedString = strList.Aggregate<string>(
                                        (s1, s2) => s1 + "," + s2); //第一次计算之后，计算的结果会替换掉第一个参数，继续参与下一次计算。

            string commaSeparatedStudentNames = studentList.Aggregate<Student, string>(//第一个类型：数据源类型；第二个类型：累积变量 的类型。
                                        "Student Names: ",  // seed value（初始值，如果没有，则是集合中的 第一个元素）
                                        (str, s) => str += s.StudentName + ",");
            Console.WriteLine(commaSeparatedString);
            Console.WriteLine(commaSeparatedStudentNames);
            #endregion

            #region Linq 方法语法



            #endregion

            #region Linq 查询语法


            #endregion
        }

        /// <summary>
        /// Linq-Average(平均数) 使用案例 
        /// 支持 数字的 基本类型 集合：float,double,decimal,int,long
        /// </summary>
        public void Linq_Average_Demo()
        {
            #region 测试数据
            //测试数据一
            IList<int> intList = new List<int>() { 10, 20, 30 };

            //测试数据二
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 },
                new Student() { StudentID = 2, StudentName = "Moin", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron", Age = 15 }
             };

            #endregion

            #region Linq 方法语法
            //测试数据一 案例
            var avg = intList.Average();

            Console.WriteLine("Average: {0}", avg);
            //测试数据二 案例
            var avgAge = studentList.Average(s => s.Age);

            Console.WriteLine("Average Age of Student: {0}", avgAge);

            #endregion

            #region Linq 查询语法

            //没有对应的查询语法

            #endregion
        }

        /// <summary>
        /// Linq-Max(最大值) 使用案例 
        /// </summary>
        public void Linq_Count_Demo()
        {
            #region 测试数据
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 },
                new Student() { StudentID = 2, StudentName = "Moin", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron", Age = 20 }
             };

            #endregion

            #region Linq 方法语法            
            var numOfStudents = studentList.Count();

            Console.WriteLine("Number of Students: {0}", numOfStudents);

            //添加过滤条件，然后计数【不使用Where过滤 计数】
            var numOfStudents1 = studentList.Count(s => s.Age >= 18);

            Console.WriteLine("Number of Students: {0}", numOfStudents1);

            #endregion

            #region Linq 查询语法

            var totalAge = (from s in studentList
                            select s.Age).Count();

            Console.WriteLine("totalAge of Students: {0}", totalAge);

            #endregion
        }

        /// <summary>
        /// Linq-Max(最大值) 使用案例  
        ///支持 数字的 基本类型 集合：float,double,decimal,int,long
        /// </summary>
        public void Linq_Max_Demo()
        {
            #region 测试数据
            //测试数据一
            IList<int> intList = new List<int>() { 10, 21, 30, 45, 50, 87 };

            //测试数据二
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 },
                new Student() { StudentID = 2, StudentName = "Moin", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron", Age = 15 }
             };
            #endregion

            #region Linq 方法语法
            //测试数据一 案例            
            var largest = intList.Max();

            Console.WriteLine("Largest Element: {0}", largest);
            //求偶数 里面的最大值。（Max中的 方法参数 功能：元素的转换）
            var largestEvenElements = intList.Max(i =>
            {
                if (i % 2 == 0)
                    return i;

                return 0;
            });

            Console.WriteLine("Largest Even Element: {0}", largestEvenElements);

            //测试数据二 案例（Max中的 方法参数 功能：元素的转换）
            var oldest = studentList.Max(s => s.Age);

            Console.WriteLine("Oldest Student Age: {0}", oldest);

            var studentWithLongName = studentList.Max(); //【重点】如果直接对 对象进行Max 操作，该对象需要实现 IComparable接口

            Console.WriteLine("Student ID: {0}, Student Name: {1}", studentWithLongName.StudentID, studentWithLongName.StudentName);
            #endregion

            #region Linq 查询语法

            //没有对应的查询语法

            #endregion
        }

        /// <summary>
        /// Linq-Sum(求和、总数) 使用案例  
        ///支持 数字的 基本类型 集合：float,double,decimal,int,long
        /// </summary>
        public void Linq_Sum_Demo()
        {
            #region 测试数据
            //测试数据一
            IList<int> intList = new List<int>() { 10, 21, 30, 45, 50, 87 };

            //测试数据二
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 },
                new Student() { StudentID = 2, StudentName = "Moin", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron", Age = 15 }
             };
            #endregion

            #region Linq 方法语法
            //测试数据一 案例            
            var total = intList.Sum();

            Console.WriteLine("Sum: {0}", total);
            //计算所有偶数的和（结合 元素转换方法 参数 来实现）
            //如果是正常情况下，我们肯定使用的是，遍历整个集合，将偶数加起来。现在我们不需要写很长的代码就可以实现了。Linq中的Sum方法也可以实现。
            var sumOfEvenElements = intList.Sum(i =>
            {
                if (i % 2 == 0)
                    return i;

                return 0;
            });

            Console.WriteLine("Sum of Even Elements: {0}", sumOfEvenElements);

            //测试数据二 案例（Sum中的 方法参数 功能：元素的转换）
            var sumOfAge = studentList.Sum(s => s.Age);

            Console.WriteLine("Sum of all student's age: {0}", sumOfAge);
            //数据汇总（结合 元素转换方法 参数 来实现，同时可以实现元素的过滤）
            var numOfAdults = studentList.Sum(s =>
            {

                if (s.Age >= 18)
                    return s.Age;
                else
                    return 0;
            });

            Console.WriteLine("Total Adult Students: {0}", numOfAdults);
            #endregion

            #region Linq 查询语法

            //没有对应的查询语法

            #endregion
        }
        #endregion

        #region ElementAt, ElementAtOrDefault 的Demo

        #region 说明
        //参考链接 https://www.cnblogs.com/lanpingwang/p/6607355.html
        //ElementAt             返回  指定索引的元素，如果索引超过集合长度，则抛出异常
        //ElementAtOrDefault    返回  指定索引的元素，如果索引超过集合长度，则返回元素的默认值，不抛出异常
        //First                 返回  集合的第一个元素，可以根据指定条件返回
        //FirstOrDefault        返回  集合的第一个元素，可以根据指定条件返回，如果集合不存在元素，则返回元素的默认值
        //Last                  返回  集合中的最后一个元素，可以根据条件返回
        //LastOrDefault         返回  集合中的最后一个元素，可以根据条件返回，如果集合不存在，则返回元素的默认值
        //Single                返回  集合中的一个元素，可以根据条件返回，如果集合不在存元素或有多个元素，则抛出异常
        //SingleOrDefault       返回  集合中的一个元素，可以根据条件返回，如果集合不在存元素，则返回元素默认值，如果存在多个元素，则抛出异常
        #endregion

        /// <summary>
        /// Linq-ElementAt使用案例
        /// 返回：指定索引的元素（索引超出异常）
        /// </summary>
        public void Linq_ElementAt_Demo()
        {
            #region 测试数据
            //测试数据一
            IList<int> intList = new List<int>() { 10, 20, 30 };

            //测试数据二
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 },
                new Student() { StudentID = 2, StudentName = "Moin", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron", Age = 15 }
             };
            #endregion

            #region Linq 方法语法
            foreach (var item in intList)
            {
                Console.Write("{0},", item);
            }
            Console.WriteLine();
            Console.WriteLine("第{0}个元素是：{1}", 1, intList.ElementAt(0));
            Student model = studentList.ElementAt(0);//集合中第 1 个元素

            #endregion

            #region Linq 查询语法

            //没有对应的查询语法
            #endregion
        }

        /// <summary>
        /// Linq-ElementAtOrDefault使用案例
        /// 返回：指定索引的元素（索引超出返回默认值，不抛异常）
        /// 值类型 数据组，索引超出 返回默认值
        /// 引用类型（包括string）数组，索引超出返回 null
        /// </summary>
        public void Linq_ElementAtOrDefault_Demo()
        {
            #region 测试数据
            //测试数据一
            IList<int> intList = new List<int>() { 10, 20, 30 };
            IList<string> strList = new List<string>() { "10", "20", "30" };
            //测试数据二
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 },
                new Student() { StudentID = 2, StudentName = "Moin", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron", Age = 15 }
             };
            #endregion

            #region Linq 方法语法
            foreach (var item in intList)
            {
                Console.Write("{0},", item);
            }
            Console.WriteLine();
            Console.WriteLine("第{0}个元素是：{1}", 4, intList.ElementAtOrDefault(3));//值类型数据组，索引超出 返回默认值
            var tempt = strList.ElementAtOrDefault(3);//string类型的数组 测试
            Student model = studentList.ElementAtOrDefault(5);//引用类型（包括string）数组，索引超出返回 null

            #endregion

            #region Linq 查询语法

            //没有对应的查询语法
            #endregion
        }

        /// <summary>
        /// Linq-First使用案例
        /// 返回：集合的第一个元素，可以根据指定条件返回
        /// 加条件的重载参数 是一个Lambda表达式（即：一个方法）
        /// </summary>
        public void Linq_First_Demo()
        {
            #region 测试数据
            //测试数据一
            IList<int> intList = new List<int>() { 10, 20, 30 };
            //测试数据二
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 },
                new Student() { StudentID = 2, StudentName = "Moin", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron", Age = 15 }
             };
            #endregion

            #region Linq 方法语法
            foreach (var item in intList)
            {
                Console.Write("{0},", item);
            }
            Console.WriteLine();
            Console.WriteLine("第{0}个元素是：{1}", 1, intList.First());//返回数组中第一个元素
            //添加指定的条件
            var tempt = intList.First(p => p > 10);//返回元素集合中大于10 的第一个元素

            Student model = studentList.First();//返回数组中第一个对象

            #endregion

            #region Linq 查询语法

            //没有对应的查询语法
            #endregion
        }

        /// <summary>
        /// Linq-FirstOrDefault使用案例
        /// 返回：集合的第一个元素，如果集合不存在元素，则返回元素的默认值（针对加条件重载，过滤集合可能为空）
        /// 加条件的重载参数 是一个Lambda表达式（即：一个方法）
        /// </summary>
        public void Linq_FirstOrDefault_Demo()
        {
            #region 测试数据
            //测试数据一
            IList<int> intList = new List<int>() { 10, 20, 30 };
            //测试数据二
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 },
                new Student() { StudentID = 2, StudentName = "Moin", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron", Age = 15 }
             };
            #endregion

            #region Linq 方法语法
            foreach (var item in intList)
            {
                Console.Write("{0},", item);
            }
            Console.WriteLine();
            Console.WriteLine("第{0}个元素是：{1}", 1, intList.FirstOrDefault());//返回数组中第一个元素
            //添加指定的条件
            var tempt = intList.FirstOrDefault(p => p > 30);//过滤元素集合中没有元素   返回元素的默认值（值类型：默认值。引用类型：null）

            Student model = studentList.FirstOrDefault(p => p.Age > 21);//返回null

            #endregion

            #region Linq 查询语法

            //没有对应的查询语法
            #endregion
        }

        /// <summary>
        /// Linq-Single使用案例
        /// 返回：集合中的 唯一 一个元素 ；(如果集合不在存元素或有多个元素，则抛出异常),可以根据指定条件返回
        /// 加条件的重载参数 是一个Lambda表达式（即：一个方法）
        /// </summary>
        public void Linq_Single_Demo()
        {
            #region 测试数据
            //测试数据一
            IList<int> intList = new List<int>() { 10, 20, 30 };
            //测试数据二
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 },
                new Student() { StudentID = 2, StudentName = "Moin", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron", Age = 15 }
             };
            #endregion

            #region Linq 方法语法
            foreach (var item in intList)
            {
                Console.Write("{0},", item);
            }
            Console.WriteLine();
            //添加指定的条件
            var tempt = intList.Single(p => p > 20);//过滤元素集合中 仅有一个元素 则 返回，否则抛出异常
            //intList.Single();
            Student model = studentList.Single(p => p.Age > 20);

            #endregion

            #region Linq 查询语法

            //没有对应的查询语法
            #endregion
        }

        /// <summary>
        /// Linq-SingleOrDefault使用案例
        /// 返回：集合中的 唯一 一个元素 ；(如果集合“不在存元素”，则 “返回元素默认值”，如果“存在多个元素”，则“抛出异常”)
        /// 加条件的重载参数 是一个Lambda表达式（即：一个方法）
        /// </summary>
        public void Linq_SingleOrDefault_Demo()
        {
            #region 测试数据
            //测试数据一
            IList<int> intList = new List<int>() { 10, 20, 30 };
            //测试数据二
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 },
                new Student() { StudentID = 2, StudentName = "Moin", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron", Age = 15 }
             };
            #endregion

            #region Linq 方法语法
            foreach (var item in intList)
            {
                Console.Write("{0},", item);
            }
            Console.WriteLine();
            //添加指定的条件
            //1、过滤元素集合中 仅有一个元素 则 返回
            var tempt = intList.SingleOrDefault(p => p > 20);
            //2、过滤元素集合中 没有元素（为空） 则 返回 元素的默认值
            var tempt1 = intList.SingleOrDefault(p => p > 30);
            //3、过滤元素集合中多个元素 则 抛异常
            //Student model = studentList.SingleOrDefault(p => p.Age > 10);

            #endregion

            #region Linq 查询语法

            //没有对应的查询语法
            #endregion
        }

        #endregion

        #region 判断集合相等（SequenceEqual）的Demo

        #region 说明
        //参考链接 https://www.cnblogs.com/lanpingwang/p/6607355.html
        //SequenceEqual             判断集合相等
        //判断集合相等(基本类型的判断，和引用类型的判断，是不相同的。)
        //1、如果集合的元素是简单类型，则判断两个集合的元素个数，元素值，出现的位置是否一样
        //2、如果集合的元素是复杂类型，则判断两个集合的元素引用是否相同、元素个数，元素值，出现的位置是否一样
        //2.1、如果要判断集合元素为复杂类型的值是否相等，则要实现IEqualityComparer<T>接口
        //2.2、比较器的定义，见下述中 “另外一套比较器”区域（region）中的定义
        //2.3、引用对象之间的比较，需要自己定义一个比较器【重点】
        #endregion

        /// <summary>
        /// Linq-SequenceEqual（顺序等值比较） 使用案例
        /// 判断集合相等【没什么用】
        /// </summary>
        public void Linq_SequenceEqual_Demo()
        {
            #region 测试数据
            //基本的类型集合（两组）
            List<int> list1 = new List<int>() { 1, 2, 3, 4, 5 };
            List<int> list2 = new List<int>() { 1, 2, 3, 4, 5 };

            //复杂类型的集合
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 },
                new Student() { StudentID = 2, StudentName = "Moin", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron", Age = 15 }
             };
            IList<Student> studentList1 = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 },
                new Student() { StudentID = 2, StudentName = "Moin", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron", Age = 15 }
             };
            #endregion

            #region Linq 方法语法

            bool result = list1.SequenceEqual(list2);//基本类型，直接使用

            bool result1 = studentList.SequenceEqual(studentList1, new Student_01Comparer());//引用类型的比较，需要自定义 比较器
            #endregion
            //没有对应的查询语法
            #region Linq 查询语法


            #endregion
        }
        #endregion

        #region 连接两个元素类型相同的集合（Concat）的Demo
        /// <summary>
        /// Linq-Concat 的使用案例
        /// 连接两个元素类型相同的集合
        /// 返回 新的集合
        /// </summary>
        public void Linq_Concat_Demo()
        {
            #region 测试数据

            IList<string> collection1 = new List<string>() { "One", "Two", "Three" };
            IList<string> collection2 = new List<string>() { "Five", "Six" };

            #endregion

            #region Linq 方法语法

            var collection3 = collection1.Concat(collection2);

            foreach (string str in collection3)
                Console.WriteLine(str);


            #endregion

            #region Linq 查询语法


            #endregion
        }
        #endregion

        #region 集合为空的处理（DefaultIfEmpty ）的Demo
        /// <summary>
        /// Linq-DefaultIfEmpty 的使用案例
        /// 1、如果集合元素个数为0，调用DefaultIfEmpty后，则返回一个新的集合，且包含一个元素（元素值为默认值）
        /// 2、另一个方法重载接收一个参数，代替默认值返回
        /// 【注意】调用该方法，返回的仍然是一个集合。
        /// </summary>
        public void Linq_DefaultIfEmpty_Demo()
        {
            #region 测试数据

            IList<string> emptyList = new List<string>();

            #endregion

            #region Linq 方法语法

            var newList1 = emptyList.DefaultIfEmpty();//集合为空的默认处理方式
            var newList2 = emptyList.DefaultIfEmpty("None");//集合为空的指定处理方式

            Console.WriteLine("Count: {0}", newList1.Count());
            Console.WriteLine("Value: {0}", newList1.ElementAt(0));

            Console.WriteLine("Count: {0}", newList2.Count());
            Console.WriteLine("Value: {0}", newList2.ElementAt(0));
            #endregion

            #region Linq 查询语法


            #endregion
        }
        #endregion

        #region Empty、Range、Repeat方法的Demo

        /// <summary>
        /// Linq-Empty 的使用案例
        /// Empty不是IEnumerable的扩展方法，是Enumerable的静态方法
        /// 返回 指定类型的空集合
        /// </summary>
        public void Linq_Empty_Demo()
        {
            #region Linq 方法语法
            var emptyCollection1 = Enumerable.Empty<string>();
            var emptyCollection2 = Enumerable.Empty<Student>();

            Console.WriteLine("Count: {0} ", emptyCollection1.Count());
            Console.WriteLine("Type: {0} ", emptyCollection1.GetType().Name);

            Console.WriteLine("Count: {0} ", emptyCollection2.Count());
            Console.WriteLine("Type: {0} ", emptyCollection2.GetType().Name);

            #endregion
        }

        /// <summary>
        /// Linq-Range  的使用案例
        /// 方法返回指定元素个数的集合，集合以给定的值开始 （元素类型为int），是Enumerable的静态方法
        /// 返回 整形的一个自增编号集合
        /// </summary>
        public void Linq_Range_Demo()
        {
            #region Linq 方法语法

            var intCollection = Enumerable.Range(10, 10);
            Console.WriteLine("Total Count: {0} ", intCollection.Count());

            for (int i = 0; i < intCollection.Count(); i++)
                Console.WriteLine("Value at index {0} : {1}", i, intCollection.ElementAt(i));

            #endregion
        }

        /// <summary>
        /// Linq-Repeat  的使用案例
        /// 返回指定元素个数的集合，且元素的值一样，是Enumerable的静态方法
        /// </summary>
        public void Linq_Repeat_Demo()
        {
            #region Linq 方法语法

            var intCollection = Enumerable.Repeat<int>(10, 10);
            Console.WriteLine("Total Count: {0} ", intCollection.Count());

            for (int i = 0; i < intCollection.Count(); i++)
                Console.WriteLine("Value at index {0} : {1}", i, intCollection.ElementAt(i));

            #endregion
        }

        #endregion

        #region TemplateDemo
        /// <summary>
        /// Linq 语法测试模板
        /// </summary>
        public void Linq_Template_Demo()
        {
            #region 测试数据

            #endregion

            #region Linq 方法语法



            #endregion

            #region Linq 查询语法


            #endregion
        }
        #endregion
    }


}

#region 当前测试对象 辅助类
public class Student : IComparable<Student>
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int StandardID { get; set; }
    public int Age { get; set; }

    /// <summary>
    /// 比较器
    /// </summary>
    /// <param name="other">下一个（other）对象</param>
    /// <returns>返回值的含义如下：值 小于 0 此对象小于 other参数。</returns>
    /// <returns>返回值的含义如下：值 等于 0 此对象等于 other参数。</returns>
    /// <returns>返回值的含义如下：值 大于 0 此对象大于 other参数。</returns>
    public int CompareTo(Student other)
    {
        if (this.StudentName.Length > other.StudentName.Length)
            return 1;
        if (this.StudentName.Length < other.StudentName.Length)
            return -1;
        return 0;
    }
}

public class Student_01
{
    public int Score { get; set; }

    public Student_01(int score)
    {
        this.Score = score;
    }
}

public class Teacher
{
    public string Name { get; set; }

    public List<Student_01> Students;

    public Teacher(string order, List<Student_01> students)
    {
        this.Name = order;

        this.Students = students;
    }
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

#region 另外一套比较器
public class Student_01Comparer : IEqualityComparer<Student>
{
    public bool Equals(Student x, Student y)
    {
        if (x.StudentID == y.StudentID &&
                    x.StudentName.ToLower() == y.StudentName.ToLower())
            return true;

        return false;
    }

    public int GetHashCode(Student obj)
    {
        return obj.GetHashCode();
    }
}
#endregion

#endregion

