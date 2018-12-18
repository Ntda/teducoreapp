using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TeduNetcore.Infrastructure.Intarfaces;
using TeduNetcore.Infrastructure.SharedKernel;

namespace TeduNetcore.Data.EF
{
    public class EFRepository<T, K> : IDisposable, IRepository<T, K> where T : DomainEntity<K>
    {
        protected AppDbContext _appDbContext { get; set; }
        public EFRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Dispose()
        {
            _appDbContext?.Dispose();
        }

        public void Add(T entity)
        {
            _appDbContext.Add(entity);
        }

        public IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _appDbContext.Set<T>() as IQueryable<T>;
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                items = items?.Include(includeProperty);
            }
            return items;
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _appDbContext.Set<T>();
            if (includeProperties != null)
            {
                foreach (Expression<Func<T, object>> includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(predicate);
        }

        public T FindById(K id, params Expression<Func<T, object>>[] includeProperties)
        {
            return FindAll(includeProperties)?.First(x => x.Id.Equals(id));
        }

        public T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return FindAll(includeProperties)?.First(predicate);
        }

        public void Remove(T entity)
        {
            _appDbContext.Remove(entity);
        }

        public void Remove(K id)
        {
            T entity = FindById(id);
            Remove(entity);
        }

        public void RemoveMultiple(List<T> entities)
        {
            _appDbContext.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _appDbContext.Update(entity);
        }
    }
}
