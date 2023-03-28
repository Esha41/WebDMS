﻿using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Model.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Intelli.DMS.Domain.Core.Repository
{
    /// <summary>
    /// The generic repository.
    /// </summary>
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Gets or sets the after save event used for audit trail.
        /// </summary>
        public Action<object> AfterSave { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Gets the Records of the TEntity, Based on the filter specified
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="currentPage">The current page.</param>
        /// <returns>A list of TEntities.</returns>
        public virtual QueryResult<TEntity> Get(
              string filter = null,
              string orderBy = "Id desc",
              int pageSize = 10,
              int currentPage = 1,
              params Expression<Func<TEntity, object>>[] includes)
        {
            return GetPaginatedByQuery(_dbSet, filter, orderBy, pageSize, currentPage, includes);
        }

        /// <summary>
        /// Gets the Records of the TEntity, Based on the filter specified
        /// </summary>
        /// <param name="filter">The query.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="currentPage">The current page.</param>
        /// <returns>A list of TEntities.</returns>
        public virtual QueryResult<TEntity> GetPaginatedByQuery(IQueryable<TEntity> query,
                                                string filter = null,
                                                string orderBy = "Id desc",
                                                int pageSize = 10,
                                                int currentPage = 1,
                                                params Expression<Func<TEntity, object>>[] includes)
        {
            int count;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            try
            {
                if (filter != null)
                    query = query.Where(filter);

                if (orderBy != null)
                    query = query.OrderBy(orderBy);

                count = query.Count();

                query = query.Skip((currentPage - 1) * pageSize)
                    .Take(pageSize);
            }
            catch (ParseException e)
            {
                throw new ArgumentException("Invalid Parameters", e);
            }

            return new QueryResult<TEntity>() { Count = count, List = query.ToList() };
        }

        /// <summary>
        /// Gets the list of all active entities.
        /// </summary>
        /// <param name="includes">The sub entities to includes.</param>
        /// <param name="orderBy">The order by clause.</param>
        /// <returns>A QueryResult.</returns>
        public virtual async Task<QueryResult<TEntity>> GetAllActiveAsync(string orderBy, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
                query = query.Include(include);

            List<TEntity> list;
            try
            {
                query = query.Where(x => x.IsActive == true);
                query = query.OrderBy(orderBy);

                list = await query.ToListAsync();
            }
            catch (ParseException e)
            {
                throw new ArgumentException("Invalid Parameters", e);
            }

            return new QueryResult<TEntity>() { Count = list.Count, List = list };
        }

        /// <summary>
        /// Gets the query result based on passed parameters.
        /// </summary>
        /// <param name="query">The provided query to be used on entity.</param>
        /// <param name="filter">The filter expression.</param>
        /// <param name="includes">The inner objects that needs to be included.</param>
        /// <returns>A QueryResult.</returns>
        public virtual QueryResult<TEntity> Get(
              IQueryable<TEntity> query,
              string filter = null,
              params Expression<Func<TEntity, object>>[] includes)
        {
            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            int count;

            try
            {
                if (filter != null)
                    query = query.Where(filter);

                count = query.Count();
            }
            catch (ParseException e)
            {
                throw new ArgumentException("Invalid Parameters", e);
            }

            return new QueryResult<TEntity>() { Count = count, List = query.ToList() };
        }

        /// <summary>
        /// Gets the query result of the specified identity including sub entities.
        /// </summary>
        /// <param name="includes">The sub entities needs to be included.</param>
        /// <returns>A QueryResult.</returns>
        public virtual QueryResult<TEntity> Get(
              params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            return new QueryResult<TEntity>() { List = query.ToList() };
        }

        /// <summary>
        /// Queries the entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>An IQueryable.</returns>
        public virtual IQueryable<TEntity> Query(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }

        /// <summary>
        /// Gets the entity by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A ValueTask.</returns>
        public virtual ValueTask<TEntity> GetById(object id)
        {
            return _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Gets the first or default entity.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>A TEntity.</returns>
        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            return query.FirstOrDefault(filter);
        }

        /// <summary>
        /// Inserts the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Insert(TEntity entity, bool isActive = true)
        {
            var value = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            entity.CreatedAt = value;
            entity.UpdatedAt = value;
            entity.IsActive = isActive;

            _dbSet.Add(entity);
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            entity.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(nameof(entity.CreatedAt)).IsModified = false;
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="id">The primary key of the entity.</param>
        /// <param name="deactivate">If true, deactivate.</param>
        public virtual void Delete(object id, bool deactivate = true)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            if (entityToDelete != null)
            {
                if (deactivate)
                {
                    entityToDelete.IsActive = false;
                    Update(entityToDelete);
                }
                else
                {
                    _context.RemoveRange(entityToDelete);
                }
            }
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        public virtual void Delete(Expression<Func<TEntity, bool>> filter)
        {
            _context.RemoveRange(_dbSet.Where(filter));
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        public virtual void UpdateRange(List<TEntity> entities)
        {
             entities.ForEach(e => e.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _context.UpdateRange(entities);
        }

        /// <summary>
        /// Deletes multiple entities.
        /// </summary>
        /// <param name="ids">The list of id of the entities to be deleted.</param>
        /// <param name="deactivate">If true, deactivate.</param>
        public virtual void Delete(int[] ids, bool deactivate = true)
        {
            var query = Query(x => ids.Contains(x.Id));
            var result = Get(query);

            if (deactivate)
                result.List.ForEach(x => x.IsActive = false);
            else
                _context.RemoveRange(result.List);
        }

        /// <summary>
        /// Deletes multiple entities on the basis of supplied criteria.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        /// <param name="deactivate">If true, deactivate.</param>
        public virtual void Delete(string filter = null, bool deactivate = true)
        {
            var result = Get(Query(), filter);

            if (deactivate)
                result.List.ForEach(x => x.IsActive = false);
            else
                _context.RemoveRange(result.List);
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public virtual void SaveChanges(string userName,string requestId, ITransactionHandler transaction)
        {
            // Create audit logs
            var logs = new AuditHelper(_context).CreateAuditLogs(userName,requestId);

            // Save changes in database
            _context.SaveChanges();

            // Add audit logs
            if (transaction != null)
                transaction.Logs.AddRange(logs);
            else
                AfterSave?.Invoke(logs);
        }

        /// <summary>
        /// SaveChanges For Data Migration With Identity Insert ON
        /// </summary>
        /// <typeparam name="TEnt">Generic Entity Type</typeparam>
        /// <param name="token">CancellationToken Token</param>
        public void SaveChangesForDataMigrationWithIdentityInsertONAsync<TEnt>(string RequestId,CancellationToken token = default)
        {
            using var transaction =  _context.Database.BeginTransactionAsync(token).Result;
            SetIdentityInsertAsync<TEnt>(true, token).Wait();
            SaveChanges("DataMigration", null, null);
            SetIdentityInsertAsync<TEnt>(false, token).Wait();
            transaction.CommitAsync(token).Wait();
        }

        /// <summary>
        /// Set IdentityInsert Async
        /// </summary>
        /// <typeparam name="TEnt">Generic Entity Type</typeparam>
        /// <param name="enable">Boolean Variable to ON or OFF Identity Insert</param>
        /// <param name="token">CancellationToken Token</param>
        /// <returns></returns>
        private async Task SetIdentityInsertAsync<TEnt>(bool enable, CancellationToken token)
        {
            var entityType = _context.Model.FindEntityType(typeof(TEnt));
            var value = enable ? "ON" : "OFF";
            string query = $"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}";
            await _context.Database.ExecuteSqlRawAsync(query, token);
        }
        /// <summary>
        /// Counts the number of elements.
        /// </summary>
        /// <returns>Number of elements.</returns>
        public int Count()
        {
            return _dbSet.Count();
        }

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <returns>An ITransactionHandler.</returns>
        public ITransactionHandler GetTransaction()
        {
            return new TransactionHandler(_context)
            {
                TransactionCommitted = AfterSave
            };
        }
    }
}
