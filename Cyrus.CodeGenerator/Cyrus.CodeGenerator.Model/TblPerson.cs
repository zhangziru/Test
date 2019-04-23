using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.CodeGenerator.Model
{
    public class TblPerson
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool? Gender { get; set; }

    }
}
