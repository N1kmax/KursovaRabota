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
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Quiz> Quizzes { get; set; }
        public Server() 
        {
            udp = new UDP("127.0.0.1", "5000", "5001");
            Task.Run(() => udp.Receive());
        }
        void StartServer() 
        {

        }
        public void ListenClients() 
        {

        }
    }
}
