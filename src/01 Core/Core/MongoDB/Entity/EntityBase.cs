using MongoDB.Bson;
using System;

namespace CompanyName.ProjectName.Core
{
    /// <summary>
    /// MongoDB 基础类
    /// </summary>
    //   [BsonIgnoreExtraElements(Inherited = true)]
    [Serializable]
    public class EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public ObjectId _id { get; set; }
    }
}