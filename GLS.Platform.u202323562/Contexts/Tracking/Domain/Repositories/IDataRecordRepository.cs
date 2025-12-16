using GLS.Platform.u202323562.Contexts.Shared.Domain.Repositories;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Model.Aggregates;

namespace GLS.Platform.u202323562.Contexts.Tracking.Domain.Repositories;

public interface IDataRecordRepository : IBaseRepository<DataRecord>
{
    Task<IEnumerable<DataRecord>> FindByDeviceMacAddressAsync(string macAddress);

    Task<IEnumerable<DataRecord>> FindByDateRangeAsync(DateTime startDate, DateTime endDate);
}