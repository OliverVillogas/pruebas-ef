using GLS.Platform.u202323562.Contexts.Assignments.Domain.Model.Aggregates;
using GLS.Platform.u202323562.Contexts.Assignments.Domain.Queries;

namespace GLS.Platform.u202323562.Contexts.Assignments.Domain.Services;

public interface IDeviceQueryService
{
    Task<IEnumerable<Device>> Handle(GetAllDevices query);
}