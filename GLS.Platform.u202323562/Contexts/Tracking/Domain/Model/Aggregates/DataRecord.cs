using GLS.Platform.u202323562.Contexts.Shared.Domain.Model.Entities;
using GLS.Platform.u202323562.Contexts.Shared.Domain.Model.ValueObjects;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Exceptions;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Model.ValueObjects;

namespace GLS.Platform.u202323562.Contexts.Tracking.Domain.Model.Aggregates;

/// <summary>
///     DataRecord aggregate root representing telemetry data from rocket engines
/// </summary>
/// <remarks>
///     Contains thrust measurements, operation mode, and engine state information.
///     Implements business rules for valid thrust ranges (700.0 - 950.0 kN).
///     Implemented by Oliver Villogas Medina (u202323562)
/// </remarks>
public class DataRecord : BaseEntity
{
    private const decimal MinTargetThrust = 700.0m;
    private const decimal MaxTargetThrust = 950.0m;
    private const decimal MinCurrentThrust = 0m;
    private const int DeletedFlag = 1;

    protected DataRecord()
    {
        DeviceMacAddress = null!;
    }

    public DataRecord(
        MacAddress deviceMacAddress,
        EOperationMode operationMode,
        decimal targetThrust,
        decimal currentThrust,
        EEngineState engineState,
        DateTime generatedAt)
    {
        DeviceMacAddress = deviceMacAddress ?? throw new ArgumentNullException(nameof(deviceMacAddress));

        if (targetThrust < MinTargetThrust || targetThrust > MaxTargetThrust)
            throw InvalidDataRecordException.InvalidTargetThrust(targetThrust, MinTargetThrust, MaxTargetThrust);

        if (currentThrust < MinCurrentThrust)
            throw InvalidDataRecordException.InvalidCurrentThrust(currentThrust);

        if (generatedAt > DateTime.UtcNow)
            throw InvalidDataRecordException.InvalidGeneratedDate(generatedAt);

        OperationMode = operationMode;
        TargetThrust = targetThrust;
        CurrentThrust = currentThrust;
        EngineState = engineState;
        GeneratedAt = generatedAt;
        CreatedDate = DateTime.UtcNow;
    }

    public MacAddress DeviceMacAddress { get; private set; }
    public EOperationMode OperationMode { get; private set; }
    public decimal TargetThrust { get; private set; }
    public decimal CurrentThrust { get; private set; }
    public EEngineState EngineState { get; private set; }
    public DateTime GeneratedAt { get; private set; }

    public void UpdateOperationMode(EOperationMode newMode)
    {
        OperationMode = newMode;
        UpdatedDate = DateTime.UtcNow;
    }

    public void UpdateEngineState(EEngineState newState)
    {
        EngineState = newState;
        UpdatedDate = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = DeletedFlag;
        UpdatedDate = DateTime.UtcNow;
    }
}