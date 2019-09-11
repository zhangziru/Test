using MongoDB.Driver;
using PingFanRen.MongoData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PingFanRen.MongoData
{
    #region 参考的文献
    //参考链接，源码：链接：https://pan.baidu.com/s/17qYIaN_SoTUpmhA4K2Pfaw  提取码：8nfl 
    //原文参考链接：https://www.cnblogs.com/99app/p/3477966.html

    //如果要学习的话推荐一个链接：https://www.cnblogs.com/99app/p/3468853.html
    //注意：要在不同的平台使用MongoDB的话，需要安装相应的驱动。eg：C# 需要引入类库。Java 需要引入java包。【重点】
    //语法的学习，自行百度
    #endregion

    /// <summary>
    /// 连接MongoDB服务，以及操作帮助类
    /// </summary>
    public class MongoDBHelper<T> : IMongoDBOperation<T> where T : class
    {
        /// <summary>
        /// MongoDB的数据库名称
        /// </summary>
        public static string MongoDb_DatabaseName = System.Configuration.ConfigurationManager.AppSettings["MongoDb_DatabaseName"];

        /// <summary>
        /// MongoDB的数据库服务地址+端口
        /// </summary>
        public static string MongoDb_Server = System.Configuration.ConfigurationManager.AppSettings["MongoDb_Server"];//MongoDB_Server服务的连接，如果不设置端口，则连接默认的端口（27017）

        /// <summary>
        /// MongoDB的数据库用户（没有开启验证，不需要设置）
        /// </summary>
        public static string MongoDb_User = System.Configuration.ConfigurationManager.AppSettings["MongoDB_User"];

        /// <summary>
        /// MongoDB的数据库密码（没有开启验证，不需要设置）
        /// </summary>
        public static string MongoDb_Pass = System.Configuration.ConfigurationManager.AppSettings["MongoDB_Pass"];
        /// <summary>
        /// 是否需要身份验证。
        /// </summary>
        public bool IsIdentityAuthentication
        {
            get
            {
                //根据是否提供用户名，来判断是否需要进行身份验证【简单的处理】。
                if (!string.IsNullOrEmpty(MongoDb_User))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// MongoDB服务的连接字符串。
        /// <para>备注：根据是否需要身份验证，自动生成。</para>
        /// </summary>
        public string MongoDb_ConnectionString
        {
            get
            {
                if (this.IsIdentityAuthentication)
                {
                    return @"mongodb://" + MongoDb_User + ":" + MongoDb_Pass + "@" + MongoDb_Server + "/" + MongoDb_DatabaseName;
                }
                else
                {
                    return @"mongodb://" + MongoDb_Server + "/" + MongoDb_DatabaseName;//MongoDB_Server服务的连接，如果不设置端口，则连接默认的端口（27017）
                }
            }
        }


        /// <summary>
        /// 从指定的连接字符串创建并返回MongoDatabase。
        /// </summary>
        /// <param name="connectionString">用于从中获取数据库的connectionstring。</param>
        /// <returns>从指定的connectionstring返回MongoDatabase。</returns>
        public static MongoDatabase GetDatabaseFromConnectionString(string connectionString)
        {
            var conMongoUrl = new MongoUrl(connectionString);
            var server = MongoServer.Create(conMongoUrl.ToServerSettings());//旧的写法：创建MongoDB连接，并获取MongoDB服务
            return server.GetDatabase(conMongoUrl.DatabaseName);
        }

        /// <summary>
        /// 从配置中获取MongoDB连接信息，获取MongoServer
        /// <para>备注：此时的数据库服务的状态（state）仍是disconnection。只有查询了数据的时候，状态才是 connction【很鸡贼】</para>
        /// </summary>
        /// <returns>从配置文件中返回MongoServer</returns>
        public MongoServer GetMongoDBServerFromConfig()
        {
            return new MongoClient(this.MongoDb_ConnectionString).GetServer();//新写法：创建MongoDB连接
        }

        /// <summary>
        /// 从配置中获取MongoDB连接信息，获取MongoDatabase        
        /// </summary>
        /// <returns>从配置文件中返回MongoDatabase</returns>
        public MongoDatabase GetMongoDatabaseFromConfig()
        {
            return this.GetMongoDBServerFromConfig().GetDatabase(MongoDb_DatabaseName);
        }

        public bool Insert(string collectionName, T model)
        {
            //数据库
            MongoDatabase db = this.GetMongoDatabaseFromConfig();
            var col = db.GetCollection(collectionName);
            var result = col.Insert<T>(model);
            return result.Ok;//返回执行状态。
        }

        public bool Update(string collectionName, QueryDocument queryDocument, UpdateDocument updateDocument)
        {
            //数据库
            MongoDatabase db = this.GetMongoDatabaseFromConfig();
            var col = db.GetCollection(collectionName);
            //执行更新操作
            var result = col.Update(queryDocument, updateDocument,UpdateFlags.Multi);//默认只更新查询到的第一条，现在更改为更新查询到的所有数据。【重点】
            return result.Ok;//返回执行状态。
        }

        public bool Delete(string collectionName, QueryDocument queryDocument)
        {
            //数据库
            MongoDatabase db = this.GetMongoDatabaseFromConfig();
            var col = db.GetCollection(collectionName);
            //执行删除操作
           var result = col.Remove(queryDocument);
            return result.Ok;//返回执行状态。
        }

        public T QueryOne(string collectionName, QueryDocument queryDocument)
        {
            //数据库
            MongoDatabase db = this.GetMongoDatabaseFromConfig();
            var col = db.GetCollection(collectionName);
            //查询指定查询条件的第一条数据，查询条件可缺省。
            var result2 = col.FindOneAs<T>();

            return result2;
        }

        public List<T> Query(string collectionName, QueryDocument queryDocument)
        {
            //数据库
            MongoDatabase db = this.GetMongoDatabaseFromConfig();
            var col = db.GetCollection(collectionName);
            //查询全部集合里的数据
            var result1 = col.FindAllAs<T>();
            //查询指定查询条件的第一条数据，查询条件可缺省。
            var result2 = col.FindOneAs<T>();
            //查询指定查询条件的全部数据
            var result3 = col.FindAs<T>(queryDocument).ToList();
            return result3;
        }


    }
}
