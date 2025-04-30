using System;
using NotesConole.Abstractions;

namespace NotesConole.Services
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
