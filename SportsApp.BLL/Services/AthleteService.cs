using SportsApp.DAL.Repositories;
using SportsApp.Domain.Entities;

namespace SportsApp.BLL.Services
{
    public class AthleteService : IAthleteService
    {
        private readonly IAthleteRepository _repo;

        public AthleteService(IAthleteRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Athlete>> GetAll() => _repo.GetAll();

        public Task<Athlete> GetById(int id) => _repo.GetById(id);

        public Task Create(Athlete athlete) => _repo.Add(athlete);

        public Task Update(Athlete athlete) => _repo.Update(athlete);

        public Task Delete(int id) => _repo.Delete(id);
    }
}