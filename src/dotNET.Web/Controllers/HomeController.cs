#region using

using dotNET.ICommonServer;
using dotNET.ICommonServer.Sys;
using dotNET.Core;
using dotNET.CommonServer;
using dotNET.Web.Host.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion using

namespace dotNET.Web.Host.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public class HomeController : CustomController
    {
        #region ini

        public IRoleAuthorizeApp RoleAuthorizeApp { get; set; }

        #endregion ini

        #region Index

        [IgnoreAuthorize]
        public async Task<IActionResult> Index()
        {
            var r = await CurrentUser();
            ViewBag.RealName = r.RealName;
            ViewBag.Avatar = r.Avatar;
            return View();
        }

        #endregion Index

        #region Menu

        [IgnoreAuthorize]
        public async Task<IActionResult> Menu()
        {
            var r = await CurrentUser();
            List<Module> modules = await RoleAuthorizeApp.GetModuleList(r.RoleId, r.IsSys);
            return Json(ToMenu(modules, 0));
        }

        #endregion Menu

        #region Dashboard

        [IgnoreAuthorize]
        public IActionResult Dashboard()
        {
            return View();
        }

        #endregion Dashboard

        #region VisitModule

        public IActionResult VisitModule()
        {
            return Content("");
        }

        #endregion VisitModule

        #region Error

        public IActionResult Error()
        {
            return View();
        }

        #endregion Error

        #region 转成菜单

        /// <summary>
        /// 转成菜单
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private static List<MenuModel> ToMenu(List<Module> data, long parentId)
        {
            var childNodeList = data.FindAll(t => t.ParentId == parentId).ToList();
            var list = new List<MenuModel>();
            foreach (Module item in childNodeList)
            {
                MenuModel mm = new MenuModel
                {
                    Id = item.Id.ToString(),
                    Icon = item.Icon,
                    IsOpen = false,
                    Text = item.FullName,
                    TargetType = item.Target == "iframe" ? "iframe-tab" : item.Target
                };
                if (!string.IsNullOrEmpty(item.UrlAddress))
                {
                    if (item.UrlAddress.StartsWith("/"))
                    {
                        mm.Url = item.UrlAddress.Substring(1);
                    }
                    else
                    {
                        mm.Url = item.UrlAddress;
                    }
                }

                mm.Children = ToMenu(data, item.Id);

                list.Add(mm);
            }
            return list;
        }

        #endregion 转成菜单
    }
}