using Microsoft.EntityFrameworkCore;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Api.Domain.Models;
using SozlukApp.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly SozlukAppContext context;


        // we set the entity according to TEntity
        protected DbSet<TEntity> entity => context.Set<TEntity>();

        public GenericRepository(SozlukAppContext _context)
        {
            this.context = _context ?? throw new ArgumentNullException(nameof(_context));
        }

        #region add
        public virtual int Add(TEntity entity)
        {
            //context.Set<TEntity>().Add(entity); use bellow line instead
            this.entity.Add(entity);
            return context.SaveChanges();
        }

        public virtual int Add(IEnumerable<TEntity> entities)
        {
            if (entities != null && !entities.Any())
                return 0;

            this.entity.AddRange(entities);
            return context.SaveChanges();
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await this.entity.AddAsync(entity);
            return await context.SaveChangesAsync();
        }

        public virtual async Task<int> AddAsync(IEnumerable<TEntity> entities)
        {
            if (entities != null && !entities.Any())
                return 0;

            await this.entity.AddRangeAsync(entities);
            return context.SaveChanges();
        }
        #endregion

        #region Bulk
        public virtual async Task BulkAdd(IEnumerable<TEntity> entities)
        {
            if (entities != null && !entities.Any()) // if entities is empty
                await  Task.CompletedTask;

            await entity.AddRangeAsync(entities);

            await context.SaveChangesAsync();

        }

        public virtual Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            context.RemoveRange(entity.Where(predicate));

            return context.SaveChangesAsync();
        }

        public virtual Task BulkDelete(IEnumerable<TEntity> entities)
        {
            context.RemoveRange(entities);

            return context.SaveChangesAsync();
        }

        public virtual Task BulkDeleteById(IEnumerable<Guid> ids)
        {
           if(ids != null && !ids.Any()) return Task.CompletedTask;

            context.RemoveRange(this.entity.Where(i => ids.Contains(i.Id)));
            return context.SaveChangesAsync();
        }

        public virtual Task BulkUpdate(IEnumerable<TEntity> entities)
        {
            if (entities != null && !entities.Any()) 
                return Task.CompletedTask;

            foreach (var item in entities)
            {
                this.entity.Update(item);
            }

            return context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public virtual int Delete(TEntity entity)
        {
            if(context.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }

            this.entity.Remove(entity);

            return context.SaveChanges();
        }

        public virtual int Delete(Guid id)
        {
            var delEntity = this.entity.Find(id);
            return Delete(delEntity);
        }

        public virtual Task<int> DeleteAsync(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }

            this.entity.Remove(entity);

            return context.SaveChangesAsync();
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            var delEntity = this.entity.Find(id);
            return DeleteAsync(delEntity);
        }

        public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            context.RemoveRange(predicate);
            return context.SaveChanges() > 0;
        }

        public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            context.RemoveRange(predicate);
            return await context.SaveChangesAsync() > 0;
        }
        #endregion

        #region Get
        public virtual IQueryable<TEntity> AsQueryable()
        {
            return entity.AsQueryable();
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = entity.AsQueryable();

            if(predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if(noTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }

        public virtual async Task<List<TEntity>> GetAll(bool noTracking = true)
        {
            if(noTracking)
            {
                entity.AsNoTracking().ToListAsync();
            }

            return await entity.ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, IOrderedQueryable<TEntity> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;

            if(predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if(orderBy != null)
            {
                query = query.OrderByDescending(i => i.CreateDate);
            }

            if(noTracking) { 
                query = query.AsNoTracking();   
            }

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = ApplyIncludes(query, includes);

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.SingleOrDefaultAsync();
        }
        #endregion

        #region Update
        public virtual int Update(TEntity entity)
        {
            this.entity.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;

            return context.SaveChanges();
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            this.entity.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;

            return await context.SaveChangesAsync();
        }

        public virtual int AddOrUpdate(TEntity entity)
        {
            // check if its already tracked
            if (!this.entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
            {
                context.Update(entity);
            }

            return context.SaveChanges();
        }

        public virtual async Task<int> AddOrUpdateAsync(TEntity entity)
        {
            if (!this.entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
            {
                context.Update(entity);
            }

            return await context.SaveChangesAsync();
        }
        #endregion

        private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            if(includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            return query;
        }

        #region savechanges


        #endregion
    }
}
