using MediatR;

namespace GLS.Platform.u202323562.Contexts.Tracking.Domain.Events;

/// <summary>
///     Integration event published when a data record is successfully registered.
/// </summary>
/// <remarks>
///     Author: Oliver Villogas Medina (u202323562)
/// </remarks>
public record DataRecordRegisteredEvent(
    string DeviceMacAddress,
    decimal TargetThrust
) : INotification;