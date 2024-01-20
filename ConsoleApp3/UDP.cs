using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class UDP
    {
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
        public async Task SendAsync(List<string> strings)
        {
            UdpClient sennder = new UdpClient();

            foreach (string s in strings) 
            {
                byte[] data = Encoding.UTF8.GetBytes(s);
                await sennder.SendAsync(data, Convert.ToInt32(new IPEndPoint(IPAddress.Parse(Ip), Convert.ToInt32(PortSend))));
            }
        }
    }
}
