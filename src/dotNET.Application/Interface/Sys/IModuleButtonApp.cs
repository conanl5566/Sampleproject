using dotNET.Domain.Entities.Sys;
using dotNET.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotNET.Application.Sys
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
        Task<R> DeleteAsync(long id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="moduleButton"></param>
        /// <returns></returns>
        Task<R> CreateAsync(ModuleButton moduleButton);

        /// <summary>
        ///
        /// </summary>
        /// <param name="moduleButton"></param>
        /// <returns></returns>
        Task<R> UpdateAsync(ModuleButton moduleButton);
    }
}