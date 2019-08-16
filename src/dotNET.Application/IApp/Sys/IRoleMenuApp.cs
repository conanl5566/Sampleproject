#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AC.Domain.Entities;
using AC.Utility;
using AC.Dto; 
#endregion

namespace AC.Application.Infrastructure
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public interface IRoleMenuApp : IApp
    {

        #region 获取角色权限
        /// <summary>
        ///  获取角色权限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<List<RoleMenu>> GetlistAsync(long Id);

        #endregion

        #region 保存角色权限
        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissionCodes"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<R> AssignPermissionFor(long roleId, string[] permissionCodes, CurrentUser CurrentUser);
         #endregion

    }
}
