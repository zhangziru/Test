using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.CodeGenerator.Model
{
    /// <summary>
    /// 数据库中列的属性信息
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
    }
}
