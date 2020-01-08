﻿#region using
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dotNET.Web.Host.Web.Model;
using dotNET.Web.Host.Framework;
using dotNET.Core;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using dotNET.Application.Sys;
using dotNET.Domain.Entities.Sys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

#endregion
namespace dotNET.Web.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [IgnoreAuthorize]
    public class AccountController : Controller
    {
        #region ini
        public IConfiguration Configuration { get; }
        public IUserApp UserApp { get; set; }
        public ILogger Logger;
        public AccountController(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            Configuration = configuration;
            Logger = loggerFactory.CreateLogger<AccountController>();
        }
        #endregion

        #region Login
        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAttribute]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewBag.IsIdentity = Configuration.GetSection("IsIdentity").Value;
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAttribute]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { state = "error", message = "数据验证失败" });
            }
            string ip = GetRemoteIpAddress();
            var r = await UserApp.SaasLoginAsync(model.Account, model.Password, ip);
            if (!string.IsNullOrEmpty(r.Error))
            {
                return Json(new { state = "error", message = r.Error });
            }
            var claims = new List<Claim>
                                        {
                                            new Claim(ClaimTypes.UserData, getCurrentUser(r.User, ip).ToString()),
                                        };
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.Now.AddMinutes(120)
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            return Json(new { state = "success", message = "登录成功。", returnUrl = RedirectToLocal(returnUrl) });
        }

        #endregion

        /// <summary>
        /// oauth认证跳转页面
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult IdentityAuth()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/Home/Index");
            return View();
        }

        #region LogOff
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            if (bool.Parse(Configuration.GetSection("IsIdentity").Value))
            {
                return SignOut("Cookies", "oidc");
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    string userdata = User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.UserData)?.Value;
                    await UserApp.LogOffAsync(CurrentUser.FromJson(userdata));
                }
                await HttpContext.SignOutAsync(
                 CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction(actionName: nameof(Login), controllerName: "Account");
            }

        }
        #endregion

        #region Nofind
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bakurl"></param>
        /// <returns></returns>
        public async Task<IActionResult> Nofind(string bakurl)
        {
            ViewBag.bakurl = bakurl;
            return View();
        }
        #endregion

        #region Helpers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        CurrentUser getCurrentUser(User user, string ip)
        {
            var operatorMode = new CurrentUser
            {
                Avatar = user.Avatar,
                IsSys = user.IsSys,
                Id = user.Id,
                RoleId = user.RoleId,
                RealName = user.Account,
                UserType = "Saas",
                AgentId = 0,
                LoginIPAddress = ip
            };
            return operatorMode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetRemoteIpAddress()
        {
            string forwardedFor = HttpContext.Request.Headers["HTTP_X_FORWARDED_FOR"].ToString();
            string remoteAddr = HttpContext.Connection.RemoteIpAddress.ToString();
            string result = !string.IsNullOrEmpty(forwardedFor) ? forwardedFor : remoteAddr;
            if (string.IsNullOrEmpty(result) || result == "::1") //|| !Utils.IsIP(result)
                return "127.0.0.1";
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        private string RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return returnUrl;
            }
            else
            {
                return "/Home/Index";
            }
        }
        #endregion
    }
}