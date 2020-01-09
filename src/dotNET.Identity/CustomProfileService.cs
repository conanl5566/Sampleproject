using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotNET.CommonServer;
using dotNET.ICommonServer;
using dotNET.ICommonServer.Sys;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.Extensions.Logging;

namespace dotNET.IdentityServer
{
    public class CustomProfileService : IProfileService
    {
        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger Logger;

        public readonly IUserApp _userApp;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestUserProfileService"/> class.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="logger">The logger.</param>
        public CustomProfileService(ILogger<TestUserProfileService> logger, IUserApp userApp)
        {
            Logger = logger;
            _userApp = userApp;
        }

        /// <summary>
        /// 只要有关用户的身份信息单元被请求（例如在令牌创建期间或通过用户信息终点），就会调用此方法
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.LogProfileRequest(Logger);

            //判断是否有请求Claim信息
            if (context.RequestedClaimTypes.Any())
            {
                //根据用户唯一标识查找用户信息
                var identityName = context.Subject.Identity.Name;
                User user = await GetUser(identityName);

                // var user = Users.FindBySubjectId(context.Subject.GetSubjectId());
                if (user != null)
                {
                    //调用此方法以后内部会进行过滤，只将用户请求的Claim加入到 context.IssuedClaims 集合中 这样我们的请求方便能正常获取到所需Claim
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.Account),  //请求用户的账号，这个可以保证User.Identity.Name有值
                        new Claim(JwtClaimTypes.Name, user.Account),  //请求用户的姓名
                    };
                    //返回apiresource中定义的claims
                    context.AddRequestedClaims(claims);
                }
            }

            context.LogIssuedClaims(Logger);

            //  return Task.CompletedTask;
        }

        /// <summary>
        /// 验证用户是否有效 例如：token创建或者验证
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual async Task IsActiveAsync(IsActiveContext context)
        {
            Logger.LogDebug("IsActive called from: {caller}", context.Caller);

            var user = await GetUser(context.Subject.Identity.Name);
            context.IsActive = user?.State == 1;
            //  return Task.CompletedTask;
        }

        private async Task<User> GetUser(string identityName)
        {
            User user;
            //if (identityName == Define.SYSTEM_USERNAME)
            //{
            //    user = new User
            //    {
            //        Account = Define.SYSTEM_USERNAME,
            //        Id = Define.SYSTEM_USERNAME,
            //        Name = Define.SYSTEM_USERNAME
            //    };
            //}
            //else
            //{
            user = await _userApp.GetAsync(identityName);
            //}

            return user;
        }
    }
}