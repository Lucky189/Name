using Microsoft.EntityFrameworkCore;
using SportsApp.Domain.Entities;

namespace SportsApp.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Athlete> Athletes { get; set; }
    public DbSet<Team> Teams { get; set; }
}
