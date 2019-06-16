using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTest
{
    //缓存数据库
    public class MemoryDataBase
    {

        /// <summary>
        /// 参考链接：https://www.cnblogs.com/MeteorSeed/articles/2086141.html
        /// </summary>
        public void DataSet_Study()
        {           
            DataSet memory = new DataSet("DB_Memory");
            DataTable tb_Student = new DataTable("Tb_Student");
            DataColumn col_Id = new DataColumn("Id", typeof(int));
            tb_Student.Columns.Add();
        }
    }
}
