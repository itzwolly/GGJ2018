using System;
using System.Net.Sockets;
using System.Text;
using System.Net;

namespace DLLLibrary
{
    public class StreamUtil
    {

        public static byte[] ReadBytes(NetworkStream pStream, int pByteCount)
        {
            byte[] bytes = new byte[pByteCount];
            int bytesRead = 0;
            int totalBytesRead = 0;

            try
            {
                while (
                    totalBytesRead != pByteCount &&
                    (bytesRead = pStream.Read(bytes, totalBytesRead, pByteCount - totalBytesRead)) > 0
                )
                {
                    totalBytesRead += bytesRead;
                }
            }
            catch { }

            return (totalBytesRead == pByteCount) ? bytes : null;
        }

        public static void SendMessage(NetworkStream pStream, byte[] pMessage)
        {
            pStream.Write(BitConverter.GetBytes(pMessage.Length), 0, 4);
            pStream.Write(pMessage, 0, pMessage.Length);
        }

        public static byte[] ReceiveMessage(NetworkStream pStream)
        {
            int byteCountToRead = BitConverter.ToInt32(ReadBytes(pStream, 4), 0);
            return ReadBytes(pStream, byteCountToRead);
        }

        public static void SendString(NetworkStream pStream, string pMessage, Encoding pEncoding)
        {
            SendMessage(pStream, pEncoding.GetBytes(pMessage));
        }

        public static string ReceiveString(NetworkStream pStream, Encoding pEncoding)
        {
            return pEncoding.GetString(ReceiveMessage(pStream));
        }

    }
}



