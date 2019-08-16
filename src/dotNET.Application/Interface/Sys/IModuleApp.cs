using System.Collections.Generic;
using System.Threading.Tasks;
using dotNET.Domain.Entities.Sys;
using dotNET.Dto;

namespace dotNET.Application.Sys
{
    public interface IModuleApp : IAppService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<R> DeleteAsync(long key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<Module> GetAsync(long key);

        /// <summary>
        /// Saas模块  
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        Task<List<Module>> GetSaasModuleListAsync(ModuleOption option = null);

        /// <summary>
        /// 返回菜单目录
        /// </summary>
        /// <returns></returns>
        Task<List<Module>> GetMenuCatalogListAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        Task<R> CreateAsync(Module moduleEntity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        Task<R> UpdateAsync(Module moduleEntity);
    }
}