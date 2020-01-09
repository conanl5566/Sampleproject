using CompanyName.ProjectName.CommonServer;
using CompanyName.ProjectName.Core;
using CompanyName.ProjectName.ICommonServer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.ICommonServer.Sys
{
    public interface IRoleAuthorizeApp : IAppService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ResultDto> CreateAsync(RoleAuthorize entity);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        Task CreateAsync(List<RoleAuthorize> entitys);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(long id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeleteAsync(IEnumerable<long> ids);

        /// <summary>
        ///
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="objectType">对象分类 1-角色 2-部门 3-用户</param>
        /// <returns></returns>
        Task<List<RoleAuthorize>> GetListAsync(long objectId, int objectType);

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="bSys"></param>
        /// <returns></returns>
        Task<List<Module>> GetModuleList(long roleId, bool bSys);

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="bSys"></param>
        /// <returns></returns>
        Task<List<ModuleButton>> GetButtonList(long roleId, bool bSys);

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        Task<bool> ActionValidate(long roleId, string action);
    }
}