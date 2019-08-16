using conan.Application.Infrastructure;
using System;
using conan.Data;
using conan.Utility.Cache;
using conan.Repository;
using System.Threading.Tasks;
using conan.Utility;
using conan.Domain.Entities;

namespace conan.Application.App
{
    public class PaymentParameterApp : App, IPaymentParameterApp
    {

        PaymentParameterRep _paymentParameterRep;
        IOperateLogApp _operateLogApp;

        public PaymentParameterApp(IDbContext dbContext,IOperateLogApp operateLogApp) : base(dbContext)
        {
            _paymentParameterRep = new PaymentParameterRep();
            _operateLogApp = operateLogApp;
        }


        /// <summary>
        /// 支付方式添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<R> CreateAsync(PaymentParameter entity, CurrentUser currentUser)
        {
            entity.Id = entity.CreateId();
            entity.CreatorTime = DateTime.Now;

            var r = await _paymentParameterRep.InsertAsync(entity);
            if (r != 1)
            {
                return R.Err("添加失败");
            }

            if (currentUser != null)
                await _operateLogApp.InsertAsync<PaymentParameter>(currentUser, "添加支付参数", entity);

            await RemoveCacheAsync(currentUser.Id);

            return R.Suc(entity);
        }

        /// <summary>
        /// 支付方式更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public async Task<R> UpdateAsync(PaymentParameter entity, CurrentUser currentUser)
        {
            var r = await _paymentParameterRep.UpdateAsync(entity);
            if (!r)
            {
                return R.Err("更新失败");
            }

            if (currentUser != null)
                await _operateLogApp.InsertAsync<PaymentParameter>(currentUser, "更新支付参数", entity);

            await RemoveCacheAsync(currentUser.Id);

            return R.Suc(entity);
        }

        /// <summary>
        /// 状态更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public async Task<R> EnabledAsync(long Id, bool enabled, CurrentUser currentUser)
        {
            var r = await _paymentParameterRep.UpdateAsync<PaymentParameter>(new { Id = Id, Enabled = enabled });
            if (!r)
            {
                return R.Err("更新失败");
            }

            if (currentUser != null)
                await _operateLogApp.InsertCusAsync(currentUser, "更新支付参数", JsonHelper.SerializeObject(new { Id = Id, Enabled = enabled }), Id, "PaymentParameters");

            await RemoveCacheAsync(currentUser.Id);

            return R.Suc();
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <returns></returns>
        private async Task RemoveCacheAsync(long id)
        {
            //var cache = CacheFactory.Cache();
            //await cache.RemoveAsync(id.ToString(), "PaymentParameter");
        }
    }
}
