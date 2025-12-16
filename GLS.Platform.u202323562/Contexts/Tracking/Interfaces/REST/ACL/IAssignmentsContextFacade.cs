namespace GLS.Platform.u202323562.Contexts.Tracking.Interfaces.REST.ACL;

/// <summary>
/// Anti-Corruption Layer facade for ASSIGNMENTS bounded context
/// </summary>
/// <remarks>
/// Provides a stable interface to query ASSIGNMENTS context capabilities
/// without creating coupling between TRACKING and ASSIGNMENTS domains.
/// This ACL protects TRACKING from changes in ASSIGNMENTS internal structure.
/// Implemented by Oliver Villogas Medina (u202323562)
/// </remarks>
public interface IAssignmentsContextFacade
{
    Task<bool> DeviceExistsByMacAddressAsync(string macAddress);
}