using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace dotNET.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<T> FindSingleAsync(Expression<Func<T, bool>> exp = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<bool> IsExistAsync(Expression<Func<T, bool>> exp);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        IQueryable<T> Find(Expression<Func<T, bool>> exp = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="orderby"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        IQueryable<T> Find(int pageindex = 1, int pagesize = 10, string orderby = "",
            Expression<Func<T, bool>> exp = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(Expression<Func<T, bool>> exp = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        Task AddAsync(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        Task BatchAddAsync(T[] entities);

        /// <summary>
        /// 更新一个实体的所有属性
        /// </summary>
        Task UpdateAsync(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        Task DeleteAsync(T entity);


        /// <summary>
        /// 实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">更新条件</param>
        /// <param name="entity">更新后的实体</param>
        Task UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);
        /// <summary>
        /// 批量删除
        /// </summary>
        Task DeleteAsync(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 
        /// </summary>
        Task SaveAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<int> ExecuteSqlAsync(string sql);
    }
}
