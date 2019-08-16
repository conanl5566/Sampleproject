#region using

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using dotNET.Domain.Entities.Sys;
using dotNET.EntityFrameworkCore;
using Hangfire;
using Microsoft.EntityFrameworkCore;

#endregion
namespace dotNET.Application.Sys
{
    /// <summary>
    /// 地区
    /// </summary>
    public class AreaListApp : AppService, IAreaListApp
    {

        //public IBaseRepository<AreaList> _areaListBaseRepository { get; set; }
        //public IDepartmentApp _departmentApp { get; set; }

        public IBaseRepository<AreaList> AreaListBaseRepository;
        public IDepartmentApp DepartmentApp;
        public AreaListApp(IBaseRepository<AreaList> areaListBaseRepository, IDepartmentApp departmentApp)
        {
            AreaListBaseRepository = areaListBaseRepository;
            DepartmentApp = departmentApp;
        }

        /// <summary>
        /// 测试job 指定队列
        /// </summary>
        /// <returns></returns>
        [Queue("critical")]
        public async Task TestAsync()
        {
            //  await  Task.Delay(1000);
            //todo
            await AreaListBaseRepository.Find(o => o.AreaType != 3).ToListAsync();
            await DepartmentApp.GetDepartmentListAsync();
        }

        /// <summary>
        /// 测试job
        /// </summary>
        /// <returns></returns>
        public async Task Test2Async()
        {
            // await Task.Delay(1000);
            //todo
            await AreaListBaseRepository.Find(o => o.AreaType != 3).ToListAsync();
            await DepartmentApp.GetDepartmentListAsync();
        }

        #region 地区菜单  
        /// <summary>
        /// 省  市 
        /// </summary>
        /// <returns></returns>
        public async Task<List<AreaList>> GetMenuListAsync()
        {
            return await AreaListBaseRepository.Find(o => o.AreaType != 3).ToListAsync();
        }
        #endregion

        #region 列表
        /// <summary>
        /// 子地区
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>

        public async Task<List<AreaList>> GetListAsync(long parentId)
        {
          
            var predicate = PredicateBuilder.True<AreaList>();
            predicate = predicate.And(o => o.ParentID == parentId);
            return await AreaListBaseRepository.Find(predicate).ToListAsync();
        }
        #endregion
    }
}