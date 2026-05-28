using SportsApp.Domain.Entities;

namespace SportsApp.DAL.Repositories
{
    public interface IAthleteRepository
    {
        Task<List<Athlete>> GetAll();
        Task<Athlete> GetById(int id);
        Task Add(Athlete athlete);
        Task Update(Athlete athlete);
        Task Delete(int id);
    }
}