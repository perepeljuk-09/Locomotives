using Locomotives.API.Models.Dto.Depot;
using Locomotives.API.Services.Contracts;
using Locomotives.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Locomotives.API.Controllers
{
    [Route("api/depots")]
    [ApiController]
    public class DepotsController : ControllerBase
    {
        private readonly ILogger<DepotsController> _logger;
        private readonly IDepotsService _service;
        private readonly IHttpContextAccessor _contextAccessor;
        public DepotsController(ILogger<DepotsController> logger, IDepotsService service, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _service = service;
            _contextAccessor = contextAccessor;
        }
        // GET: api/<DepotsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepotDto>>> Get()
        {
            throw new Exception("Don't found depots");
            List<DepotDto> depots = await _service.GetAllAsync();
            return Ok(depots);
        }

        // GET api/<DepotsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepotDto>> Get(int id)
        {
            DepotDto? depot = await _service.GetByIdAsync(id);

            if (depot == null)
                return BadRequest("Id without range");

            return Ok(depot);
        }

        // POST api/<DepotsController>
        [HttpPost]
        public async Task<ActionResult<DepotDto>> Post([FromBody] DepotCreateDto dto)
        {
            DepotDto depot = await _service.CreateAsync(dto);

            return Ok(depot);
        }

        // PUT api/<DepotsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DepotDto>> Put(int id, [FromBody] DepotUpdateDto dto)
        {
            DepotDto? depot = await _service.UpdateAsync(id, dto);

            if (depot == null)
                return BadRequest("Id without range");

            return Ok(depot);
        }

        // DELETE api/<DepotsController>/5
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
