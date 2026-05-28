using Microsoft.AspNetCore.Mvc;
using SportsApp.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _service;

    public TeamsController(ITeamService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
        => Ok(await _service.GetById(id));

    [HttpPost]
    public async Task<IActionResult> Post(Team team)
    {
        await _service.Create(team);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put(Team team)
    {
        await _service.Update(team);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return Ok();
    }
}