using Microsoft.EntityFrameworkCore;
using DbOperationsApp.Models;

namespace DbOperationsApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<Person> People => Set<Person>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Host=localhost;Port=5432;Database=mydb;Username=myuser;Password=mypassword");
    }
}