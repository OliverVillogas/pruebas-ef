using GLS.Platform.u202323562.Contexts.Assignments.Domain.Model.Aggregates;
using GLS.Platform.u202323562.Contexts.Shared.Domain.Repositories;

namespace GLS.Platform.u202323562.Contexts.Assignments.Domain.Repositories;

public interface IDeviceRepository : IBaseRepository<Device>
{
    Task<Device?> FindByMacAddressAsync(string macAddress);
    Task<bool> ExistsByMacAddressAsync(string macAddress);
}