using System;

namespace NotesConole.Abstract
{
    internal interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
