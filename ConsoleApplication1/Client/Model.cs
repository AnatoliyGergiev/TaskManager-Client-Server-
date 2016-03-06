using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Client
{
    class Model
    {
        IPAddress ipAddr;
        IPEndPoint ipEndPoint;
        Socket sock;
        public List<string> Receive()
        {
                string data = null;
                byte[] bytes = new byte[8096];
                int bytesRec;
                bytesRec = sock.Receive(bytes);
                if (bytesRec == 0)
                {
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                    return null;
                }
                data = Encoding.Default.GetString(bytes, 0, bytesRec);
                int amount = new Regex("~").Matches(data).Count;
                List<string> ls = new List<string>();
                foreach (var s in data.Split('~'))
                    ls.Add(s);
                return ls;
        }
        public string Connect(string ip)
        {
                ipAddr = IPAddress.Parse(ip);
                ipEndPoint = new IPEndPoint(ipAddr /* IP-адрес */, 49152 /* порт */);

                sock = new Socket(AddressFamily.InterNetwork /*схема адресации*/, SocketType.Stream /*тип сокета*/, ProtocolType.Tcp /*протокол*/);

                sock.Connect(ipEndPoint);
                byte[] msg = Encoding.Default.GetBytes(Dns.GetHostName() /* имя узла локального компьютера */);
                int bytesSent = sock.Send(msg);
                return ("Клиент " + Dns.GetHostName() + " установил соединение с " + sock.RemoteEndPoint.ToString());
        }
        public void Exchange(string command)
        {
                string theMessage = command;
                byte[] msg = Encoding.Default.GetBytes(theMessage);
                int bytesSent = sock.Send(msg);
        }
        public void Disconnect()
        {
            sock.Shutdown(SocketShutdown.Both); 
            sock.Close(); 
        }
    }
}
