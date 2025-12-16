using MediatR;
using GLS.Platform.u202323562.Contexts.Shared.Domain.Model.ValueObjects;
using GLS.Platform.u202323562.Contexts.Shared.Domain.Repositories;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Commands;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Events;
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
/// Emits DataRecordRegisteredEvent for inter-context communication using MediatR.
/// Implemented by Oliver Villogas Medina (u202323562)
/// </remarks>
public class DataRecordCommandService : IDataRecordCommandService
{
    private readonly IDataRecordRepository _dataRecordRepository;
    private readonly IAssignmentsContextFacade _assignmentsContextFacade;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public DataRecordCommandService(
        IDataRecordRepository dataRecordRepository,
        IAssignmentsContextFacade assignmentsContextFacade,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _dataRecordRepository = dataRecordRepository;
        _assignmentsContextFacade = assignmentsContextFacade;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<DataRecord> Handle(CreateDataRecordCommand command)
    {
        // Validate device exists through ACL
        var deviceExists = await _assignmentsContextFacade.DeviceExistsByMacAddressAsync(command.DeviceMacAddress);
        
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
        
        await _dataRecordRepository.AddAsync(dataRecord);
        await _unitOfWork.CompleteAsync();
        
        // Emit integration event using MediatR
        var domainEvent = new DataRecordRegisteredEvent(
            command.DeviceMacAddress,
            command.TargetThrust
        );
        
        await _mediator.Publish(domainEvent);

        return dataRecord;
    }
}