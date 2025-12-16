namespace GLS.Platform.u202323562.Contexts.Tracking.Interfaces.REST.Resources;

/// <summary>
///     DTO for creating a new data record
/// </summary>
public record CreateDataRecordResource(
    string DeviceMacAddress,
    string OperationMode,
    decimal TargetThrust,
    decimal CurrentThrust,
    string EngineState,
    DateTime GeneratedAt
);