namespace SportsNet.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SportsNet.Data.Repositories.Interfaces;

    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private SportsNetDbContext context;
        private DbSet<TEntity> dbSet;

        public EfRepository(SportsNetDbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> All() => this.dbSet;

        public virtual IQueryable<TEntity> AllAsNoTracking() => this.dbSet.AsNoTracking();
        
        public virtual async void AddAsync(TEntity entity) => await this.dbSet.AddAsync(entity);

        public virtual void Update(TEntity entity)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity) => this.dbSet.Remove(entity);

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) 
        {
            if (disposing)
            {
                this.context.Dispose();
            }
        }

        public Task<int> SaveChangesAsync() => this.context.SaveChangesAsync();
    }
}
