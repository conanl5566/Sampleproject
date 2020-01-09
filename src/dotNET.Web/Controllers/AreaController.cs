#region using

using dotNET.ICommonServer;
using dotNET.ICommonServer.Sys;
using dotNET.Core;
using dotNET.CommonServer;
using dotNET.Web.Host.Framework;
using dotNET.Web.Host.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion using

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
namespace dotNET.Web.Host.Controllers
{
    public class AreaController : CustomController
    {
        #region ini

        public IAreaListApp AreaListApp { get; set; }

        #endregion ini

        #region tree

        [IgnoreAuthorize]
        public async Task<IActionResult> Loadtree(long parentId)
        {
            var data = await AreaListApp.GetMenuListAsync();
            data = data.OrderBy(o => o.SortID).ToList();
            var trees = await Trees(data, 0, parentId);
            var treedata = JsonHelper.SerializeObject(trees, true, true); //json long 转成 string, 名称用驼峰结构输出
            return Content(treedata);
        }

        #endregion tree

        #region Index

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(areaOption filter)
        {
            ViewBag.filter = filter;
            var itemsDatalistmodel = new AreaListlistmodel();
            var modules = await AreaListApp.GetListAsync(filter.ParentId ?? 0);
            itemsDatalistmodel.AreaListlist = modules.ToList();
            ViewBag.ItemsData = itemsDatalistmodel;
            return View();
        }

        #endregion Index

        #region 内部方法

        private async Task<List<TreeModel>> Trees(List<AreaList> data, long parentnodes, long sid)
        {
            var treeList = new List<TreeModel>();
            foreach (var item in data.Where(o => o.ParentID == parentnodes))
            {
                var a = data.FirstOrDefault(o => o.Id == sid)?.ParentID ?? 0;
                var treeModel = new TreeModel
                {
                    Id = item.Id,
                    Value = item.Id.ToString(),
                    Img = "fa fa-navicon",
                    Text = item.AreaName,
                    Parentnodes = parentnodes,
                    Showcheck = false,
                    Complete = false,
                    Isexpand = (0 == parentnodes && (sid == item.Id || (a == item.Id))) ? true : false,
                    Checkstate = (sid != 0 && sid == item.Id) ? 1 : 0,
                    HasChildren = data.Count(o => o.ParentID == item.Id) > 0
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