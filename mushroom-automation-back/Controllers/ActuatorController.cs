using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

[Route("api/actuators")]
[ApiController]
public class ActuatorController : ControllerBase
{
    private readonly IActuatorService _service;
    private readonly ILogger<ActuatorController> _logger;

    public ActuatorController(IActuatorService service, ILogger<ActuatorController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Actuator>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var actuators = await _service.GetAllAsync(page, pageSize);
            return Ok(actuators);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting actuators.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Actuator>> GetById(int id)
    {
        try
        {
            var actuator = await _service.GetByIdAsync(id);
            return actuator != null ? Ok(actuator) : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting actuator by id.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody, Required] ActuatorDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto }, dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding a new actuator.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody, Required] ActuatorDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating the actuator.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting the actuator.");
            return StatusCode(500, "Internal server error");
        }
    }
}
