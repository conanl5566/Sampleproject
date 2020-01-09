#region using

using dotNET.ICommonServer;
using dotNET.ICommonServer.Sys;
using dotNET.Core;
using dotNET.Enum;
using dotNET.Web.Host.Framework;
using dotNET.Web.Host.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

#endregion using

namespace dotNET.Web.Host.Controllers
{
    public class WebConfigController : CustomController
    {
        public IWebConfigApp WebConfigApp { get; set; }
        public IUserApp UserApp { get; set; }
        public SiteConfig Config;

        public WebConfigController(IOptions<SiteConfig> option)
        {
            Config = option.Value;
            DefaultPageSize = ZConvert.StrToInt(Config.Configlist.FirstOrDefault(o => o.Key == "pagesize")?.Values);
        }

        #region Index

        // GET: /<controller>/
        public async Task<IActionResult> Index(WebConfigOption filter, int? page)
        {
            ViewBag.filter = filter;
            var currentPageNum = page ?? 1;
            filter.RowsPrePage = DefaultPageSize;
            filter.PageNumber = currentPageNum;
            var result = await WebConfigApp.GetPageAsync(filter);
            var model = new BaseListViewModel<WebConfigDto>
            {
                list = result.Data.Data,
                Paging =
                {
                    CurrentPage = currentPageNum,
                    ItemsPerPage = DefaultPageSize,
                    TotalItems = result.Data.ItemCount
                }
            };
            return View(model);
        }

        #endregion Index

        #region Create

        public async Task<IActionResult> Create()
        {
            CreateWebConfigDto model = new CreateWebConfigDto();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWebConfigDto model)
        {
            if (!ModelState.IsValid)
            {
                return Operation(false, "数据验证失败;" + GetErrorFromModelStateStr(), "");
            }
            var r = await WebConfigApp.CreateAsync(model, await CurrentUser());
            return Json(r);
        }

        #endregion Create

        #region 删除

        [HttpPost]
        public async Task<IActionResult> Delete(long Id)
        {
            var r = await WebConfigApp.DeleteAsync(Id, await CurrentUser());
            return Json(r);
        }

        /// <summary>
        /// 修改用户登录状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Updatestatus(long id)
        {
            var r = await UserApp.Updatestatus(id, await CurrentUser());
            return Json(r);
        }

        #endregion 删除

        #region 修改

        public async Task<IActionResult> Edit(long Id)
        {
            var r = await WebConfigApp.GetDetailAsync(Id);
            return r.Code != CodeEnum.Ok ? NotFind() : View(r.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateWebConfigDto model)
        {
            if (!ModelState.IsValid)
            {
                return Operation(false, GetErrorFromModelStateStr());
            }
            var r = await WebConfigApp.UpdateAsync(model, await CurrentUser());
            return Json(r);
        }

        #endregion 修改
    }
}