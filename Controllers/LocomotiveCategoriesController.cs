using Locomotives.API.Models.Dto.LocomotiveCategories;
using Locomotives.API.Services.Contracts;
using Locomotives.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Locomotives.API.Controllers
{
    [Route("api/locomotivecategories")]
    [ApiController]
    public class LocomotiveCategoriesController : ControllerBase
    {
        private readonly ILocomotiveCategoriesService _service;
        private readonly ILogger<LocomotiveCategoriesController> _logger;
        public LocomotiveCategoriesController(ILogger<LocomotiveCategoriesController> logger, ILocomotiveCategoriesService service)
        {
            _logger = logger;
            _service = service;
        }
        // GET: api/<LocomotiveCategoriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocomotiveCategoriesDto>>> Get()
        {
            List<LocomotiveCategoriesDto> categories = await _service.GetAllAsync();
            return categories;
        }

        // GET api/<LocomotiveCategoriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocomotiveCategoriesDto>> Get(int id)
        {
            LocomotiveCategoriesDto? category = await _service.GetByIdAsync(id);

            if (category == null)
                return BadRequest("Id without range");

            return category;
        }

        // POST api/<LocomotiveCategoriesController>
        [HttpPost]
        public async Task<ActionResult<LocomotiveCategoriesDto>> Post([FromBody] LocomotiveCategoriesCreateDto dto)
        {
            LocomotiveCategoriesDto category = await _service.CreateAsync(dto);

            return category;
        }

        // PUT api/<LocomotiveCategoriesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<LocomotiveCategoriesDto>> Put(int id, [FromBody] LocomotiveCategoriesUpdateDto dto)
        {
            LocomotiveCategoriesDto? category = await _service.UpdateAsync(id, dto);

            if (category == null)
                return BadRequest("Id without range");

            return category;
        }

        // DELETE api/<LocomotiveCategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            bool isDeleted = await _service.DeleteAsync(id);

            if (!isDeleted)
                return BadRequest("Id without range");

            return isDeleted;
        }
    }
}
