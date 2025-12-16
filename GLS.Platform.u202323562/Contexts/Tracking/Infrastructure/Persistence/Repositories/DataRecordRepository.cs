using GLS.Platform.u202323562.Contexts.Shared.Infrastructure.Persistence.Configuration;
using GLS.Platform.u202323562.Contexts.Shared.Infrastructure.Repositories;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Model.Aggregates;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GLS.Platform.u202323562.Contexts.Tracking.Infrastructure.Persistence.Repositories;

public class DataRecordRepository(GLSContext context)
    : BaseRepository<DataRecord>(context), IDataRecordRepository
{
    public async Task<IEnumerable<DataRecord>> FindByDeviceMacAddressAsync(string macAddress)
    {
        if (string.IsNullOrWhiteSpace(macAddress))
            return Enumerable.Empty<DataRecord>();

        return await context.DataRecords
            .Where(dr => dr.IsDeleted == 0)
            .Where(dr => dr.DeviceMacAddress.Value == macAddress.ToUpperInvariant())
            .OrderByDescending(dr => dr.GeneratedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<DataRecord>> FindByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await context.DataRecords
            .Where(dr => dr.IsDeleted == 0)
            .Where(dr => dr.GeneratedAt >= startDate && dr.GeneratedAt <= endDate)
            .OrderByDescending(dr => dr.GeneratedAt)
            .ToListAsync();
    }
}