using System;
using System.Runtime.InteropServices;

namespace Whoswho
{

    // Protcol Data Unit to be sent whilst game is being played
    [StructLayout(LayoutKind.Sequential)]
    public struct GameMessageStruct
    {
        // Singals how to handle received message
        [MarshalAs(UnmanagedType.I4, SizeConst = 4)]
        public int messageType;

        // The player name for the Player List
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
        public String playerName;

        // The full names list as of sending
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 300)]
        public String nameslist;

        // The senders character choice
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
        public String characterChoice;

        // To determine who's turn it is
        [MarshalAs(UnmanagedType.Bool, SizeConst = 4)]
        public bool activeTurn;

        // The question or chat to sent to the opponent
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60)]
        public String text;

        // The guess the player wants to make
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
        public String guess;

        // The response to the guess
        [MarshalAs(UnmanagedType.Bool, SizeConst = 4)]
        public bool response;

        // The status the player needs to be changed to
        [MarshalAs(UnmanagedType.I4, SizeConst = 4)]
        public int playerStatus;
    }

    public class PDU
    {           
        // Convert PDU into an array of Bytes
        public static byte[] SerializeStruct(Object PDU)
        {
            // Setting the pointer to the size of the object
            int size = Marshal.SizeOf(PDU);
            IntPtr ptr = Marshal.AllocHGlobal(size);

            //Now copy structure to pointer 
            Marshal.StructureToPtr(PDU, ptr, false);

            // Declare Byte Array with size of PDU
            byte[] byteArray = new byte[size];            
            
            // Place contents of memory into byte array
            Marshal.Copy(ptr, byteArray, 0, size);

            //Now use ByteArray ready for use 
            Marshal.FreeHGlobal(ptr);
            
            return byteArray;
        }
        
        // Convert array of Bytes into PDU
        public static GameMessageStruct DeserializeStruct(byte[] message)
        {
            // Allocate unmanaged memory
            IntPtr ip = Marshal.AllocHGlobal(message.Length);

            // Copy the byte to the unmanaged memory
            Marshal.Copy(message, 0, ip, message.Length);

            // Marshal the unmanaged memory contents to the GameMessageStruct
            GameMessageStruct structure = (GameMessageStruct)Marshal.PtrToStructure(ip, typeof(GameMessageStruct));

            // Free unmanaged memory 
            Marshal.FreeHGlobal(ip);
            return structure; 
        }              
    }
}
