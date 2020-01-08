using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNET.Core.Cache
{
    /// <summary>
    ///
    /// </summary>
    public class RedisCacheService : ICacheService
    {
        protected IDatabase Cache;
        private readonly ConnectionMultiplexer _connection;

        public RedisCacheService()
        {
            try
            {
                IConfiguration config = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true }).Build();
                string connectionString = config.GetValue<string>("Data:Redis:ConnectionString");
                _connection = ConnectionMultiplexer.Connect(connectionString);
                Cache = _connection.GetDatabase(0);
            }
            catch (Exception ex)
            {
                NLogger.Error("RedisCacheService:" + ex.ToString());
            }
        }

        /// <summary>
        /// 合并key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        private string MergeKey(string key, string prefix = "")
        {
            if (!string.IsNullOrWhiteSpace(prefix))
                return $"{prefix}:{key}";
            else
                return key;
        }

        /// <summary>
        /// 验证缓存项是否存在
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public bool Exists(string key, string prefix = "")
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                return Cache.KeyExists(MergeKey(key, prefix));
            }
            catch (Exception ex)
            {
                NLogger.Error("验证缓存项是否存在:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 验证缓存项是否存在
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key, string prefix = "")
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                return await Cache.KeyExistsAsync(MergeKey(key, prefix));
            }
            catch (Exception ex)
            {
                NLogger.Error("验证缓存项是否存在:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <returns></returns>
        public bool Add(string key, object value, string prefix = "")
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                return Cache.StringSet(MergeKey(key, prefix), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
            }
            catch (Exception ex)
            {
                NLogger.Error("添加缓存:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(string key, object value, string prefix = "")
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                return await Cache.StringSetAsync(MergeKey(key, prefix), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
            }
            catch (Exception ex)
            {
                NLogger.Error("添加缓存:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expiressAbsoulte, string prefix = "")
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                return Cache.StringSet(MergeKey(key, prefix), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), expiressAbsoulte);
            }
            catch (Exception ex)
            {
                NLogger.Error("添加缓存:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(string key, object value, TimeSpan expiressAbsoulte, string prefix = "")
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                return await Cache.StringSetAsync(MergeKey(key, prefix), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), expiressAbsoulte);
            }
            catch (Exception ex)
            {
                NLogger.Error("添加缓存:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public bool Remove(string key, string prefix = "")
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                return Cache.KeyDelete(MergeKey(key, prefix));
            }
            catch (Exception ex)
            {
                NLogger.Error("删除缓存:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string key, string prefix = "")
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                return await Cache.KeyDeleteAsync(MergeKey(key, prefix));
            }
            catch (Exception ex)
            {
                NLogger.Error("删除缓存:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 批量删除缓存
        /// </summary>
        /// <param name="key">缓存Key集合</param>
        /// <returns></returns>
        public void RemoveAll(IEnumerable<string> keys, string prefix = "")
        {
            try
            {
                if (keys == null)
                {
                    throw new ArgumentNullException(nameof(keys));
                }

                keys.ToList().ForEach(item => Remove(item, prefix));
            }
            catch (Exception ex)
            {
                NLogger.Error("批量删除缓存:" + ex.ToString());
            }
        }

        /// <summary>
        /// 批量删除缓存
        /// </summary>
        /// <param name="key">缓存Key集合</param>
        /// <returns></returns>
        public async Task RemoveAllAsync(IEnumerable<string> keys, string prefix = "")
        {
            try
            {
                if (keys == null)
                {
                    throw new ArgumentNullException(nameof(keys));
                }

                foreach (string key in keys)
                {
                    await RemoveAsync(key, prefix);
                }
            }
            catch (Exception ex)
            {
                NLogger.Error("批量删除缓存:" + ex.ToString());
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public T Get<T>(string key, string prefix = "") where T : class
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                var value = Cache.StringGet(MergeKey(key, prefix));
                if (!value.HasValue)
                {
                    return default(T);
                }

                return JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                NLogger.Error("获取缓存:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key, string prefix = "") where T : class
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                var value = await Cache.StringGetAsync(MergeKey(key, prefix));
                if (!value.HasValue)
                {
                    return default(T);
                }

                return JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                NLogger.Error("获取缓存:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public object Get(string key, string prefix = "")
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                var value = Cache.StringGet(MergeKey(key, prefix));

                if (!value.HasValue)
                {
                    return null;
                }
                return JsonConvert.DeserializeObject(value);
            }
            catch (Exception ex)
            {
                NLogger.Error("获取缓存:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public async Task<object> GetAsync(string key, string prefix = "")
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                var value = await Cache.StringGetAsync(MergeKey(key, prefix));

                if (!value.HasValue)
                {
                    return null;
                }
                return JsonConvert.DeserializeObject(value);
            }
            catch (Exception ex)
            {
                NLogger.Error("获取缓存:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取缓存集合
        /// </summary>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        public IDictionary<string, object> GetAll(IEnumerable<string> keys, string prefix = "")
        {
            try
            {
                if (keys == null)
                {
                    throw new ArgumentNullException(nameof(keys));
                }
                var dict = new Dictionary<string, object>();
                keys.ToList().ForEach(item => dict.Add(item, Get(MergeKey(item, prefix))));

                return dict;
            }
            catch (Exception ex)
            {
                NLogger.Error("获取缓存集合:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取缓存集合
        /// </summary>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        public async Task<IDictionary<string, object>> GetAllAsync(IEnumerable<string> keys, string prefix = "")
        {
            try
            {
                if (keys == null)
                {
                    throw new ArgumentNullException(nameof(keys));
                }
                var dict = new Dictionary<string, object>();

                foreach (string key in keys)
                {
                    dict.Add(key, await GetAsync(MergeKey(key, prefix)));
                }
                return dict;
            }
            catch (Exception ex)
            {
                NLogger.Error("获取缓存集合:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <returns></returns>
        public bool Replace(string key, object value, string prefix = "")
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (Exists(key, prefix))
                {
                    if (!Remove(key, prefix))
                        return false;
                }

                return Add(key, value, prefix);
            }
            catch (Exception ex)
            {
                NLogger.Error("修改缓存:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <returns></returns>
        public async Task<bool> ReplaceAsync(string key, object value, string prefix = "")
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (await ExistsAsync(key, prefix))
                {
                    if (!await RemoveAsync(key, prefix))
                        return false;
                }

                return await AddAsync(key, value, prefix);
            }
            catch (Exception ex)
            {
                NLogger.Error("修改缓存:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        /// <returns></returns>
        public bool Replace(string key, object value, TimeSpan expiressAbsoulte, string prefix = "")
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (Exists(key, prefix))
                {
                    if (!Remove(key, prefix))
                        return false;
                }

                return Add(key, value, expiressAbsoulte, prefix);
            }
            catch (Exception ex)
            {
                NLogger.Error("修改缓存:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        /// <returns></returns>
        public async Task<bool> ReplaceAsync(string key, object value, TimeSpan expiressAbsoulte, string prefix = "")
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (await ExistsAsync(key, prefix))
                {
                    if (!await RemoveAsync(key, prefix))
                        return false;
                }

                return await AddAsync(key, value, expiressAbsoulte, prefix);
            }
            catch (Exception ex)
            {
                NLogger.Error("修改缓存:" + ex.ToString());
                return false;
            }
        }

        //public async Task<bool> ListAddAsync(string key, string value, bool isSliding = false, string prefix = "")
        //{
        //    if (key == null)
        //    {
        //        throw new ArgumentNullException(nameof(key));
        //    }
        //    return await _cache.SetAddAsync(MergeKey(key, prefix), value);
        //}

        public void Dispose()
        {
            _connection?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}