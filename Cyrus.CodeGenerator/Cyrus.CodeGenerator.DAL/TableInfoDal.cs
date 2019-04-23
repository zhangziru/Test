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
    public class TableInfoDal
    {
        #region 获取当前库中的所有表（包括：基础表+视图）
        /// <summary>
        /// 获取当前库中的所有表（包括：基础表+视图）
        /// </summary>
        /// <returns></returns>
        public List<TableInfo> FindAll()
        {
            List<TableInfo> list = new List<TableInfo>();
            string sql = "SELECT * FROM INFORMATION_SCHEMA.TABLES ";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text))
            {
                if (reader.HasRows)
                {
                    int tableNameIndex = reader.GetOrdinal("TABLE_NAME");
                    int tableTypeIndex = reader.GetOrdinal("TABLE_TYPE");
                    while (reader.Read())
                    {
                        TableInfo model = new TableInfo();
                        model.TableName = reader.GetString(tableNameIndex);//表名
                        model.TableType = reader.GetString(tableTypeIndex);//表类型
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion

        #region 获取当前库中的 基础表
        /// <summary>
        /// 获取当前库中的 基础表
        /// </summary>
        /// <returns></returns>
        public List<TableInfo> FindBasicTable()
        {
            List<TableInfo> list = new List<TableInfo>();
            string sql = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text))
            {
                if (reader.HasRows)
                {
                    int tableNameIndex = reader.GetOrdinal("TABLE_NAME");
                    int tableTypeIndex = reader.GetOrdinal("TABLE_TYPE");
                    while (reader.Read())
                    {
                        TableInfo model = new TableInfo();
                        model.TableName = reader.GetString(tableNameIndex);//表名
                        model.TableType = reader.GetString(tableTypeIndex);//表类型
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion

        #region 获取当前库中的 视图
        /// <summary>
        /// 获取当前库中的 视图
        /// </summary>
        /// <returns></returns>
        public List<TableInfo> FindViewTable()
        {
            List<TableInfo> list = new List<TableInfo>();
            string sql = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'VIEW'";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text))
            {
                if (reader.HasRows)
                {
                    int tableNameIndex = reader.GetOrdinal("TABLE_NAME");
                    int tableTypeIndex = reader.GetOrdinal("TABLE_TYPE");
                    while (reader.Read())
                    {
                        TableInfo model = new TableInfo();
                        model.TableName = reader.GetString(tableNameIndex);//表名
                        model.TableType = reader.GetString(tableTypeIndex);//表类型
                        list.Add(model);
                    }
                }
            }
            return list;
        } 
        #endregion
    }
}
