using dotNET.Domain.Entities.Sys;
using dotNET.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotNET.Application.Sys
{
    public interface IDepartmentApp : IAppService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<R> DeleteAsync(long key);

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<Department> GetAsync(long key);

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        Task<List<Department>> GetListAsync(DepartmentOption option);

        /// <summary>
        /// 返回部门目录
        /// </summary>
        /// <returns></returns>
        Task<List<Department>> GetDepartmentListAsync();

        /// <summary>
        ///
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        Task<R> CreateAsync(Department moduleEntity);

        /// <summary>
        ///
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        Task<R> UpdateAsync(Department moduleEntity);
    }
}