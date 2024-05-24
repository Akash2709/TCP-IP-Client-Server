using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class ServerSide
{
    static async Task Main(string[] args)
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 3000;

        TcpListener server = new TcpListener(ipAddress, port);
        server.Start();
        Console.WriteLine("Server started. Waiting for connections...");

        while (true)
        {
            TcpClient client = await server.AcceptTcpClientAsync();
            Console.WriteLine("Client connected: " + ((IPEndPoint)client.Client.RemoteEndPoint).Address);

            HandleClient(client);
        }
    }

    static async Task HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead;

        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
        {
            string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received from client: " + data);

            // Echo back to the client.
            byte[] responseBuffer = Encoding.ASCII.GetBytes("Server echo: " + data);
            await stream.WriteAsync(responseBuffer, 0, responseBuffer.Length);
        }

        client.Close();
    }
}
