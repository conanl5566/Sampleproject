using dotNET.Domain.Entities.Sys;
using dotNET.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotNET.Application.Sys
{
    public interface IItemsDataApp : IAppService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<ResultDto> DeleteAsync(long key);

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<ItemsData> GetAsync(long key);

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        Task<List<ItemsData>> GetListAsync(ItemsDataOption option);

        /// <summary>
        /// 返回部门目录
        /// </summary>
        /// <returns></returns>
        Task<List<ItemsData>> GetItemsDataListAsync();

        /// <summary>
        ///
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        Task<ResultDto> CreateAsync(ItemsData moduleEntity);

        /// <summary>
        ///
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <returns></returns>
        Task<ResultDto> UpdateAsync(ItemsData moduleEntity);
    }
}