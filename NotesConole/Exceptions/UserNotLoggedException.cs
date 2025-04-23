using System;

namespace NotesConole.Exceptions
{
    internal class UserNotLoggedException : Exception
    {
        public UserNotLoggedException() : base("Not logged") { }
    }
}
