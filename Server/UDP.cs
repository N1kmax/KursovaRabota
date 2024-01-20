using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace ServerProgramm
{
    public class UDP
    {
        ObservableCollection<Quiz> quizzes;
        ObservableCollection<User> users;
        public string Ip { get; set; }
        public string PortSend { get; set; }
        public string PortReceive { get; set; }
        public UdpClient server;
        public UDP()
        {

        }
        public UDP(string portsend, string portreceive)
        {
            Ip = GetLocalIPAddress();
            Console.WriteLine(Ip);
            PortSend = portsend;
            PortReceive = portreceive;
            server = new UdpClient(5000);
        }
        public void SetPoint(string ip,string portsend)
        {
            Ip = ip;
            PortSend = portsend;
        }

        public async Task SendQuizzesAsync(ObservableCollection<Quiz> quizzes)
        {
            using (UdpClient udpClient = new UdpClient())
            {

                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(Ip), int.Parse(PortSend));
                
                string serializedList = JsonSerializer.Serialize(quizzes);

                byte[] data = Encoding.UTF8.GetBytes(serializedList);

                udpClient.Send(data, data.Length, endPoint);
                
            }
        }
        public async Task SendUsersAsync(ObservableCollection<User> users)
        {
            using (UdpClient udpClient = new UdpClient())
            {

                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(Ip), int.Parse(PortSend));
                string serializedList = JsonSerializer.Serialize(users);
                byte[] data = Encoding.UTF8.GetBytes(serializedList);

                udpClient.Send(data, data.Length, endPoint);
                Console.WriteLine("Data was sended");
            }
        }
        public async Task ReceiveUsers()
        {
            using (UdpClient recriver = new UdpClient(int.Parse(PortReceive)))
            {
                var receivedData = await recriver.ReceiveAsync();
                string receivedString = Encoding.UTF8.GetString(receivedData.Buffer);
                this.quizzes= JsonSerializer.Deserialize<ObservableCollection<Quiz>>(receivedString);
            }
            Console.WriteLine("Data was added");
        }
        public async Task ReceiveQuizzes()
        {
            using (UdpClient recriver = new UdpClient(int.Parse(PortReceive)))
            {
                var receivedData = await recriver.ReceiveAsync();
                string receivedString = Encoding.UTF8.GetString(receivedData.Buffer);
                this.quizzes= JsonSerializer.Deserialize<ObservableCollection<Quiz>>(receivedString);
            }
            Console.WriteLine("Data was added");
        }
        
        public ObservableCollection<Quiz> GetQuizzes()
        {
            return quizzes;
        } 
        public ObservableCollection<User> GetUsers() 
        {
            return users;
        }
        static string GetLocalIPAddress()
        {
            string localIp = "127.0.0.1";

            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    localIp = endPoint.Address.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении локального IP-адреса: {ex.Message}");
            }

            return localIp;
        }
    }
}
