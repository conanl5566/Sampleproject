using dotNET.Domain.Entities.Sys;
using dotNET.Dto;
using dotNET.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotNET.Application.Sys
{
    public class ModuleButtonApp : IModuleButtonApp
    {
        #region 注入

        public IBaseRepository<ModuleButton> ModuleButtonRep { get; set; }

        #endregion 注入

        /// <summary>
        /// Saas模块按钮
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public async Task<List<ModuleButton>> GetSaasModuleListAsync(ModuleButtonOption option = null)
        {
            return await GetListAsync(option);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        private async Task<List<ModuleButton>> GetListAsync(ModuleButtonOption option)
        {
            var predicate = PredicateBuilder.True<ModuleButton>();

            if (option != null)
            {
                if (option.ModuleId.HasValue && option.ModuleId != 0)
                {
                    predicate = predicate.And(o => o.ModuleId == option.ModuleId.Value);
                }
                if (option.ParentId.HasValue)
                {
                    predicate = predicate.And(o => o.ParentId == option.ParentId.Value);
                }
            }
            return await ModuleButtonRep.Find(predicate).ToListAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ModuleButton> GetAsync(long id)
        {
            return await ModuleButtonRep.FindSingleAsync(o => o.Id == id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultDto> DeleteAsync(long id)
        {
            if (await ModuleButtonRep.GetCountAsync(o => o.ParentId == id) > 0)
            {
                return ResultDto.Err(msg: "删除失败！操作的对象包含了下级数据。");
            }
            await ModuleButtonRep.DeleteAsync(o => o.Id == id);
            return ResultDto.Suc();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="moduleButton"></param>
        /// <returns></returns>
        public async Task<ResultDto> CreateAsync(ModuleButton moduleButton)
        {
            moduleButton.Id = moduleButton.CreateId();
            moduleButton.CreatorTime = DateTime.Now;
            await ModuleButtonRep.AddAsync(moduleButton);
            return ResultDto.Suc();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="moduleButton"></param>
        /// <returns></returns>
        public async Task<ResultDto> UpdateAsync(ModuleButton moduleButton)
        {
            await ModuleButtonRep.UpdateAsync(moduleButton);
            return ResultDto.Suc();
        }
    }
}