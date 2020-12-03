using System;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace Whoswho
{
    class TCPConnection : Connection
    {

        public TCPConnection ()
        {
            try
            {
                // Initalised socket to operate using TCP protocols 
                connectionSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    //set connection in blocking mode
                    Blocking = false
                };
                Console.WriteLine("TCP receiveSocket initialised");

            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Listen(int inGamePlayers)
        {
            try
            {
                // Listen for connections, with a backlog / queue maximum of the parameter given
                connectionSocket.Listen(inGamePlayers);    
                Console.WriteLine("Listening");
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
            // Silently handle any other exception
            catch 
            {
            }
        }

        public void CloseSocket()
        {
            // Close the socket
            try
            {
                connectionSocket.Shutdown(SocketShutdown.Both);
                connectionSocket.Close();
                
                Console.WriteLine("TCP Socket Closed");
            }
            catch
            {
            }
        }

    }
}
