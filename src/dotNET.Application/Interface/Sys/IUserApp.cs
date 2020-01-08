#region using

/**************************************************************************
* 作者：X
* 日期：2017.01.18
* 描述：后台管理用户 操作 Saas后台和代理后台
* 修改记录：
* ***********************************************************************/

using dotNET.Core;
using dotNET.Domain.Entities.Sys;
using dotNET.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion using

namespace dotNET.Application.Sys
{
    /// <summary>
    /// 用户
    /// </summary>
    public interface IUserApp : IAppService
    {
        #region 修改登录状态

        /// <summary>
        /// 修改登录状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="agentId"></param>
        /// <param name="curUser"></param>
        /// <returns></returns>
        Task<R> Updatestatus(long Id, CurrentUser curUser);

        #endregion 修改登录状态

        #region 用户退出操作

        /// <summary>
        /// 用户退出操作
        /// </summary>
        /// <param name="curUser">登录用户信息</param>
        /// <returns></returns>
        Task LogOffAsync(CurrentUser curUser);

        #endregion 用户退出操作

        /// <summary>
        /// 根据账号模糊查询获取列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        Task<List<IdAccountDto>> SelectDataAsync(string q);

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="rowsPrePage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>

        Task<PageResult<UserSunpleDto>> GetPageAsync(int pageNumber, int rowsPrePage, UserOption filter);

        /// <summary>
        /// 添加管理人员
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<R> InsertAsync(User entity, CurrentUser curUser);

        /// <summary>
        ///  更新用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="curUser"></param>
        /// <returns></returns>
        Task<R> UpdateUserInfoAsync(User entity, CurrentUser curUser);

        /// <summary>
        /// 删除管理人员
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<R> DeleteAsync(long Id, long agentId, CurrentUser curUser);

        /// <summary>
        /// Saas后台管理登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<(string Error, User User)> SaasLoginAsync(string account, string password, string ip = "");

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="password">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        Task<R> ChangePasswordAsync(long Id, string password, string newPassword, long agentId, CurrentUser curUser);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="newPassword"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        Task<R> ResetPasswordAsync(long Id, string password, long agentId, CurrentUser curUser);

        /// <summary>
        ///
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<User> GetAsync(long Id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<User> GetAsync(string account);
    }
}