using Bogus;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IUserGeneratorService, BogusUserGeneratorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();





public record AddressDto(string Street, string City, string State, string Zip, string Country);

public record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string FullName,
    string Email,
    string Phone,
    DateTime DateOfBirth,
    string Gender,
    AddressDto Address
);

public interface IUserGeneratorService
{
    UserDto Generate();
}



public class BogusUserGeneratorService : IUserGeneratorService
{
    private readonly Faker _faker = new();

    public UserDto Generate()
    {
        var gender = _faker.PickRandom<Bogus.DataSets.Name.Gender>();
        var first = _faker.Name.FirstName(gender);
        var last = _faker.Name.LastName(gender);
        var dob = _faker.Date.PastOffset(60, DateTime.UtcNow.AddYears(-18)).Date;

        var address = new AddressDto(
            Street: _faker.Address.StreetAddress(),
            City: _faker.Address.City(),
            State: _faker.Address.State(),
            Zip: _faker.Address.ZipCode(),
            Country: _faker.Address.Country()
        );

        return new UserDto(
            Id: Guid.NewGuid(),
            FirstName: first,
            LastName: last,
            FullName: $"{first} {last}",
            Email: _faker.Internet.Email(first, last),
            Phone: _faker.Phone.PhoneNumber(),
            DateOfBirth: dob,
            Gender: gender.ToString(),
            Address: address
        );
    }
}



[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserGeneratorService _generator;

    public UsersController(IUserGeneratorService generator)
    {
        _generator = generator;
    }

    [HttpGet("random")]
    public ActionResult<UserDto> GetRandomUser()
    {
        var user = _generator.Generate();
        return Ok(user);
    }
}
