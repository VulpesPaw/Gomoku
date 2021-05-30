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
    class NetMessage
    {
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
                   }*/
               // MessageBox.Show(msg, "Revieved message over network");
                return msg;
                // translateNetworkMessage(client);
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "ERROR!");
            }
            return null;

        }

    }
}
