using GLS.Platform.u202323562.Contexts.Shared.Domain.Repositories;
using GLS.Platform.u202323562.Contexts.Shared.Infrastructure.Persistence.Configuration;

namespace GLS.Platform.u202323562.Contexts.Shared.Infrastructure.Repositories;

public class UnitOfWork(GLSContext context) : IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}