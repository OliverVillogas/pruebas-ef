using GLS.Platform.u202323562.Contexts.Shared.Domain.Events;

namespace GLS.Platform.u202323562.Contexts.Tracking.Domain.Events;

/// <summary>
/// Integration event published when a data record is successfully registered.
/// </summary>
/// <remarks>
/// Author: [Tu Nombre Completo]
/// </remarks>
public record DataRecordRegisteredEvent(
    string DeviceMacAddress,
    decimal TargetThrust
) : IDomainEvent;