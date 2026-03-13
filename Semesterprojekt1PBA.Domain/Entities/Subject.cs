namespace Semesterprojekt1PBA.Domain.Entities
{
    /// <summary>
    /// Author: Mikkel
    /// Represents a Subject such as: Danish, math, biology.
    /// </summary>
    public class Subject
    {
        //Fields
        private string name;
        private Level level;
        private List<Topic> topics;
        
        //Properties
        public string Name
        {
            get;
            private set;
        }
        public Level Level
        {
            get;
            private set;
        }
        public List<Topic> Topics 
        {
            get;
            private set;
        }   

        //Constructors
        public Subject(string name, Level level)
        {
            Name = name;
            Level = level;
            Topics = [];
        }

        public Subject(string name, Level level, List<Topic> topics)
        {
            Name = name;
            Level = level;
            Topics = topics;
        }

        //Methods
        public void AddTopic(Topic topic)
        {
            Topics.Add(topic);
        }
    }
}
