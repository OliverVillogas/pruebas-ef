using System.Net.Mime;
using GLS.Platform.u202323562.Contexts.Assignments.Domain.Queries;
using GLS.Platform.u202323562.Contexts.Assignments.Domain.Services;
using GLS.Platform.u202323562.Contexts.Assignments.Interfaces.REST.Resources;
using GLS.Platform.u202323562.Contexts.Assignments.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace GLS.Platform.u202323562.Contexts.Assignments.Interfaces.REST;

/// <summary>
///     REST controller for Device management
/// </summary>
[ApiController]
[Route("api/v1/devices")]
[Produces(MediaTypeNames.Application.Json)]
public class DevicesController(IDeviceQueryService deviceQueryService) : ControllerBase
{
    /// <summary>
    ///     Gets all devices
    /// </summary>
    /// <returns>List of all devices</returns>
    /// <response code="200">Returns the list of devices</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DeviceResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDevices()
    {
        var query = new GetAllDevices();
        var devices = await deviceQueryService.Handle(query);
        var resources = DeviceResourceFromEntityAssembler.ToResourceFromEntity(devices);
        return Ok(resources);
    }
}