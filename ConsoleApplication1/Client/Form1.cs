using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        //IPHostEntry ipHost;
        IPAddress ipAddr;
        IPEndPoint ipEndPoint;
        Socket sock;
        public SynchronizationContext uiContext;
        public Form1()
        {
            InitializeComponent();
            uiContext = SynchronizationContext.Current;
        }

        private async void Receive(Socket handler)
        {
            await Task.Run(() =>
            {
                try
                {
                    string data = null;
                    byte[] bytes = new byte[1024];
                    // Метод Receive получает данные от сокета и заполняет массив байтов, переданный в качестве аргумента
                    int bytesRec;
                    while (true)
                    {
                        bytesRec = handler.Receive(bytes); // принимаем данные, переданные клиентом. Если данных нет, поток блокируется
                        if (bytesRec == 0)
                        {
                            handler.Shutdown(SocketShutdown.Both); // Блокируем передачу и получение данных для объекта Socket.
                            handler.Close(); // закрываем сокет
                            return;
                        }
                        data = Encoding.Default.GetString(bytes, 0, bytesRec);// конвертируем массив байтов в строку
                        int amount = new Regex("~").Matches(data).Count;
                        List<string> ls = new List<string>();
                        for (int i = 0; i < amount; i++)
                        {
                            ls.Add(data.Substring(0, data.IndexOf("~")));
                            data = data.Remove(0, data.IndexOf("~") + 1);
                        }
                        foreach(var t in ls)
                            uiContext.Send(d => listBox1.Items.Add(t), null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("\nКлиент: " + ex.Message + "\n");
                    //handler.Shutdown(SocketShutdown.Both); // Блокируем передачу и получение данных для объекта Socket.
                    //handler.Close(); // закрываем сокет
                }
            });
        }
        private async void Connect()
        {
            await Task.Run(() =>
            {
                // соединяемся с удаленным устройством
                try
                {
                    /* Класс Dns предоставляет методы, возвращающие информацию о сетевых адресах, поддерживаемых устройством в локальной сети.
                       Если у устройства имеется более одного сетевого адреса, класс Dns возвращает информацию обо всех сетевых адресах.
                       При этом приложение должно выбрать из массива подходящий адрес для обслуживания
                       Dns.Resolve разрешает DNS-имя узла или IP-адрес в экземпляр IPHostEntry.
                       IPHostEntry - список IP-адресов, связанных с узлом (хост-компьютером)
                     */
                    // ipHost = Dns.Resolve("127.0.0.1"); // если клиент и сервер - один и тот же компьютер
                    // ipHost = Dns.GetHostEntry(ip_address.Text); // IP-адрес удаленного компьютера (сервера), с которым будет установлено соединение
                    // ipAddr = ipHost.AddressList[2]; // IPv4-адрес
                    ipAddr = IPAddress.Parse(textBox_IP.Text);
                    // устанавливаем удаленную конечную точку для сокета
                    // уникальный адрес для обслуживания TCP/IP определяется комбинацией IP-адреса хоста с номером порта обслуживания
                    ipEndPoint = new IPEndPoint(ipAddr /* IP-адрес */, 49152 /* порт */);

                    // создаем потоковый сокет
                    sock = new Socket(AddressFamily.InterNetwork /*схема адресации*/, SocketType.Stream /*тип сокета*/, ProtocolType.Tcp /*протокол*/);
                    /* Значение InterNetwork указывает на то, что при подключении объекта Socket к конечной точке предполагается использование IPv4-адреса.
                      SocketType.Stream поддерживает надежные двусторонние байтовые потоки в режиме с установлением подключения, без дублирования данных и 
                      без сохранения границ данных. Объект Socket этого типа взаимодействует с одним узлом и требует предварительного установления подключения 
                      к удаленному узлу перед началом обмена данными. Тип Stream использует протокол Tcp и схему адресации AddressFamily.
                    */

                    // соединяем сокет с удаленной конечной точкой
                    sock.Connect(ipEndPoint);
                    byte[] msg = Encoding.Default.GetBytes(Dns.GetHostName() /* имя узла локального компьютера */);// конвертируем строку, содержащую имя хоста, в массив байтов
                    int bytesSent = sock.Send(msg); // отправляем серверу сообщение через сокет
                    MessageBox.Show("Клиент " + Dns.GetHostName() + " установил соединение с " + sock.RemoteEndPoint.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Клиент: " + ex.Message);
                }
            });
        }
        private async void Exchange(string command)
        {
            await Task.Run(() =>
            {
                try
                {
                    string theMessage = command;
                    byte[] msg = Encoding.Default.GetBytes(theMessage); // конвертируем строку, содержащую сообщение, в массив байтов
                    int bytesSent = sock.Send(msg); // отправляем серверу сообщение через сокет
                    //if (theMessage.IndexOf("<end>") > -1) // если клиент отправил эту команду, то принимаем сообщение от сервера
                    //{
                    //    byte[] bytes = new byte[1024];
                    //    int bytesRec = sock.Receive(bytes); // принимаем данные, переданные сервером. Если данных нет, поток блокируется
                    //    MessageBox.Show("Сервер (" + sock.RemoteEndPoint.ToString() + ") ответил: " + Encoding.Default.GetString(bytes, 0, bytesRec) /*конвертируем массив байтов в строку*/);
                    //    sock.Shutdown(SocketShutdown.Both); // Блокируем передачу и получение данных для объекта Socket.
                    //    sock.Close(); // закрываем сокет
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Клиент: " + ex.Message);
                }
            });
        }


        private void button_Get_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Exchange(button_Get.Text);
            Receive(sock);
        }

        private void button_Kill_Click(object sender, EventArgs e)
        {
            Exchange(button_Kill.Text + (string)listBox1.SelectedItem);
        }

        private void button_Create_Click(object sender, EventArgs e)
        {
            Exchange(button_Create.Text + textBox_Create.Text);
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                sock.Shutdown(SocketShutdown.Both); // Блокируем передачу и получение данных для объекта Socket.
                sock.Close(); // закрываем сокет
            }
            catch (Exception ex)
            {
                MessageBox.Show("Клиент: " + ex.Message);
            }
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            if (button_Connect.Text == "Connect to server")
            {
                Connect();
                button_Connect.Text = "Disconnect";
            }
            else
            {
                Exchange("Disconnect");
                button_Connect.Text = "Connect to server";
                sock.Shutdown(SocketShutdown.Both); // Блокируем передачу и получение данных для объекта Socket.
                sock.Close(); // закрываем сокет
            }
        }
    }
}
