using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ServerUD
{
    class User
    {
        public TcpListener listner { get; }
        public TcpClient client { get; set; }
        private bool Is_Connect = true;
        private bool Is_Sending;
        private string Msg;

        public User(TcpListener listner, TcpClient client)
        {
            this.client = client;
            this.listner = listner;
        }
        public void Send_Msg(string Msg)
        {
            Is_Sending = true;
            this.Msg = Msg; 
        }
        public string Get_Msg()
        {
            byte[] bytes = new byte[512];
            NetworkStream networkStream = client.GetStream();
            int size = networkStream.Read(bytes, 0, bytes.Length);
            return Encoding.Unicode.GetString(bytes, 0, size);
        }
        public bool Get_Is_Connect()
        {
            return Is_Connect;
        }
        public bool Get_Is_Sending()
        {
            return Is_Sending;
        }

        public void Send_Proverka()
        {
            try
            {
                if (Is_Sending == true)
                {
                    byte[] message = Encoding.Unicode.GetBytes(Msg);
                    NetworkStream networkStream = client.GetStream();
                    networkStream.Write(message, 0, message.Length);
                    networkStream.Flush();
                    Is_Sending = false;
                    Msg = "";
                }
                else
                {
                    byte[] message = Encoding.Unicode.GetBytes("~");
                    NetworkStream networkStream = client.GetStream();
                    networkStream.Write(message, 0, message.Length);
                    networkStream.Flush();
                }
            }
            catch
            {
                    Is_Connect = false;
            }
            
        }
    }
}
