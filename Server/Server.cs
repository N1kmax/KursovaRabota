using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerProgramm
{
    public class Server
    {
        UDP udp { get; set; }
        DatabaseContext db;
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Quiz> Quizzes { get; set; }
        public Server()
        {
            udp = new UDP("5000", "5001");
            db = new DatabaseContext();
            Users = db.GetUsers();
            Task.Run(() => udp.ReceiveQuizzes());
            Task.Run(() => udp.ReceiveUsers());
        } 
        public void ListenClients() 
        {
            Quizzes = udp.GetQuizzes();
            Users = udp.GetUsers();
        }
        public void ShowUsers() 
        {
            foreach (var user in Users) 
            {
                Console.WriteLine($"Name: {user.Login}");
            }
        }
    }
}
