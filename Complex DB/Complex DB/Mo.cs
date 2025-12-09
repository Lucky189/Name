public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }

    // One-to-One
    public StudentProfile Profile { get; set; }

    // Many-to-Many
    public List<StudentCourse> StudentCourses { get; set; } = new();
}

public class StudentProfile
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

    // One-to-One
    public int StudentId { get; set; }
    public Student Student { get; set; }
}

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }

    // Many-to-One (Teacher)
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }

    // Many-to-Many
    public List<StudentCourse> StudentCourses { get; set; } = new();
}

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }

    // One-to-Many (Course)
    public List<Course> Courses { get; set; } = new();
}

// Many-to-Many join table
public class StudentCourse
{
    public int StudentId { get; set; }
    public Student Student { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }
}