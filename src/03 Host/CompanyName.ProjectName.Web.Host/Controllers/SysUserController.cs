#region using

using CompanyName.ProjectName.ICommonServer;
using CompanyName.ProjectName.ICommonServer.Sys;
using CompanyName.ProjectName.Core;
using CompanyName.ProjectName.Web.Host.Framework;
using CompanyName.ProjectName.Web.Host.Model;
using CompanyName.ProjectName.Web.Host.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion using

namespace CompanyName.ProjectName.Web.Host.Controllers
{
    public class SysUserController : CustomController
    {
        public IUserApp UserApp { get; set; }
        public IRoleApp RoleApp { get; set; }
        public SiteConfig Config;
        public IDepartmentApp DepartmentApp { get; set; }

        public SysUserController(IOptions<SiteConfig> option)
        {
            Config = option.Value;
            DefaultPageSize = ZConvert.StrToInt(Config.Configlist.FirstOrDefault(o => o.Key == "pagesize")?.Values);
        }

        #region Index

        // GET: /<controller>/
        public async Task<IActionResult> Index(UserOption filter, int? page)
        {
            ViewBag.filter = filter;
            var currentPageNum = page ?? 1;
            var result = await UserApp.GetPageAsync(currentPageNum, DefaultPageSize, filter);
            var model = new BaseListViewModel<UserSunpleDto>
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

        #endregion Index

        #region Create

        public async Task<IActionResult> Create()
        {
            var model = new UserCreateModel();
            var data = await RoleApp.GetListAsync(new RoleOption { });
            var selects = data.Select(o => new SelectListItem { Value = o.Id.ToString(), Text = o.Name }).ToList();
            model.Rolelist = selects;
            model.GoBackUrl = SetingBackUrl(this.HttpContext.Request);
            var deptdata = await DepartmentApp.GetDepartmentListAsync();
            deptdata = deptdata.SortDepartmentsForTree().ToList();
            var selectList = new List<SelectListItem>();
            foreach (var c in deptdata)
                selectList.Add(new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.GetFormattedBreadCrumb(deptdata),
                    Selected = c.Id == model.DepartmentId
                });

            model.deptlist = selectList;
            ViewBag.GoBackUrl = SetingBackUrl(this.HttpContext.Request);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(ResultDto.Err("数据验证失败;" + GetErrorFromModelStateStr()));
            }
            var user = model.MapTo<User>();
            user.Password = CompanyName.ProjectName.Core.Security.MD5Encrypt.MD5(user.Password);
            if (!string.IsNullOrWhiteSpace(user.Avatar))
            {
                var saveUrl = IMGOperate.BaseSave(ImagePathType.员工头像, user.Avatar);
                user.Avatar = saveUrl;
            }
            var r = await UserApp.InsertAsync(user, await CurrentUser());
            return Json(r);
        }

        #endregion Create

        #region 删除

        [HttpPost]
        public async Task<IActionResult> Delete(long Id)
        {
            var r = await UserApp.DeleteAsync(Id, 0, await CurrentUser());
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

        public async Task<IActionResult> Edit(long id)
        {
            UserEditModel model = new UserEditModel();
            User user = await UserApp.GetAsync(id);
            if (user == null)
            {
                return NotFind();
            }
            model = user.MapTo<UserEditModel>();
            model.GoBackUrl = SetingBackUrl(this.HttpContext.Request);
            var data = await RoleApp.GetListAsync(new RoleOption { });
            List<SelectListItem> selects = data.Select(o => new SelectListItem { Value = o.Id.ToString(), Text = o.Name }).ToList();
            model.Rolelist = selects;

            var deptdata = await DepartmentApp.GetDepartmentListAsync();
            deptdata = deptdata.SortDepartmentsForTree().ToList();
            var selectList = new List<SelectListItem>();

            foreach (var c in deptdata)
                selectList.Add(new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.GetFormattedBreadCrumb(deptdata),
                    Selected = c.Id == model.DepartmentId
                });

            model.deptlist = selectList;

            ViewBag.GoBackUrl = SetingBackUrl(this.HttpContext.Request);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(ResultDto.Err(GetErrorFromModelStateStr()));
            }

            if (!string.IsNullOrWhiteSpace(model.Avatar))
            {
                if (model.Avatar.Contains("["))
                {
                    var saveUrl = IMGOperate.BaseSave(ImagePathType.员工头像, model.Avatar);
                    model.Avatar = saveUrl;
                }
            }

            var m = await UserApp.GetAsync(model.Id);

            if (m == null)
            {
                return Json(ResultDto.Err("数据不存在或已被删除"));
            }

            m = model.MapToMeg<UserEditModel, User>(m);

            var r = await UserApp.UpdateUserInfoAsync(m, await CurrentUser());
            return Json(r);
        }

        #endregion 修改

        #region ChangePassword

        [IgnoreAuthorize]
        public async Task<IActionResult> ChangePassword()
        {
            var r = await CurrentUser();
            ViewBag.RealName = r.RealName;
            return View();
        }

        [HttpPost]
        [IgnoreAuthorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(ResultDto.Err(GetErrorFromModelStateStr()));
            }
            var c = await CurrentUser();
            var r = await UserApp.ChangePasswordAsync(c.Id, model.Password, model.NewPassword, 0, c);

            return Json(r);
        }

        [HttpPost]
        //[IgnoreAuthorize]
        public async Task<IActionResult> ResertPassword(string id)
        {
            var r = await UserApp.ResetPasswordAsync(ZConvert.StrToLong(id), "123456", 0, await CurrentUser());
            return Json(r);
        }

        #endregion ChangePassword
    }
}