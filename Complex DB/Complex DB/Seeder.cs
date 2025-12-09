public static class Seeder
{
    public static void Seed(UniversityContext context)
    {
        if (context.Students.Any()) return;

        var teachers = new List<Teacher>();
        for (int i = 1; i <= 5; i++)
            teachers.Add(new Teacher { Name = $"Teacher {i}" });
        context.Teachers.AddRange(teachers);
        context.SaveChanges();

        var courses = new List<Course>();
        for (int i = 1; i <= 10; i++)
            courses.Add(new Course { Title = $"Course {i}", TeacherId = teachers[i % 5].Id });
        context.Courses.AddRange(courses);
        context.SaveChanges();

        var students = new List<Student>();
        var profiles = new List<StudentProfile>();
        var studentCourses = new List<StudentCourse>();

        for (int i = 1; i <= 15; i++)
        {
            var student = new Student { Name = $"Student {i}" };
            students.Add(student);

            profiles.Add(new StudentProfile
            {
                Student = student,
                Email = $"student{i}@university.com",
                Address = $"Address {i}"
            });

            // Randomly enroll student to courses
            var rnd = new Random();
            var enrolledCourses = courses.OrderBy(x => rnd.Next()).Take(3).ToList();
            foreach (var course in enrolledCourses)
                studentCourses.Add(new StudentCourse { Student = student, Course = course });
        }

        context.Students.AddRange(students);
        context.Profiles.AddRange(profiles);
        context.StudentCourses.AddRange(studentCourses);

        context.SaveChanges();
    }
}