using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;

// Xabur
public class Class : Entity
{

    #region Properties

    public Title Title { get; protected set; }
    public DateRange ClassDateRange { get; protected set; }


    private readonly List<User> _teachers = [];
    private readonly List<User> _students = [];
    private readonly List<Subject> _subjects = [];

    public IReadOnlyCollection<User> Teachers => _teachers;
    public IReadOnlyCollection<User> Students => _students;
    public IReadOnlyCollection<Subject>? Subjects => _subjects;


    //TODO: AssignmentSheet

    #endregion


    #region Constructors

    protected Class()
    {
    }

    private Class(Title title, DateOnly startDate, DateOnly endDate, IEnumerable<Class> otherClasses)
    {
        var otherSchoolTitles = otherClasses.Select(s => s.Title.Value);

        Title = Title.Create(title, otherSchoolTitles);
        ClassDateRange = DateRange.Create(startDate, endDate);
    }

    private Class(Title title, DateOnly startDate, DateOnly endDate, IEnumerable<User> students,
        IEnumerable<Subject> subjects, IEnumerable<Class> otherClasses)
    {
        var otherSchoolTitles = otherClasses.Select(s => s.Title.Value);

        Title = Title.Create(title, otherSchoolTitles);
        ClassDateRange = DateRange.Create(startDate, endDate);
        _students = students.ToList();
        _subjects = subjects.ToList();
    }

    #endregion

    #region Class Methods

    public static Class Create(Title title, DateOnly startDate, DateOnly endDate, IEnumerable<Class> otherClasses)
    {
        return new Class(title, startDate, endDate, otherClasses);
    }

    public static Class Create(Title title, DateOnly startDate, DateOnly endDate, IEnumerable<User> students,
        IEnumerable<Subject> subjects, IEnumerable<Class> otherClasses)
    {
        return new Class(title, startDate, endDate, students, subjects, otherClasses);
    }

    #endregion

    #region Relational Methods

    public void AddSubject(Subject subject)
    {
        AssureNoDuplicateSubject(subject, _subjects);
        _subjects.Add(subject);
    }

    //TODO: Dette skal vi kigge på når user og rollerne er done
    public void AddStudent(User student)
    {
        // AssureCorrectRole("User", student);
        AssureNoDuplicateUser(student, _students);
        _students.Add(student);
    }

    //TODO: Dette skal vi kigge på når user og rollerne er done
    public void AddTeacher(User teacher)
    {
        // AssureCorrectRole("Teacher", teacher);
        AssureNoDuplicateUser(teacher, _teachers);
        _teachers.Add(teacher);
    }

    #endregion

    #region Relation Business Logic Methods
    protected void AssureNoDuplicateUser(User user, List<User> otherUsers)
    {
        if (otherUsers.Any(u => u.Id == user.Id))
            throw new ArgumentException(
                "This teacher/student has already been added to this Class.");
    }
    protected void AssureNoDuplicateSubject(Subject subjectToCreate, List<Subject> subjects)
    {
        if (subjects.Any(c => c.Id == subjectToCreate.Id))
            throw new ArgumentException("This subject has already been added to this class.");
    }
    //TODO: Dette skal vi kigge på når user og rollerne er done
    /* protected void AssureCorrectRole(string roleValueName, User user)
     {
         if (user.AccountClaims.All(c => c.ClaimValue != roleValueName))
             throw new ArgumentException("Brugeren har ikke den korrekte rolle");
     }*/

    #endregion
}
