using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Gomoku
{
    internal class NetworkClient : NetMessage
    {
        private int PORT = 42069;
        public IPAddress ip;
        public TcpClient client = new TcpClient();

        /// <summary>
        /// Network Server
        /// </summary>
        /// <param name="_ip" >Default value 42069</param>
        public NetworkClient(IPAddress _ip)
        {
            try
            {
                this.ip = _ip;
            } catch(Exception)
            {
            }
        }

        public NetworkClient()
        {
        }

        public async Task<bool> connectClient()
        {
            try
            {
                if(!client.Connected)
                {
                    client.NoDelay = true;
                    await client.ConnectAsync(ip, PORT);
                    return true;
                }
            } catch(Exception)
            {
                MessageBox.Show("Seems like you have feed a none active IP address, make sure your host is online and try again");
            }
            return false;
        }

        public async void sendData(string msg)
        {
            try
            {
                if(client.Connected)
                {
                    byte[] data = Encoding.UTF8.GetBytes(msg);

                    await client.GetStream().WriteAsync(data, 0, data.Length);
                }
            } catch(Exception)
            {
            }
        }
    }
}