namespace Semesterprojekt1PBA.Domain.Entities
{
    /// <summary>
    /// 
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
        }
        public string Level
        {
            get;
        }
        public List<Topic> Topics 
        {
            get;
            set;
        }   

        //Constructors
        public Subject(string name, Level level, List<Topic> topics)
        {
            this.name = name;
            this.level = level;
            this.topics = topics;
        }
    }
}
