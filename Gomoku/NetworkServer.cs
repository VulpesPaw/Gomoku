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
    internal class NetworkServer : NetMessage
    {
        public TcpListener listener;
        public TcpClient client;

        private int PORT;

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

                // waitForClient();
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, err.Source);
            }
        }

        public async Task<bool> waitForClient()
        {
            try
            {
                client = await listener.AcceptTcpClientAsync();
                client.NoDelay = true;

                return true;

            } catch(Exception err)
            {
                MessageBox.Show(err.Message, err.Source);
                return false;
            }
        }

        /*
        public async Task<string> translateNetworkMessage(TcpClient client)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int n = 0;

                n = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);

                string msg = Encoding.UTF8.GetString(buffer, 0, n);

             /*  if(msg[0] == '@')
                {
                    msgCommands(msg);
                    //callback(msg);
                    return msg;
                }*//*
                MessageBox.Show(msg, "Revieved message over network");
                return msg;
                // translateNetworkMessage(client);
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "ERROR!");
            }
            return null;
        }*/
        // class playfield?

        public async void sendData(string msg)
        {
            try
            {
                if(client.Connected)
                {
                    byte[] data = Encoding.UTF8.GetBytes(msg);

                    await client.GetStream().WriteAsync(data, 0, data.Length);
                    System.Diagnostics.Debug.WriteLine("-- Net-S msg" + msg + " --");
                }
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "ERROR");
            }
        }
    }
}