namespace GLS.Platform.u202323562.Contexts.Assignments.Domain.Exceptions;

public class InvalidDeviceDataException : Exception
{
    public InvalidDeviceDataException(string message) : base(message)
    {
    }

    public static InvalidDeviceDataException InvalidMissionId(int missionId)
    {
        return new InvalidDeviceDataException($"Mission ID {missionId} is invalid");
    }

    public static InvalidDeviceDataException InvalidThrust(decimal thrust)
    {
        return new InvalidDeviceDataException($"Thrust {thrust} cannot be negative");
    }
}