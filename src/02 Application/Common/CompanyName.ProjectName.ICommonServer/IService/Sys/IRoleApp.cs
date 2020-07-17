#region using

using CompanyName.ProjectName.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion using

namespace CompanyName.ProjectName.ICommonServer.Sys
{
    /// <summary>
    /// 角色
    /// </summary>
    public interface IRoleApp : IAppService
    {
        #region 添加Role

        /// <summary>
        /// 添加Role
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="permissionIds"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        Task<ResultDto<Role>> CreateAsync(Role entity, List<long> permissionIds, CurrentUser currentUser);

        #endregion 添加Role

        #region 修改

        /// <summary>
        ///  修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="permissionIds"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        Task<ResultDto<Role>> UpdateAsync(Role entity, List<long> permissionIds, CurrentUser currentUser);

        #endregion 修改

        #region 删除Role

        /// <summary>
        /// 删除Role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        Task<(bool s, string msg)> DeleteAsync(long id, CurrentUser currentUser);

        #endregion 删除Role

        #region 分页

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        Task<IEnumerable<Role>> GetListAsync(RoleOption option);

        Task<PageResult<Role>> GetPageAsync(int pageNumber, int rowsPrePage, RoleOption filter);

        #endregion 分页

        #region 名称是否存在

        /// <summary>
        /// 名称是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> CheckCode(string name, long id);

        #endregion 名称是否存在

        #region 获取

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Role> GetAsync(long id);

        #endregion 获取
    }
}