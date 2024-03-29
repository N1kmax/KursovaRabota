﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerProgramm
{
    public class Server
    {
        public UDP udp { get; set; }
        public bool gotmessage;
        DatabaseContext db;
        private static ConcurrentDictionary<string, int> clientPorts = new ConcurrentDictionary<string, int>();
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Quiz> Quizzes { get; set; }
        int action;
        public Server()
        {
            udp = new UDP("1000","5000");
            db = new DatabaseContext();
            Users = db.GetUsers();
            Quizzes = db.GetQuiz();
        } 
        public void ListenClientsUser() 
        {
            
            Users = udp.GetUsers();
        }
        public void ListenClientsQuizzes() 
        {
            Quizzes = udp.GetQuizzes();
        }
        public async void StartServer() 
        {
            gotmessage = false;
            var result = await udp.server.ReceiveAsync();
            Console.WriteLine(Encoding.UTF8.GetString(result.Buffer));
            action = int.Parse(Encoding.UTF8.GetString(result.Buffer));
            Task.Run(() => ProcessClient(new ClientData { EndPoint = result.RemoteEndPoint, Data = result.Buffer }));
            Console.WriteLine(result.RemoteEndPoint.Port.ToString());
            udp.SetPoint(result.RemoteEndPoint.Address.ToString(),result.RemoteEndPoint.Port.ToString());
            Chooseaction(action);
        }
        public async void Chooseaction(int action) 
        {
            switch (action) 
            {
                case 0:
                    gotmessage = true;
                    await udp.SendUsersAsync(Users);
                    break;
                case 1:
                    udp.ReceiveUsers();
                    ListenClientsUser();
                    db.AddNewUser(Users[Users.Count-1]);
                    gotmessage = true;
                    break;
                case 2:
                    gotmessage = true;
                    await udp.SendQuizzesAsync(Quizzes);
                    break;
                case 3:
                    udp.ReceiveQuizzes();
                    ListenClientsQuizzes();
                    db.SaveQuiz(Quizzes);
                    gotmessage = true;
                    break;
            }
        }
        static void ProcessClient(ClientData clientData)
        {
            string receivedMessage = Encoding.UTF8.GetString(clientData.Data);
            Console.WriteLine($"Получено сообщение от {clientData.EndPoint}: {receivedMessage}");

            // Сохранение порта клиента
            string clientIdentifier = $"{clientData.EndPoint.Address}:{clientData.EndPoint.Port}";
            clientPorts.AddOrUpdate(clientIdentifier, clientData.EndPoint.Port, (key, existingValue) => clientData.EndPoint.Port);

            Console.WriteLine($"Порт клиента сохранен: {clientData.EndPoint.Port}");
        }
        class ClientData
        {
            public IPEndPoint EndPoint { get; set; }
            public byte[] Data { get; set; }
        }

        public void ShowUsers() 
        {
            foreach (var user in Users) 
            {
                Console.WriteLine($"Name: {user.Login} Paaword: {user.Password} Mail: {user.Mail} Usertype: {user.UserType}");
            }
        }
        public void ShowQuizzes() 
        {
            Console.WriteLine($"count {Quizzes.Count}");
            foreach (var quiz in Quizzes) 
            {
                Console.WriteLine($"Name: {quiz.Name}");
            }
        }
    }
}
