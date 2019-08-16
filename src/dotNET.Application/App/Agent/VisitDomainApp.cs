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
using conan.Utility.Cache;


namespace conan.Application.App
{
    public class VisitDomainApp :App, IVisitDomainApp
    {
        ICacheService _cache;
        IDbContext context;
        VisitDomainRep _visitDomainRep;
        IOperateLogApp _operateLogApp;

        string prefix = "VisitDomain";

        public VisitDomainApp(IDbContext dbContext, IOperateLogApp operateLogApp):base(dbContext)
        {
            context = dbContext;
            _visitDomainRep = new VisitDomainRep();
            _operateLogApp = operateLogApp;
         //   _cache = CacheFactory.Cache();
        }

        /// <summary>
        /// 代理访问域名加入缓存
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public async Task<R> AddToCacheAsync(VisitDomain vdomain)
        {
            bool b = await _cache.ExistsAsync(vdomain.Domain.ToLower(), prefix);
            if (b)
            {
                return R.Suc();
            }

            b = await _cache.AddAsync(vdomain.Domain.ToLower(), vdomain.AgentId, prefix);
            if (!b)
            {
                return R.Err("添加缓存失败");
            }
            return R.Suc();
        }

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public async Task<R> EnableAsync(long id, CurrentUser curUser)
        {
            var domain = await _visitDomainRep.GetAsync(id);
            if (domain == null)
            {
                return R.Err("域名不存在");
            }

            bool b = await _visitDomainRep.ChangeStateAsync(domain.Id, domain.Domain, 1, domain.AgentId);
            if (b == false)
            {
                return R.Err();
            }
            await UpdateCacheAsync(domain.Domain, domain.AgentId, true);

            return R.Suc();
        }

        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public async Task<R> DisableAsync(long id, CurrentUser curUser)
        {
            var domain = await _visitDomainRep.GetAsync(id);
            if (domain == null)
            {
                return R.Err("域名不存在");
            }

            bool b = await _visitDomainRep.ChangeStateAsync(domain.Id, domain.Domain, 2, domain.AgentId);
            if (b==false)
            {
                return R.Err();
            }
            await UpdateCacheAsync(domain.Domain, domain.AgentId, false);
            return R.Suc();
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="agentId"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        async Task<bool> UpdateCacheAsync(string domain, long agentId, bool enable)
        {
            bool b = true;
            if (await _cache.ExistsAsync(domain.ToLower(), prefix))
            {
                if (!enable)
                {
                    b = await _cache.RemoveAsync(domain.ToLower(), prefix);
                }
            }
            else
            {
                b = await _cache.AddAsync(domain.ToLower(), agentId, prefix);
            }
            return b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<R> InsertAsync(VisitDomain entity, CurrentUser curUser)
        {
            if (await _visitDomainRep.ExistsAsync(entity.Domain.ToLower()))
            {
                return R.Err("域名已存在");
            }
            entity.Id = entity.CreateId();
            if (await _visitDomainRep.InsertAsync(entity) == 1)
            {
                await _operateLogApp.InsertAsync<VisitDomain>(curUser, "添加代理访问域名",  entity);
                return R.Suc(entity);
            }

            return R.Err("添加失败");
        }

        /// <summary>
        /// 删除（不在启用状态的数据）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<R> DeleteAsync(long Id, CurrentUser curUser)
        {
            VisitDomain visitDomain = await _visitDomainRep.GetAsync(Id);
            if (visitDomain == null)
            {
                return R.Err("域名不存在");
            }
            if (visitDomain.State == 1)
            {
                return R.Err("启用状态无法删除");
            }
            bool b = await _visitDomainRep.DeleteAsync(Id);

            if (b)
            {
                await _operateLogApp.InsertAsync<VisitDomain>(curUser, "删除代理访问域名",  visitDomain);
                return R.Suc();
            }
            return R.Err("删除失败");
        }

        /// <summary>
        /// 检查域名对应代理
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public async Task<object> CheckAgnet(string domain)
        {
            object agentId = await _cache.GetAsync(domain, "VisitDomain");
            if (agentId == null)
            {
                VisitDomain vd = await _visitDomainRep.QueryFirstOrDefaultAsync<VisitDomain>("select `AgentId` from `VisitDomains` where `state`=1 and `domain`=@domain", new { domain = domain });
                if (vd != null)
                {
                    await _cache.AddAsync(domain, vd.AgentId, "VisitDomain");
                    agentId = vd.AgentId;
                }
            }
            return agentId;
        }
    }
}
