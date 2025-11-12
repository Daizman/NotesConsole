using System.Collections.Generic;
using NotesConole.Models;

namespace NotesConole.Abstractions
{
    internal interface INoteRepository
    {
        IReadOnlyList<Note> GetNotes();
        void AddNote(string title, string description);
        void EditNote(int id, string title, string description);
        void CompleteNote(int id);
        void RemoveNote(int id);
    }
}
