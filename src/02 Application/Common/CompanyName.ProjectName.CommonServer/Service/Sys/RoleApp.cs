#region using

using CompanyName.ProjectName.CommonServer;
using CompanyName.ProjectName.Core;
using CompanyName.ProjectName.Core.Cache;
using CompanyName.ProjectName.ICommonServer;
using CompanyName.ProjectName.ICommonServer.Sys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion using

namespace CompanyName.ProjectName.CommonServer
{
    public class RoleApp : AppService, IRoleApp
    {
        #region 注入

        public IBaseRepository<RoleAuthorize> RoleAuthorizeRep { get; set; }
        public IBaseRepository<Role> RoleRep { get; set; }
        public IRoleAuthorizeApp RoleAuthorizeApp { get; set; }
        public IOperateLogApp OperateLogApp { get; set; }
        public IModuleApp ModuleApp { get; set; }
        public IModuleButtonApp ModuleButtonApp { get; set; }
        public ICacheService Cache { get; set; }
        public IUnitWork UnitWork { get; set; }

        #endregion 注入

        #region 添加

        /// <summary>
        /// 角色添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ResultDto<Role>> CreateAsync(Role entity, List<long> permissionIds, CurrentUser currentUser)
        {
            entity.CreatorTime = DateTime.Now;
            var moduledata = await ModuleApp.GetSaasModuleListAsync();
            var buttondata = await ModuleButtonApp.GetSaasModuleListAsync();
            List<long> allpermissionIds = new List<long>();
            foreach (long id in permissionIds)
            {
                allpermissionIds.Add(id);
                if (moduledata.Count(o => o.Id == id) > 0)
                {
                    var md = moduledata.Where(o => o.Id == id).FirstOrDefault();
                    if (md.ParentId != 0)
                        allpermissionIds.AddRange(await Fibonacci(md.ParentId));
                }
                else if (buttondata.Count(o => o.Id == id) > 0)
                {
                    var md = buttondata.Where(o => o.Id == id).FirstOrDefault();
                    if (md.ModuleId != 0)
                        allpermissionIds.AddRange(await Fibonacci(md.ModuleId));
                }
            }
            allpermissionIds = allpermissionIds.Distinct().ToList();
            List<RoleAuthorize> ras = new List<RoleAuthorize>();
            foreach (long id in allpermissionIds)// permissionIds
            {
                int itemType = 0;
                if (moduledata.Count(o => o.Id == id) > 0)
                    itemType = 1;
                else if (buttondata.Count(o => o.Id == id) > 0)
                    itemType = 2;
                if (itemType > 0)
                {
                    RoleAuthorize ra = new RoleAuthorize
                    {
                        ObjectId = entity.Id,
                        ObjectType = 1,
                        ItemId = id,
                        ItemType = itemType,
                        CreatorTime = DateTime.Now
                    };

                    ras.Add(ra);
                }
            }

            UnitWork.Add<Role>(entity);
            UnitWork.BatchAdd<RoleAuthorize>(ras.ToArray());
            UnitWork.Save();

            if (currentUser != null)
                await OperateLogApp.InsertLogAsync<Role>(currentUser, "添加角色", entity);
            return ResultDto<Role>.Suc(entity);
        }

        #endregion 添加

        #region 修改

        public async Task<ResultDto<Role>> UpdateAsync(Role entity, List<long> permissionIds, CurrentUser currentUser)
        {
            var moduledata = await ModuleApp.GetSaasModuleListAsync();
            var buttondata = await ModuleButtonApp.GetSaasModuleListAsync();
            var authorizs = await RoleAuthorizeApp.GetListAsync(entity.Id, 1);
            List<long> allpermissionIds = new List<long>();
            foreach (long id in permissionIds)
            {
                allpermissionIds.Add(id);
                if (moduledata.Count(o => o.Id == id) > 0)
                {
                    var md = moduledata.Where(o => o.Id == id).FirstOrDefault();
                    if (md.ParentId != 0)
                        allpermissionIds.AddRange(await Fibonacci(md.ParentId));
                }
                else if (buttondata.Count(o => o.Id == id) > 0)
                {
                    var md = buttondata.Where(o => o.Id == id).FirstOrDefault();
                    if (md.ModuleId != 0)
                        allpermissionIds.AddRange(await Fibonacci(md.ModuleId));
                }
            }
            allpermissionIds = allpermissionIds.Distinct().ToList();
            //现有
            List<long> itemIds = authorizs.Select(o => o.ItemId).ToList();
            List<long> deleteIds = authorizs.Where(o => !allpermissionIds.Contains(o.ItemId) && o.ObjectId == entity.Id && o.ObjectType == 1).Select(o => o.Id).ToList();
            List<RoleAuthorize> ras = new List<RoleAuthorize>();
            foreach (long id in allpermissionIds)
            {
                if (itemIds.Contains(id))
                {
                    continue;
                }
                int itemType = 0;
                if (moduledata.Count(o => o.Id == id) > 0)
                    itemType = 1;
                else if (buttondata.Count(o => o.Id == id) > 0)
                    itemType = 2;
                if (itemType > 0)
                {
                    RoleAuthorize ra = new RoleAuthorize
                    {
                        ObjectId = entity.Id,
                        ObjectType = 1,
                        ItemId = id,
                        ItemType = itemType,
                        CreatorTime = DateTime.Now
                    };

                    ras.Add(ra);
                }
            }

            UnitWork.Update<Role>(entity);
            UnitWork.BatchAdd<RoleAuthorize>(ras.ToArray());
            UnitWork.Delete<RoleAuthorize>(o => deleteIds.Contains(o.Id));
            UnitWork.Save();

            if (currentUser != null)
                await OperateLogApp.InsertLogAsync<Role>(currentUser, "修改角色", entity);
            await RemoveCacheAsync(entity.Id);
            return ResultDto<Role>.Suc(entity);
        }

