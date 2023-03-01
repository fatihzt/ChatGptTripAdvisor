using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptBot.DataAccess.EntityFrameworkCore
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, new()
        where TContext : DbContext, new()
    {
        public int Add(TEntity entity)
        {
            using (TContext dbcontext = new TContext())
            {
                var addedEntity = dbcontext.Entry(entity);
                addedEntity.State = EntityState.Added;
                return dbcontext.SaveChanges();
            }
        }

        public bool Delete(TEntity entity)
        {
            using (TContext dbcontext = new TContext())
            {
                var deletedEntity = dbcontext.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                bool value = dbcontext.SaveChanges() > 0;
                return value;
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext dbcontext = new TContext())
            {
                var query = dbcontext.Set<TEntity>().AsQueryable();
                if (filter != null) query = query.Where(filter);

                return query.FirstOrDefault();
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext dbcontext = new TContext())
            {
                var querry = dbcontext.Set<TEntity>().AsQueryable();
                if (filter != null) querry = querry.Where(filter);
                return querry.ToList();
            }
        }

        public bool Update(TEntity entity)
        {
            using (TContext dbcontext = new TContext())
            {
                var updatedEntity = dbcontext.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                bool value = dbcontext.SaveChanges() > 0;
                return value;
            }
        }
    }
}
