using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Text.Json;
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
        ObservableCollection<User> users;
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
            using (UdpClient udpClient = new UdpClient())
            {

                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(Ip), int.Parse(PortSend));

                string serializedList = JsonSerializer.Serialize(quizzes);

                byte[] data = Encoding.UTF8.GetBytes(serializedList);

                await udpClient.SendAsync(data, data.Length, endPoint);
            }
        }
        public async Task SendUsersAsync(ObservableCollection<User> users)
        {
            using (UdpClient udpClient = new UdpClient())
            {

                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(Ip), int.Parse(PortSend));

                string serializedList = JsonSerializer.Serialize(users);

                byte[] data = Encoding.UTF8.GetBytes(serializedList);

                await udpClient.SendAsync(data, data.Length, endPoint);
            }
        }
        public async Task ReceiveUsers()
        {
            var receivedData = await udpClient.ReceiveAsync();
            MessageBox.Show(receivedData.RemoteEndPoint.Port.ToString());
            string receivedString = Encoding.UTF8.GetString(receivedData.Buffer);
            MessageBox.Show(receivedString);
            MessageBox.Show($"{Ip}:{PortReceive}");
            users = JsonSerializer.Deserialize<ObservableCollection<User>>(receivedString);
        }
        public async Task ReceiveQuizzes()
        {
            var receivedData = await udpClient.ReceiveAsync();
            string receivedString = Encoding.UTF8.GetString(receivedData.Buffer);
            quizzes= JsonSerializer.Deserialize<ObservableCollection<Quiz>>(receivedString);
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
