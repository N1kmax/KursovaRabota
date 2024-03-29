﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WpfApp27
{
    public class Client
    {
        public User User { get; set; }
        int action;
        UDP udp { get; set; }
        ObservableCollection<Quiz> quizzes { get; set; }
        ObservableCollection<User> users { get; set; }
        public Client()
        {
            udp = new UDP();
        }
        public ObservableCollection<User> GetUsers()
        {
            return users;
        }
        public void SetQuizzes(ObservableCollection<Quiz> quizzes) 
        {
            this.quizzes = quizzes;
        }
        public ObservableCollection <Quiz> GetQuizzes() 
        {
            return quizzes;
        }
        public void SendMessage(string message)
        {
            action = int.Parse(message);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(udp.Ip), int.Parse(udp.PortSend));
            byte[] data = Encoding.UTF8.GetBytes(message);
            udp.udpClient.SendAsync(data, data.Length, endPoint);
            ListenServer();
        }
        public async void ListenServer()
        {
            switch (action) 
            {
                case 0:
                    udp.ReceiveUsers();
                    Set();
                    break;
                case 1:
                    await udp.SendUsersAsync(users);
                    break;
                case 2:
                    udp.ReceiveQuizzes();
                    Set();
                    break;
                case 3:
                    await udp.SendQuizzesAsync(quizzes);
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
