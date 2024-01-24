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
        public class MyClass
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        static void Main(string[] args)
        {

            Server server = new Server();
            server.Quizzes = new ObservableCollection<Quiz>() { };
            server.ShowUsers();
            server.ShowQuizzes();
            while (true) 
            {
                try
                {
                    server.StartServer();
                    while (!server.gotmessage) 
                    {
                    }
                }
                catch(Exception ex) 
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
