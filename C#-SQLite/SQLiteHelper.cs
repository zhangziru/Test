using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTest
{
    /// <summary>
    /// SQLite数据库辅助类
    /// </summary>
    public class SQLiteHelper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConStr = ConfigurationManager.ConnectionStrings["SQLiteStr"].ConnectionString;
        
        #region GetDataTable
        /// <summary>
        /// 根据Sql 返回查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql)
        {
            //根据连接字符串创建连接对象
            using (SQLiteConnection con = new SQLiteConnection(ConStr))
            {
                //创建DataAdapter对象，用于执行查询
                SQLiteDataAdapter dap = new SQLiteDataAdapter(sql, con);
                DataTable result = new DataTable();
                dap.Fill(result);//将数据库中的数据填充到DataTable中
                return result;//返回数据
            }
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 根据sql执行无返回查询操作（可执行：增、删、改）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params SQLiteParameter[] pms)
        {
            using (SQLiteConnection con = new SQLiteConnection(ConStr))
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.Parameters.AddRange(pms);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        } 
        #endregion
    }
}
