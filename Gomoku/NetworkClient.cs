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
    internal class NetworkClient:NetMessage
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

        public async Task<bool> connectClient()
        {
            try
            {
                if(!client.Connected)
                {
                    client.NoDelay = true;
                    await client.ConnectAsync(ip, PORT);
                    return true;
                   // translateNetworkMessage(client);
                }
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "Error");
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
                    System.Diagnostics.Debug.WriteLine("-- Net-C msg" + msg + " --");

                    
                }
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "ERROR");
            }
        }
       /* private async void translateNetworkMessage(TcpClient client)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int n = 0;

                n = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);

                string msg = Encoding.UTF8.GetString(buffer, 0, n);

                MessageBox.Show(msg);
                
                //sendData("Hello Thus World from Client =D");
                translateNetworkMessage(client);
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "ERROR!");
            }
        }*/
        private void msgCommands(string command)
        {
            char c0 = command[1];

            command = command.Substring(2);
            switch(c0)
            {
                case 'r':


                    break;

                default:
                    break;
            }
            MessageBox.Show(command);
        }
    }
}