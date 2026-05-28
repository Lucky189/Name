using Microsoft.EntityFrameworkCore;
using SportsApp.DAL;
using SportsApp.DAL.Repositories;
using SportsApp.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔷 Controllers
builder.Services.AddControllers();

// 🔷 DbContext (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=sports.db"));

// 🔷 SERVICES (BLL)
builder.Services.AddScoped<IAthleteService, AthleteService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IAthleteService, AthleteService>();
builder.Services.AddScoped<ITeamService, TeamService>();

builder.Services.AddScoped<IAthleteRepository, AthleteRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
// 🔷 REPOSITORIES (DAL)
builder.Services.AddScoped<IAthleteRepository, AthleteRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

// 🔷 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔷 Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

// 🔷 Controllers mapping
app.MapControllers();

app.Run();