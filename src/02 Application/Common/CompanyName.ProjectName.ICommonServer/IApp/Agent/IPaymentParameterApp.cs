using conan.Domain.Entities;
using conan.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace conan.Application.Infrastructure
{
    public interface IPaymentParameterApp : IApp
    {

        /// <summary>
        /// 支付方式添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<R> CreateAsync(PaymentParameter entity, CurrentUser currentUser);


        /// <summary>
        /// 支付方式更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        Task<R> UpdateAsync(PaymentParameter entity, CurrentUser currentUser);


        /// <summary>
        /// 状态更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        Task<R> EnabledAsync(long Id, bool enabled, CurrentUser currentUser);
     
   
    }
}
