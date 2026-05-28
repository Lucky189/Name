using SportsApp.Domain.Entities;

public interface ITeamService
{
    Task<List<Team>> GetAll();
    Task<Team> GetById(int id);
    Task Create(Team team);
    Task Update(Team team);
    Task Delete(int id);
}