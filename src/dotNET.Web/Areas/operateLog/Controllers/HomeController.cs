#region using
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotNET.Web.Host.Framework;
using dotNET.Application.Sys;
using dotNET.Dto;
using dotNET.Web.Host.Model;
using dotNET.Core;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
#endregion
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
namespace dotNET.Web.Host.Areas.operateLog.Controllers
{
    [Area("operateLog")]
    public class HomeController : CustomController
    {
        #region ini
        public IOperateLogApp OperateLogApp { get; set; }
        public SiteConfig Config;
        public HomeController(IOptions<SiteConfig> option )
        {
            Config = option.Value;
            DefaultPageSize = ZConvert.StrToInt(Config.Configlist.FirstOrDefault(o => o.Key == "pagesize")?.Values);
        }

        #endregion

        #region Index
        // GET: /<controller>/
        public async Task<IActionResult> Index(OperateLogOption filter, int? page)
        {
            ViewBag.filter = filter;
            var currentPageNum = page ?? 1;
            var result = await OperateLogApp.GetPageAsync(currentPageNum, DefaultPageSize, filter);
            var model = new BaseListViewModel<OperateLogDto>
            {
                list = result.Data,
                Paging =
                {
                    CurrentPage = currentPageNum,
                    ItemsPerPage = DefaultPageSize,
                    TotalItems = result.ItemCount
                }
            };
            return View(model);
        }
        #endregion
    }
}