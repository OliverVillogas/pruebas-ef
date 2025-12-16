using GLS.Platform.u202323562.Contexts.Assignments.Domain.Model.Aggregates;
using GLS.Platform.u202323562.Contexts.Assignments.Interfaces.REST.Resources;

namespace GLS.Platform.u202323562.Contexts.Assignments.Interfaces.REST.Transform;

public static class DeviceResourceFromEntityAssembler
{
    public static DeviceResource ToResourceFromEntity(Device entity)
    {
        return new DeviceResource(
            entity.Id,
            entity.MacAddress.Value,
            entity.MissionId,
            entity.PreferredThrust,
            entity.CreatedDate,
            entity.UpdatedDate
        );
    }

    public static IEnumerable<DeviceResource> ToResourceFromEntity(IEnumerable<Device> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}