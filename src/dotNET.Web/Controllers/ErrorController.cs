#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotNET.Web.Host.Framework;
using dotNET.Dto;
using dotNET.Core;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Configuration;
#endregion
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace dotNET.Web.Host.Controllers
{
    public class ErrorController : CustomController
    {
        public ErrorController(IConfiguration configuration) : base(configuration)
        {
        }

        #region Index
        // GET: /<controller>/
        public async Task<IActionResult> Index(ErrorLogOption filter)
        {
            ViewBag.filter = filter;
            var model = new List<ErrorLogDto>();
            var contentRoot = Directory.GetCurrentDirectory();
            var webRoot = Path.Combine(contentRoot, "logs\\Error");
            var filelist = new List<string>();
            new ServiceCollection()
            .AddSingleton<IFileProvider>(new PhysicalFileProvider(webRoot))
             .AddSingleton<IFileManager, FileManager>()
             .BuildServiceProvider()
            .GetService<IFileManager>()
           .ShowStructure((layer, name) => filelist.Add(name));
            var result = new List<ErrorLogDto>();
            if (filelist.Count > 0)
            {
                int i = 1;
                foreach (var item in filelist.OrderByDescending(o => o))
                {
                    var m = new ErrorLogDto
                    {
                        Id = i,
                        filename = item,
                        CreatorTime = ZConvert.StrToDateTime(item.Replace(".log", ""), DateTime.Now)
                    };
                    result.Add(m);
                    i++;
                }
            }
            if (filter.kCreatorTime != null)
            {
                result = result.Where(o => o.CreatorTime >= filter.kCreatorTime.Value).ToList();
            }
            if (filter.eCreatorTime != null)
            {
                result = result.Where(o => o.CreatorTime <= filter.eCreatorTime.Value).ToList();
            }
            model = result.OrderByDescending(o => o.CreatorTime).ToList();
            return View(model);
        }

        #endregion

        #region 查看
        [IgnoreAuthorize]
        public async Task<IActionResult> Details(string Id)
        {
            var contentRoot = Directory.GetCurrentDirectory();
            var webRoot = Path.Combine(contentRoot, "logs\\Error");
            var content = new ServiceCollection()
              .AddSingleton<IFileProvider>(new PhysicalFileProvider(webRoot))
              .AddSingleton<IFileManager, FileManager>()
              .BuildServiceProvider()
             .GetService<IFileManager>()
              .ReadAllTextAsync(Id).Result;
            ViewBag.content = content;
            ViewBag.filename = Id;
            ViewBag.GoBackUrl = SetingBackUrl(this.HttpContext.Request);
            return View();
        }
        #endregion
    }
}