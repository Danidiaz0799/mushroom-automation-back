using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MushroomAutomation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DhtSensorController : ControllerBase
    {
        private readonly IDhtSensorService _service;
        private readonly ILogger<DhtSensorController> _logger;

        public DhtSensorController(IDhtSensorService service, ILogger<DhtSensorController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DhtSensor>>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var sensors = await _service.GetAllAsync(page, pageSize);
                return Ok(sensors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting sensors.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DhtSensor>> Get(int id)
        {
            try
            {
                var sensor = await _service.GetByIdAsync(id);
                return sensor != null ? Ok(sensor) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting sensor by id.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody, Required] DhtSensorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.AddAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = dto }, dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new sensor.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody, Required] DhtSensorDto dto)
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
                _logger.LogError(ex, "Error occurred while updating the sensor.");
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
                _logger.LogError(ex, "Error occurred while deleting the sensor.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
