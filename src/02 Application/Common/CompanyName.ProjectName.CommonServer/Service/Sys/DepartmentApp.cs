using CompanyName.ProjectName.CommonServer;
using CompanyName.ProjectName.Core;
using CompanyName.ProjectName.ICommonServer;
using CompanyName.ProjectName.ICommonServer.Sys;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.CommonServer
{
    /// <summary>
    /// 部门
    /// </summary>
    public class DepartmentApp : AppService, IDepartmentApp
    {
        #region 注入

        public IBaseRepository<Department> DepartmentRep { get; set; }

        #endregion 注入

        /// <summary>
        ///
        /// </summary>
        /// <param name="isSaas"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public async Task<List<Department>> GetListAsync(DepartmentOption option)
        {
            var predicate = PredicateBuilder.True<Department>();
            predicate = predicate.And(o => o.ParentId == option.ParentId);
            if (option != null)
            {
                if (!string.IsNullOrEmpty(option.Name))
                {
                    predicate = predicate.And(o => o.Name.Contains(option.Name));
                }
            }
            var t = await DepartmentRep.Find(predicate).ToListAsync();
            return t;
        }

        /// <summary>
        /// 部门
        /// </summary>
        /// <param name="isSaas"></param>
        /// <returns></returns>
        public async Task<List<Department>> GetDepartmentListAsync()
        {
            var r = await DepartmentRep.Find(null).ToListAsync();
            return r.ToList();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultDto> DeleteAsync(long id)
        {
            if (await DepartmentRep.GetCountAsync(o => o.ParentId == id) > 0)
            {
                return ResultDto.Err(msg: "含有子部门不能删除");
            }
            await DepartmentRep.DeleteAsync(o => o.Id == id);
            await RemoveCacheAsync();
            return ResultDto.Suc();
        }

        /// <summary>
        /// 单个部门
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<Department> GetAsync(long key)
        {
            Department m = await DepartmentRep.FindSingleAsync(o => o.Id == key);
            return m;
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        public async Task<ResultDto> CreateAsync(Department moduleEntity)
        {
            moduleEntity.Name = moduleEntity.Name?.Trim();
            moduleEntity.Code = moduleEntity.Code?.Trim();
            moduleEntity.ContactNumber = moduleEntity.ContactNumber?.Trim();
            moduleEntity.Remarks = moduleEntity.Remarks?.Trim();
            int count = await DepartmentRep.GetCountAsync(o => o.Code == moduleEntity.Code);
            if (count > 0)
            {
                return ResultDto.Err(msg: moduleEntity.Code + " 已存在");
            }
            await DepartmentRep.AddAsync(moduleEntity);
            await RemoveCacheAsync();
            return ResultDto.Suc();
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        public async Task<ResultDto> UpdateAsync(Department moduleEntity)
        {
            moduleEntity.Name = moduleEntity.Name?.Trim();
            moduleEntity.Code = moduleEntity.Code?.Trim();
            moduleEntity.ContactNumber = moduleEntity.ContactNumber?.Trim();
            moduleEntity.Remarks = moduleEntity.Remarks?.Trim();
            int count = await DepartmentRep.GetCountAsync(o => o.Code == moduleEntity.Code && o.Id != moduleEntity.Id);
            if (count > 0)
            {
                return ResultDto.Err(msg: moduleEntity.Code + " 已存在");
            }
            await DepartmentRep.UpdateAsync(moduleEntity);
            await RemoveCacheAsync();
            return ResultDto.Suc();
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <returns></returns>
        private async Task RemoveCacheAsync()
        {
            //var cache = CacheFactory.Cache();
            //await cache.RemoveAsync("agent", "Department");
        }
    }
}