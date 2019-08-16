using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNET.Domain.Entities.Sys;
using dotNET.Dto;
using dotNET.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotNET.Application.Sys
{
    public class ItemsDataApp : AppService, IItemsDataApp
    {
        #region 注入
        public IBaseRepository<ItemsData> ItemsDataRep { get; set; }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSaas"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public async Task<List<ItemsData>> GetListAsync(ItemsDataOption option)
        {
            var predicate = PredicateBuilder.True<ItemsData>();

            if (option.ParentId == 0)
            {
                return new List<ItemsData>();
            }
            predicate = predicate.And(o => o.ParentId == option.ParentId);


            if (!string.IsNullOrEmpty(option.Name))
            {

                predicate = predicate.And(o => o.Name.Contains(option.Name));


            }
            var t = await ItemsDataRep.Find(predicate).ToListAsync();
            return t;
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="isSaas"></param>
        /// <returns></returns>
        public async Task<List<ItemsData>> GetItemsDataListAsync()
        {
            var r = await ItemsDataRep.Find(null).ToListAsync();
            return r.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<R> DeleteAsync(long id)
        {
            if (await ItemsDataRep.GetCountAsync(o => o.ParentId == id) > 0)
            {
                return R.Err(msg: "含有子数据不能删除");
            }
            await ItemsDataRep.DeleteAsync(o => o.Id == id);

            await RemoveCacheAsync();
            return R.Suc();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<ItemsData> GetAsync(long key)
        {
            ItemsData m = await ItemsDataRep.FindSingleAsync(o => o.Id == key);
            return m;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        public async Task<R> CreateAsync(ItemsData moduleEntity)
        {
            moduleEntity.Name = moduleEntity.Name?.Trim();
            moduleEntity.Remarks = moduleEntity.Remarks?.Trim();
            int count = await ItemsDataRep.GetCountAsync(o => o.Name == moduleEntity.Name && o.ParentId == moduleEntity.ParentId);
            if (count > 0)
            {
                return R.Err(msg: moduleEntity.Name + " 已存在");
            }
            await ItemsDataRep.AddAsync(moduleEntity);

            await RemoveCacheAsync();
            return R.Suc();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        public async Task<R> UpdateAsync(ItemsData moduleEntity)
        {
            moduleEntity.Name = moduleEntity.Name?.Trim();
            moduleEntity.Remarks = moduleEntity.Remarks?.Trim();
            int count = await ItemsDataRep.GetCountAsync(o => o.Name == moduleEntity.Name && o.Id != moduleEntity.Id && o.ParentId == moduleEntity.ParentId);
            if (count > 0)
            {
                return R.Err(msg: moduleEntity.Name + " 已存在");
            }
            await ItemsDataRep.UpdateAsync(moduleEntity);

            await RemoveCacheAsync();
            return R.Suc();
        }


        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <returns></returns>
        private async Task RemoveCacheAsync()
        {
            //var cache = CacheFactory.Cache();
            //await cache.RemoveAsync("agent", "ItemsData");
        }
    }
}
