using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp27
{
    public class User: INotifyPropertyChanged
    {
        string login;
        string password;
        string mail;
        string usertype;
        public string Login
        {
            get { return login; }
            set 
            {
                login = value;
                OnPropetyChnaged("Login");
            } 
        }
        public string Mail
        {
            get { return mail; }
            set
            {
                mail = value;
                OnPropetyChnaged("Mail");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropetyChnaged("Password");
            }
        }
        
        public string UserType
        {
            get { return usertype; }
            set
            {
                usertype = value;
                OnPropetyChnaged("UserType");
            }
        }
        public User() 
        {
            Login = "New User";
            Password = "User1234";
            Mail = "user@gmail.com";
            UserType = "Student";
        }
        public User(string login, string mail, string password, string usertype) 
        {
            Login = login;
            Password = password;
            Mail = mail;
            UserType = usertype;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropetyChnaged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
