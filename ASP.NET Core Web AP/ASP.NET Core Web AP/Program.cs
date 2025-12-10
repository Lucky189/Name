using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();            
builder.Services.AddEndpointsApiExplorer();  
builder.Services.AddSwaggerGen();            

builder.Services.AddSingleton<StudentRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();     
    app.UseSwaggerUI();   
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();     

app.Run();


// Модель студента
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

// Репозиторій (in-memory)
public class StudentRepository
{
    private readonly List<Student> _students = new();
    private int _nextId = 1;

    public List<Student> GetAll() => _students;

    public Student Add(Student s)
    {
        s.Id = _nextId++;
        _students.Add(s);
        return s;
    }

    public bool Delete(int id)
    {
        var s = _students.FirstOrDefault(x => x.Id == id);
        if (s == null) return false;
        _students.Remove(s);
        return true;
    }
}


// Контролер студентів
[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly StudentRepository _repo;

    public StudentsController(StudentRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<List<Student>> GetAll() => _repo.GetAll();

    [HttpPost]
    public ActionResult<Student> Add(Student s) => _repo.Add(s);

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_repo.Delete(id)) return NotFound();
        return NoContent();
    }
}
