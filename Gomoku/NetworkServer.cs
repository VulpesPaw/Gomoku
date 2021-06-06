using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
                System.Diagnostics.Debug.WriteLine(err.Message);
            }
        }

        /// <summary>
        /// Waits for a client to connect, is cancelable
        /// </summary>
        /// <param name="token">CancellationTokenSource, enables cancellation</param>
        /// <returns>Boolean, true if a client connected, false if not</returns>
        // 'Task<bool>' returns a task, which we can await
        public async Task<bool> waitForClient(CancellationTokenSource token)
        {
            try
            {
                /*
                 * By putting listener.Accept... in a task.Run we can wait for the task to compelte,
                 * while including a cancelationtoken within it.
                 * The CancellationToken is a thread-safe token which can cancel a thread/task
                 */

                bool res = false;
                // Defines the task
                Task clientWaiter = Task.Run(async () =>
                {
                    client = await listener.AcceptTcpClientAsync();
                    client.NoDelay = true;

                    res = true;
                    return;
                });

                try
                {
                    // Runns task and waits for it to be finished or canceld. If task is cancelled, a OperationCanceledError is thrown
                    clientWaiter.Wait(token.Token);

                    // Dispoes task (not generally needed, but easy to do here)
                    clientWaiter.Dispose();

                    // Returns true if a client connected
                    return res;
                } catch(Exception)
                {
                    clientWaiter.Dispose();
                    return res;
                }
            } catch(Exception)
            {
                return false;
            }
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