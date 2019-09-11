using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace PingFanRen.MongoData
{
    /// <summary>
    /// MongoDB服务数据库中的一个集合。
    /// <para>注意：集合的名称 与 类的名称一致。</para>
    /// <para>注意：集合中文档的键 与 类的属性名称、类型保持一致。【需要特别注意的是类型，类型不对，是会报错的。】</para>    
    /// </summary>
    public class firstCol
    {
        /// <summary>
        /// _id 属性必须要有，否则在更新数据时会报错:“Element '_id' does not match any field or property of class”。
        /// </summary>
        public ObjectId _id { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string by { get; set; }

        public string url { get; set; }
        /// <summary>
        /// MongoDB中的文档键（tages）是一个Array（数组）
        /// </summary>
        public string[] tags { get; set; }

        public double likes { get; set; }

        
    }
}
