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
    public class ColumnInfoDal
    {
        #region 根据表名返回该表的所有字段信息
        /// <summary>
        /// 根据表名返回该表的所有字段信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public List<ColumnInfo> FindAllByTableName(string tableName)
        {
            List<ColumnInfo> list = new List<ColumnInfo>();
            string sql = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName";
            SqlParameter pTableName = new SqlParameter("@TableName", SqlDbType.VarChar) { Value = tableName };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text, pTableName))
            {
                if (reader.HasRows)
                {
                    int columnNameIndex = reader.GetOrdinal("COLUMN_NAME");//列名索引
                    int dataTypeIndex = reader.GetOrdinal("DATA_TYPE");//数据类型索引
                    int isNullableIndex = reader.GetOrdinal("IS_NULLABLE");//是否可空的状态索引
                    while (reader.Read())
                    {
                        ColumnInfo model = new ColumnInfo();
                        model.ColumnName = reader.GetString(columnNameIndex);
                        model.DataType = SQLServerUtils.GetTypeByDBType(reader.GetString(dataTypeIndex));//数据库中的类型，转为C#中的类型【每个数据库的类型不一致】
                        model.IsNullable = reader.GetString(isNullableIndex) == "Yes" ? true : false;//返回Yes或者No
                        list.Add(model);
                    }
                }

            }
            return list;
        } 
        #endregion
    }
}
