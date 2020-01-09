#region using

using CompanyName.ProjectName.CommonServer;
using CompanyName.ProjectName.Core;
using CompanyName.ProjectName.Core.Security;
using CompanyName.ProjectName.ICommonServer.Sys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion using

namespace CompanyName.ProjectName.ICommonServer
{
    public class UserApp : AppService, IUserApp
    {
        #region 注入

        public IBaseRepository<User> UserRep { get; set; }
        public IBaseRepository<Role> RoleRep { get; set; }
        public IBaseRepository<Department> DepartmentRep { get; set; }
        public IOperateLogApp OperateLogApp { get; set; }

        #endregion 注入

        #region 修改登录状态

        /// <summary>
        /// 修改登录状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="agentId"></param>
        /// <param name="curUser"></param>
        /// <returns></returns>
        public async Task<ResultDto> Updatestatus(long Id, CurrentUser curUser)
        {
            var entry = await GetAsync(Id);
            if (entry == null)
                return ResultDto.Err(msg: "该用户不存在");
            int s = 1;
            if (entry.State == 1)
            {
                s = 0;
            }
            await UserRep.UpdateAsync(u => u.Id == Id, u => new User { State = s });
            return ResultDto.Suc();
        }

        #endregion 修改登录状态

        #region 用户退出操作

        /// <summary>
        /// 用户退出操作
        /// </summary>
        /// <param name="curUser">登录用户信息</param>
        /// <returns></returns>
        public async Task LogOffAsync(CurrentUser curUser)
        {
            await OperateLogApp.CustomLogAsync(curUser, "用户退出", curUser.RealName + "进行了退出操作");
        }

        #endregion 用户退出操作

        /// <summary>
        ///  根据账号模糊查询获取列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public async Task<List<IdAccountDto>> SelectDataAsync(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return new List<IdAccountDto>();

            var result = UserRep.Find(o => o.Account.Contains(q.Trim()));

            if (result == null || !result.Any())
            {
                return new List<IdAccountDto>();
            }
            return result.Select(o => new IdAccountDto() { Id = o.Id, Account = o.Account }).ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="rowsPrePage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<PageResult<UserSunpleDto>> GetPageAsync(int pageNumber, int rowsPrePage, UserOption filter)
        {
            List<UserSunpleDto> data = new List<UserSunpleDto>();
            PageResult<UserSunpleDto> list = new PageResult<UserSunpleDto>();
            string orderby = " id desc";
            var predicate = PredicateBuilder.True<User>();
            predicate = predicate.And(o => o.DeleteMark == null);
            if (!string.IsNullOrWhiteSpace(filter.Account))
            {
                predicate = predicate.And(o => o.Account == filter.Account);
            }
            if (!string.IsNullOrWhiteSpace(filter.RealName))
            {
                predicate = predicate.And(o => o.RealName == filter.RealName);
            }
            var tlist = await UserRep.Find(pageNumber, rowsPrePage, orderby, predicate).ToListAsync() ?? new List<User>();
            data = tlist.MapToList<UserSunpleDto>();
            List<long> roleIds = tlist.Select(o => o.RoleId).Distinct().ToList();
            if (roleIds.Any())
            {
                var roles = await RoleRep.Find(o => roleIds.Contains(o.Id)).ToListAsync();
                foreach (var d in data)
                {
                    var r = roles.FirstOrDefault(o => o.Id == d.RoleId);
                    d.RoleName = r?.Name;
                }
            }
            List<long?> departmentIds = tlist.Select(o => o.DepartmentId).Distinct().ToList();
            departmentIds.Remove(null);
            if (departmentIds.Any())
            {
                var departments = await DepartmentRep.Find(o => departmentIds.Contains(o.Id)).ToListAsync();
                foreach (var d in data)
                {
                    var r = departments.FirstOrDefault(o => o.Id == d.DepartmentId);
                    d.deptname = r?.Name;
                }
            }
            list.Data = data.ToList();
            int total = await UserRep.GetCountAsync(predicate);
            list.ItemCount = total;
            return list;
        }

        /// <summary>
        /// 添加管理人员
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ResultDto> InsertAsync(User entity, CurrentUser curUser)
        {
            entity.Id = entity.CreateId();
            entity.UserSecretkey = "";
            entity.CreatorTime = DateTime.Now;
            entity.CreatorUserId = curUser.Id;
            var r = await InsertAsync(entity);
            if (!string.IsNullOrEmpty(r.Error))
            {
                return ResultDto.Err(msg: r.Error);
            }
            await OperateLogApp.InsertLogAsync<User>(curUser, "添加用户", entity);
            return ResultDto.Suc();
        }

