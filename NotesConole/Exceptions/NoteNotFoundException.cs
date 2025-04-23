using System;

namespace NotesConole.Exceptions
{
    internal class NoteNotFoundException : Exception
    {
        public NoteNotFoundException(int id) : base($"Note with id = {id} is not found.") { }
    }
}
