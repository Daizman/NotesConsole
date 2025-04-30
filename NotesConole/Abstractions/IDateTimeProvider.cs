using System;

namespace NotesConole.Abstractions
{
    internal interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
