using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using conan.Domain.Entities;
using conan.Utility;

namespace conan.Application.Infrastructure
{
    public interface IPaymentSettingApp : IApp
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<PaymentSetting> GetAsync(long Id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PaymentSetting>> ListAsync();
        
        /// <summary>
        /// 支付方式添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<R> CreateAsync(PaymentSetting entity, CurrentUser currentUser);

        /// <summary>
        /// 支付方式更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        Task<R> UpdateAsync(PaymentSetting entity, CurrentUser currentUser);
    }
}
