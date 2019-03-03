using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NetHomeTest
{
    public static class SqlHelper
    {
        /// <summary>
        /// 定义一个连接字符串(从配置文件中读取,名字：HOME_TEST)
        /// </summary>
        private static readonly string conStr = ConfigurationManager.ConnectionStrings["HOME_TEST"].ConnectionString;

        #region ExcuteNonQuery()
        /// <summary>
        ///1、执行增(insert)、删(delete)、改(update)的方法
        ///ExcuteNonQuery()
        /// </summary>
        /// <param name="sql">带参数的查询语句</param>
        /// <param name="cmdType">命令的类型</param>
        /// <param name="pms">参数数组（语句中无参数，可为空null）</param>
        /// <returns>受影响的行数</returns>
        public static int ExcuteNonQuery(string sql, CommandType cmdType = CommandType.Text, params SqlParameter[] pms)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = cmdType;
                    if (pms != null)
                    {
                        //添加参数
                        cmd.Parameters.AddRange(pms);
                    }
                    //打开连接
                    con.Open();
                    //执行操作
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 2、执行查询，返回单个值的方法（一般为聚合操作）
        /// ExecuteScalar()
        /// </summary>
        /// <param name="sql">带参数的查询语句</param>
        /// <param name="cmdType">命令的类型</param>
        /// <param name="pms">参数数组（语句中无参数，可为空null）</param>
        /// <returns>查询的单个值</returns>
        public static object ExecuteScalar(string sql, CommandType cmdType = CommandType.Text, params SqlParameter[] pms)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = cmdType;
                    if (pms != null)
                    {
                        //添加参数
                        cmd.Parameters.AddRange(pms);
                    }
                    //打开连接
                    con.Open();
                    //执行操作
                    return cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region ExecuteReader
        /// <summary>
        /// 3、执行查询，返回多行、多列的方法
        ///ExecuteReader()
        /// </summary>
        /// <param name="sql">带参数的查询语句</param>
        /// <param name="cmdType">命令的类型</param>
        /// <param name="pms">参数数组（语句中无参数，可为空null）</param>
        /// <returns>查询的多行、多列值 的数据读取器</returns>
        public static SqlDataReader ExecuteReader(string sql, CommandType cmdType = CommandType.Text, params SqlParameter[] pms)
        {
            SqlConnection con = new SqlConnection(conStr);
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.CommandType = cmdType;
                if (pms != null)
                {
                    cmd.Parameters.AddRange(pms);
                }
                try
                {
                    con.Open();
                    //System.Data.CommandBehavior.CloseConnection这个枚举参数，表示：将来使用完毕SqlDataReader后，在关闭SqlDataReader对象的同时，在SqlDataReader内部会将关联的SqlConnection对象也关闭掉。
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }
                catch
                {
                    //防止执行操作的时候，抛出异常，连接对象无法关闭。【非常考验你的try...catch...finally的使用】
                    con.Close();
                    con.Dispose();
                    throw;//同时把异常抛到调用的地方。
                }
            }
        }

        #endregion

        #region ExecuteReader 使用案例
        /// <summary>
        /// ExecuteReader 使用案例
        /// </summary>
        public static void ExecuteReader_UseDemo()
        {
            string sql = "select * from Student";
            using (SqlDataReader reader = ExecuteReader(sql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //这里是每一行的数据，通过索引来获取每一列的数据
                        //reader.GetOrdinal()方法，根据列名来获取该列的索引。
                        //将数据添加到 程序的集合中（缓存中）
                        for (int i = 0; i < reader.FieldCount; i++)
                        {

                        }
                    }
                }
            }
        }

        #endregion

        #region SqlDataAdapter使用（查询数据返回DataTable）
        /// <summary>
        /// SqlDataAdapter使用（支持存储过程）
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="pms">可变参数集合</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, CommandType cmdType =CommandType.Text, params SqlParameter[] pms)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conStr))
            {
                adapter.SelectCommand.CommandType = cmdType;
                if (pms != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(pms);
                }
                adapter.Fill(dt);
            }
            return dt;
        } 
        #endregion
    }
}
