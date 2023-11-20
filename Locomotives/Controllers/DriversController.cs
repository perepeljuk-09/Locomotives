using Locomotives.API.Models.Dto.Driver;
using Locomotives.API.Services.Contracts;
using Locomotives.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Locomotives.API.Controllers
{
    [Route("api/drivers")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly ILogger<DriversController> _logger;
        private readonly IDriversService _service;
        private List<string> _errors;

        public DriversController(ILogger<DriversController> logger, IDriversService service)
        {
            _logger = logger;
            _service = service;
            _errors = new();
        }
        // GET: api/<DriversController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverDto>>> Get()
        {
            List<DriverDto> drivers = await _service.GetAllAsync();
            return drivers;
        }

        // GET api/<DriversController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverDto>> Get(int id)
        {
            DriverDto? driver = await _service.GetByIdAsync(id);

            if (driver == null)
                return NoContent();

            return driver;
        }

        // POST api/<DriversController>
        [HttpPost]
        public async Task<ActionResult<DriverDto>> Post([FromBody] DriverCreateDto dto)
        {
            DriverDto? driver = await _service.CreateAsync(dto, _errors);

            if (_errors.Count > 0)
            {
                string? error = null;
                _errors.ForEach(e => error += (e + "; "));
                return BadRequest(error);
            }

            return Ok(driver);
        }

        // PUT api/<DriversController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DriverDto>> Put(int id, [FromBody] DriverUpdateDto dto)
        {
            DriverDto? driver = await _service.UpdateAsync(id, dto, _errors);

            if (_errors.Count > 0)
            {
                string? error = null;
                _errors.ForEach(e => error += (e + "; "));
                return BadRequest(error);
            }

            if (driver == null)
                return BadRequest();

            return Ok(driver);
        }

        // DELETE api/<DriversController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            bool isDeleted = await _service.DeleteAsync(id);

            if (!isDeleted)
                return NoContent();

            return isDeleted;
        }
    }
}
