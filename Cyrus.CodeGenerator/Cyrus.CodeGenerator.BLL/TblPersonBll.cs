using Cyrus.CodeGenerator.DAL;
using Cyrus.CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.CodeGenerator.BLL
{
    public class TblPersonBll
    {
        TblPersonDal dal = new TblPersonDal();//在对应的业务逻辑层中，声明一个该业务逻辑层 对应的 数据访问层对象 成员。

        #region 返回人数的个数
        /// <summary>
        /// 返回人数的个数
        /// </summary>
        /// <returns></returns>
        public int GetPersonCount()
        {
            return dal.GetCountNum();
        }
        #endregion

        #region 增加一个人
        /// <summary>
        /// 增加一个人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddPerson(TblPerson model)
        {
            //如果说，业务逻辑层 做的严谨的话，这里应该对传入的数据进行一下验证（非空，逻辑 验证）
            //这里暂时不处理。【简化操作流程】
            return dal.Insert(model);
        }
        #endregion

        #region 获取所有的人员列表
        /// <summary>
        /// 获取所有的人员列表
        /// </summary>
        /// <returns></returns>
        public List<TblPerson> GetAllPerson()
        {
            return dal.FindAll();
        }
        #endregion

        #region 删除一个人
        /// <summary>
        /// 删除一个人（根据Id）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeletePersonById(int id)
        {
            return dal.DeleteById(id) > 0;
        }
        #endregion

        #region 更新一个人
        /// <summary>
        /// 更新一个人
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public bool UpdatePerson(TblPerson person)
        {
            return dal.UpdateById(person) > 0;
        } 
        #endregion

    }
}
