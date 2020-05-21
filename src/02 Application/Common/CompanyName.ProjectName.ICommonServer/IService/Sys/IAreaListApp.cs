#region using

using CompanyName.ProjectName.CommonServer;
using CompanyName.ProjectName.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion using

namespace CompanyName.ProjectName.ICommonServer.Sys
{
    /// <summary>
    /// 地区
    /// </summary>
    public interface IAreaListApp : IAppService
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task TestAsync();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task Test2Async();

        /// <summary>
        /// 地区菜单
        /// </summary>
        /// <returns></returns>
        Task<List<AreaList>> GetMenuListAsync();

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<List<AreaList>> GetListAsync(long parentId);
    }
}