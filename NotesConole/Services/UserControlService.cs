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

        public User GetUser() => _user;

        public bool Login()
        {
            if (!File.Exists(_userFile))
            {
                return false;
            }

            var userFromFile = File.ReadAllText(_userFile);
            if (!string.IsNullOrEmpty(userFromFile))
            {
                _user = JsonSerializer.Deserialize<User>(userFromFile);
                return true;
            }

            return false;
        }

        public User Register(string userName)
        {
            _user = new User
            {
                Name = userName,
            };
            using (var writer = new StreamWriter(_userFile))
            {
                writer.Write(JsonSerializer.Serialize(_user));
            }
            return _user;
        }
    }
}
