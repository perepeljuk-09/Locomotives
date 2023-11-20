using Locomotives.API.Models.Dto.Locomotive;
using Locomotives.API.Services.Contracts;
using Locomotives.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Locomotives.API.Controllers
{
    [Route("api/locomotives")]
    [ApiController]
    public class LocomotivesController : ControllerBase
    {
        private readonly ILogger<LocomotivesController> _logger;
        private readonly ILocomotivesService _service;
        private List<string> _errors;

        public LocomotivesController(ILogger<LocomotivesController> logger, ILocomotivesService service)
        {
            _logger = logger;
            _service = service;
            _errors = new();
        }
        // GET: api/<LocomotivesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocomotiveDto>>> Get()
        {
            List<LocomotiveDto> locomotives = await _service.GetAllAsync();
            return locomotives;
        }

        // GET api/<LocomotivesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocomotiveDto>> Get(int id)
        {
            LocomotiveDto? locomotive = await _service.GetByIdAsync(id);

            if (locomotive == null)
                return BadRequest("Id without range");

            return Ok(locomotive);
        }

        // POST api/<LocomotivesController>
        [HttpPost]
        public async Task<ActionResult<LocomotiveDto>> Post([FromBody] LocomotiveCreateDto dto)
        {
            LocomotiveDto? locomotive = await _service.CreateAsync(dto, _errors);

            if (_errors.Count > 0)
            {
                string? error = null;
                _errors.ForEach(e => error += (e + "; "));
                return BadRequest(error);
            }

            return Ok(locomotive);
        }

        // PUT api/<LocomotivesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<LocomotiveDto>> Put(int id, [FromBody] LocomotiveUpdateDto dto)
        {
            LocomotiveDto? locomotive = await _service.UpdateAsync(id, dto, _errors);

            if (_errors.Count > 0)
            {
                string? error = null;
                _errors.ForEach(e => error += (e + "; "));
                return BadRequest(error);
            }

            if (locomotive == null)
                return BadRequest("Id without range");

            return Ok(locomotive);
        }

        // DELETE api/<LocomotivesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            bool isDeleted = await _service.DeleteAsync(id);

            if (!isDeleted)
                return BadRequest("Id without range");

            return Ok(isDeleted);
        }
    }
}
