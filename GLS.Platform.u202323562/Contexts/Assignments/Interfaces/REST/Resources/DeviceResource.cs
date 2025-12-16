namespace GLS.Platform.u202323562.Contexts.Assignments.Interfaces.REST.Resources;

/// <summary>
///     Device resource for API responses
/// </summary>
/// <remarks>
///     Does not include audit fields (CreatedDate, UpdatedDate) as per exam requirements.
///     Implemented by Oliver Villogas Medina (u202323562)
/// </remarks>
public record DeviceResource(
    int Id,
    string MacAddress,
    int MissionId,
    decimal PreferredThrust
);