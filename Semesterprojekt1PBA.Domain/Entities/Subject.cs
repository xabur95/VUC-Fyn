using System.Reflection.Metadata;

namespace Semesterprojekt1PBA.Domain.Entities
{
    /// <summary>
    /// Author: Mikkel
    /// Represents a Subject such as: Danish, math, biology.
    /// </summary>
    public class Subject
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
        private Subject(string name, Level level, List<Topic> topics)
        {
            Name = name;
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
            _topics.Add(topic);
        }

        protected void AssureTeacher(User teacher)
        {
            //Todo: Throw exception if not teacher
        }

        protected void AssureUnique(List<Subject> subjects, Subject subject)
        {
            //ToDo assure List is consiting of unique objects. else throw execption.
        }
    }
}
