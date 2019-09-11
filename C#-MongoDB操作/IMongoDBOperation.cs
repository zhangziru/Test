using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingFanRen.MongoData
{
    public interface IMongoDBOperation<T> where T : class
    {
        /// <summary>
        /// 插入一个文档
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="model">要被插入到集合的文档在程序中对应的对象</param>
        bool Insert(string collectionName, T model);

        /// <summary>
        /// 更新某个集合中的文档
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="queryDocument">查询条件文档</param>
        /// <param name="updateDocument">更新内容的文档</param>
        bool Update(string collectionName, QueryDocument queryDocument, UpdateDocument updateDocument);

        /// <summary>
        /// 删除某个集合中的文档
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="queryDocument">查询条件文档</param>
        bool Delete(string collectionName, QueryDocument queryDocument);

        /// <summary>
        /// 查询某个集合，返回第一个文档
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="queryDocument">查询条件文档</param>
        /// <returns></returns>
        T QueryOne(string collectionName, QueryDocument queryDocument);

        /// <summary>
        /// 查询某个集合
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="queryDocument">查询条件文档</param>
        /// <returns></returns>
        List<T> Query(string collectionName, QueryDocument queryDocument);
    }
}
