using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingFanRen.MongoData
{
    public class MongoDB_Demo
    {
        public MongoDB_Demo()
        {
            //InsertDemo();// 插入文档的Demo
            //UpdateDemo(); //更新文档的Demo
            //DeleteDemo();//删除文档的Demo
            QueryDemo();//查询文档的Demo
            QueryOneDemo();//查询集合中第一个文档的Demo
        }

        /// <summary>
        /// 插入文档的Demo
        /// </summary>
        public void InsertDemo()
        {
            IMongoDBOperation<firstCol> firstCol = new MongoDBHelper<firstCol>();
            firstCol model = new firstCol()
            {
                url = "www.baidu.com",
                title = "百度",
                tags = new string[] { "网站", "好好" },
                by = "Cyrus",
                likes = 0.12,
                description = "哈哈，葫芦娃！"
            };
            firstCol.Insert(model.GetType().Name, model);
        }

        /// <summary>
        /// 更新文档的Demo
        /// </summary>
        public void UpdateDemo() {
            IMongoDBOperation<firstCol> firstCol = new MongoDBHelper<firstCol>();
            //定义获取“title”值为“百度”的查询条件
            var query = new QueryDocument { { "title", "度娘" } };
            //定义更新文档
            var update = new UpdateDocument { { "$set", new QueryDocument { { "title", "百度" } } } };
            firstCol.Update(typeof(firstCol).Name, query, update);
        }

        /// <summary>
        /// 删除文档的Demo
        /// </summary>
        public void DeleteDemo()
        {
            IMongoDBOperation<firstCol> firstCol = new MongoDBHelper<firstCol>();
            //定义获取“Name”值为“test”的查询条件
            var query = new QueryDocument { { "title", "度娘" } };
            firstCol.Delete(typeof(firstCol).Name, query);
        }

        /// <summary>
        /// 查询文档的Demo
        /// </summary>
        public void QueryDemo()
        {
            IMongoDBOperation<firstCol> firstCol = new MongoDBHelper<firstCol>();
            //定义获取“Name”值为“test”的查询条件
            var query = new QueryDocument { { "by", "百变则是新" } };
            var result = firstCol.Query(typeof(firstCol).Name, query);
        }

        /// <summary>
        /// 查询集合中第一个文档的Demo
        /// </summary>
        public void QueryOneDemo()
        {
            IMongoDBOperation<firstCol> firstCol = new MongoDBHelper<firstCol>();
            //定义获取“Name”值为“test”的查询条件
            var query = new QueryDocument { { "by", "百变则是新" } };
            var result = firstCol.QueryOne(typeof(firstCol).Name, query);
        }
    }
}
