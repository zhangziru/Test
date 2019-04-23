using Cyrus.CodeGenerator.DAL;
using Cyrus.CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.CodeGenerator.BLL
{
    public class ColumnInfoBll
    {
        #region 根据表名返回该表的所有字段信息
        /// <summary>
        /// 根据表名返回该表的所有字段信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public List<ColumnInfo> FindColumnInfoByTableName(string tableName)
        {
            ColumnInfoDal dal = new ColumnInfoDal();
            List<ColumnInfo> list = dal.FindAllByTableName(tableName);//底层不可能返回null，最多是个空集合
            return list;
        }
        #endregion

        #region 动态生成Model文件
        /// <summary>
        /// 动态生成Model文件
        /// </summary>       
        /// <param name="tableName">表名</param>
        /// <param name="nameSpace">命名空间(默认值：Cyrus.CodeGenerator.Model)</param>
        /// <returns></returns>
        public string DynamicGenerateModel(string tableName, string nameSpace = "Cyrus.CodeGenerator.Model")
        {
            List<ColumnInfo> list = this.FindColumnInfoByTableName(tableName);
            string result = string.Empty;
            StringBuilder sb = new StringBuilder();
            //文件 引用部分（固定的）
            sb.Append("using System;" + Environment.NewLine);
            sb.Append("using System.Collections.Generic;" + Environment.NewLine);
            sb.Append("using System.Linq;" + Environment.NewLine);
            sb.Append("using System.Text;" + Environment.NewLine);
            sb.Append("using System.Threading.Tasks;" + Environment.NewLine);

            sb.Append(string.Format("namespace {0}{1}{{{2}", nameSpace, Environment.NewLine, Environment.NewLine));
            sb.Append(string.Format("\tpublic class {0}{1}\t{{{2}", tableName, Environment.NewLine, Environment.NewLine));
            foreach (var item in list)
            {              
                //文件 动态生成部分
                if (item.DataType == typeof(string).Name)
                {
                    //字符串类型
                    sb.Append(string.Format("\t\tpublic {0} {1} {{ get; set; }}", item.DataType, item.ColumnName));
                }
                else
                {
                    if (item.IsNullable)
                    {
                        //可为空
                        sb.Append(string.Format("\t\tpublic {0}? {1} {{ get; set; }}", item.DataType, item.ColumnName));
                    }
                    else
                    {
                        //其他类型（不可为空）
                        sb.Append(string.Format("\t\tpublic {0} {1} {{ get; set; }}", item.DataType, item.ColumnName));
                    }
                }
                //不是最后一个添加换行
                if (list.IndexOf(item) != (list.Count - 1))
                {
                    sb.Append(Environment.NewLine);
                }
            }
            sb.Append(string.Format("{0}\t}}", Environment.NewLine));
            sb.Append(string.Format("{0}}}", Environment.NewLine));
            result = sb.ToString();
            return result;
        } 
        #endregion
    }
}
