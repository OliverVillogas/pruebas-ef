using GLS.Platform.u202323562.Contexts.Assignments.Domain.Commands;
using GLS.Platform.u202323562.Contexts.Assignments.Domain.Model.Aggregates;

namespace GLS.Platform.u202323562.Contexts.Assignments.Domain.Services;

public interface IDeviceCommandService
{
    Task<Device?> Handle(UpdatePreferredThrustCommand command);
}