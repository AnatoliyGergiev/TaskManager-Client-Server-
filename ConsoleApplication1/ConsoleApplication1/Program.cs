using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Server srv = new Server();
            srv.Accept();
            Console.Read();
        }
        class Server
        {
            public Server()
            {
                uiContext = SynchronizationContext.Current;
            }
            public SynchronizationContext uiContext;
            private async void Exchange(Socket handler, List<string> theMessage)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        byte[] msg;
                        int bytesSent;
                        foreach (string s in theMessage)
                        {
                            msg = Encoding.Default.GetBytes(s); // конвертируем строку, содержащую сообщение, в массив байтов
                            bytesSent = handler.Send(msg); // отправляем серверу сообщение через сокет
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n"+ex.Message+"\n");
                    }
                });
            }
            private async void Receive(Socket handler)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        string client = null;
                        string data = null;
                        byte[] bytes = new byte[1024];
                        // Получим от клиента DNS-имя хоста.
                        // Метод Receive получает данные от сокета и заполняет массив байтов, переданный в качестве аргумента
                        int bytesRec = handler.Receive(bytes); // Возвращает фактически считанное число байтов
                        client = Encoding.Default.GetString(bytes, 0, bytesRec); // конвертируем массив байтов в строку
                        client += "(" + handler.RemoteEndPoint.ToString() + ")";
                        while (true)
                        {
                            bytesRec = handler.Receive(bytes); // принимаем данные, переданные клиентом. Если данных нет, поток блокируется
                            if (bytesRec == 0)
                            {
                                handler.Shutdown(SocketShutdown.Both); // Блокируем передачу и получение данных для объекта Socket.
                                handler.Close(); // закрываем сокет
                                return;
                            }
                            data = Encoding.Default.GetString(bytes, 0, bytesRec); // конвертируем массив байтов в строку
                            if (data.Contains("Get processes"))
                            {
                                List<string> processNames = new List<string>();
                                foreach (Process process in Process.GetProcesses())
                                    processNames.Add(process.ProcessName+"~");
                                Exchange(handler, processNames);
                            }
                            else if (data.Contains("Create process"))
                            {
                                data = data.Replace("Create process", null);
                                Process.Start(data);
                            }
                            else if (data.Contains("Kill process"))
                            {
                                data = data.Replace("Kill process", null);
                                Process[] lp = Process.GetProcessesByName(data);
                                lp[0].Kill();
                            }
                            else if (data == "Disconnect") // если клиент отправил эту команду, то заканчиваем обработку сообщений
                                break;
                        }
                        string theReply = "Я завершаю обработку сообщений";
                        byte[] msg = Encoding.Default.GetBytes(theReply); // конвертируем строку в массив байтов
                        handler.Send(msg); // отправляем клиенту сообщение
                        handler.Shutdown(SocketShutdown.Both); // Блокируем передачу и получение данных для объекта Socket.
                        handler.Close(); // закрываем сокет
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\nСервер: " + ex.Message+"\n");
                        //handler.Shutdown(SocketShutdown.Both); // Блокируем передачу и получение данных для объекта Socket.
                        //handler.Close(); // закрываем сокет
                    }
                });
            }

            //  ожидать запросы на соединение будем в отдельном потоке
            public async void Accept()
            {
                await Task.Run(() =>
                {
                    try
                    {
                        // установим для сокета адрес локальной конечной точки
                        // уникальный адрес для обслуживания TCP/IP определяется комбинацией IP-адреса хоста с номером порта обслуживания
                        IPEndPoint ipEndPoint = new IPEndPoint(
                       IPAddress.Any /* Предоставляет IP-адрес, указывающий, что сервер должен контролировать действия клиентов на всех сетевых интерфейсах.*/,
                       49152 /* порт */);

                        // создаем потоковый сокет
                        Socket sListener = new Socket(AddressFamily.InterNetwork /*схема адресации*/, SocketType.Stream /*тип сокета*/, ProtocolType.Tcp /*протокол*/ );
                        /* Значение InterNetwork указывает на то, что при подключении объекта Socket к конечной точке предполагается использование IPv4-адреса.
                           SocketType.Stream поддерживает надежные двусторонние байтовые потоки в режиме с установлением подключения, без дублирования данных и 
                           без сохранения границ данных. Объект Socket этого типа взаимодействует с одним узлом и требует предварительного установления подключения 
                           к удаленному узлу перед началом обмена данными. Тип Stream использует протокол Tcp и схему адресации AddressFamily.
                         */

                        // Чтобы сокет клиента мог идентифицировать потоковый сокет TCP, сервер должен дать своему сокету имя
                        sListener.Bind(ipEndPoint); // Свяжем объект Socket с локальной конечной точкой.

                        // Установим объект Socket в состояние прослушивания.
                        sListener.Listen(10 /* Максимальная длина очереди ожидающих подключений.*/ );
                        while (true)
                        {
                            /* Блокируется поток до поступления от клиента запроса на соединение. При этом устанавливается связь имен клиента и сервера. 
                               Метод Accept извлекает из очереди ожидающих запросов первый запрос на соединение и создает для его обработки новый сокет.
                               Хотя новый сокет создан, первоначальный сокет продолжает слушать и может использоваться с многопоточной обработкой для 
                               приема нескольких запросов на соединение от клиентов. Сервер не должен закрывать слушающий сокет, который продолжает работать
                               наряду с сокетами, созданными методом Accept для обработки входящих запросов клиентов.
                             */
                            Socket handler = sListener.Accept();
                            Console.WriteLine("Подключился клиент {0}\n", handler.RemoteEndPoint);
                            // обслуживание текущего запроса будем выполнять в отдельной асинхронной задаче
                            Receive(handler);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Сервер: " + ex.Message);
                    }
                });
            }
        }
    }
}
