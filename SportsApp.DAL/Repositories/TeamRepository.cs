using Microsoft.EntityFrameworkCore;
using SportsApp.DAL;
using SportsApp.Domain.Entities;

public class TeamRepository : ITeamRepository
{
    private readonly AppDbContext _context;

    public TeamRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Team>> GetAll()
        => await _context.Teams.ToListAsync();

    public async Task<Team> GetById(int id)
        => await _context.Teams.FindAsync(id);

    public async Task Add(Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Team team)
    {
        _context.Teams.Update(team);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var item = await _context.Teams.FindAsync(id);
        if (item != null)
        {
            _context.Teams.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}