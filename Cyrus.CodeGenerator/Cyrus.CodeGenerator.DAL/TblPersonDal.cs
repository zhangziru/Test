using Cyrus.CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.CodeGenerator.DAL
{
    public class TblPersonDal
    {
        #region 获取总数
        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public int GetCountNum()
        {
            string sql = "select COUNT(1) from TblPerson";
            return (int)SqlHelper.ExecuteScalar(sql);
        }
        #endregion

        #region 插入一条记录
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(TblPerson model)
        {
            string sql = "insert into TblPerson values(@Name,@Age,@Height,@Gender)";
            SqlParameter[] pms = new SqlParameter[] {
                new SqlParameter("@Name",SqlDbType.NVarChar,50) { Value=model.Name},//如果这里不写数据类型，它内部会帮我们推断一个类型，我们显式的写了，就不需要内部推断了。
                new SqlParameter("@Age",SqlDbType.Int) { Value = model.Age},
                new SqlParameter("@Height",SqlDbType.Int) { Value = model.Height},
                new SqlParameter("@Gender",SqlDbType.Bit) { Value =model.Gender == null ? DBNull.Value:(object)model.Gender}//【C#中的 null值 转换为 数据库中的 DBNull.Value,if-else返回类型不统一，任意一个转为object类型】
            };
            return SqlHelper.ExcuteNonQuery(sql, CommandType.Text, pms);
        }
        #endregion

        #region 查找所有记录
        /// <summary>
        /// 查找所有记录
        /// </summary>
        /// <returns></returns>
        public List<TblPerson> FindAll()
        {
            List<TblPerson> list = new List<TblPerson>();
            string sql = "select * from TblPerson";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql))
            {
                if (reader.HasRows)
                {
                    //根据列名来获取索引【放在循环外边，这样可以提高效率。因为这个检索是非常耗时的】
                    #region 获取列名的索引
                    int IdIndex = reader.GetOrdinal("Id");
                    int NameIndex = reader.GetOrdinal("Name");
                    int AgeIndex = reader.GetOrdinal("Age");
                    int HeightIndex = reader.GetOrdinal("Height");
                    int GenderIndex = reader.GetOrdinal("Gender");
                    #endregion

                    #region 循环取数据
                    while (reader.Read())
                    {
                        TblPerson person = new TblPerson()
                        {
                            Id = reader.GetInt32(IdIndex),
                            Name = reader.GetString(NameIndex),
                            Age = reader.GetInt32(AgeIndex),
                            Height = reader.GetInt32(HeightIndex),
                            Gender = reader.IsDBNull(GenderIndex) ? null : (bool?)reader.GetBoolean(GenderIndex)//这个可能为空，取出来的时候要转换【注意，存和取，对空值的处理】
                        };
                        list.Add(person);
                    }
                    #endregion
                }
            }
            return list;
        }
        #endregion

        #region 删除一条记录
        /// <summary>
        /// 删除一条记录（根据Id）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteById(int Id)
        {
            string sql = "delete TblPerson where Id = @Id";
            SqlParameter p1 = new SqlParameter("@Id", SqlDbType.Int) { Value = Id };
            return SqlHelper.ExcuteNonQuery(sql, CommandType.Text, p1);
        }
        #endregion

        #region 更新一条记录
        /// <summary>
        /// 更新一条记录（根据Id）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateById(TblPerson model)
        {
            string sql = "update TblPerson set Name =@Name,Age=@Age,Height=@Height,Gender=@Gender where Id = @Id";
            SqlParameter[] pms = new SqlParameter[] {
                new SqlParameter("@Id",SqlDbType.Int) { Value = model.Id},
                new SqlParameter("@Name",SqlDbType.NVarChar,50) { Value=model.Name},//如果这里不写数据类型，它内部会帮我们推断一个类型，我们显式的写了，就不需要内部推断了。
                new SqlParameter("@Age",SqlDbType.Int) { Value = model.Age},
                new SqlParameter("@Height",SqlDbType.Int) { Value = model.Height},
                new SqlParameter("@Gender",SqlDbType.Bit) { Value =model.Gender == null ? DBNull.Value:(object)model.Gender}//【C#中的 null值 转换为 数据库中的 DBNull.Value,if-else返回类型不统一，任意一个转为object类型】
            };
            return SqlHelper.ExcuteNonQuery(sql, CommandType.Text, pms);
        } 
        #endregion
       
    }
}
