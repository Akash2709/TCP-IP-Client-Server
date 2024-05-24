using System.Net.Sockets;
using System.Text;

internal class ClientSide
{
    class TCPClient
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1",3000);
            Console.WriteLine("Connected to server.");

            NetworkStream stream = client.GetStream();
            byte[] dataToSend = Encoding.ASCII.GetBytes("Hello from client!");
            stream.Write(dataToSend, 0, dataToSend.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received from server: " + response);

            client.Close();
        }
    }

}