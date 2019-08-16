#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using conan.Saas.Framework;
using dotNET.Application.Infrastructure;
using dotNET.Dto;
using conan.Saas.Model;
using dotNET.Domain.Entities;
using dotNET.Utility;
using Microsoft.Extensions.Options;

#endregion
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace conan.Saas.Controllers
{
    public class LoginLogController : CustomController
    {
        public ILoginLogApp _loginlogApp { get; set; }
        public IUserApp _IUserApp  { get; set; }
        public SiteConfig Config;
     
        public LoginLogController(IOptions<SiteConfig> option)
        {
            Config = option.Value;
            DefaultPageSize = ZConvert.StrToInt(Config.Configlist.FirstOrDefault(o => o.Key == "pagesize").Values);
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(LoginLogOption filter, int? page)
        {
      
            ViewBag.filter = filter;
            var currentPageNum = page.HasValue ? page.Value : 1;
            var result = await _loginlogApp.GetPageAsync(currentPageNum, DefaultPageSize, filter);
            var model = new BaseListViewModel<LoginLogDtoext>();
            model.list = result.Data;
            model.Paging.CurrentPage = currentPageNum;
            model.Paging.ItemsPerPage = DefaultPageSize;
            model.Paging.TotalItems = result.ItemCount;
            return View(model);

        }

        [IgnoreAuthorize]
        public async Task<IActionResult> select2data(string q)
        {
            List<select2data> list = new List<select2data>();
            list.Add(new select2data() { id = "0", text = "=请输入用户账号=" });
            if (string.IsNullOrWhiteSpace(q))
                return Json(list);
            var result = await _IUserApp.SelectDataAsync(q);
            if (result != null && result.Count() > 0)
            {
                foreach (var item in result)
                {
                    list.Add(new select2data() { id = item.Id.ToString(), text = item.Account });
                }
            }

            return Json(list);

        }

        [IgnoreAuthorize]
        public async Task<IActionResult> select2Agentdata(string q)
        {
            List<select2data> list = new List<select2data>();
            list.Add(new select2data() { id = "0", text = "=请输入代理商账号=" });
            if (string.IsNullOrWhiteSpace(q))
                return Json(list);
            //var result = await _IAgentApp.GetLsitAsync(q);
            //if (result != null && result.Count() > 0)
            //{
            //    foreach (var item in result)
            //    {
            //        list.Add(new select2data() { id = item.Id.ToString(), text = item.Name });
            //    }
            //}


            return Json(list);

        }

    }
    public class select2data
    {

        public string id { get; set; }
        public string text { get; set; }
    }
}