        private async Task<List<long>> Fibonacci(long permissionIds)
        {
            List<long> a = new List<long>();
            var moduledata = await ModuleApp.GetSaasModuleListAsync();
            var p = moduledata.Where(o => o.Id == permissionIds).FirstOrDefault();
            if (p.ParentId == 0)
                a.Add(p.Id);
            else
            {
                a.Add(p.Id);
                a.AddRange(await Fibonacci(p.ParentId));
            }
            return a;
        }

        #endregion 修改

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<(bool s, string msg)> DeleteAsync(long Id, CurrentUser currentUser)
        {
            var entity = await RoleRep.FindSingleAsync(o => o.Id == Id);
            if (entity == null)
                return (false, "数据不存在");
            await RoleRep.DeleteAsync(o => o.Id == Id);

            if (currentUser != null)
                await OperateLogApp.RemoveLogAsync<Role>(currentUser, "删除角色", entity);
            await RemoveCacheAsync(Id);
            return (true, "操作成功");
        }

        #endregion 删除

        #region 列表

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="rowsPrePage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<PageResult<Role>> GetPageAsync(int pageNumber, int rowsPrePage, RoleOption filter)
        {
            List<Role> data = new List<Role>();
            PageResult<Role> list = new PageResult<Role>();
            string orderby = " id desc";
            var predicate = PredicateBuilder.True<Role>();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                predicate = predicate.And(o => o.Name.Contains(filter.Name));
            }
            var tlist = await RoleRep.Find(pageNumber, rowsPrePage, orderby, predicate).ToListAsync();
            list.Data = tlist.ToList();
            int total = await RoleRep.GetCountAsync(predicate);
            list.ItemCount = total;
            return list;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Role>> GetListAsync(RoleOption option)
        {
            if (!string.IsNullOrWhiteSpace(option.Name))
            {
                return await RoleRep.Find(o => o.Name.Contains(option.Name)).ToListAsync();
            }
            else
            {
                return await RoleRep.Find(null).ToListAsync();
            }
        }

        #endregion 列表

        #region 检测名称是否存在

        /// <summary>
        ///
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> CheckCode(string Name, long Id)
        {
            int i = 0;
            //添加
            if (Id == 0)
            {
                i = await RoleRep.GetCountAsync(o => o.Name == Name);
                if (i > 0)
                    return true;
                else
                    return false;
            }
            //编辑
            i = await RoleRep.GetCountAsync(o => o.Name == Name && o.Id != Id);
            if (i > 0)
                return true;
            else
                return false;
        }

        #endregion 检测名称是否存在

        #region 获取角色

        /// <summary>
        ///
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Role> GetAsync(long Id)
        {
            var r = await RoleRep.FindSingleAsync(o => o.Id == Id);
            return r;
        }

        #endregion 获取角色

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <returns></returns>
        private async Task RemoveCacheAsync(long roleId)
        {
            string strRoleId = roleId.ToString();
            await Cache.RemoveAsync(strRoleId, "modules");
            await Cache.RemoveAsync(strRoleId, "buttons");
            await Cache.RemoveAsync(strRoleId, "authorizeurl");
            await Cache.RemoveAsync("GetClientsDataJson", strRoleId);
            await Cache.RemoveAsync("sys", "modules");
        }
    }
}