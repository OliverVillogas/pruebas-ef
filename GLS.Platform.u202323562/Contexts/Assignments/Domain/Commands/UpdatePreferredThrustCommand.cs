namespace GLS.Platform.u202323562.Contexts.Assignments.Domain.Commands;

public record UpdatePreferredThrustCommand(
    string MacAddress,
    decimal NewPreferredThrust
);