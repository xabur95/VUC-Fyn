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
    public class Topic
    {
        //Fields
        private string name;

        //Properties
        public string Name 
        { 
            get;
        }

        //Constructor
        public Topic(string name) 
        {
            this.name = name;
        }
    }
}
