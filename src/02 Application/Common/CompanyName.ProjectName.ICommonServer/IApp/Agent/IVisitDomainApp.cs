using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using conan.Domain.Entities;
using conan.Utility;

namespace conan.Application.Infrastructure
{
    /// <summary>
    /// 代理访问域名
    /// </summary>
    public interface IVisitDomainApp:IApp
    {
        /// <summary>
        /// 代理访问域名加入缓存
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        Task<R> AddToCacheAsync(VisitDomain vdomain);
        /// <summary>
        /// 代理访问域名启用
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        Task<R> EnableAsync(long id, CurrentUser curUser);
        /// <summary>
        /// 代理访问域名禁用
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        Task<R> DisableAsync(long id, CurrentUser curUser);

        /// <summary>
        /// 代理访问域名添加
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        Task<R> InsertAsync(VisitDomain entity, CurrentUser curUser);

        /// <summary>
        /// 代理访问域名删除
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        Task<R> DeleteAsync(long Id, CurrentUser curUser);

        /// <summary>
        /// 检查域名对应代理
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        Task<object> CheckAgnet(string domain);
    }
}
