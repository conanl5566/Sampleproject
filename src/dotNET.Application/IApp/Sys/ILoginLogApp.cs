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
using dotNET.Domain.Entities;
using dotNET.Utility;
using dotNET.Dto;
#endregion

namespace dotNET.Application.Infrastructure
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public interface ILoginLogApp : IApp
    {  
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        Task<Page<LoginLogDtoext>> PagerAsync(LoginLogOption option);

        Task<PageResult<LoginLogDtoext>> GetPageAsync(int pageNumber, int rowsPrePage, LoginLogOption filter);
    }
}
