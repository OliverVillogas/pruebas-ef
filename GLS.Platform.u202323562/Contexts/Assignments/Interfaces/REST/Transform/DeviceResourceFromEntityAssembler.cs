using GLS.Platform.u202323562.Contexts.Assignments.Domain.Model.Aggregates;
using GLS.Platform.u202323562.Contexts.Assignments.Interfaces.REST.Resources;

namespace GLS.Platform.u202323562.Contexts.Assignments.Interfaces.REST.Transform;

/// <summary>
///     Assembler to transform Device entity to DeviceResource
/// </summary>
/// <remarks>
///     Excludes audit fields (CreatedDate, UpdatedDate) as per requirements.
///     Implemented by Oliver Villogas Medina (u202323562)
/// </remarks>
public static class DeviceResourceFromEntityAssembler
{
    public static DeviceResource ToResourceFromEntity(Device entity)
    {
        return new DeviceResource(
            entity.Id,
            entity.MacAddress.Value,
            entity.MissionId,
            entity.PreferredThrust
        );
    }

    public static IEnumerable<DeviceResource> ToResourceFromEntity(IEnumerable<Device> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}