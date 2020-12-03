using System;
using System.Net.Sockets;
using System.Net;

namespace Whoswho
{
    class UDPConnection : Connection
    {

        public UDPConnection()
        {
            // Initialise connectionSocket using UDP Protocols
            try
            {
                connectionSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
                {
                    Blocking = false
                };
                Console.WriteLine("UDP Socket initialised");
            }
            catch (SocketException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public void Broadcast(byte[] message, String port)
        {
            try
            {
                // IP addresses //
                // All Broadcast
                //IPAddress DestinationIPAddress = IPAddress.Parse("255.255.255.255");
                // Lab Subnet broadcast Address
                IPAddress DestinationIPAddress = IPAddress.Parse("172.16.171.255");
                // Loopback
                //IPAddress DestinationIPAddress = IPAddress.Parse("127.0.0.1");
                // Personal computer
                //IPAddress DestinationIPAddress = IPAddress.Parse("172.16.170.43");

                // Get the Port number from the appropriate text box
                int iPort = Convert.ToInt16(port, 10);

                // Combine Address and Port to create an Endpoint
                IPEndPoint remoteEndPoint = new IPEndPoint(DestinationIPAddress, iPort);
                
                // Send message to endpoint
                connectionSocket.SendTo(message, remoteEndPoint);
                Console.WriteLine("Broadcast sent to {0}", DestinationIPAddress);

            }
            catch (SocketException se)
            {
                // If an exception occurs, display an error message
                System.Windows.Forms.MessageBox.Show(se.Message);
            }
        }

        public void CloseSocket()
        {
            // Close the socket
            try
            {
                connectionSocket.Shutdown(SocketShutdown.Both);
                connectionSocket.Close();
                Console.WriteLine("UDP Socket Closed");
            }
            catch
            {
            }
        }
    }
}
