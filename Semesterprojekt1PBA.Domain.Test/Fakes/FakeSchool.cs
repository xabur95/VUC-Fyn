using Semesterprojekt1PBA.Domain.ValueObjects;
using System.Xml.Linq;

namespace Semesterprojekt1PBA.Domain.Test.Fakes
{
    public class FakeSchool : Entities.School
    {        
        public void SetScoolTitle(Title title, IEnumerable<string> otherSchoolsNames)
        {
            Title = Title.Create(title, otherSchoolsNames);
        }


        public new void AssureNoDuplicateUser(Entities.User teacher, List<Entities.User> semesterResponsibles)
        {
            base.AssureNoDuplicateUser(teacher, semesterResponsibles);
        }

        public new void AssureNoDuplicateClass(Entities.Class classToCheck, List<Entities.Class> currentClasses)
        {
            base.AssureNoDuplicateClass(classToCheck, currentClasses);
        }

        #region Constructors

        public FakeSchool()
        {
        }

        #endregion
    }
}
