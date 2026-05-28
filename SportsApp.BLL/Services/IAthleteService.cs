using SportsApp.Domain.Entities;

namespace SportsApp.BLL.Services
{
    public interface IAthleteService
    {
        Task<List<Athlete>> GetAll();
        Task<Athlete> GetById(int id);
        Task Create(Athlete athlete);
        Task Update(Athlete athlete);
        Task Delete(int id);
    }
}