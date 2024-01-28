using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Text.Json;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;


namespace WpfApp27
{
    public class UDP
    {
        ObservableCollection<Quiz> quizzes;
        ObservableCollection<User> users { get; set; }
        public string action;
        public string Ip { get; set; }
        public string PortSend { get; set; }
        public string PortReceive { get; set; }
        public UdpClient udpClient;
        public UDP()
        {
            Ip = GetLocalIPAddress();
            PortSend = "5000";
            PortReceive = getPort();
            udpClient = new UdpClient(int.Parse(PortReceive));
        }
        public void SetPoint(string portreceive)
        {
            PortReceive = portreceive;
        }
        public async Task SendQuizzesAsync(ObservableCollection<Quiz> quizzes)
        {

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(Ip), int.Parse(PortSend));
            byte[] data = Serialize(quizzes);
            await udpClient.SendAsync(data, data.Length, endPoint);
        }
        public async Task SendUsersAsync(ObservableCollection<User> users)
        {

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(Ip), int.Parse(PortSend));
            byte[] data = Serialize(users);
            await udpClient.SendAsync(data, data.Length, endPoint);
        }
        public void ReceiveUsers()
        {
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Parse(Ip), int.Parse(PortReceive));
            byte[] receivedData = udpClient.Receive(ref clientEndPoint);
            users = Deserialize<ObservableCollection<User>>(receivedData);
        }
        public void ReceiveQuizzes()
        {
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Parse(Ip), 0);
            byte[] receivedData = udpClient.Receive(ref clientEndPoint);
            quizzes = Deserialize<ObservableCollection<Quiz>>(receivedData);
        }
        static T Deserialize<T>(byte[] data)
        {
            string jsonString = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        static byte[] Serialize(object obj)
        {
            string jsonString = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(jsonString);
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
        string getPort()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 0);
            tcpListener.Start();
            IPEndPoint localEndPoint = (IPEndPoint)tcpListener.LocalEndpoint;
            tcpListener.Stop();
            return localEndPoint.Port.ToString();
        }
    }
}
