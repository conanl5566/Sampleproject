using dotNET.Core;
using dotNET.Core.Cache;
using dotNET.ICommonServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using dotNET.ICommonServer;

using dotNET.ICommonServer.Sys;
using dotNET.CommonServer;

namespace dotNET.ICommonServer
{
    public class RoleAuthorizeApp : AppService, IRoleAuthorizeApp
    {
        #region 注入

        public IBaseRepository<RoleAuthorize> RoleAuthorizeRep { get; set; }
        public IModuleApp ModuleApp { get; set; }
        public IModuleButtonApp ModuleButtonApp { get; set; }
        public ICacheService Cache { get; set; }

        #endregion 注入

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ResultDto> CreateAsync(RoleAuthorize entity)
        {
            await RoleAuthorizeRep.AddAsync(entity);
            return ResultDto.Suc();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public async Task CreateAsync(List<RoleAuthorize> entitys)
        {
            await RoleAuthorizeRep.BatchAddAsync(entitys.ToArray());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(long Id)
        {
            await RoleAuthorizeRep.DeleteAsync(o => o.Id == Id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public async Task DeleteAsync(IEnumerable<long> Ids)
        {
            if (Ids.Count() > 0)
                await RoleAuthorizeRep.DeleteAsync(o => Ids.Contains(o.Id));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ObjectId"></param>
        /// <param name="ObjectType"></param>
        /// <returns></returns>
        public async Task<List<RoleAuthorize>> GetListAsync(long ObjectId, int ObjectType)
        {
            List<RoleAuthorize> data;
            data = await Cache.GetAsync<List<RoleAuthorize>>("GetClientsDataJson", ObjectId.ToString());
            if (data == null)
            {
                data = await RoleAuthorizeRep.Find(o => o.ObjectId == ObjectId && o.ObjectType == ObjectType).ToListAsync();
                await Cache.AddAsync("GetClientsDataJson", data, new TimeSpan(0, 30, 0), ObjectId.ToString());
            }
            return data;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="curUser"></param>
        /// <returns></returns>
        public async Task<List<Module>> GetModuleList(long roleId, bool bSys)
        {
            List<Module> data;
            if (bSys)
            {
                data = await Cache.GetAsync<List<Module>>("sys", "modules");
                if (data == null)
                {
                    data = await ModuleApp.GetSaasModuleListAsync(new ModuleOption { IsEnabled = true });
                    await Cache.AddAsync("sys", data, new TimeSpan(0, 30, 0), "modules");
                }
            }
            else
            {
                data = await Cache.GetAsync<List<Module>>(roleId.ToString(), "modules");
                if (data == null)
                {
                    data = new List<Module>();
                    var moduledata = await ModuleApp.GetSaasModuleListAsync(new ModuleOption { IsEnabled = true });
                    var authorizedata = await RoleAuthorizeRep.Find(o => o.ObjectId == roleId && o.ItemType == 1).ToListAsync();
                    foreach (var item in authorizedata)
                    {
                        Module moduleEntity = moduledata.Find(t => t.Id == item.ItemId);
                        if (moduleEntity != null)
                        {
                            data.Add(moduleEntity);
                        }
                    }
                    await Cache.AddAsync(roleId.ToString(), data, new TimeSpan(0, 30, 0), "modules");
                }
            }
            return data.OrderBy(t => t.SortCode).ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="curUser"></param>
        /// <returns></returns>
        public async Task<List<ModuleButton>> GetButtonList(long roleId, bool bSys)
        {
            List<ModuleButton> data;
            if (bSys)
            {
                data = await Cache.GetAsync<List<ModuleButton>>("sys", "buttons");
                if (data == null)
                {
                    data = await ModuleButtonApp.GetSaasModuleListAsync(new ModuleButtonOption { IsEnabled = true });
                    await Cache.AddAsync("sys", data, new TimeSpan(0, 30, 0), "buttons");
                }
            }
            else
            {
                data = await Cache.GetAsync<List<ModuleButton>>(roleId.ToString(), "buttons");
                if (data == null)
                {
                    data = new List<ModuleButton>();
                    var buttondata = await ModuleButtonApp.GetSaasModuleListAsync(new ModuleButtonOption { IsEnabled = true });
                    var authorizedata = await RoleAuthorizeRep.Find(o => o.ObjectId == roleId && o.ItemType == 2).ToListAsync();
                    foreach (var item in authorizedata)
                    {
                        ModuleButton moduleButtonEntity = buttondata.Find(t => t.Id == item.ItemId);
                        if (moduleButtonEntity != null)
                        {
                            data.Add(moduleButtonEntity);
                        }
                    }
                    await Cache.AddAsync(roleId.ToString(), data, new TimeSpan(0, 30, 0), "buttons");
                }
            }
            return data.OrderBy(t => t.SortCode).ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task<bool> ActionValidate(long roleId, string action)
        {
            var authorizeurldata = new List<AuthorizeActionModel>();
            var cachedata = await Cache.GetAsync<List<AuthorizeActionModel>>(roleId.ToString(), "authorizeurl");
            if (cachedata == null)
            {
                var moduledata = await ModuleApp.GetSaasModuleListAsync();
                var buttondata = (await ModuleButtonApp.GetSaasModuleListAsync());
                var authorizedata = await RoleAuthorizeRep.Find(o => o.ObjectId == roleId).ToListAsync();
                foreach (var item in authorizedata)
                {
                    if (item.ItemType == 1)
                    {
                        Module module = moduledata.Find(t => t.Id == item.ItemId);
                        if (module != null)
                            authorizeurldata.Add(new AuthorizeActionModel { Id = module.Id, UrlAddress = module.UrlAddress });
                    }
                    else if (item.ItemType == 2)
                    {
                        ModuleButton moduleButton = buttondata.Find(t => t.Id == item.ItemId);
                        if (moduleButton != null)
                            authorizeurldata.Add(new AuthorizeActionModel { Id = moduleButton.ModuleId, UrlAddress = moduleButton.UrlAddress });
                    }
                }
                await Cache.AddAsync(roleId.ToString(), authorizeurldata, new TimeSpan(0, 30, 0), "authorizeurl");
            }
            else
            {
                authorizeurldata = cachedata;
            }
            foreach (var item in authorizeurldata)
            {
                if (!string.IsNullOrEmpty(item.UrlAddress))
                {
                    string[] url = item.UrlAddress.Split('?');
                    if (url[0].ToLower() == action.ToLower())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}