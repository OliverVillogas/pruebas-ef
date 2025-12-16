namespace GLS.Platform.u202323562.Contexts.Tracking.Domain.Exceptions;

public class InvalidDataRecordException : Exception
{
    public InvalidDataRecordException(string message) : base(message)
    {
    }

    public static InvalidDataRecordException InvalidTargetThrust(decimal thrust, decimal min, decimal max)
    {
        return new InvalidDataRecordException(
            $"Target thrust {thrust} kN is out of valid range ({min} - {max} kN)");
    }

    public static InvalidDataRecordException InvalidCurrentThrust(decimal thrust)
    {
        return new InvalidDataRecordException(
            $"Current thrust {thrust} kN cannot be negative");
    }

    public static InvalidDataRecordException InvalidGeneratedDate(DateTime generatedAt)
    {
        return new InvalidDataRecordException(
            $"Generated date {generatedAt:O} cannot be in the future");
    }

    public static InvalidDataRecordException DeviceNotFound(string macAddress)
    {
        return new InvalidDataRecordException(
            $"Device with MAC address {macAddress} does not exist in ASSIGNMENTS context");
    }
}