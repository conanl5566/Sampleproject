using System.Collections.Generic;
using MongoDB.Driver;

namespace dotNET.Core
{
    /// <summary>
    /// MongoDB 操作接口
    /// </summary>
    public interface IMongoDBHelper
    {
        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IMongoCollection<T> GetCollection<T>();

        /// <summary>
        /// 获取连接对象
        /// </summary>
        IMongoCollection<T> GetCollection<T>(string name);

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        void Insert<T>(T msg);

        /// <summary>
        /// 插入多条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msgList"></param>
        void InsertMany<T>(IEnumerable<T> msgList);

        /// <summary>
        /// 根据条件 更新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        int Update<T>(FilterDefinition<T> filter, UpdateDefinition<T> update);

        /// <summary>
        /// 根据条件  删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        int Delete<T>(FilterDefinition<T> filter);

        /// <summary>
        /// 删除所有的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        int DeleteAll<T>();

        /// <summary>
        /// 根据条件 查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<T> Query<T>(FilterDefinition<T> filter);

        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> QueryAll<T>();

        /// <summary>
        /// 根据条件 查询数据+排序 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        List<T> QueryBySort<T>(FilterDefinition<T> filter, SortDefinition<T> sort);

        /// <summary>
        /// 根据查询条件获取多条数据（分页）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        List<T> GetManyByPageCondition<T>(FilterDefinition<T> filter, SortDefinition<T> sort, int pageIndex, int pageSize, out int Total);

    }
}
