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
    public class AgentApp22 : App, IAgentApp22
    {
        IDbContext context;
        AgentRep22 _agentRep;
        IOperateLogApp _operateLogApp;

        public AgentApp22(IDbContext dbContext, IOperateLogApp operateLogApp) : base(dbContext)
        {
            context = dbContext;
            _agentRep = new AgentRep22();
            _operateLogApp = operateLogApp;
        }

     

        #region 创建
        public async Task<R> CreateAsync(conan.Domain.Entitiestt.User entity, CurrentUser CurrentUser)
        {
         
        

         

            int r = await _agentRep.InsertAsync(entity);
        
                return R.Suc();
         
        }

        #endregion

    
    }
}
