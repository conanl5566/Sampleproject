using System;
using System.Collections.Generic;
using System.Text;
using conan.Data;
using conan.Utility.Cache;
using conan.Repository;
using System.Threading.Tasks;
using conan.Domain.Entities;
using conan.Utility;
using conan.Application.Infrastructure;

namespace conan.Application.App
{
    public class PaymentSettingApp : App, IPaymentSettingApp
    {

        IPaymentSettingRep _paymentSettingRep;
        IOperateLogApp _operateLogApp;

        public PaymentSettingApp(IDbContext dbContext, IOperateLogApp operateLogApp) : base(dbContext)
        {
            _paymentSettingRep = new IPaymentSettingRep();
            _operateLogApp = operateLogApp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<PaymentSetting> GetAsync(long Id)
        {
            return await _paymentSettingRep.GetAsync(Id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PaymentSetting>> ListAsync()
        {
            return await _paymentSettingRep.GetListAsync();
        }

        /// <summary>
        /// 支付方式添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<R> CreateAsync(PaymentSetting entity, CurrentUser currentUser)
        {
            entity.Id = entity.CreateId();
            entity.CreatorTime = DateTime.Now;

            var r = await _paymentSettingRep.InsertAsync(entity);
            if (r != 1)
            {
                return R.Err("添加失败");
            }

            if (currentUser != null)
                await _operateLogApp.InsertAsync<PaymentSetting>(currentUser, "添加支付方式", entity);

            return R.Suc(entity);
        }

        /// <summary>
        /// 支付方式更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public async Task<R> UpdateAsync(PaymentSetting entity, CurrentUser currentUser)
        {
            var r = await _paymentSettingRep.UpdateAsync(entity);
            if (!r)
            {
                return R.Err("更新失败");
            }

            if (currentUser != null)
                await _operateLogApp.InsertAsync<PaymentSetting>(currentUser, "更新支付方式", entity);

            //await RemoveCacheAsync(entity.Id);

            return R.Suc(entity);
        }


        ///// <summary>
        ///// 移除缓存
        ///// </summary>
        ///// <returns></returns>
        //private async Task RemoveCacheAsync(long id)
        //{
        //    var cache = CacheFactory.Cache();
           
        //}
    }
}
