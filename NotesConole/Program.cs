using NotesConole.Services;

namespace NotesConole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dateTimeProvider = new DateTimeProvider();
            var userService = new UserControlService();
            var noteRepository = new FileNoteRepository(dateTimeProvider);

            var ui = new ConsoleUIControlService(userService, noteRepository);

            ui.RunUI();
        }
    }
}
