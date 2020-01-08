using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dotNET.Core.Cache
{
    public class MemoryCacheService
    {
        private static readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        /// <summary>
        /// 验证缓存项是否存在
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                object cached;
                return _cache.TryGetValue(key, out cached);
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
        public bool Add(string key, object value)
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _cache.Set(key, value);
                return Exists(key);
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
        /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _cache.Set(key, value,
                        new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(expiresSliding)
                        .SetAbsoluteExpiration(expiressAbsoulte)
                        );

                return Exists(key);
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
        /// <param name="expiresIn">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                if (isSliding)
                    _cache.Set(key, value,
                        new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(expiresIn)
                        );
                else
                    _cache.Set(key, value,
                    new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(expiresIn)
                    );

                return Exists(key);
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
        public bool Remove(string key)
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                _cache.Remove(key);

                return !Exists(key);
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
        public void RemoveAll(IEnumerable<string> keys)
        {
            try
            {
                if (keys == null)
                {
                    throw new ArgumentNullException(nameof(keys));
                }

                keys.ToList().ForEach(item => _cache.Remove(item));
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
        public T Get<T>(string key) where T : class
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                return _cache.Get(key) as T;
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
        public object Get(string key)
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                return _cache.Get(key);
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
        public IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            try
            {
                if (keys == null)
                {
                    throw new ArgumentNullException(nameof(keys));
                }

                var dict = new Dictionary<string, object>();

                keys.ToList().ForEach(item => dict.Add(item, _cache.Get(item)));

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
        public bool Replace(string key, object value)
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                if (Exists(key))
                    if (!Remove(key)) return false;

                return Add(key, value);
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
        /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        /// <returns></returns>
        public bool Replace(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                if (Exists(key))
                    if (!Remove(key)) return false;

                return Add(key, value, expiresSliding, expiressAbsoulte);
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
        /// <param name="expiresIn">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <returns></returns>
        public bool Replace(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                if (Exists(key))
                    if (!Remove(key)) return false;

                return Add(key, value, expiresIn, isSliding);
            }
            catch (Exception ex)
            {
                NLogger.Error("修改缓存:" + ex.ToString());
                return false;
            }
        }

        public void Dispose()
        {
            if (_cache != null)
                _cache.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}