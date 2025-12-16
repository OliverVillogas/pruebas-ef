using GLS.Platform.u202323562.Contexts.Assignments.Domain.Model.Aggregates;
using GLS.Platform.u202323562.Contexts.Assignments.Domain.Queries;
using GLS.Platform.u202323562.Contexts.Assignments.Domain.Repositories;
using GLS.Platform.u202323562.Contexts.Assignments.Domain.Services;

namespace GLS.Platform.u202323562.Contexts.Assignments.Application.QueryServices;

public class DeviceQueryService(IDeviceRepository deviceRepository) : IDeviceQueryService
{
    public async Task<IEnumerable<Device>> Handle(GetAllDevices query)
    {
        return await deviceRepository.ListAsync();
    }
}