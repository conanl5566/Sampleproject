using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Core.Cache
{
    public interface ICacheService
    {
        /// <summary>
        /// 验证缓存项是否存在
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        bool Exists(string key, string prefix = "");

        /// <summary>
        /// 验证缓存项是否存在（异步方式）
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string key, string prefix = "");

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        bool Add(string key, object value, string prefix = "");

        /// <summary>
        /// 添加缓存（异步方式）
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Task<bool> AddAsync(string key, object value, string prefix = "");

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        bool Add(string key, object value, TimeSpan expiressAbsoulte, string prefix = "");

        /// <summary>
        /// 添加缓存（异步方式）
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Task<bool> AddAsync(string key, object value, TimeSpan expiressAbsoulte, string prefix = "");

        ///// <summary>
        ///// 添加缓存
        ///// </summary>
        ///// <param name="key">缓存Key</param>
        ///// <param name="value">缓存Value</param>
        ///// <param name="expiresIn">缓存时长</param>
        ///// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        ///// <returns></returns>
        //bool Add(string key, object value, TimeSpan expiresIn, bool isSliding = false, string prefix = "");

        ///// <summary>
        ///// 添加缓存（异步方式）
        ///// </summary>
        ///// <param name="key">缓存Key</param>
        ///// <param name="value">缓存Value</param>
        ///// <param name="expiresIn">缓存时长</param>
        ///// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        ///// <returns></returns>
        //Task<bool> AddAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false, string prefix = "");

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        bool Remove(string key, string prefix = "");

        /// <summary>
        /// 删除缓存（异步方式）
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(string key, string prefix = "");

        /// <summary>
        /// 批量删除缓存
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        void RemoveAll(IEnumerable<string> keys, string prefix = "");

        /// <summary>
        /// 批量删除缓存（异步方式）
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Task RemoveAllAsync(IEnumerable<string> keys, string prefix = "");

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        T Get<T>(string key, string prefix = "") where T : class;

        /// <summary>
        /// 获取缓存（异步方式）
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key, string prefix = "") where T : class;

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        object Get(string key, string prefix = "");

        /// <summary>
        /// 获取缓存（异步方式）
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Task<object> GetAsync(string key, string prefix = "");

        /// <summary>
        /// 获取缓存集合
        /// </summary>
        /// <param name="keys">缓存Key集合</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        IDictionary<string, object> GetAll(IEnumerable<string> keys, string prefix = "");

        /// <summary>
        /// 获取缓存集合（异步方式）
        /// </summary>
        /// <param name="keys">缓存Key集合</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Task<IDictionary<string, object>> GetAllAsync(IEnumerable<string> keys, string prefix = "");

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        bool Replace(string key, object value, string prefix = "");

        /// <summary>
        /// 修改缓存（异步方式）
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Task<bool> ReplaceAsync(string key, object value, string prefix = "");

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        bool Replace(string key, object value, TimeSpan expiressAbsoulte, string prefix = "");

        /// <summary>
        /// 修改缓存（异步方式）
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Task<bool> ReplaceAsync(string key, object value, TimeSpan expiressAbsoulte, string prefix = "");

        ///// <summary>
        ///// 修改缓存
        ///// </summary>
        ///// <param name="key">缓存Key</param>
        ///// <param name="value">新的缓存Value</param>
        ///// <param name="expiresIn">缓存时长</param>
        ///// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        ///// <returns></returns>
        //bool Replace(string key, object value, TimeSpan expiresIn, bool isSliding = false, string prefix = "");

        ///// <summary>
        ///// 修改缓存（异步方式）
        ///// </summary>
        ///// <param name="key">缓存Key</param>
        ///// <param name="value">新的缓存Value</param>
        ///// <param name="expiresIn">缓存时长</param>
        ///// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        ///// <returns></returns>
        //Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false, string prefix = "");

        //Task<bool> ListAddAsync(string key, string value, string prefix = "");

        //Task<bool> ListRemoveAsync(string key, string value, string prefix = "");
    }
}