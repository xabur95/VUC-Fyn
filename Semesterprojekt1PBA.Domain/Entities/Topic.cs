using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Domain.Entities
{
    /// <summary>
    /// Author: Mikkel
    /// Represents a topic for a school subject. Such algebra and functions for math, or cells and evolution for biologic.
    /// primarily used with Class Subject.
    /// </summary>
    public class Topic : Entity
    {
        //Properties
        public string Name
        {
            get;
            private set;
        }
         = null!;

        //Constructor
        protected Topic() { } // For EF Core

        private Topic(string name) 
        {
            Id = Guid.NewGuid();
            SetName(name);
        }

        public static Topic Create(string name)
        {
            return new Topic(name);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ErrorException("Topic name cannot be empty.", "INVALID_TOPIC_NAME");

            Name = name;
        }
    }
}
