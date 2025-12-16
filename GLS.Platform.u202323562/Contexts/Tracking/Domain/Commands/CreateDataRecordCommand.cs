using GLS.Platform.u202323562.Contexts.Tracking.Domain.Model.ValueObjects;

namespace GLS.Platform.u202323562.Contexts.Tracking.Domain.Commands;

public record CreateDataRecordCommand(
    string DeviceMacAddress,
    EOperationMode OperationMode,
    decimal TargetThrust,
    decimal CurrentThrust,
    EEngineState EngineState,
    DateTime GeneratedAt
);