using CompanyName.ProjectName.ICommonServer;
using CompanyName.ProjectName.ICommonServer.Sys;
using CompanyName.ProjectName.Core;
using CompanyName.ProjectName.CommonServer;
using CompanyName.ProjectName.Web.Host.Framework;
using CompanyName.ProjectName.Web.Host.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CompanyName.ProjectName.Web.Host.Controllers
{
    [IgnoreAuthorize]
    public class ModuleController : CustomController
    {
        public IModuleApp ModuleApp { get; set; }
        public IModuleButtonApp ModuleButtonApp { get; set; }

        // 列表
        // GET: /<controller>/     [Bind(Prefix ="")]
        public async Task<IActionResult> Index(ModuleOption option)
        {
            //返回json
            if (Request.IsAjaxRequest())
            {
                List<Module> modules = await ModuleApp.GetSaasModuleListAsync(option);
                return Json(new { rows = modules });
            }
            //菜单数据
            var data = await ModuleApp.GetSaasModuleListAsync();
            ViewData["tree"] = JsonHelper.SerializeObject(await Trees(data, 0), true, true); //json long 转成 string, 名称用驼峰结构输出
            return View();
        }

        #region Details

        public async Task<IActionResult> Details(long Id)
        {
            var module = await ModuleApp.GetAsync(Id);
            if (module == null)
            {
                return NotFind();
            }
            ViewData["Model"] = JsonHelper.SerializeObject(module, false, true);
            var data = await ModuleApp.GetMenuCatalogListAsync();
            var selectList = data.OrderBy(o => o.SortCode).Select(o => new SelectModel { Id = o.Id, Text = o.FullName }).ToList();
            ViewData["ParentIdSelect"] = SelectModel.ToJson(selectList);
            return View();
        }

        #endregion Details

        #region 添加

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            var data = await ModuleApp.GetMenuCatalogListAsync();
            var selectList = data.Select(o => new SelectModel { Id = o.Id, Text = o.FullName }).ToList();
            ViewData["ParentIdSelect"] = SelectModel.ToJson(selectList);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModuleModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(ResultDto.Err("数据验证失败"));
            }
            var module = model.MapTo<Module>();
            module.Id = module.CreateId();
            module.CreatorTime = DateTime.Now;
            var r = await ModuleApp.CreateAsync(module);
            return Json(r);
        }

        #endregion 添加

        #region 删除

        [HttpPost]
        public async Task<IActionResult> Delete(long Id)
        {
            var r = await ModuleApp.DeleteAsync(Id);
            return Json(r);
        }

        #endregion 删除

        #region 修改

        public async Task<IActionResult> Edit(long Id)
        {
            var module = await ModuleApp.GetAsync(Id);
            if (module == null)
            {
                return NotFind();
            }
            ViewData["Model"] = JsonHelper.SerializeObject(module, false, true);//json 名称用驼峰结构输出
            var data = await ModuleApp.GetMenuCatalogListAsync();
            var selectList = data.Select(o => new SelectModel { Id = o.Id, Text = o.FullName }).ToList();
            ViewData["ParentIdSelect"] = SelectModel.ToJson(selectList);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ModuleModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(ResultDto.Err((GetErrorFromModelStateStr())));
            }

            var m = await ModuleApp.GetAsync(model.Id);
            if (m == null)
            {
                return Json(ResultDto.Err(("数据不存在或已被删除")));
            }

            m = model.MapToMeg<ModuleModel, Module>(m);
            var r = await ModuleApp.UpdateAsync(m);

            return Json(r);
        }

        #endregion 修改

        #region 内部方法

        private async Task<List<TreeModel>> Trees(List<Module> data, long parentnodes)
        {
            var treeList = new List<TreeModel>();
            foreach (var item in data.Where(o => o.ParentId == parentnodes).OrderBy(o => o.SortCode))
            {
                var treeModel = new TreeModel
                {
                    Id = item.Id,
                    Value = item.Id.ToString(),
                    Img = item.Icon,
                    Text = item.FullName,
                    Parentnodes = parentnodes,
                    Showcheck = false,
                    Complete = false,
                    Isexpand = true,
                    HasChildren = data.Count(o => o.ParentId == item.Id) > 0
                };

                if (treeModel.HasChildren)
                {
                    treeModel.HasChildren = false;
                    treeModel.ChildNodes = await Trees(data, item.Id);
                    if (treeModel.ChildNodes.Count > 0)
                    {
                        treeModel.Complete = true;
                        treeModel.HasChildren = true;
                    }
                    treeList.Add(treeModel);
                }
            }
            return treeList;
        }

        #endregion 内部方法
    }
}