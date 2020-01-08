using dotNET.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace dotNET.EntityFrameworkCore
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly DbSet<T> _dbSet;
        private readonly EFCoreDBContext _context;

        public BaseRepository(EFCoreDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        /// <summary>
        /// 根据过滤条件，获取记录
        /// </summary>
        /// <param name="exp">The exp.</param>
        public IQueryable<T> Find(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> exp)
        {
            return await _dbSet.AnyAsync(exp);
        }

        /// <summary>
        /// 查找单个，且不被上下文所跟踪
        /// </summary>
        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> exp)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(exp);
        }

        /// <summary>
        /// 得到分页记录
        /// </summary>
        /// <param name="pageindex">The pageindex.</param>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="orderby">排序，格式如："Id"/"Id descending"</param>
        public IQueryable<T> Find(int pageindex, int pagesize, string orderby = "", Expression<Func<T, bool>> exp = null)
        {
            if (pageindex < 1) pageindex = 1;
            if (string.IsNullOrEmpty(orderby))
                orderby = "Id descending";

            return Filter(exp).OrderBy(orderby).Skip(pagesize * (pageindex - 1)).Take(pagesize);
        }

        /// <summary>
        /// 根据过滤条件获取记录数
        /// </summary>
        public async Task<int> GetCountAsync(Expression<Func<T, bool>> exp = null)
        {
            return await Filter(exp).CountAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
            _context.Entry(entity).State = EntityState.Detached;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">The entities.</param>
        public async Task BatchAddAsync(T[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await SaveAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            var entry = this._context.Entry(entity);
            entry.State = EntityState.Modified;

            //如果数据没有发生变化
            if (!this._context.ChangeTracker.HasChanges())
            {
                return;
            }

            await SaveAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }

        /// <summary>
        /// 实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="entity">The entity.</param>
        public async Task UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
        {
            await _dbSet.Where(where).UpdateAsync(entity);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(Expression<Func<T, bool>> exp)
        {
            await _dbSet.Where(exp).DeleteAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            //try
            //{
            await _context.SaveChangesAsync();
            //}
            //catch (DbEntityValidationException e)
            //{
            //    throw new Exception(e.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage);
            //}
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private IQueryable<T> Filter(Expression<Func<T, bool>> exp)
        {
            var dbSet = _dbSet.AsNoTracking().AsQueryable();
            if (exp != null)
                dbSet = dbSet.Where(exp);
            return dbSet;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<int> ExecuteSqlAsync(string sql)
        {
            return await _context.Database.ExecuteSqlCommandAsync(sql);
        }
    }
}