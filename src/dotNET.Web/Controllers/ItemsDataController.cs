#region using

using dotNET.Application;
using dotNET.Application.Sys;
using dotNET.Core;
using dotNET.Domain.Entities.Sys;
using dotNET.Dto;
using dotNET.Web.Host.Framework;
using dotNET.Web.Host.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion using

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace dotNET.Web.Host.Controllers
{
    public class ItemsDataController : CustomController
    {
        public IItemsDataApp ItemsDataApp { get; set; }

        #region tree

        [IgnoreAuthorize]
        public async Task<IActionResult> Loadtree(long parentId)
        {
            //菜单数据
            var data = await ItemsDataApp.GetItemsDataListAsync();

            var s = await Trees(data, 0, parentId);

            string treedata = JsonHelper.SerializeObject(s, true, true); //json long 转成 string, 名称用驼峰结构输出

            return Content(treedata);
        }

        #endregion tree

        #region 列表

        public async Task<IActionResult> Index(ItemsDataOption filter)
        {
            var itemsDatalistmodel = new ItemsDatalistmodel();
            ViewBag.filter = filter;
            var modules = await ItemsDataApp.GetListAsync(filter);

            itemsDatalistmodel.ItemsDatalist = modules.OrderBy(o => o.SortCode).ToList();
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
            var data = await ItemsDataApp.GetItemsDataListAsync();

            var model = new ItemsDataModel();
            data = data.SortDepartmentsForTree().ToList();
            var selectList = new List<SelectListItem>();
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
        public async Task<IActionResult> Create(ItemsDataModel model)
        {
            if (!ModelState.IsValid)
            {
                return Operation(false, "数据验证失败;" + GetErrorFromModelStateStr(), model.GoBackUrl);
            }
            ItemsData module = model.MapTo<ItemsData>();
            module.Id = module.CreateId();
            module.CreatorTime = DateTime.Now;
            module.CreatorUserId = CurrentUser().Id;

            var r = await ItemsDataApp.CreateAsync(module);

            return Json(r);
        }

        #endregion 添加

        #region 删除

        [HttpPost]
        public async Task<IActionResult> Delete(long Id)
        {
            var r = await ItemsDataApp.DeleteAsync(Id);
            return Json(r);
        }

        #endregion 删除

        #region 修改

        public async Task<IActionResult> Edit(long Id)
        {
            ItemsDataModel model;
            var module = await ItemsDataApp.GetAsync(Id);
            if (module == null)
            {
                return NotFind();
            }
            model = module.MapTo<ItemsDataModel>();
            model.GoBackUrl = SetingBackUrl(this.HttpContext.Request);
            ViewBag.GoBackUrl = SetingBackUrl(this.HttpContext.Request);
            var data = await ItemsDataApp.GetItemsDataListAsync();

            data = data.SortDepartmentsForTree().ToList();

            var selectList = new List<SelectListItem>();
            foreach (var c in data)
                selectList.Add(new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.GetFormattedBreadCrumb(data),
                    Selected = c.Id == model.ParentId
                });

            model.pids = selectList;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemsDataModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(R.Err(GetErrorFromModelStateStr()));
            }
            var m = await ItemsDataApp.GetAsync(model.Id);
            if (m == null)
            {
                return Json(R.Err("数据不存在或已被删除"));
            }
            m = model.MapToMeg<ItemsDataModel, ItemsData>(m);
            var r = await ItemsDataApp.UpdateAsync(m);

            return Json(r);
        }

        #endregion 修改

        #region 内部方法

        private async Task<List<TreeModel>> Trees(List<ItemsData> data, long parentnodes, long sid)
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
    }
}