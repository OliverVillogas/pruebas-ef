namespace GLS.Platform.u202323562.Contexts.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}