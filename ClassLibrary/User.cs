using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class User
    {
        public User(int id, string nick, string email)
        {
            Id = id;
            Nick = nick;
            Email = email;
        }
        public int Id { get; }
        public string Nick { get; }
        public string Email { get; }
    }
}
