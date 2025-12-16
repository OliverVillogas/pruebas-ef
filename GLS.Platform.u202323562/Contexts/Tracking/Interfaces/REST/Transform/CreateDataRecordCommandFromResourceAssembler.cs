using GLS.Platform.u202323562.Contexts.Tracking.Domain.Commands;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Model.ValueObjects;
using GLS.Platform.u202323562.Contexts.Tracking.Interfaces.REST.Resources;

namespace GLS.Platform.u202323562.Contexts.Tracking.Interfaces.REST.Transform;

/// <summary>
///     Assembler to transform REST resource into domain command
/// </summary>
public static class CreateDataRecordCommandFromResourceAssembler
{
    public static CreateDataRecordCommand ToCommandFromResource(CreateDataRecordResource resource)
    {
        var operationMode = Enum.Parse<EOperationMode>(resource.OperationMode, true);

        var engineState = Enum.Parse<EEngineState>(resource.EngineState, true);

        return new CreateDataRecordCommand(
            resource.DeviceMacAddress,
            operationMode,
            resource.TargetThrust,
            resource.CurrentThrust,
            engineState,
            resource.GeneratedAt
        );
    }
}