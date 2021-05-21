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
        private int PORT;

        /* TODO:
         * • Get users IP (local)
         * • Display users IP
         */

        public NetworkServer(int _PORT = 42069)
        {
            this.PORT = _PORT != 0 ? _PORT : 42069;
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
                System.Diagnostics.Debug.WriteLine("-- Waiting for client -- ");
                client = await listener.AcceptTcpClientAsync();
                System.Diagnostics.Debug.WriteLine("-- Found client -- " + client);

                translateNetworkMessage(client);
                System.Diagnostics.Debug.WriteLine("-- Translating client message -- ");

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

                sendData("Hello Thus World from Server =)");
              /*  if(msg[0] == '@')
                {
                    msgCommands(msg);
                    return;
                }*/
                MessageBox.Show(msg, "Revieved message over network");

                translateNetworkMessage(client);
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "ERROR!");
            }
        }

        /* ? Keep for shutdown procedure?
         
         private void msgCommands(string command)
        {
            char c0 = command[1];
            System.Diagnostics.Debug.WriteLine("-- Command C0 -- " + c0);
            command = command.Substring(3);
            switch(c0)
            {
                case 'r':
                    System.Diagnostics.Debug.WriteLine("-- Command Arg -- " + command);

                    break;

                default:
                    break;
            }
            MessageBox.Show(command);
        }
        */
        public async void sendData(string msg)
        {
            try
            {
                if(client.Connected)
                {
                    byte[] data = Encoding.UTF8.GetBytes(msg);

                    await client.GetStream().WriteAsync(data, 0, data.Length);
                }
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "ERROR");
            }
        }
    }
}