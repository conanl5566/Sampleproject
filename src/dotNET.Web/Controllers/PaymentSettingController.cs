using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using conan.Saas.Framework;
using conan.Application.Infrastructure;
using conan.Utility;
using conan.Domain.Entities;
using conan.Saas.Model;
using conan.Dto;

namespace conan.Saas.Controllers
{
    public class PaymentSettingController : CustomController
    {
        private IPaymentSettingApp _paymentSettingApp;
        private IOperateLogApp _operateLogApp;


        public PaymentSettingController(IPaymentSettingApp paymentSettingApp, IOperateLogApp operateLogApp)
        {
            _paymentSettingApp = paymentSettingApp;
            _operateLogApp = operateLogApp;
        }


        public async Task<IActionResult> Index()
        {
            if (Request.IsAjaxRequest())
            {
                var list = await _paymentSettingApp.ListAsync();
                return List(list);
            }
            //var list = await _paymentSettingApp.ListAsync();
            //ViewData["data"] = JsonHelper.SerializeObject(list, true, false);
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Setting(long Id)
        {
            var ps = await _paymentSettingApp.GetAsync(Id);
            if (ps == null)
                return NotFind();
            return View(ps);
        }

        [HttpPost]
        public async Task<IActionResult> Setting(PaymentSettingModel model)
        {
            if (!ModelState.IsValid)
            {
                return Operation(false, "数据验证失败");
            }

            PaymentSetting ps = await _paymentSettingApp.GetAsync(model.Id);
            if (ps == null)
                return Operation(false, "数据错误");

            ps = MapperHelper.Map<PaymentSettingModel, PaymentSetting>(model, ps);

            var r = await _paymentSettingApp.UpdateAsync(ps, CurrentUser);
            if (!r.IsSuc)
                return Operation(false, r.Msg);

            return Operation(true, "操作成功");
        }

    }
}