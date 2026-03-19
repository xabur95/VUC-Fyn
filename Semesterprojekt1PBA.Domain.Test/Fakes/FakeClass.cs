using Semesterprojekt1PBA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Semesterprojekt1PBA.Domain.Test.Fakes
{
    public class FakeClass : Entities.Class
    {
        public void SetClassDateRange(DateOnly startDate, DateOnly endDate)
        {
            ClassDateRange = DateRange.Create(startDate, endDate);
        }

        public void SetClassTitle(Title title, IEnumerable<string> otherClassesNames)
        {
            Title = Title.Create(title, otherClassesNames);
        }

        public new void AssureNoDuplicateSubject(Entities.Subject subjectToCheck, List<Entities.Subject> currentSubjects)
        {
            base.AssureNoDuplicateSubject(subjectToCheck, currentSubjects);
        }

        public new void AssureNoDuplicateUser(Entities.User userToCheck, List<Entities.User> currentUsers)
        {
            base.AssureNoDuplicateUser(userToCheck, currentUsers);
        }

        #region Constructors
        public FakeClass()
        {
        }

        public FakeClass(Guid id, Title title)
        {
            Id = id;
            Title = title;
        }
        #endregion
    }
}
