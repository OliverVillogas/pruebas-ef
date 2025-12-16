using GLS.Platform.u202323562.Contexts.Assignments.Domain.Commands;
using GLS.Platform.u202323562.Contexts.Assignments.Domain.Services;
using GLS.Platform.u202323562.Contexts.Shared.Domain.Events;

namespace GLS.Platform.u202323562.Contexts.Assignments.Application.EventHandlers;

/// <summary>
/// Event handler that listens to DataRecordRegisteredEvent and updates device preferred thrust
/// </summary>
/// <remarks>
/// When a data record is registered in the TRACKING context, this handler updates
/// the preferred thrust of the corresponding device in ASSIGNMENTS context if the value differs.
/// Implemented by Oliver Villogas Medina (u202323562)
/// </remarks>
public class DataRecordRegisteredEventHandler(IDeviceCommandService deviceCommandService)
{
    public async Task HandleAsync(DataRecordRegisteredEvent @event)
    {
        var command = new UpdatePreferredThrustCommand(
            @event.DeviceMacAddress,
            @event.TargetThrust
        );
        
        await deviceCommandService.Handle(command);
    }
}