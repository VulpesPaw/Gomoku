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
        private IPAddress ip;
        private TcpClient client = new TcpClient();

        /// <summary>
        /// Network Server
        /// </summary>
        /// <param name="_ip" >Default value 42069</param>
        public NetworkClient(IPAddress _ip)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("-- IP -- "+ _ip);
               
                // IP validation
                //https://stackoverflow.com/questions/11412956/what-is-the-best-way-of-validating-an-ip-address
              
                this.ip = _ip;
                if(!client.Connected) connectClient();
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "Error");
            }
        }

        private async void connectClient()
        {
            try
            {
                await client.ConnectAsync(ip, PORT);
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
                //! Client connected Retruns false!

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

    }
}