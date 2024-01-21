using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;

namespace WpfApp27
{
    public class Client
    {
        public User User { get; set; }
        int action;
        UDP udp { get; set; }
        public ObservableCollection<Quiz> quizzes { get; set; }
        ObservableCollection<User> users { get; set; }
        public Client()
        {
            udp = new UDP();
        }
        public ObservableCollection<User> GetUsers()
        {
            return users;
        }
        public async void SendMessage(string message)
        {
            action = int.Parse(message);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(udp.Ip), int.Parse(udp.PortSend));
            byte[] data = Encoding.UTF8.GetBytes(message);
            await udp.udpClient.SendAsync(data, data.Length, endPoint);
            ListenServer();
        }
        public async void ListenServer()
        {
            switch (action) 
            {
                case 0:
                    await udp.ReceiveUsers();
                    break;
                case 2:
                    await udp.ReceiveQuizzes();
                    break;

            }

        }
        public void Set()
        {
            quizzes = udp.GetQuizzes();
            users = udp.GetUsers();
        }

    }
}
