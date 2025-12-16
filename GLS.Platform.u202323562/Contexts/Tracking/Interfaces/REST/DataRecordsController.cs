using System.Net.Mime;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Exceptions;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Services;
using GLS.Platform.u202323562.Contexts.Tracking.Interfaces.REST.Resources;
using GLS.Platform.u202323562.Contexts.Tracking.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace GLS.Platform.u202323562.Contexts.Tracking.Interfaces.REST;

/// <summary>
///     REST controller for DataRecord management
/// </summary>
/// <remarks>
///     Provides endpoints for registering telemetry data from rocket engines.
///     Validates device existence through Anti-Corruption Layer before registration.
///     Implemented by Oliver Villogas Medina (u202323562)
/// </remarks>
[ApiController]
[Route("api/v1/data-records")]
[Produces(MediaTypeNames.Application.Json)]
public class DataRecordsController(IDataRecordCommandService dataRecordCommandService) : ControllerBase
{
    /// <summary>
    ///     Registers a new data record from a rocket engine device
    /// </summary>
    /// <param name="resource">Data record information</param>
    /// <returns>Confirmation of successful registration</returns>
    /// <response code="201">Data record created successfully</response>
    /// <response code="400">Invalid data (thrust out of range, invalid date, device not found)</response>
    /// <remarks>
    ///     Business Rules:
    ///     - Target thrust must be between 700.0 and 950.0 kN
    ///     - Device MAC address must exist in ASSIGNMENTS context (validated via ACL)
    ///     - Generated date cannot be in the future
    ///     - Emits DataRecordRegisteredEvent for ASSIGNMENTS context to update preferred thrust
    ///     Implemented by Oliver Villogas Medina (u202323562)
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDataRecord([FromBody] CreateDataRecordResource resource)
    {
        try
        {
            var command = CreateDataRecordCommandFromResourceAssembler.ToCommandFromResource(resource);
            var dataRecord = await dataRecordCommandService.Handle(command);

            return StatusCode(StatusCodes.Status201Created, new
            {
                id = dataRecord.Id,
                deviceMacAddress = dataRecord.DeviceMacAddress.Value,
                operationMode = dataRecord.OperationMode.ToString(),
                targetThrust = dataRecord.TargetThrust,
                currentThrust = dataRecord.CurrentThrust,
                engineState = dataRecord.EngineState.ToString(),
                generatedAt = dataRecord.GeneratedAt
            });
        }
        catch (InvalidDataRecordException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}