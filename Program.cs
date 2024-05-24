using System.Net.Sockets;
using System.Net;
using System.Net.WebSockets;
using System.IO;

internal class Program
{
    private static void Main(string[] args)
    {
        Socket? ClientSocket = null;
        int Counter = 0;
        try
        {
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    int port = 3000;
                    string Ipaddress = "127.0.0.1";

                    ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse(Ipaddress), port);
                    ClientSocket.Connect(ep);
                    Console.WriteLine(" Client is Connected");

                    string messageFromClient = "";
                    Console.WriteLine("Enter the Message to Display");
                    messageFromClient = "I am Client " + Counter;
                    Console.WriteLine("I am Client " + Counter);
                    ClientSocket.Send(System.Text.Encoding.ASCII.GetBytes(messageFromClient), 0, messageFromClient.Length, SocketFlags.None);

                    byte[] MsgFromServer = new byte[1024];
                    int size = ClientSocket.Receive(MsgFromServer);
                    Console.WriteLine("Server reply ->  " + System.Text.Encoding.ASCII.GetString(MsgFromServer, 0, size));
                    Console.WriteLine(size);
                    Counter++;
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}