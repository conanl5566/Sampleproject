﻿#region using

using dotNET.Core;
using dotNET.Dto.WebConfig;
using System.Threading.Tasks;

#endregion using

namespace dotNET.Application.Sys
{
    /// <summary>
    /// 配置服务
    /// </summary>
    public interface IWebConfigApp : IAppService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        Task<R<long>> CreateAsync(CreateWebConfigDto entityDto, CurrentUser currentUser);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        Task<R> UpdateAsync(UpdateWebConfigDto entityDto, CurrentUser currentUser);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        Task<R> DeleteAsync(long Id, CurrentUser currentUser);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="rowsPrePage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<R<PageResult<WebConfigDto>>> GetPageAsync(WebConfigOption filter);

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<R<WebConfigDto>> GetDetailAsync(long id);
    }
}