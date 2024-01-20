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
        public string Ip { get; set; }
        public string PortSend { get; set; }
        public string PortReceive { get; set; }
        public UDP()
        {

        }
        public UDP(string ip, string portsend, string portreceive)
        {
            Ip = ip;
            PortSend = portsend;
            PortReceive = portreceive;
        }
        public void SetPoint(string ip, string portsend, string portreceive)
        {
            Ip = ip;
            PortSend = portsend;
            PortReceive = portreceive;

        }
        public async Task SendAsync(ObservableCollection<Quiz> quizzes)
        {
            using (UdpClient udpClient = new UdpClient())
            {

                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(Ip), int.Parse(PortSend));

                string serializedList = JsonSerializer.Serialize(quizzes);

                byte[] data = Encoding.UTF8.GetBytes(serializedList);

                udpClient.Send(data, data.Length, endPoint);
            }
        }
        public async Task Receive() 
        {
            while (true)
            {
                using (UdpClient recriver = new UdpClient(int.Parse(PortReceive)))
                {
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                    var receivedData = await recriver.ReceiveAsync();
                    string receivedString = Encoding.UTF8.GetString(receivedData.Buffer);
                    this.quizzes= JsonSerializer.Deserialize<ObservableCollection<Quiz>>(receivedString);
                }
                Console.WriteLine("Data was added");
            }
        }
        public ObservableCollection<Quiz> GetQuizzes()
        {
            return quizzes;
        } 
    }
}
