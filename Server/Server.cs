using System;
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
        DatabaseContext db;
        private static ConcurrentDictionary<string, int> clientPorts = new ConcurrentDictionary<string, int>();
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Quiz> Quizzes { get; set; }
        int action;
        public Server()
        {
            udp = new UDP("5001", "5000");
            db = new DatabaseContext();
            Users = db.GetUsers();

        } 
        public void ListenClients() 
        {
            Quizzes = udp.GetQuizzes();
            Users = udp.GetUsers();
        }
        public async void StartServer() 
        {
            var result = await udp.server.ReceiveAsync();
            Console.WriteLine(Encoding.UTF8.GetString(result.Buffer));
            action = int.Parse(Encoding.UTF8.GetString(result.Buffer));
            await Task.Run(() => ProcessClient(new ClientData { EndPoint = result.RemoteEndPoint, Data = result.Buffer }));
            Console.WriteLine(result.RemoteEndPoint.Port.ToString());
            udp.SetPoint(result.RemoteEndPoint.Address.ToString(),result.RemoteEndPoint.Port.ToString());
            Chooseaction(action);
        }
        public async void Chooseaction(int action) 
        {
            switch (action) 
            {
                case 0:
                    await udp.SendUsersAsync(Users);
                    ListenClients();
                    Console.WriteLine("Get");
                    break;
                case 1:
                    await udp.ReceiveUsers();
                    ListenClients();
                    break;
                case 2:
                    await udp.SendQuizzesAsync(Quizzes);
                    ListenClients();
                    break;
                case 3:
                    await udp.ReceiveQuizzes();
                    ListenClients();
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

            // Отправка ответа обратно клиенту
            UdpClient udpClient = new UdpClient();
            udpClient.Send(clientData.Data, clientData.Data.Length, clientData.EndPoint);
            udpClient.Close();
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
                Console.WriteLine($"Name: {user.Login}");
            }
        }
    }
}
