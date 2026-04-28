using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Entities
{
    /// <summary>
    /// Author: Xabur
    /// Represents a School 
    /// </summary>
    public class School : Entity
    {
        #region Properties

        public Title Title { get; protected set; }

        private readonly List<User> _schoolResponsibles = [];
        private readonly List<Class> _classes = [];

        public IReadOnlyCollection<User> SchoolResponsibles => _schoolResponsibles;
        public IReadOnlyCollection<Class> Classes => _classes;

        #endregion


        #region Constructors

        protected School()
        {
        }

        private School(Title title,  IEnumerable<School> otherSchools)
        {
            var otherSchoolTitles = otherSchools.Select(s => s.Title.Value);

            Title = Title.Create(title, otherSchoolTitles);
        }

        private School(Title title, IEnumerable<Class> classes, IEnumerable<School> otherSchools)
        {
            var otherSchoolTitles = otherSchools.Select(s => s.Title.Value);

            Title = Title.Create(title, otherSchoolTitles);
            _classes = classes.ToList();
        }

        #endregion


        #region School Methods

        public static School Create(Title title, IEnumerable<School> otherSchools)
        {
            return new School(title, otherSchools);
        }

        public static School Create(Title title, IEnumerable<Class> classes, IEnumerable<School> otherSchools)
        {
            return new School(title, classes, otherSchools);
        }

       public void UpdateTitle(Title title, IEnumerable<School> otherSchools)
        {
            var otherSchoolTitles = otherSchools.Select(s => s.Title.Value);
            Title = Title.Create(title, otherSchoolTitles);
        }
        #endregion

        #region Relational Methods

        // TODO: Dette skal vi kigge på når user og rollerne er done

        /*        public void AddResponsible(User Admin)
                {
                    AssureNoDuplicateUser(Admin, _schoolResponsibles);
                    AssureCorrectRole("Teacher", Admin);

                    _schoolResponsibles.Add(Admin);
                }*/

        public Class AddClass(Title title,
            DateOnly startDate,
            DateOnly endDate,
            IEnumerable<Class> otherClasses)
        {
            var classToCreate = Class.Create(title, startDate, endDate, otherClasses);

            AssureNoDuplicateClass(classToCreate, _classes);

            _classes.Add(classToCreate);

            return classToCreate;
        }
       

        #endregion


        #region Relational Business Logic Methods

        protected void AssureNoDuplicateUser(User admin, List<User> schoolResponsibles)
        {
            if (schoolResponsibles.Any(u => u.Id == admin.Id))
                throw new ErrorException(
                    "This admin has already been added to this School as one of its responsibles.");
        }

        protected void AssureNoDuplicateClass(Class classToCreate, List<Class> classes)
        {
            if (classes.Any(c => c.Id == classToCreate.Id))
                throw new ErrorException("This class has already been added to this School.");
        }

        // TODO: Dette skal vi kigge på når user og rollerne er done
    /*    protected void AssureCorrectRole(string roleValueName, User user)
        {
            if (user.AccountClaims.All(c => c.ClaimValue != roleValueName))
                throw new ArgumentException("The User doen't have the correct Role");
        }*/

        #endregion
    }
}
