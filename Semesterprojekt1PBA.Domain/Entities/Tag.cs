using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Entities
{
    /// <summary>
    /// Tag entity used to classify <see cref="Question"/>s.
    /// </summary>
    public class Tag : Entity
    {
        public Title Title { get; private set; }
        public string Description { get; private set; }

        // EF Core binds to this private constructor via parameter-name matching.
        private Tag(Title title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description ?? string.Empty;
        }

        public static Tag Create(Title title, string description)
            => new(title, description ?? string.Empty);

        public void Rename(Title newTitle) => Title = newTitle;

        public void UpdateDescription(string description)
            => Description = description ?? string.Empty;
    }
}
