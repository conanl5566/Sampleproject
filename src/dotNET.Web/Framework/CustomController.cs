using dotNET.Application.Sys;
using dotNET.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotNET.Web.Host.Framework
{
    public class CustomController : Controller
    {
        public IUserApp UserApp { get; set; }
        public IConfiguration Configuration { get; set; }

        //分页数量
        public int DefaultPageSize = 5;

        /// <summary>
        /// 获取ModelState中的错误信息，以字典集合的形式返回
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetErrorFromModelState()
        {
            var errors = new Dictionary<string, string>();
            foreach (var key in ModelState.Keys)
            {
                if (ModelState[key].Errors.Count > 0)
                {
                    errors[key] = ModelState[key].Errors[0].ErrorMessage;
                }
            }
            return errors;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetErrorFromModelStateStr()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            var errors = new Dictionary<string, string>();
            foreach (var key in ModelState.Keys)
            {
                if (ModelState[key].Errors.Count > 0)
                {
                    sb.AppendFormat($"{key}:{ModelState[key].Errors[0].ErrorMessage}");
                }
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override JsonResult Json(object data)
        {
            var setting = new Newtonsoft.Json.JsonSerializerSettings();
            setting.Converters.Add(new HexLongConverter());
            setting.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            setting.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            setting.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
            setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            return base.Json(data, setting);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IActionResult NotFind()
        {
            return View();
        }

        /// <summary>
        /// 返回操作成功/失败提示
        /// </summary>
        /// <param name="IsSucceeded"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        protected JsonResult Operation(bool IsSucceeded, string Message, string GoBackUrl = "")
        {
            return Json(new { GoBackUrl = GoBackUrl, IsSucceeded = IsSucceeded, Message = Message });
        }

        /// <summary>
        /// 返回 Bootstrap Table 不分页 json 数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected JsonResult List(dynamic list)
        {
            return Json(new { rows = list });
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentUser> CurrentUser()
        {
            CurrentUser CurrentUser;
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }
            if (bool.Parse(Configuration.GetSection("IsIdentity").Value))
            {
                //  var name = this.HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value;
                var name = HttpContext.User.Identity.Name;// User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value;
                var user = await UserApp.GetAsync(name);
                CurrentUser = new CurrentUser
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
                return CurrentUser;
            }
            string userdata = User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.UserData).Value;
            return CurrentUser.FromJson(userdata);
        }

        /// <summary>
        /// 当前的url     string url = GetRedirectUrl(this.HttpContext.Request);
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetRedirectUrl(HttpRequest request)
        {
            var builder = new UriBuilder()
            {
                Path = request.Path,
                Query = request.QueryString.ToUriComponent()
            };

            return builder.Uri.PathAndQuery;
        }

        public string SetingBackUrl(HttpRequest request)
        {
            string goBackUrl = request.HttpContext.Request.Query["GoBackUrl"].ToString();
            if (string.IsNullOrWhiteSpace(goBackUrl))
            {
                var urlRef = request.HttpContext.Request.Headers["Referer"].FirstOrDefault();
                if (urlRef != null)
                    goBackUrl = urlRef.ToString();
            }

            return goBackUrl;
        }

        public ActionResult RedirectToUrl(string url, string actionName = "index")
        {
            if (!string.IsNullOrWhiteSpace(url))
                return Redirect(url);
            else
                return RedirectToAction(actionName);
        }
    }
}