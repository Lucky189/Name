using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
class Program
{
    private const int PORT = 5000;
    private const string SERVER = "127.0.0.1";

    static async Task Main(string[] args)
    {
        TcpClient client = new TcpClient();
        await client.ConnectAsync(SERVER, PORT);
        NetworkStream stream = client.GetStream();

        Console.Write("Введіть свій нік: ");
        string name = Console.ReadLine();
        byte[] nameBuffer = Encoding.UTF8.GetBytes(name);
        await stream.WriteAsync(nameBuffer, 0, nameBuffer.Length);

        // Запускаємо читання повідомлень від сервера
        _ = Task.Run(async () =>
        {
            byte[] buffer = new byte[1024];
            while (true)
            {
                int byteCount = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (byteCount == 0) break;
                string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                Console.WriteLine(message);
            }
        });

        // Відправка повідомлень
        while (true)
        {
            string message = Console.ReadLine();
            if (message.ToLower() == "/exit") break;
            byte[] msgBuffer = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(msgBuffer, 0, msgBuffer.Length);
        }

        client.Close();
    }
}