namespace GLS.Platform.u202323562.Contexts.Shared.Domain.Events;

public record DataRecordRegisteredEvent(
    string DeviceMacAddress,
    decimal TargetThrust
);