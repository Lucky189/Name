using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
class Program
{
    private static TcpListener? listener;
    private static List<TcpClient> clients = new List<TcpClient>();
    private static Dictionary<TcpClient, string> clientNames = new Dictionary<TcpClient, string>();
    private const int PORT = 5000;

    static async Task Main(string[] args)
    {
        listener = new TcpListener(IPAddress.Any, PORT);
        listener.Start();
        Console.WriteLine($"Сервер запущено на порту {PORT}");

        while (true)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            clients.Add(client);
            _ = HandleClientAsync(client);
        }
    }

    private static async Task HandleClientAsync(TcpClient client)
    {
        NetworkStream stream = client.GetStream();

        // Попросимо клієнта ввести нік
        byte[] buffer = new byte[1024];
        int byteCount = await stream.ReadAsync(buffer, 0, buffer.Length);
        string name = Encoding.UTF8.GetString(buffer, 0, byteCount).Trim();
        clientNames[client] = name;
        Console.WriteLine($"{name} підключився");

        BroadcastMessage($"{name} приєднався до чату", client);

        try
        {
            while (true)
            {
                byteCount = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (byteCount == 0) break; // клієнт відключився

                string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                Console.WriteLine($"{name}: {message}");
                BroadcastMessage($"{name}: {message}", client);
            }
        }
        catch { }

        // Відключення клієнта
        Console.WriteLine($"{name} відключився");
        clients.Remove(client);
        clientNames.Remove(client);
        BroadcastMessage($"{name} покинув чат", client);
        client.Close();
    }

    private static void BroadcastMessage(string message, TcpClient sender)
    {
        byte[] msgBuffer = Encoding.UTF8.GetBytes(message);
        foreach (var client in clients)
        {
            if (client == sender) continue; // не відправляємо самому собі
            try
            {
                client.GetStream().WriteAsync(msgBuffer, 0, msgBuffer.Length);
            }
            catch { }
        }
    }
}