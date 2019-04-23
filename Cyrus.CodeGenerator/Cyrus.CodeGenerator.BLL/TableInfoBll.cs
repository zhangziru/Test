using Cyrus.CodeGenerator.DAL;
using Cyrus.CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.CodeGenerator.BLL
{
    public class TableInfoBll
    {
        #region 根据表的类型 返回数据库中表的列表
        /// <summary>
        /// 根据表的类型 返回数据库中表的列表
        /// </summary>
        /// <param name="type">表的类型</param>
        /// <returns></returns>
        public List<TableInfo> FindTableInfoByTableType(enumTableType type)
        {
            List<TableInfo> list = null;
            TableInfoDal dal = new TableInfoDal();
            switch (type)
            {
                case enumTableType.全部:
                    list = dal.FindAll();
                    break;
                case enumTableType.基础表:
                    list = dal.FindBasicTable();
                    break;
                case enumTableType.视图:
                    list = dal.FindViewTable();
                    break;
                default:
                    list = new List<TableInfo>();
                    break;
            }
            return list;
        } 
        #endregion
    }
}
