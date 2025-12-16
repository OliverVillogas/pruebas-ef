namespace GLS.Platform.u202323562.Contexts.Assignments.Domain.Exceptions;

public class DeviceNotFoundException : Exception
{
    public DeviceNotFoundException(int id) 
        : base($"Device with id {id} not found")
    {
        DeviceId = id;
    }

    public int DeviceId { get; }
}