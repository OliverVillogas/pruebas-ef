using GLS.Platform.u202323562.Contexts.Assignments.Domain.Repositories;

namespace GLS.Platform.u202323562.Contexts.Tracking.Interfaces.REST.ACL.Services;

/// <summary>
///     Implementation of ASSIGNMENTS context facade (Anti-Corruption Layer)
/// </summary>
/// <remarks>
///     This ACL translates TRACKING context needs into ASSIGNMENTS context operations.
///     It acts as a boundary that prevents TRACKING from depending directly on ASSIGNMENTS domain.
///     Uses IDeviceRepository from ASSIGNMENTS context to verify device existence.
///     Implemented by Oliver Villogas Medina (u202323562)
/// </remarks>
public class AssignmentsContextFacade(IDeviceRepository deviceRepository) : IAssignmentsContextFacade
{
    public async Task<bool> DeviceExistsByMacAddressAsync(string macAddress)
    {
        if (string.IsNullOrWhiteSpace(macAddress))
            return false;

        return await deviceRepository.ExistsByMacAddressAsync(macAddress);
    }
}