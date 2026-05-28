using SportsApp.Domain.Entities;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _repo;

    public TeamService(ITeamRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Team>> GetAll() => _repo.GetAll();
    public Task<Team> GetById(int id) => _repo.GetById(id);
    public Task Create(Team team) => _repo.Add(team);
    public Task Update(Team team) => _repo.Update(team);
    public Task Delete(int id) => _repo.Delete(id);
}
