using GLS.Platform.u202323562.Contexts.Assignments.Domain.Exceptions;
using GLS.Platform.u202323562.Contexts.Shared.Domain.Model.Entities;
using GLS.Platform.u202323562.Contexts.Shared.Domain.Model.ValueObjects;

namespace GLS.Platform.u202323562.Contexts.Assignments.Domain.Model.Aggregates;

public class Device : BaseEntity
{
    private const int MinMissionId = 1;
    private const decimal MinThrust = 0;
    private const int DeletedFlag = 1;

    protected Device()
    {
        MacAddress = null!;
    }

    public Device(MacAddress macAddress, int missionId, decimal preferredThrust)
    {
        if (missionId < MinMissionId)
            throw InvalidDeviceDataException.InvalidMissionId(missionId);

        if (preferredThrust < MinThrust)
            throw InvalidDeviceDataException.InvalidThrust(preferredThrust);

        MacAddress = macAddress ?? throw new ArgumentNullException(nameof(macAddress));
        MissionId = missionId;
        PreferredThrust = preferredThrust;
        CreatedDate = DateTime.UtcNow;
    }

    public MacAddress MacAddress { get; private set; }
    public int MissionId { get; private set; }
    public decimal PreferredThrust { get; private set; }

    public void UpdateMission(int newMissionId)
    {
        if (newMissionId < MinMissionId)
            throw InvalidDeviceDataException.InvalidMissionId(newMissionId);

        MissionId = newMissionId;
        UpdatedDate = DateTime.UtcNow;
    }

    public void UpdatePreferredThrust(decimal newThrust)
    {
        if (newThrust < MinThrust)
            throw InvalidDeviceDataException.InvalidThrust(newThrust);

        PreferredThrust = newThrust;
        UpdatedDate = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = DeletedFlag;
        UpdatedDate = DateTime.UtcNow;
    }
}