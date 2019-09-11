using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingFanRen.MongoData
{
    //原文参考链接：https://blog.csdn.net/qq_27462223/article/details/83183330
    public class MongoDataBaseContext
    {
        public IMongoClient _client = null;
        public IMongoDatabase _database = null;
        public MongoDataBaseContext(MongodbHost host)
        {
            _client = new MongoClient(host.Connection);
            _database = _client.GetDatabase(host.DataBase);
        }
    }
    public class MongodbHost
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Connection { get; set; }
        /// <summary>
        /// 库
        /// </summary>
        public string DataBase { get; set; }
        /// <summary>
        /// 表
        /// </summary>
        public string Table { get; set; }

    }
}
