#region using

using CompanyName.ProjectName.ICommonServer;
using CompanyName.ProjectName.ICommonServer.Sys;
using CompanyName.ProjectName.Core;
using CompanyName.ProjectName.CommonServer;
using CompanyName.ProjectName.Web.Host.Framework;
using CompanyName.ProjectName.Web.Host.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion using

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
namespace CompanyName.ProjectName.Web.Host.Controllers
{
    public class DepartmentController : CustomController
    {
        #region ini

        public IDepartmentApp DepartmentApp { get; set; }

        #endregion ini

        #region tree

        [IgnoreAuthorize]
        public async Task<IActionResult> Loadtree(long parentId)
        {
            //菜单数据
            var data = await DepartmentApp.GetDepartmentListAsync();
            var treeModellist = new List<TreeModel>();
            var s = await Trees(data, 0, parentId);
            var treeModel = new TreeModel
            {
                Id = 0,
                Value = "0",
                Img = "",
                Text = "根节点",
                Parentnodes = -1,
                Showcheck = false,
                Complete = false,
                Isexpand = true,
                Checkstate = 0,
                HasChildren = true,
                ChildNodes = s
            };
            treeModellist.Add(treeModel);
            var treedata = JsonHelper.SerializeObject(treeModellist, true, true); //json long 转成 string, 名称用驼峰结构输出
            return Content(treedata);
        }

        #endregion tree

        #region 内部方法

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentnodes"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        private async Task<List<TreeModel>> Trees(List<Department> data, long parentnodes, long sid)
        {
            var treeList = new List<TreeModel>();
            foreach (var item in data.Where(o => o.ParentId == parentnodes))
            {
                var treeModel = new TreeModel
                {
                    Id = item.Id,
                    Value = item.Id.ToString(),
                    Img = "",
                    Text = item.Name,
                    Parentnodes = parentnodes,
                    Showcheck = false,
                    Complete = false,
                    Isexpand = true,
                    Checkstate = (sid != 0 && sid == item.Id) ? 1 : 0,
                    HasChildren = data.Count(o => o.ParentId == item.Id) > 0
                };
                treeModel.HasChildren = false;
                treeModel.ChildNodes = await Trees(data, item.Id, sid);
                if (treeModel.ChildNodes.Count > 0)
                {
                    treeModel.Complete = true;
                    treeModel.HasChildren = true;
                }
                treeList.Add(treeModel);
            }
            return treeList;
        }

        #endregion 内部方法

        #region 列表

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(DepartmentOption filter)
        {
            var itemsDatalistmodel = new Departmentlistmodel();
            ViewBag.filter = filter;
            var modules = await DepartmentApp.GetListAsync(filter);
            itemsDatalistmodel.Departmentlist = modules;
            ViewBag.ItemsData = itemsDatalistmodel;
            return View();
        }

        #endregion 列表

        #region 添加

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            var data = await DepartmentApp.GetDepartmentListAsync();
            var model = new DepartmentModel();
            data = data.SortDepartmentsForTree().ToList();
            var selectList = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Value = "0",
                    Text = "顶级节点",
                    Selected = false
                }
            };
            foreach (var c in data)
                selectList.Add(new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.GetFormattedBreadCrumb(data),
                    Selected = c.Id == model.ParentId
                });
            model.pids = selectList;
            model.GoBackUrl = SetingBackUrl(this.HttpContext.Request);
            ViewBag.GoBackUrl = SetingBackUrl(this.HttpContext.Request);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return Operation(false, "数据验证失败;" + GetErrorFromModelStateStr(), model.GoBackUrl);
            }
            Department module = model.MapTo<Department>();
            module.Id = module.CreateId();
            module.CreatorTime = DateTime.Now;
            module.CreatorUserId = CurrentUser().Id;
            var r = await DepartmentApp.CreateAsync(module);
            return Json(r);
        }

        #endregion 添加

        #region 删除

        [HttpPost]
        public async Task<IActionResult> Delete(long Id)
        {
            var r = await DepartmentApp.DeleteAsync(Id);
            return Json(r);
        }

        #endregion 删除

        #region 修改

        public async Task<IActionResult> Edit(long Id)
        {
            DepartmentModel model;
            var module = await DepartmentApp.GetAsync(Id);
            if (module == null)
            {
                return NotFind();
            }
            model = module.MapTo<DepartmentModel>();
            model.GoBackUrl = SetingBackUrl(this.HttpContext.Request);
            var data = await DepartmentApp.GetDepartmentListAsync();
            data = data.SortDepartmentsForTree().ToList();
            var selectList = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Value = "0",
                    Text = "顶级节点",
                    Selected = false
                }
            };
            foreach (var c in data)
                selectList.Add(new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.GetFormattedBreadCrumb(data),
                    Selected = c.Id == model.ParentId
                });

            model.pids = selectList;
            ViewBag.GoBackUrl = SetingBackUrl(this.HttpContext.Request);
            return View(model);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return Operation(false, "数据验证失败;" + GetErrorFromModelStateStr(), model.GoBackUrl);
            }
            var m = await DepartmentApp.GetAsync(model.Id);
            if (m == null)
            {
                return Operation(false, "数据不存在或已被删除");
            }
            m = model.MapToMeg<DepartmentModel, Department>(m);
            var r = await DepartmentApp.UpdateAsync(m);
            return Json(r);
        }

        #endregion 修改
    }
}