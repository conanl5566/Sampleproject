#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using dotNET.Utility;
using dotNET.Utility.Security;
using dotNET.Data;
using dotNET.Repository;
using dotNET.Domain.Entities;
using dotNET.Application.Infrastructure;
using dotNET.Domain.Entities.Sys;
using dotNET.Dto;
#endregion

namespace dotNET.Application.App
{
    public class LoginLogApp : App, ILoginLogApp
    {
        #region 注入
        public IRepositoryBase<LoginLog> _LoginLogRep { get; set; }
        public IRepositoryBase<User> _UserRep { get; set; }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public async Task<Page<LoginLogDtoext>> PagerAsync(LoginLogOption option)
        {
            var sql = Sql.Builder;
            if (!string.IsNullOrWhiteSpace(option.LoginId) && option.LoginId != "0")
            {
                sql = sql.Where("`LoginId` = @LoginId", new { LoginId = option.LoginId });
            }
            if (!string.IsNullOrWhiteSpace(option.AgentId) && option.AgentId != "0")
            {
                sql = sql.Where("`AgentId` = @AgentId", new { AgentId = option.AgentId });
            }
            if (!string.IsNullOrWhiteSpace(option.LoginType))
            {
                sql = sql.Where("`LoginType`=@LoginType", new { LoginType = option.LoginType.Trim() });
            }
            if (option.kCreatorTime != null && option.kCreatorTime.HasValue)
            {
                sql = sql.Where("`CreatorTime`>=@kCreatorTime", new { kCreatorTime = option.kCreatorTime.Value });
            }
            if (option.eCreatorTime != null && option.eCreatorTime.HasValue)
            {
                sql = sql.Where("`CreatorTime`<=@eCreatorTime", new { eCreatorTime = option.eCreatorTime.Value });
            }
            List<LoginLogDtoext> data = new List<LoginLogDtoext>();
            int total = await _LoginLogRep.RecordCountAsync(sql.SQL, sql.Arguments);
            if (total > 0)
            {
                if (option.Offset > total)
                    option.Offset = total;
                List<LoginLog> lists = (await _LoginLogRep.GetListPagedAsync<LoginLog>(option.PageIndex, option.Limit, sql.SQL, option.OrderBy, sql.Arguments)).ToList();
                data = MapperHelper.MapList<LoginLog, LoginLogDtoext>(lists);
                var lids = lists.Select(o => o.LoginId).ToList();
                var ulist = await _UserRep.GetListAsync("where `id` in (" + string.Join(",", lids) + ")");
            }
            return new Page<LoginLogDtoext>(total, data);
        }
        #endregion

        public async Task<PageResult<LoginLogDtoext>> GetPageAsync(int pageNumber, int rowsPrePage, LoginLogOption option)
        {
            List<LoginLogDtoext> data = new List<LoginLogDtoext>();
            PageResult<LoginLogDtoext> list = new PageResult<LoginLogDtoext>();
            string orderby = " id desc";
            var sql = Sql.Builder;
            if (!string.IsNullOrWhiteSpace(option.LoginId) && option.LoginId != "0")
            {
                sql = sql.Where("`LoginId` = @LoginId", new { LoginId = option.LoginId });
            }
            if (option.kCreatorTime != null && option.kCreatorTime.HasValue)
            {
                sql = sql.Where("`CreatorTime`>=@kCreatorTime", new { kCreatorTime = option.kCreatorTime.Value });
            }
            if (option.eCreatorTime != null && option.eCreatorTime.HasValue)
            {
                sql = sql.Where("`CreatorTime`<=@eCreatorTime", new { eCreatorTime = option.eCreatorTime.Value });
            }
            var tlist = await _LoginLogRep.GetListPagedAsync(pageNumber, rowsPrePage, sql.SQL, orderby, sql.Arguments);
            data = MapperHelper.MapList<LoginLog, LoginLogDtoext>(tlist.ToList());
            if (data != null && data.Count > 0)
            {
                var lids = tlist.Select(o => o.LoginId).ToList();
                var ulist = await _UserRep.GetListAsync("where `id` in (" + string.Join(",", lids) + ")");
            }
            list.Data = data.ToList();
            int total = await _LoginLogRep.RecordCountAsync(sql.SQL, sql.Arguments);
            list.ItemCount = total;
            return list;
        }
    }
}