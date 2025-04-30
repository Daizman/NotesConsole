using System.IO;
// ToDo: Не забыть написать, что для .NET Framework нужно поставить пакет
using System.Text.Json;
using NotesConole.Abstract;
using NotesConole.Models;

namespace NotesConole.Services
{
    internal class UserControlService : IUserControlService
    {
        private const string _userFile = "user.json";

        private User _user = null;

        public User Login()
        {
            if (!File.Exists(_userFile))
            {
                return _user;
            }

            var userFromFile = File.ReadAllText(_userFile);
            if (!string.IsNullOrEmpty(userFromFile))
            {
                _user = JsonSerializer.Deserialize<User>(userFromFile);
            }

            return _user;
        }

        public User Register(string userName)
        {
            _user = new User
            {
                Name = userName,
            };
            File.WriteAllText(_userFile, JsonSerializer.Serialize(_user));
            return _user;
        }
    }
}
