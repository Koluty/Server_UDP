
using System.Net.Sockets;
using System.Net;
using System;
using System.Threading;

namespace Server_UDP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); // Перетворення строки у айпи адерсу
            IPEndPoint ipLocal = new IPEndPoint(ipAddress, 8888); // створення локальної точки

            Socket udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(ipLocal);

            Console.WriteLine("Сервер запущено. Очікування підключення");

            byte[] buffer = new byte[65535]; // буфер для зберігання даних
            EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);

            udpSocket.ReceiveFrom(buffer, ref remoteIp);

            double[] receivedNumbers = new double[3];
            for (int i = 0; i < 3; i++)
            {
                receivedNumbers[i] = BitConverter.ToDouble(buffer, i * sizeof(double));
            }

            // Виведення отриманих чисел
            Console.WriteLine("Отримані числа:");
            foreach (double number in receivedNumbers)
            {
                Console.WriteLine(number);
            }
            Console.WriteLine(remoteIp);
            Calculate calculate = new Calculate();
            double result = calculate.Calculate_formula(receivedNumbers[0], receivedNumbers[1], receivedNumbers[2]);
            Console.WriteLine(result);
            udpSocket.SendTo(BitConverter.GetBytes(result), remoteIp);
            udpSocket.Close();
        }
    }
}
