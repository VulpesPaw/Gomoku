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
    internal class NetworkClient
    {
        private int PORT = 42069;
        private int ClientPORT = 42070;
        public IPAddress ip;
        private TcpClient client = new TcpClient();

        /// <summary>
        /// Network Server
        /// </summary>
        /// <param name="_ip" >Default value 42069</param>
        public NetworkClient(IPAddress _ip)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("-- IP -- " + _ip);

                // IP validation
                //https://stackoverflow.com/questions/11412956/what-is-the-best-way-of-validating-an-ip-address

                this.ip = _ip;
                /* if(!client.Connected)
                 {
                     System.Diagnostics.Debug.WriteLine("-- CLIENT -- " + client);
                     System.Diagnostics.Debug.WriteLine("-- IP -- "+ ip);
                     System.Diagnostics.Debug.WriteLine("-- CLIENT CONNECTED-- " + client.Connected);

                     connectClient();
                 }*/
                //     connectClient();
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "Error");
            }
        }

        public NetworkClient()
        {
        }

        public async void connectClient()
        {
            try
            {
                if(!client.Connected)
                {
                    await client.ConnectAsync(ip, PORT);
                    
                }
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "Error");
            }
        }
        public async void connectClientAndResponse()
        {
            try
            {
                if(!client.Connected)
                {
                    await client.ConnectAsync(ip, PORT);
                    sendData("@r_" + ClientPORT);
                    translateNetworkMessage(client);
                }
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "Error");
            }
        }
        public async void sendData(string msg)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("-- Client Connected -- " + client.Connected);

                if(client.Connected)
                {
                    byte[] data = Encoding.UTF8.GetBytes(msg);

                    await client.GetStream().WriteAsync(data, 0, data.Length);
                    System.Diagnostics.Debug.WriteLine("-- Msg -- " + "Procedure after message");
                }
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "ERROR");
            }
        }
        private async void translateNetworkMessage(TcpClient client)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int n = 0;

                n = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);

                string msg = Encoding.UTF8.GetString(buffer, 0, n);

               /* if(msg[0] == '@')
                {
                    //   msgCommands(msg);
                    MessageBox.Show(msg);
                    return;
                }*/
                MessageBox.Show(msg, "Revieved message over network");
                sendData("Hello Thus World from Client =D");
                translateNetworkMessage(client);
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "ERROR!");
            }
        }
    }
}