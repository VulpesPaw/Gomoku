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
    internal class NetworkServer
    {
        private TcpListener listener;
        private TcpClient client;

        // Not used by offical stuffies
        private int PORT = 42069;

        /* TODO:
         * • Get users IP (local)
         * • Display users IP
         */

        public NetworkServer()
        {
            initiateServer();
        }

        private void initiateServer()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, PORT);
                listener.Start();

                waitForClient();
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, err.Source);
            }
        }

        private async void waitForClient()
        {
            try
            {
                client = await listener.AcceptTcpClientAsync();
                translateNetworkMessage(client);
                //?   if(!clientResp.Connected) startConnection(); // callback to client
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, err.Source);
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
                
                MessageBox.Show(msg, "Revieved message over network");

                translateNetworkMessage(client);

            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "ERROR!");
            }
        }
    }
}