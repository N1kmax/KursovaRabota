using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ServerProgramm
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Server server = new Server();
            server.Quizzes = new ObservableCollection<Quiz>() { };
            server.ShowUsers();
            while (true) 
            {
                try
                {
                    server.StartServer();
                    Console.ReadLine();
                }
                catch(Exception ex) 
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
