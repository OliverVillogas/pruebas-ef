using GLS.Platform.u202323562.Contexts.Assignments.Domain.Model.Aggregates;
using GLS.Platform.u202323562.Contexts.Assignments.Domain.Repositories;
using GLS.Platform.u202323562.Contexts.Shared.Infrastructure.Persistence.Configuration;
using GLS.Platform.u202323562.Contexts.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GLS.Platform.u202323562.Contexts.Assignments.Infrastructure.Persistence.Repositories;

public class DeviceRepository(GLSContext context)
    : BaseRepository<Device>(context), IDeviceRepository
{
    public async Task<Device?> FindByMacAddressAsync(string macAddress)
    {
        if (string.IsNullOrWhiteSpace(macAddress))
            return null;

        return await context.Devices
            .Where(d => d.IsDeleted == 0)
            .FirstOrDefaultAsync(d => d.MacAddress.Value == macAddress.ToUpperInvariant());
    }

    public async Task<bool> ExistsByMacAddressAsync(string macAddress)
    {
        if (string.IsNullOrWhiteSpace(macAddress))
            return false;

        return await context.Devices
            .Where(d => d.IsDeleted == 0)
            .AnyAsync(d => d.MacAddress.Value == macAddress.ToUpperInvariant());
    }
}