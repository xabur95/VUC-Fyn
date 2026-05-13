using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Entities
{
    /// <summary>
    /// Author: Mikkel
    /// Represents a Subject such as: Danish, math, biology.
    /// </summary>
    public class Subject : Entity
    {
        //Fields
        private readonly List<Topic> _topics = [];

        //Properties
        public Title Title
        {
            get;
            protected set;
        } = null!;
        public Level Level
        {
            get;
            protected set;
        }

        public IReadOnlyCollection<Topic> Topics => _topics.AsReadOnly();


        //Constructors
        protected Subject() { } // for EF Core

        private Subject(string title, Level level)
        {
            Id = Guid.NewGuid();
            SetTitle(title);
            Level = level;
        }

        //Methods
        public static Subject Create(Title title, Level level)
        {
            return new Subject(title, level);
        }

        public void AddTopic(Topic topic)
        {
            AssureUniqueTopic(topic);
            _topics.Add(topic);
        }

        public void DeleteTopic(Topic topic)
        {
            _topics.Remove(topic);
        }

        private void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ErrorException("Subject name cannot be empty.", "INVALID_SUBJECT_NAME");

            Title = title;
        }

        private void AssureUniqueTopic(Topic newTopic)
        {
            if (_topics.Any(t =>
                t.Name.Equals(newTopic.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ErrorException("Topic already exists in this subject.");
            }
        }
    }
}
