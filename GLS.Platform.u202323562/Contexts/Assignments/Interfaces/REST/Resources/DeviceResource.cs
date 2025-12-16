namespace GLS.Platform.u202323562.Contexts.Assignments.Interfaces.REST.Resources;

public record DeviceResource(
    int Id,
    string MacAddress,
    int MissionId,
    decimal PreferredThrust,
    DateTime CreatedDate,
    DateTime? UpdatedDate
);