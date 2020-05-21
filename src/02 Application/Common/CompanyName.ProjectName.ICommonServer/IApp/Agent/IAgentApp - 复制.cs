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
    public interface IAgentApp22 : IApp
    {

      
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        Task<R> CreateAsync(conan.Domain.Entitiestt.User Entity, CurrentUser CurrentUser);

    
        


    }
}
