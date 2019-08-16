#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using conan.Saas.Framework;
using conan.Application.Infrastructure;
using conan.Dto;
using conan.Saas.Model;
using conan.Domain.Entities;
using conan.Utility;

#endregion
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace conan.Saas.Controllers
{
    public class AgentController : CustomController
    {
     
        private readonly IAgentApp _agentApp;
        public   AgentController(IAgentApp agentApp)
        {
            _agentApp = agentApp;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(AgentOption option)
        {
         
            if (Request.IsAjaxRequest())
            {
                var result = await _agentApp.GetPage(option);
                return Json(result);
            }

            return View();
        }

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public async  Task<IActionResult> Create()
        {

         //   return new JsonResult(new { IsSucceeded = false, Message = "您没有操作权限" });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(agentModel model)
        {
            if (!ModelState.IsValid)
            {
                return Operation(false, "数据验证失败");
            }
            Agent module = MapperHelper.Map<agentModel, Agent>(model);
            module.Id = module.CreateId();
          
            var r = await _agentApp.CreateAsync(module, CurrentUser);

            return Operation(r.IsSuc, r.IsSuc ? "数据添加成功" : r.Msg);
        }
        #endregion

        #region 修改
        public async Task<IActionResult> Edit(long Id)
        {
            Agent module = await _agentApp.GetAsync(Id);
            if (module == null)
            {
                return NotFind();
            }
            ViewData["Model"] = JsonHelper.SerializeObject(module, false, true);//json 名称用驼峰结构输出
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(agentModel model)
        {
            if (!ModelState.IsValid)
            {
                return Operation(false, GetErrorFromModelStateStr());
            }

            Agent m = await _agentApp.GetAsync(model.Id);
            if (m == null)
            {
                return Operation(false, "数据不存在或已被删除");
            }

            m = MapperHelper.Map<agentModel, Agent>(model, m);
            var r = await _agentApp.UpdateAsync(m, CurrentUser);

            return Operation(r.IsSuc, r.Msg);
        }
        #endregion

        #region 删除
        [HttpPost]
        public async Task<IActionResult> Delete(long Id)
        {
            var r = await _agentApp.DeleteAsync(Id, CurrentUser);
            return Operation(r.IsSuc, r.IsSuc ? "数据删除成功" : r.Msg);
        }
        #endregion

        #region 修改状态
        [HttpPost]
        public async Task<IActionResult> Updatestatus(long Id)
        {
            var r = await _agentApp.UpdateStatusAsync(Id, CurrentUser);
            return Operation(r.IsSuc, r.IsSuc ? "状态成功" : r.Msg);
        }
        #endregion

        #region 查看
        public async Task<IActionResult> Details(long Id)
        {
            Agent module = await _agentApp.GetAsync(Id);
            if (module == null)
            {
                return NotFind();
            }
            ViewData["Model"] = JsonHelper.SerializeObject(module, false, true);//json 名称用驼峰结构输出
            return View();
        } 
        #endregion





        //[HttpPost]
        //public IActionResult SubmitFrom()
        //{
        //    return View();
        //}
    }
}
