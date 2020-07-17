using CompanyName.ProjectName.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.ICommonServer.Sys
{
    public interface IModuleApp : IAppService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<ResultDto> DeleteAsync(long key);

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
        Task<ResultDto> CreateAsync(Module moduleEntity);

        /// <summary>
        ///
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        Task<ResultDto> UpdateAsync(Module moduleEntity);
    }
}