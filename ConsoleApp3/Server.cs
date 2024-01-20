using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Server
    {
        UDP udp { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Quiz> Quizzes { get; set; }
        public Server() 
        {
            udp = new UDP($"{System.Net.IPAddress.Any}:8888", "1000", "1000");
            
        }
        void StartServer() 
        {

        }
        public void ListenClients() 
        {

        }
    }
}
