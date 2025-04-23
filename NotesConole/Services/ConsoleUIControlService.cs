using System;
using System.Collections.Generic;
using NotesConole.Abstract;
using NotesConole.Exceptions;
using NotesConole.Models;

namespace NotesConole.Services
{
    internal class ConsoleUIControlService
    {
        private readonly IUserControlService _userControlService;
        private readonly INoteRepository _noteRepository;

        private readonly IReadOnlyDictionary<string, Action> _uiCommandsHandlers;

        private bool _running = false;
        private User _user = null;

        public ConsoleUIControlService(
            IUserControlService userControlService,
            INoteRepository noteRepository)
        {
            _userControlService = userControlService;
            _noteRepository = noteRepository;

            _uiCommandsHandlers = new Dictionary<string, Action>
            {
                { "register", Register },
                { "view_notes", ViewNotes },
                { "create_note", CreateNote },
                { "complete_note", CompleteNote },
                { "update_note", UpdateNote },
                { "remove_note", RemoveNote },
                { "help", PrintAllowedCommands },
                { "exit", Exit },
            };
        }

        public void RunUI()
        {
            _running = true;
            Console.WriteLine("Hello! This is note application. You can manage your notes here.");
            Console.WriteLine("You can start typing comands (write \"help\" for more info):");
            while (_running)
            {
                if (_user == null && _userControlService.Login())
                {
                    _user = _userControlService.GetUser();
                    Console.WriteLine($"Wellcome back, {_user.Name}");
                    ViewNotes();
                }

                var command = Console.ReadLine().Trim().ToLowerInvariant();
                if (_uiCommandsHandlers.ContainsKey(command))
                {
                    _uiCommandsHandlers[command]();
                }
                else
                {
                    Console.WriteLine("Unkown command!");
                    PrintAllowedCommands();
                }
            }

            Console.WriteLine($"Goodbye, {_user?.Name ?? "anonymus"}!");
        }

        private void Register()
        {
            if (_user != null)
            {
                Console.WriteLine("You already loged in");
                return;
            }
            Console.WriteLine("Enter your name:");
            var userName = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine("Name is empty. Couldn't register. Repeat command for new registration attempt");
                return;
            }
            _user = _userControlService.Register(userName);

            Console.WriteLine($"Nice to meet you, {_user.Name}!");
        }

        private void ViewNotes()
        {
            ThrowIfUserNotLogged();
            Console.WriteLine("Your notes:");
            foreach(var note in _noteRepository.GetNotes())
            {
                Console.WriteLine(note);
            }
            Console.WriteLine("------------------------------------");
        }

        private void CreateNote()
        {
            ThrowIfUserNotLogged();
            Console.WriteLine("Title:");
            var title = Console.ReadLine().Trim();
            Console.WriteLine("Desciption:");
            var desciption = Console.ReadLine().Trim();

            _noteRepository.AddNote(title, desciption, _user);
            Console.WriteLine("Added.");
            ViewNotes();
        }

        private void CompleteNote()
        {
            ThrowIfUserNotLogged();
            if (!TryToGetNoteIdFromConsole(out var id))
            {
                return;
            }

            _noteRepository.CompleteNote(id);

            Console.WriteLine($"Note {id} completed!");
            ViewNotes();
        }

        private void UpdateNote()
        {
            ThrowIfUserNotLogged();
            if (!TryToGetNoteIdFromConsole(out var id))
            {
                return;
            }

            Console.WriteLine("Title:");
            var title = Console.ReadLine().Trim();
            Console.WriteLine("Desciption:");
            var desciption = Console.ReadLine().Trim();

            _noteRepository.EditNote(id, title, desciption);

            Console.WriteLine($"Note {id} updated!");
            ViewNotes();
        }

        private void RemoveNote()
        {
            ThrowIfUserNotLogged();
            if(!TryToGetNoteIdFromConsole(out var id))
            {
                return;
            }

            _noteRepository.RemoveNote(id);
            Console.WriteLine("Removed");
            ViewNotes();
        }

        private void PrintAllowedCommands()
        {
            Console.WriteLine("Allowed commands:");
            Console.Write($"\t{string.Join("\n\t", _uiCommandsHandlers.Keys)}");
            Console.WriteLine();
        }

        private void Exit() => _running = false;

        private void ThrowIfUserNotLogged()
        {
            if (_user is null)
            {
                throw new UserNotLoggedException();
            }
        }

        private bool TryToGetNoteIdFromConsole(out int id)
        {
            Console.WriteLine("Input note id:");
            var idStr = Console.ReadLine().Trim();
            if (!int.TryParse(idStr, out id))
            {
                Console.WriteLine("Id is not number!");
                return false;
            }

            return true;
        }
    }
}
