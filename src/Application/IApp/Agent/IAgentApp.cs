#region using
/**************************************************************************
* 作者：X   
* 日期：2017.01.18   
* 描述：后台管理用户 操作 Saas后台和代理后台
* 修改记录：    
* ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using conan.Domain.Entities;
using conan.Utility;
using conan.Dto;
#endregion

namespace conan.Application.Infrastructure
{
    /// <summary>
    /// 代理商
    /// </summary>
    public interface IAgentApp : IApp
    {

         Task test22(string sql);
         Task test();

        /// <summary>
        /// 根据账号模糊查询获取列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        Task<List<Agent>> GetLsitAsync(string q);


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        Task<Page<AgentDto>> GetPage(AgentOption option);
       

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        Task<R> CreateAsync(Agent Entity, CurrentUser CurrentUser);

        /// <summary>
        /// 获取 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<Agent> GetAsync(long key);


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        Task<R> UpdateAsync(Agent Entity, CurrentUser CurrentUser);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<R> DeleteAsync(long Id, CurrentUser CurrentUser);

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<R> UpdateStatusAsync(long Id, CurrentUser CurrentUser);

        


    }
}
