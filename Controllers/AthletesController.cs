using Microsoft.AspNetCore.Mvc;
using SportsApp.BLL.Services;
using SportsApp.Domain.Entities;

namespace SportsApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AthletesController : ControllerBase
    {
        private readonly IAthleteService _service;

        public AthletesController(IAthleteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Athlete athlete)
        {
            await _service.Create(athlete);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Athlete athlete)
        {
            await _service.Update(athlete);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}