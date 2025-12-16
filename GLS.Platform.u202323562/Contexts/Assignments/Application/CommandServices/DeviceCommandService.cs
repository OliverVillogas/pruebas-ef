using GLS.Platform.u202323562.Contexts.Assignments.Domain.Commands;
using GLS.Platform.u202323562.Contexts.Assignments.Domain.Model.Aggregates;
using GLS.Platform.u202323562.Contexts.Assignments.Domain.Repositories;
using GLS.Platform.u202323562.Contexts.Assignments.Domain.Services;
using GLS.Platform.u202323562.Contexts.Shared.Domain.Repositories;

namespace GLS.Platform.u202323562.Contexts.Assignments.Application.CommandServices;

public class DeviceCommandService(
    IDeviceRepository deviceRepository,
    IUnitOfWork unitOfWork) : IDeviceCommandService
{
    public async Task<Device?> Handle(UpdatePreferredThrustCommand command)
    {
        var device = await deviceRepository.FindByMacAddressAsync(command.MacAddress);

        if (device == null)
            return null;

        if (device.PreferredThrust != command.NewPreferredThrust)
        {
            device.UpdatePreferredThrust(command.NewPreferredThrust);
            deviceRepository.Update(device);
            await unitOfWork.CompleteAsync();
        }

        return device;
    }
}