#region using

using dotNET.ICommonServer.Sys;
using dotNET.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

#endregion using

namespace dotNET.Web.Host.Framework
{
    /// <summary>
    /// 权限
    /// </summary>
    public class MvcMenuFilter : ActionFilterAttribute, IActionFilter
    {
        public IConfiguration Configuration { get; }
        public IRoleAuthorizeApp _roleAuthorizeApp;
        public IUserApp _UserApp;

        public MvcMenuFilter(IRoleAuthorizeApp roleAuthorizeApp, IConfiguration configuration, IUserApp userApp)
        {
            Configuration = configuration;
            _roleAuthorizeApp = roleAuthorizeApp;
            _UserApp = userApp;
        }

        #region 权限控制

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionDescriptor.FilterDescriptors.Count(o => o.Filter.GetType().Name == "AllowAttribute") == 0)
            {
                string path = "";
                string controller = context.RouteData.Values["controller"].ToString();
                string action = context.RouteData.Values["action"].ToString();
                string area = "";
                try
                {
                    area = context.RouteData.Values["area"].ToString();
                }
                catch
                {
                    area = "";
                }
                if (string.IsNullOrWhiteSpace(area))
                {
                    path = "/" + controller + "/" + action;
                }
                else
                {
                    path = "/" + area + "/" + controller + "/" + action;
                }
                var currentUser = await getAuthorization(context);
                if (currentUser == null)
                {
                    context.Result =
                   new RedirectResult("/account/login?returnUrl=" + path);
                }
                bool ignore = context.ActionDescriptor.FilterDescriptors.Count(o => o.Filter.GetType().Name == "IgnoreAuthorizeAttribute") > 0;
                if (ignore)
                {
                }
                else
                {
                    if (currentUser.IsSys)
                    {
                    }
                    else
                    {
                        // var _roleAuthorizeApp = AutofacExt.GetFromFac<IRoleAuthorizeApp>();
                        bool b = await _roleAuthorizeApp.ActionValidate(currentUser.RoleId, path);
                        if (!b)

                        {
                            if (context.HttpContext.Request.IsAjaxRequest())
                            {
                                JsonResult json = new JsonResult(new { IsSucceeded = false, Message = "您没有操作权限" });
                                context.Result = json;
                            }
                            else
                            {
                                context.Result = new RedirectResult("/account/Nofind?bakurl=" + context.HttpContext.Request.Headers["Referer"].FirstOrDefault());
                            }
                        }
                    }
                }
            }

            await base.OnActionExecutionAsync(context, next);
        }

        #endregion 权限控制

        #region 当前用户

        /// <summary>
        /// 当前用户
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private async Task<CurrentUser> getAuthorization(ActionExecutingContext filterContext)
        {
            if (bool.Parse(Configuration.GetSection("IsIdentity").Value))
            {
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    return null;
                }
                //
                var name = filterContext.HttpContext.User.Identity.Name;
                //filterContext.HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value;
                var user = await _UserApp.GetAsync(name);
                var operatorMode = new CurrentUser
                {
                    Avatar = user.Avatar,
                    IsSys = user.IsSys,
                    Id = user.Id,
                    RoleId = user.RoleId,
                    RealName = user.RealName,
                    UserType = "Saas",
                    AgentId = 0,
                    LoginIPAddress = ""
                };
                return operatorMode;
            }
            var result = await filterContext.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return result.Succeeded ? CurrentUser.FromJson(result.Principal.Claims.FirstOrDefault(o => o.Type == ClaimTypes.UserData)?.Value) : null;
        }

        #endregion 当前用户
    }
}