#region using

using dotNET.Core;
using dotNET.ICommonServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using dotNET.ICommonServer;

using dotNET.ICommonServer.Sys;
using dotNET.CommonServer;

#endregion using

namespace dotNET.ICommonServer
{
    public class WebConfigApp : AppService, IWebConfigApp
    {
        #region 注入

        public IBaseRepository<WebConfig> WebConfigAppRep { get; set; }

        #endregion 注入

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entityDto"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public async Task<ResultDto<long>> CreateAsync(CreateWebConfigDto entityDto, CurrentUser currentUser)
        {
            var isExist = await WebConfigAppRep.Find(o => o.ConfigKey == entityDto.ConfigKey).AnyAsync();
            if (isExist)
            {
                return ResultDto<long>.Err(msg: "key已存在");
            }
            var dto = entityDto.MapTo<WebConfig>();
            dto.Id = dto.CreateId();
            dto.CreatorUserId = currentUser?.Id;
            dto.CreatorTime = DateTime.Now;
            await WebConfigAppRep.AddAsync(dto);
            return ResultDto<long>.Suc(dto.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entityDto"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public async Task<ResultDto> UpdateAsync(UpdateWebConfigDto entityDto, CurrentUser currentUser)
        {
            var entity = await WebConfigAppRep.FindSingleAsync(o => o.Id == entityDto.Id);
            if (entity == null)
            {
                return ResultDto.Err(msg: "数据不存在");
            }
            var isExist = await WebConfigAppRep.Find(o => o.ConfigKey == entityDto.ConfigKey && o.Id != entityDto.Id).AnyAsync();
            if (isExist)
            {
                return ResultDto.Err("key已存在");
            }
            var dto = entityDto.MapToMeg<UpdateWebConfigDto, WebConfig>(entity);
            await WebConfigAppRep.UpdateAsync(dto);
            return ResultDto.Suc();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public async Task<ResultDto> DeleteAsync(long id, CurrentUser currentUser)
        {
            var entity = await WebConfigAppRep.FindSingleAsync(o => o.Id == id);
            if (entity == null)
            {
                return ResultDto.Err(msg: "数据不存在");
            }
            await WebConfigAppRep.DeleteAsync(o => o.Id == id);
            return ResultDto.Suc();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<ResultDto<PageResult<WebConfigDto>>> GetPageAsync(WebConfigOption filter)
        {
            List<WebConfigDto> data = new List<WebConfigDto>();
            PageResult<WebConfigDto> list = new PageResult<WebConfigDto>();
            string orderby = " id desc";
            var predicate = PredicateBuilder.True<WebConfig>();
            if (!string.IsNullOrWhiteSpace((filter.ConfigKey)))
            {
                predicate = predicate.And(o => o.ConfigKey == filter.ConfigKey);
            }
            var tlist = await WebConfigAppRep.Find(filter.PageNumber, filter.RowsPrePage, orderby, predicate).ToListAsync() ?? new List<WebConfig>();
            data = tlist.MapToList<WebConfigDto>();
            list.Data = data.ToList();
            int total = await WebConfigAppRep.GetCountAsync(predicate);
            list.ItemCount = total;
            return ResultDto<PageResult<WebConfigDto>>.Suc(list);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultDto<WebConfigDto>> GetDetailAsync(long id)
        {
            var entity = await WebConfigAppRep.FindSingleAsync(o => o.Id == id);
            return entity == null ? ResultDto<WebConfigDto>.Err(msg: "数据不存在") : ResultDto<WebConfigDto>.Suc(entity.MapTo<WebConfigDto>());
        }
    }
}