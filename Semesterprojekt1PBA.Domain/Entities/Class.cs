using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Entities;

// Xabur
public class Class : Entity
{

    #region Properties

    public Title Title { get; private set; } = null!;
    public DateRange ClassDateRange { get; private set; } = null!;


    private readonly List<Teacher> _teachers = [];
    private readonly List<Student> _students = [];
    private readonly List<Subject> _subjects = [];

    public IReadOnlyCollection<Teacher> Teachers => _teachers;
    public IReadOnlyCollection<Student> Students => _students;
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

    private Class(Title title, DateOnly startDate, DateOnly endDate, IEnumerable<Student> students,
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

    public static Class Create(Title title, DateOnly startDate, DateOnly endDate, IEnumerable<Student> students,
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

    
    public void AddStudent(Student student)
    {
        AssureNoDuplicateUser(student, _students);
        _students.Add(student);
    }

    public void AddTeacher(Teacher teacher)
    {
        AssureNoDuplicateUser(teacher, _teachers);
        _teachers.Add(teacher);
    }

    #endregion

    #region Relation Business Logic Methods
    protected void AssureNoDuplicateUser<T>(T user, IEnumerable<T> otherUsers) where T : User
    {
        if (otherUsers.Any(u => u.Id == user.Id))
            throw new ErrorException(
                "This teacher/student has already been added to this Class.");
    }

    protected void AssureNoDuplicateSubject(Subject subjectToCreate, IEnumerable<Subject> subjects)
    {
        if (subjects.Any(c => c.Name == subjectToCreate.Name))
            throw new ErrorException("This subject has already been added to this class.");
    }

    #endregion
}
