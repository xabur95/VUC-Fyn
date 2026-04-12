using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjects;
using System.Reflection.Metadata;

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
        public string Name
        {
            get;
            protected set;
        }
        public Level Level
        {
            get;
            protected set;
        }

        public IReadOnlyCollection<Topic> Topics => _topics.AsReadOnly();     
           

        //Constructors
        protected Subject () { } // for EF Core

        private Subject(string name, Level level, List<Topic> topics)
        {
            SetName(name);
            Level = level;
            _topics = topics;
        }

        //Methods
        public static Subject Create(string name, Level level)
        {
            return new Subject(name, level, []);
        }

        public static Subject Create(string name, Level level, List<Topic> topics)
        {
            return new Subject(name, level, topics);
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

        /* 
        //This should probably be in it's own service since it's useful for several entities.
        protected void AssureUserIsAuthorised(User user)
        {
            if (user is not Teacher and not Admin)
                throw new UnauthorizedAccessException("User most be either a teacher or an admin");
        }*/

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ErrorException("Subject name cannot be empty.", nameof(name));

            Name = name;
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
