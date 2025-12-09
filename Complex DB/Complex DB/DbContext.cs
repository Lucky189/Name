public class UniversityContext : DbContext
using Microsoft.EntityFrameworkCore;
{
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentProfile> Profiles { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }

    public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // One-to-One
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Profile)
            .WithOne(p => p.Student)
            .HasForeignKey<StudentProfile>(p => p.StudentId);

        // Many-to-Many
        modelBuilder.Entity<StudentCourse>()
            .HasKey(sc => new { sc.StudentId, sc.CourseId });

        modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Student)
            .WithMany(s => s.StudentCourses)
            .HasForeignKey(sc => sc.StudentId);

        modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Course)
            .WithMany(c => c.StudentCourses)
            .HasForeignKey(sc => sc.CourseId);
    }
}