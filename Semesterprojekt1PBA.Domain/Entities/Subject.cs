using Semesterprojekt1PBA.Domain.Helpers;


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
        //Fields
        private readonly List<Topic> _topics = [];

        //Properties
        public string Name
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


        private Subject(string name, Level level)
        {
            Id = Guid.NewGuid();
            SetName(name);
            Level = level;
        }

        //Methods
        public static Subject Create(string name, Level level)
        {
            return new Subject(name, level);
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

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ErrorException("Subject name cannot be empty.", "INVALID_SUBJECT_NAME");

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