        /// <summary>
        /// 添加（判断Account是否已存在）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>-1： 失败 ， 0：已存在 ，1：成功</returns>
        public async Task<(string Error, User user)> InsertAsync(User entity)
        {
            //已存在
            int count = await UserRep.GetCountAsync(o => o.Account == entity.Account);
            if (count > 0)
            {
                return ("帐号已存在", null);
            }
            await UserRep.AddAsync(entity);

            return (string.Empty, null);
        }

        /// <summary>
        ///  更新用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="curUser"></param>
        /// <returns></returns>
        public async Task<ResultDto> UpdateUserInfoAsync(User entity, CurrentUser curUser)
        {
            await UserRep.UpdateAsync(entity);

            return ResultDto.Suc();
        }

        /// <summary>
        /// 删除管理人员
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ResultDto> DeleteAsync(long Id, long agentId, CurrentUser curUser)
        {
            var r = await DeleteAsync(Id, agentId, curUser.Id);
            if (!string.IsNullOrEmpty(r.Error))
            {
                return ResultDto.Err(msg: r.Error);
            }
            return ResultDto.Suc();
        }

        /// <summary>
        /// 删除（假删除）
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="agentId"></param>
        /// <param name="optId"></param>
        /// <returns>-1： 失败 ， 0：不存在 ，1：成功</returns>
        public async Task<(string Error, User User)> DeleteAsync(long Id, long agentId, long optId)
        {
            User user = await UserRep.FindSingleAsync(o => o.Id == Id);
            if (user == null || user.DeleteMark == true)
            {
                return ("帐号不存在", null);
            }
            user.DeleteMark = true;
            user.DeleteTime = DateTime.Now;
            user.DeleteUserId = optId;
            await UserRep.UpdateAsync(user);

            return (string.Empty, user);
        }

        /// <summary>
        /// Saas后台管理登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<(string Error, User User)> SaasLoginAsync(string account, string password, string ip = "")
        {
            User user = await UserRep.FindSingleAsync(o => o.Account == account);
            if (user == null)
            {
                return ($"帐号不存在", null);
            }
            if (user.State == 0)
            {
                return ($"帐号禁止登录", null);
            }
            if (user.Password != password)
            {
                return ($"密码不正确", null);
            }
            CurrentUser curUser = new CurrentUser
            {
                Id = user.Id,
                RealName = user.Account,
                LoginIPAddress = ip
            };
            await OperateLogApp.CustomLogAsync(curUser, "用户登录", user.RealName + "进行了登录操作");
            await UserRep.UpdateAsync(o => o.Id == user.Id, o => new User() { LastLoginTime = DateTime.Now });
            return (string.Empty, user);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="password">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public async Task<ResultDto> ChangePasswordAsync(long Id, string password, string newPassword, long agentId, CurrentUser curUser)
        {
            User user = await UserRep.FindSingleAsync(o => o.Id == Id);
            if (user == null || user.DeleteMark == true)
            {
                return ResultDto.Err($"帐号({Id})不存在");
            }
            if (user.Password != MD5Encrypt.MD5(password))
            {
                return ResultDto.Err($"原密码不正确");
            }
            newPassword = MD5Encrypt.MD5(newPassword);
            await UserRep.UpdateAsync(o => o.Id == Id, o => new User() { Password = newPassword });

            return ResultDto.Suc();
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="newPassword"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public async Task<ResultDto> ResetPasswordAsync(long Id, string password, long agentId, CurrentUser curUser)
        {
            User user = await UserRep.FindSingleAsync(o => o.Id == Id);
            if (user == null)
            {
                return ResultDto.Err($"帐号({Id})不存在");
            }
            password = MD5Encrypt.MD5(password);
            await UserRep.UpdateAsync(o => o.Id == Id, o => new User() { Password = password });

            return ResultDto.Suc();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<User> GetAsync(long Id)
        {
            return await UserRep.FindSingleAsync(o => o.Id == Id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<User> GetAsync(string account)
        {
            return await UserRep.FindSingleAsync(o => o.Account == account);
        }
    }
}