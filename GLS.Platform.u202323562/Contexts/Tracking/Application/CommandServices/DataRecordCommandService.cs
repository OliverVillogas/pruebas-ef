using GLS.Platform.u202323562.Contexts.Shared.Domain.Events;
using GLS.Platform.u202323562.Contexts.Shared.Domain.Model.ValueObjects;
using GLS.Platform.u202323562.Contexts.Shared.Domain.Repositories;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Commands;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Exceptions;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Model.Aggregates;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Repositories;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Services;
using GLS.Platform.u202323562.Contexts.Tracking.Interfaces.REST.ACL;

namespace GLS.Platform.u202323562.Contexts.Tracking.Application.CommandServices;

/// <summary>
/// Command service for DataRecord operations
/// </summary>
/// <remarks>
/// Validates device existence through ACL before creating records.
/// Emits DataRecordRegisteredEvent for inter-context communication.
/// Implemented by Oliver Villogas Medina (u202323562)
/// </remarks>
public class DataRecordCommandService(
    IDataRecordRepository dataRecordRepository,
    IAssignmentsContextFacade assignmentsContextFacade,
    IDomainEventPublisher eventPublisher,
    IUnitOfWork unitOfWork) : IDataRecordCommandService
{
    public async Task<DataRecord> Handle(CreateDataRecordCommand command)
    {
        var deviceExists = await assignmentsContextFacade.DeviceExistsByMacAddressAsync(command.DeviceMacAddress);
        
        if (!deviceExists)
            throw InvalidDataRecordException.DeviceNotFound(command.DeviceMacAddress);
        
        var macAddress = new MacAddress(command.DeviceMacAddress);
        
        var dataRecord = new DataRecord(
            macAddress,
            command.OperationMode,
            command.TargetThrust,
            command.CurrentThrust,
            command.EngineState,
            command.GeneratedAt
        );
        
        await dataRecordRepository.AddAsync(dataRecord);
        await unitOfWork.CompleteAsync();
        
        var domainEvent = new DataRecordRegisteredEvent(
            command.DeviceMacAddress,
            command.TargetThrust
        );
        
        await eventPublisher.PublishAsync(domainEvent);

        return dataRecord;
    }
}