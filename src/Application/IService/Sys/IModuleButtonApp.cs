using CompanyName.ProjectName.Core;
using CompanyName.ProjectName.ICommonServer;
using System.Collections.Generic;
using System.Threading.Tasks;

using CompanyName.ProjectName.ICommonServer;

using CompanyName.ProjectName.CommonServer;

namespace CompanyName.ProjectName.ICommonServer.Sys
{
    public interface IModuleButtonApp : IAppService
    {
        /// <summary>
        /// Saas模块按钮
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        Task<List<ModuleButton>> GetSaasModuleListAsync(ModuleButtonOption option = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ModuleButton> GetAsync(long id);

        /// <summary>
        /// 按Id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultDto> DeleteAsync(long id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="moduleButton"></param>
        /// <returns></returns>
        Task<ResultDto> CreateAsync(ModuleButton moduleButton);

        /// <summary>
        ///
        /// </summary>
        /// <param name="moduleButton"></param>
        /// <returns></returns>
        Task<ResultDto> UpdateAsync(ModuleButton moduleButton);
    }
}