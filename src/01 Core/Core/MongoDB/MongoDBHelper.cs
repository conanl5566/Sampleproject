using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace CompanyName.ProjectName.Core
{
    /// <summary>
    /// MongoDB 操作帮助类
    /// </summary>
    public class MongoDBHelper : IMongoDBHelper
    {
        private IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(Zconfig.Getconfig("MongoDB"));
            return client.GetDatabase(Zconfig.Getconfig("MongoDB_DBNAME"));
        }

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>()
        {
            return GetDatabase().GetCollection<T>(typeof(T).Name);
        }

        /// <summary>
        /// 获取连接对象
        /// </summary>
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return GetDatabase().GetCollection<T>(name);
        }

        /// <summary>
        /// 插入一条记录
        /// 调用例子     demoMongoDB demoMongoDB = new demoMongoDB();
        ///  demoMongoDB.Title = "456";
        ///  MongoDBHelper.Insert(demoMongoDB);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        public void Insert<T>(T msg)
        {
            var collect = GetCollection<T>();
            collect.InsertOne(msg);
        }

        /// <summary>
        /// 插入一条记录
        /// 调用例子     demoMongoDB demoMongoDB = new demoMongoDB();
        ///  demoMongoDB.Title = "456";
        ///  MongoDBHelper.Insert(demoMongoDB);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msgList"></param>
        public void InsertMany<T>(IEnumerable<T> msgList)
        {
            var collect = GetCollection<T>();
            collect.InsertMany(msgList);
        }

        /// <summary>
        /// 根据条件 更新数据
        ///  调用例子   var filter = Builders<demoMongoDB>.Filter.Eq("Title", "456");
        ///  var update = Builders<demoMongoDB>.Update.Set("Title", "123");
        ///  MongoDBHelper.Update<demoMongoDB>(filter, update);
        /// </summary>
        /// <typeparam name="BsonDocument"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        public int Update<T>(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            var collect = GetCollection<T>();
            var result = collect.UpdateMany(filter, update);
            return (int)result.ModifiedCount;
        }

        /// <summary>
        /// 根据条件 删除
        ///   调用例子    var filter = Builders<demoMongoDB>.Filter.Eq("Title", "456");
        ///   Response.Write(MongoDBHelper.DeleteAll<demoMongoDB>(filter));
        /// </summary>
        /// <typeparam name="BsonDocument"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        public int Delete<T>(FilterDefinition<T> filter)
        {
            var collect = GetCollection<T>();
            var result = collect.DeleteMany(filter);
            return (int)result.DeletedCount;
        }

        /// <summary>
        /// 删除所有的数据
        /// 调用例子 Response.Write(MongoDBHelper.DeleteAll<demoMongoDB>());
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public int DeleteAll<T>()
        {
            var filter = new BsonDocument();
            return Delete<T>(filter);
        }

        /// <summary>
        /// 根据条件  查询数据   调用例子       var filter = Builders<demoMongoDB>.Filter.Eq("Title", "456");
        ///   Response.Write(MongoDBHelper.Query<demoMongoDB>(filter).FirstOrDefault().Title);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<T> Query<T>(FilterDefinition<T> filter)
        {
            var collect = GetCollection<T>();
            var result = collect.Find(filter).ToList();
            return result;
        }

        /// <summary>
        /// 查询全部数据  调用例子   Response.Write(MongoDBHelper.QueryAll<demoMongoDB>().FirstOrDefault().Title);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> QueryAll<T>()
        {
            var filter = new BsonDocument();
            var result = Query<T>(filter);
            return result;
        }

        /// <summary>
        /// 根据条件 查询数据+排序
        ///   //var filter = Builders<testTitle>.Filter.Eq("Title", "456");

        ///var sort = Builders<testTitle>.Sort.Descending("Title");

        /// MongoDBHelper.QueryBySort<testTitle>(filter, sort);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public List<T> QueryBySort<T>(FilterDefinition<T> filter, SortDefinition<T> sort)
        {
            var collect = GetCollection<T>();
            var result = collect.Find(filter).Sort(sort).ToList();
            return result;
        }

        /// <summary>
        /// 根据查询条件获取多条数据（分页）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex">1 开始</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="Total"></param>
        /// <returns></returns>
        public List<T> GetManyByPageCondition<T>(FilterDefinition<T> filter, SortDefinition<T> sort, int pageIndex, int pageSize, out int Total)
        {
            var collect = GetCollection<T>();
            Total = (int)collect.CountDocuments(filter);
            var result =
                collect.Find(filter).Sort(sort).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
            return result;
        }
    }
}