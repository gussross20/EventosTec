using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data
{
    public class EfRepository : IEfRepository
    {
        readonly WebAppCtx _WebAppCtx;
        private IDbContextTransaction? _transaction;

        public EfRepository(WebAppCtx WebAppCtx)
        {
            _WebAppCtx = WebAppCtx;
        }

        public void BeginTransaction()
        {
            _transaction = _WebAppCtx.Database.BeginTransaction();
        }

        public void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public Task? CommitAsync()
        {
            if (_transaction != null)
            {
                return _transaction.CommitAsync();
            }
            else return null;
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            if (_WebAppCtx.Entry(entity).State == EntityState.Detached)
            {
                _WebAppCtx.Set<TEntity>().Attach(entity);
            }
            _WebAppCtx.Set<TEntity>().Remove(entity);
        }

        public TEntity? Find<TEntity, TId>(TId id) where TEntity : class
        {
            return _WebAppCtx.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> Queryanle<TEntity>() where TEntity : class
        {
            return _WebAppCtx.Set<TEntity>().AsQueryable();
        }

        public Task? RollbackAsync()
        {
            if (_transaction != null)
            {
                return _transaction.RollbackAsync();
            }
            else return null;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _WebAppCtx.Set<TEntity>().Add(entity);

        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _WebAppCtx.Set<TEntity>().Attach(entity);
            _WebAppCtx.Entry(entity).State = EntityState.Modified;
        }

        public Task SaveChangesAsync()
        {
            return _WebAppCtx.SaveChangesAsync();
        }
    }
}
