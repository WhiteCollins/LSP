public abstract class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }

    public abstract string GetDetails();
    public abstract void Subscribe(Student student);
}

public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
}

public interface ICourseSubscription
{
    void Subscribe(Student student, Course course);
}

public class OnlineCourseSubscription : ICourseSubscription
{
    public void Subscribe(Student student, Course course)
    {
        Console.WriteLine($"Suscribiendo al estudiante {student.Name} al curso en línea {course.Title}");
    }
}

public class OfflineCourseSubscription : ICourseSubscription
{
    public void Subscribe(Student student, Course course)
    {
        Console.WriteLine($"Suscribiendo al estudiante {student.Name} al curso presencial {course.Title}");
    }
}

public class OnlineCourse : Course
{
    public override string GetDetails()
    {
        return $"Curso en línea: {Title}";
    }

    public override void Subscribe(Student student)
    {
        var strategy = new OnlineCourseSubscription();
        strategy.Subscribe(student, this);
    }
}

public class OnsiteCourse : Course
{
    public override string GetDetails()
    {
        return $"Curso presencial: {Title}";
    }

    public override void Subscribe(Student student)
    {
        var strategy = new OfflineCourseSubscription();
        strategy.Subscribe(student, this);
    }
}

public class HybridCourse : Course
{
    private ICourseSubscription onlineSubscription;
    private ICourseSubscription offlineSubscription;

    public HybridCourse(ICourseSubscription onlineSubscription, ICourseSubscription offlineSubscription)
    {
        this.onlineSubscription = onlineSubscription;
        this.offlineSubscription = offlineSubscription;
    }

    public override void Subscribe(Student student)
    {
        onlineSubscription.Subscribe(student, this);
        offlineSubscription.Subscribe(student, this);
    }

    public override string GetDetails()
    {
        return $"Curso híbrido: {Title}";
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Student student = new Student { StudentId = 1, Name = "Eric Jimenez" };

        ICourseSubscription onlineSubscription = new OnlineCourseSubscription();
        ICourseSubscription offlineSubscription = new OfflineCourseSubscription();

        Course onlineCourse = new OnlineCourse { CourseId = 101, Title = "Curso de Java basico en linea" };
        Course onsiteCourse = new OnsiteCourse { CourseId = 102, Title = "Curso presencial de Java avanzado" };
        Course hybridCourse = new HybridCourse(onlineSubscription, offlineSubscription) { CourseId = 103, Title = "Curso de Pyton" };

        Console.WriteLine(onlineCourse.GetDetails());
        Console.WriteLine(onsiteCourse.GetDetails());
        Console.WriteLine(hybridCourse.GetDetails());

        onlineCourse.Subscribe(student);
        onsiteCourse.Subscribe(student);
        hybridCourse.Subscribe(student);
    }
}
