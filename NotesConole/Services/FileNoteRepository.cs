using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using NotesConole.Abstract;
using NotesConole.Exceptions;
using NotesConole.Models;

namespace NotesConole.Services
{
    internal class FileNoteRepository : INoteRepository
    {
        private const string _saveFile = "notes.json";

        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly List<Note> _notes;

        public FileNoteRepository(IDateTimeProvider dateTimeProvider)
        {
            InitSaveFileIfNotExists();

            _dateTimeProvider = dateTimeProvider;

            var notesFromFile = File.ReadAllText(_saveFile);
            _notes = JsonSerializer.Deserialize<List<Note>>(notesFromFile);
        }

        public IEnumerable<Note> GetNotes() => _notes;

        public void AddNote(string title, string description, User user)
            => ExecuteWithSave(() =>
            {
                var note = new Note
                {
                    Id = _notes.Count + 1,
                    Title = title,
                    Description = description,
                    CreatedDate = _dateTimeProvider.UtcNow,
                    User = user,
                    IsCompleted = false,
                };
                _notes.Add(note);
            });

        public void EditNote(int id, string title, string description)
            => ExecuteWithSave(() =>
            {
                var note = GetNoteAndThrowIfNotFound(id);
                note.Title = title;
                note.Description = description;
            });

        public void CompleteNote(int id)
            => ExecuteWithSave(() =>
            {
                var note = GetNoteAndThrowIfNotFound(id);
                note.IsCompleted = true;
            });

        public void RemoveNote(int id)
            => ExecuteWithSave(() =>
            {
                var note = GetNoteAndThrowIfNotFound(id);
                _notes.Remove(note);
            });

        private static void InitSaveFileIfNotExists()
        {
            if (!File.Exists(_saveFile))
            {
                using (var writer = new StreamWriter(_saveFile))
                {
                    var empty = new List<Note>();
                    writer.Write(JsonSerializer.Serialize(empty));
                }
            }
        }

        private void ExecuteWithSave(Action action)
        {
            action();
            Save();
        }

        private void Save()
        {
            using (var writer = new StreamWriter(_saveFile))
            {
                writer.Write(JsonSerializer.Serialize(_notes));
            }
        }

        private Note GetNoteAndThrowIfNotFound(int id)
        {
            var note = _notes.SingleOrDefault(x => x.Id == id);
            if (note == null)
            {
                throw new NoteNotFoundException(id);
            }

            return note;
        }
    }
}
