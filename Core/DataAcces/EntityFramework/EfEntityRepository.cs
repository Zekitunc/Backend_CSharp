using Core.DataAccess;
using Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAcces.EntityFramework
{
    //bir entity ve context ile Ef içinde kod yazılabilir 
    //generic hale getirdik önceden EF içindeydi
    public class EfEntityRepository<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext,new()
    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext()) //using bittiği zaman resource yok olur performans işidir
            {
                var addedEntity = context.Entry(entity); //eşleme yap 
                addedEntity.State = EntityState.Added; //kaydet
                context.SaveChanges(); //işlemi kaydet
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext()) //using bittiği zaman resource yok olur performans işidir
            {
                var deletedEntity = context.Entry(entity); //eşleme yap 
                deletedEntity.State = EntityState.Deleted; //kaydet
                context.SaveChanges(); //işlemi kaydet
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
                //standar ise generic base yapabilirsin tüm içeriği category haliyle yazmak gibi
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
                //DB SET kodları bunun için

            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext()) //using bittiği zaman resource yok olur performans işidir
            {
                var updatedEntity = context.Entry(entity); //eşleme yap 
                updatedEntity.State = EntityState.Modified; //kaydet
                context.SaveChanges(); //işlemi kaydet
            }
        }
    }
}
