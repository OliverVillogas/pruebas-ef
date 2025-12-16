using GLS.Platform.u202323562.Contexts.Assignments.Domain.Repositories;
using GLS.Platform.u202323562.Contexts.Shared.Domain.Repositories;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Events;

namespace GLS.Platform.u202323562.Contexts.Assignments.Application.EventHandlers;

/// <summary>
/// Event handler that listens to DataRecordRegisteredEvent and updates device preferred thrust.
/// </summary>
/// <remarks>
/// When a data record is registered in the TRACKING context, this handler updates
/// the preferred thrust of the corresponding device in ASSIGNMENTS context if the value differs.
/// Author: [Tu Nombre Completo]
/// </remarks>
public class DataRecordRegisteredEventHandler
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DataRecordRegisteredEventHandler(
        IDeviceRepository deviceRepository,
        IUnitOfWork unitOfWork)
    {
        _deviceRepository = deviceRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the DataRecordRegisteredEvent by updating device preferred thrust.
    /// </summary>
    /// <param name="event">Event containing device MAC address and target thrust</param>
    public async Task Handle(DataRecordRegisteredEvent @event)
    {
        // Find device by MAC Address
        var device = await _deviceRepository.FindByMacAddressAsync(@event.DeviceMacAddress);
        
        if (device == null)
            return; // Device not found, ignore event silently

        // Only update if thrust value is different
        if (device.PreferredThrust != @event.TargetThrust)
        {
            device.UpdatePreferredThrust(@event.TargetThrust);
            _deviceRepository.Update(device);
            await _unitOfWork.CompleteAsync();
        }
    }
}