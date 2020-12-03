using System;
using System.Net;
using System.Net.Sockets;

namespace Whoswho
{
    class Connection
    {
        public Socket connectionSocket;

        // Endpoint for the specific process
        public IPEndPoint endPoint;
        
        public String BindPort(string portN)
        {
            // Bind to argument passed through
            try
            {
                int port = Convert.ToInt16(portN, 10);
                endPoint = new IPEndPoint(IPAddress.Any, port);
                connectionSocket.Bind(endPoint);
                Console.WriteLine("receiveSocket successfully binded");
            }
            catch
            {
                BindFailed();
            }
            return portN;
        }

        public void BindFailed()
        {
            // HCI to notify the user the bind has failed
            System.Windows.Forms.MessageBox.Show("Bind Failed");
        }     
        
        public static IPAddress GetLocalIP()
        {
            // Returns the IP Address the process will be running on
            IPAddress LocalIPaddress = IPAddress.Parse("127.0.0.1");
            IPHostEntry IPHost = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress IP in IPHost.AddressList)
            {
                if (IP.AddressFamily == AddressFamily.InterNetwork)
                {
                    LocalIPaddress = IP;
                    break;
                }
            }
            return LocalIPaddress;
        }

    }
}
