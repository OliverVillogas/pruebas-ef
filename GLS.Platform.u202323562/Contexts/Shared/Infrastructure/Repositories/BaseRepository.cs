using GLS.Platform.u202323562.Contexts.Shared.Domain.Model.Entities;
using GLS.Platform.u202323562.Contexts.Shared.Domain.Repositories;
using GLS.Platform.u202323562.Contexts.Shared.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace GLS.Platform.u202323562.Contexts.Shared.Infrastructure.Repositories;

public class BaseRepository<TEntity>(GLSContext context)
    : IBaseRepository<TEntity> where TEntity : class
{
    public virtual async Task AddAsync(TEntity entity)
    {
        await context.AddAsync(entity);
    }

    public virtual async Task<TEntity?> FindByIdAsync(int id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        
        if (entity is BaseEntity baseEntity && baseEntity.IsDeleted == 1)
            return null;

        return entity;
    }

    public virtual void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public virtual async Task<IEnumerable<TEntity>> ListAsync()
    {
        if (typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            return await context.Set<TEntity>()
                .Where(e => ((BaseEntity)(object)e).IsDeleted == 0)
                .ToListAsync();

        return await context.Set<TEntity>().ToListAsync();
    }
}