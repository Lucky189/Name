using Microsoft.EntityFrameworkCore;
using SportsApp.Domain.Entities;

namespace SportsApp.DAL.Repositories
{
    public class AthleteRepository : IAthleteRepository
    {
        private readonly AppDbContext _context;

        public AthleteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Athlete>> GetAll()
        {
            return await _context.Athletes.ToListAsync();
        }

        public async Task<Athlete> GetById(int id)
        {
            return await _context.Athletes.FindAsync(id);
        }

        public async Task Add(Athlete athlete)
        {
            _context.Athletes.Add(athlete);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Athlete athlete)
        {
            _context.Athletes.Update(athlete);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var athlete = await _context.Athletes.FindAsync(id);
            if (athlete != null)
            {
                _context.Athletes.Remove(athlete);
                await _context.SaveChangesAsync();
            }
        }
    }
}