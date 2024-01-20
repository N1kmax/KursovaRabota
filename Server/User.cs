using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProgramm
{
    public class User
    {
        string login;
        string password;
        string usertype;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
            }
        }
        public string UserType
        {
            get { return usertype; }
            set
            {
                usertype = value;
            }
        }
        public User()
        {
            Login = "New User";
            Password = "User1234";
            UserType = "Student";
        }
        public User(string login, string password, string usertype)
        {
            Login = login;
            Password = password;
            UserType = usertype;
        }
    }
}
