using System;

namespace Whoswho
{
    interface IMessageManager
    {
        // Manages messages received through a Socked using UDP Protocols
        void UDPMessageManager(String[] message);

        // Manages messages received through a Socked using TCP Protocols
        void TCPMessageManager(GameMessageStruct gameMessageStruct);
    }
}
