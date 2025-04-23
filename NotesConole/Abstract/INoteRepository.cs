using System.Collections.Generic;
using NotesConole.Models;

namespace NotesConole.Abstract
{
    internal interface INoteRepository
    {
        IEnumerable<Note> GetNotes();
        void AddNote(string title, string description, User user);
        void EditNote(int id, string title, string description);
        void CompleteNote(int id);
        void RemoveNote(int id);
    }
}
