using dotNET.Domain.Entities.Sys;
using dotNET.Dto;
using dotNET.EntityFrameworkCore;
using dotNET.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNET.Application.Sys
{
    public class ModuleApp : IModuleApp
    {
        #region 注入

        public IBaseRepository<Module> ModuleRep { get; set; }
        public IUnitWork UnitWork { get; set; }

        #endregion 注入

        /// <summary>
        /// Saas模块
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public async Task<List<Module>> GetSaasModuleListAsync(ModuleOption option = null)
        {
            return await GetListAsync(option);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="isSaas"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        private async Task<List<Module>> GetListAsync(ModuleOption option)
        {
            var predicate = PredicateBuilder.True<Module>();

            if (option != null)
            {
                if (!string.IsNullOrEmpty(option.FullName))
                {
                    predicate = predicate.And(o => o.FullName.Contains(option.FullName));
                }
                if (option.ParentId.HasValue)
                {
                    predicate = predicate.And(o => o.ParentId == option.ParentId);
                }
                if (option.IsEnabled.HasValue)
                {
                    predicate = predicate.And(o => o.IsEnabled == option.IsEnabled);
                }
            }
            var t = (await ModuleRep.Find(predicate).ToListAsync()).OrderBy(o => o.SortCode).ToList();
            return t;
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="isSaas"></param>
        /// <returns></returns>
        public async Task<List<Module>> GetMenuCatalogListAsync()
        {
            return await ModuleRep.Find(o => o.IsMenu == false).ToListAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<R> DeleteAsync(long id)
        {
            if (await ModuleRep.GetCountAsync(o => o.ParentId == id) > 0)
            {
                return R.Err("含有子菜单不能删除");
            }
            try
            {
                UnitWork.Delete<Module>(o => o.Id == id);
                UnitWork.Delete<ModuleButton>(o => o.ModuleId == id);
                UnitWork.Save();
            }
            catch (Exception exc)
            {
                return R.Err(exc.Message);
            }
            await RemoveCacheAsync();
            return R.Suc();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<Module> GetAsync(long key)
        {
            Module m = await ModuleRep.FindSingleAsync(o => o.Id == key);
            return m;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        public async Task<R> CreateAsync(Module moduleEntity)
        {
            moduleEntity.FullName = moduleEntity.FullName?.Trim();
            moduleEntity.UrlAddress = moduleEntity.UrlAddress?.Trim();
            moduleEntity.EnCode = "m";
            int count = await ModuleRep.GetCountAsync(o => o.FullName == moduleEntity.FullName);
            if (count > 0)
            {
                return R.Err(msg: moduleEntity.FullName + " 已存在");
            }

            await ModuleRep.AddAsync(moduleEntity);

            await RemoveCacheAsync();
            return R.Suc();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        public async Task<R> UpdateAsync(Module moduleEntity)
        {
            moduleEntity.FullName = moduleEntity.FullName?.Trim();
            moduleEntity.UrlAddress = moduleEntity.UrlAddress?.Trim();
            int count = await ModuleRep.GetCountAsync(o => o.FullName == moduleEntity.FullName && o.Id != moduleEntity.Id);
            if (count > 0)
            {
                return R.Err(msg: moduleEntity.FullName + " 已存在");
            }
            await ModuleRep.UpdateAsync(moduleEntity);

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
            //await cache.RemoveAsync("sys", "modules");
            //await cache.RemoveAsync("sys", "buttons");
        }
    }
}