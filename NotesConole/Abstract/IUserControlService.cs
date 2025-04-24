using NotesConole.Models;

namespace NotesConole.Abstract
{
    internal interface IUserControlService
    {
        User Register(string userName);
        User Login();
    }
}
