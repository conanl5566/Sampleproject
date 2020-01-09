#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using conan.Utility;
using conan.Utility.Security;
using conan.Data;
using conan.Repository;
using conan.Domain.Entities;
using conan.Application.Infrastructure;
using conan.Dto;
#endregion

namespace conan.Application.App
{
    public class AgentApp : App, IAgentApp
    {
        IDbContext context;
        AgentRep _agentRep;
        IOperateLogApp _operateLogApp;

        public AgentApp(IDbContext dbContext, IOperateLogApp operateLogApp) : base(dbContext)
        {
            context = dbContext;
            _agentRep = new AgentRep();
            _operateLogApp = operateLogApp;
        }

        #region 测试

        public async Task test22(string sql)
        {
             await  _agentRep.ExecuteAsync(sql);
        }
            public async Task test()
        {


            Agent entity2 = new Agent();
            entity2.Id = entity2.CreateId();
            entity2.Name = "111";
            entity2.ContactNumber = "111";
            entity2.Key = "111";
            entity2.Secret = "111";
            entity2.Remarks = "111";
         await _agentRep.InsertAsync(entity2);


          context.BeginTransaction();

            Agent entity = new Agent();
            entity.Id = entity.CreateId();
            entity.Name ="111" ;
            entity.ContactNumber = "111";
            entity.Key = "111";
            entity.Secret = "111";
            entity.Remarks = "111";
            int r = await _agentRep.InsertAsync(entity);
           context.Commit();


            Agent entity3 = new Agent();
            entity3.Id = entity3.CreateId();
            entity3.Name = "111";
            entity3.ContactNumber = "111";
            entity3.Key = "111";
            entity3.Secret = "111";
            entity3.Remarks = "111";
            await _agentRep.InsertAsync(entity3);



        }

        #endregion



        #region 根据账号模糊查询获取列表
        /// <summary>
        /// 根据账号模糊查询获取列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public async Task<List<Agent>> GetLsitAsync(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return new List<Agent>();
            }
            else
            {
                var result = await _agentRep.GetListAsync("where Name like @Name", new { Name = "%" + q.Trim() + "%" });
                if (result == null || result.Count() == 0)
                {
                    return new List<Agent>();
                }
                else
                {
                    return result.ToList();
                }
            }
        } 
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public async Task<Page<AgentDto>> GetPage(AgentOption option)
        {
            var sql = Sql.Builder;
            //   sql.Append("where 1<>1 or id<@id",new { id= 2072 });
            if (!string.IsNullOrWhiteSpace(option.Name))
            {
                sql = sql.Where("`Name` like @Name", new { Name = "%" + option.Name.Trim() + "%" });
            }
            if (!string.IsNullOrWhiteSpace(option.ContactNumber))
            {
                sql = sql.Where("`ContactNumber`=@ContactNumber", new { ContactNumber = option.ContactNumber.Trim() });
            }
            //if (option.State.HasValue)
            //{
            //    sql = sql.Where("`State`=@State", new { State = option.State.Value });
            //}

            List<AgentDto> data = new List<AgentDto>();
            int total = await _agentRep.RecordCountAsync(sql.SQL, sql.Arguments);
            if (total > 0)
            {
                List<Agent> lists = (await _agentRep.GetListPagedAsync<Agent>(option.PageIndex, option.Limit, sql.SQL, option.OrderBy, sql.Arguments)).ToList();
                data = MapperHelper.MapList<Agent, AgentDto>(lists);

            }
            return new Page<AgentDto>(total, data);
        }

        #endregion

        #region 创建
        public async Task<R> CreateAsync(Agent entity, CurrentUser CurrentUser)
        {
            entity.Name = entity.Name.Trim();
            entity.ContactNumber = entity.ContactNumber?.Trim();
            entity.Key = entity.Key?.Trim();
            entity.Secret = entity.Secret?.Trim();
            entity.Remarks = entity.Remarks?.Trim();

            int count = await _agentRep.RecordCountAsync(new { Name = entity.Name });
            if (count > 0)
            {
                return R.Err(msg: entity.Name + " 已存在");
            }

            int r = await _agentRep.InsertAsync(entity);
            if (r > 0)
            {
                await _operateLogApp.InsertAsync<Agent>(CurrentUser, "添加代理商", entity);

                return R.Suc();
            }

            return R.Err();
        }

        #endregion

        #region 获取
        public async Task<Agent> GetAsync(long key)
        {
            Agent m = await _agentRep.GetAsync(key);
            return m;
        }
        #endregion

        #region 修改
        public async Task<R> UpdateAsync(Agent entity, CurrentUser CurrentUser)
        {
            entity.Name = entity.Name.Trim();
            entity.ContactNumber = entity.ContactNumber?.Trim();
            entity.Key = entity.Key?.Trim();
            entity.Secret = entity.Secret?.Trim();
            entity.Remarks = entity.Remarks?.Trim();

            int count = await _agentRep.RecordCountAsync("where `Name`=@Name and `Id`<>@Id", new { Name = entity.Name, Id = entity.Id });
            if (count > 0)
            {
                return R.Err(msg: entity.Name + " 已存在");
            }
            bool b = await _agentRep.UpdateAsync(entity);
            if (b)
            {
                await _operateLogApp.InsertAsync<Agent>(CurrentUser, "修改代理商", entity);

                return R.Suc();
            }

            return R.Err();
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<R> DeleteAsync(long Id, CurrentUser CurrentUser)
        {
            var entity = await _agentRep.GetAsync(Id);
            if (entity == null)
                return R.Err("数据不存在");

            bool status = await _agentRep.DeleteAsync(Id);
            if (status == false)
            {
                return R.Err("删除失败");
            }
            await _operateLogApp.InsertAsync<Agent>(CurrentUser, "删除代理商", entity);

            return R.Suc();
        }

        #endregion

        #region 修改状态
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<R> UpdateStatusAsync(long Id, CurrentUser CurrentUser)
        {
            var model = await GetAsync(Id);
            if (model == null)
            {
                return R.Err(msg: Id + " 不存在");
            }
            bool s = model.State == true ? false : true;


            bool r = await _agentRep.UpdateAsync<Agent>(new { Id = Id, state = s });
            if (r == false)
            {
                return R.Err("修改失败");
            }

            string logstr = "代理商状态从" + (model.State == true ? "启用到禁用" : "禁用到启用");

            await _operateLogApp.InsertCusAsync(CurrentUser, "代理商状态", logstr, Id, "AgentS");

            return R.Suc();
        }

        #endregion
    }
}
