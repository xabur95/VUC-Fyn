using Semesterprojekt1PBA.Domain.Helpers;
using System.Runtime.InteropServices;

namespace Semesterprojekt1PBA.Domain.Entities
{
    /// <summary>
    /// Author: Mikkel
    /// Represents a AssignmentSheet with Questions used curricular tests.
    /// </summary>
    public class AssignmentSheet:Entity
    {
        #region Properties
        public User Author 
        { 
            get; 
            protected set; 
        }
        public Subject Subject 
        { 
            get; 
            protected set; 
        }
        private readonly List<Topic> _topics = [];
        public IReadOnlyCollection<Topic> Topics => _topics;
        private readonly List<Question> _questions = [];
        public IReadOnlyCollection<Question> Questions => _questions;
        #endregion

        #region Constructor
        private AssignmentSheet() { } //For EF

        private AssignmentSheet(User author, Subject subject)
        {
            Author = author;
            Subject = subject;
        }
        #endregion

        #region Methods
        public static AssignmentSheet Create(User author, Subject subject)
        {
            return new AssignmentSheet(author, subject);
        }

        public void AddTopic(Topic topic)
        {
            if (Topics.Any(t => t.Id == topic.Id))
                throw new ErrorException($"The topic {topic.Name} is already part of the sheet", errorCode: "TOPIC_ALREADY_ASSIGNED");

            _topics.Add(topic);
        }

        public void RemoveTopic(Topic topic)
        {
            if (!Topics.Any(t => t.Id == topic.Id))
                throw new ErrorException($"The topic {topic.Name} is not part of the sheet", errorCode: "TOPIC_NOT_ASSIGNED");
            _topics.Remove(topic);
        }

        public void AddQuestion(Question question)
        {
            if (Questions.Any(q => q.Id == question.Id))
                throw new ErrorException($"The question is already part of the sheet", errorCode: "QUESTION_ALREADY_USED");

            _questions.Add(question);
        }

        public void RemoveQuestion(Question question)
        {
            if (!Questions.Any(q => q.Id == question.Id))
                throw new ErrorException($"The question is not part of the sheet", errorCode: "QUESTION_NOT_USED");
            _questions.Remove(question);
        }

        #endregion
    }
}
