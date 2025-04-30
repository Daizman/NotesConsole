using NotesConole.Models;

namespace NotesConole.Abstractions
{
    internal interface IUserControlService
    {
        User Register(string userName);
        User Login();
    }
}
