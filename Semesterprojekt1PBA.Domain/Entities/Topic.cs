using Semesterprojekt1PBA.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

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

        //Constructor
        protected Topic() { } // For EF Core

        private Topic(string name) 
        {
            SetName(name);
        }

        public static Topic Create(string name)
        {
            return new Topic(name);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ErrorException("Topic name cannot be empty.", nameof(name));

            Name = name;
        }
    }
}
