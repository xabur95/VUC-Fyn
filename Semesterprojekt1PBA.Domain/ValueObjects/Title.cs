using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Domain.ValueObjects
{
    public record Title
    {
        private Title(string value)
        {
            Validate(value);
            Value = value;
        }

        public string Value { get; init; }


        public static Title Create(string title, IEnumerable<string> otherTitles)
        {
            var newTitle = new Title(title);
            AssureIsUniqueTitle(otherTitles, title);
            return newTitle;
        }

        private static void AssureIsUniqueTitle(IEnumerable<string> otherTitles, string title)
        {
            if (otherTitles.Any(otherTitle => otherTitle == title))
                throw new ErrorException($"The title '{title}' already exists.");
        }


        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ErrorException("The title can't be null or whitespace.", nameof(value));

            if (value.Length > 50)
                throw new ErrorException("The title has a max length of 50 characters", nameof(value));
        }

        public static implicit operator string(Title value)
        {
            return value.Value;
        }

        public static implicit operator Title(string value)=> new(value);
    }
}
