using System;

namespace NotesConole.Models
{
    internal class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }

        public User User { get; set; }

        public override string ToString()
        {
            var completedStatus = IsCompleted ? 'v' : 'x';
            return $"[{completedStatus}] {Id}. {Title} ({CreatedDate.ToLocalTime()})\n\tDescription: {Description}";
        }
    }
}
