using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

// rnd tester
using System.Security.Cryptography;

namespace Gomoku
{
    public partial class Form1 : Form
    {
        /* TODO:
         *  ▼ File IO
         *  • Settings
         *  • Network
         *  • GameRules
         *
         */

        private string @settingsPath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"c-gomoku");
        //   private NetworkClient client = new NetworkClient();

        private enum e_active
        {
            none = 0,
            server = 1,
            client = 2,
        }

        private e_active playerType;

        private int[] playarea =
        {
            0,0,0,0,0,0,0,0,0,
        };

        private bool playerWin = false;
        private bool pContinue = false;
        private int playpeace = 0;

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(600, 600);
            gbxPlayArea.Location = new Point(12, 12);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // This essentially creates a class, known as pg (type ServerForm)

            //MessageBox.Show("LOL", Text);

            //if()
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // this.Hide();
            // string @path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // System.Diagnostics.Debug.WriteLine(path);
            /*
            MessageBox.Show("Random ass message, lol!");
            string @path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            System.Diagnostics.Debug.WriteLine(path);

            Task.Run(() =>
            {
                var dialogResult = MessageBox.Show("Message", "Title", MessageBoxButtons.OKCancel);
                if(dialogResult == DialogResult.OK)
                    MessageBox.Show("OK CliWcked");
                else
                    MessageBox.Show("Cancel Clicked");
            });
            */
            //  client.sendData("Frick");
            //
            //comboBoxFiller('x');
            /*
            cBx1.Items.Clear();
            cBx1.Enabled = false;
            cBx1.Text = 'X'.ToString();*/
            pContinue = true;
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            playerWin = false;
            playarea = new int[]{
                       0,0,0,0,0,0,0,0,0,
                    };
            HostOptionsForm hostOptions = new HostOptionsForm();
            DialogResult r = hostOptions.ShowDialog();
            if(r == DialogResult.Yes)
            {
                //Server is selected

                initiateServer();
            } else if(r == DialogResult.No)
            {
                //Client is selected
                prepareGrid('O', 2);
                initiateClient();
            }
        }

        // The normal randomgenerator was for some reaseon predictible, thus true random generator.
        private static int trueRandom(int min, int max)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[4];

            rng.GetBytes(buffer);
            int result = BitConverter.ToInt32(buffer, 0);

            return new Random(result).Next(min, max);
        }

        private async void initiateServer()
        {
            try
            {
                // Keep track if player is server or client
                playerType = e_active.server;

                NetworkServer server = new NetworkServer();

                bool call = await server.waitForClient();
                if(!call) return; // break operation

                // random between 1 or 2

                gbxStartMenu.Visible = false;
                gbxStartMenu.Enabled = false;
                gbxPlayArea.Visible = true;
                gbxPlayArea.Enabled = true;

                if(trueRandom(0, 2) > 0)
                {
                    // server starts first
                    server.sendData("@t1");
                    prepareGrid('X', 3);
                    comboBoxFiller('X');

                    initiateServerPlayer(server);
                } else
                {
                    // client starts first
                    server.sendData("@t2");
                    prepareGrid('X', 2);

                    await serverWaitingForPlayerMove(server);
                    initiateServerPlayer(server);
                    comboBoxFiller('X');
                }

                /* TODO:
                 //* Initate game, send prompt to client
                 * Draw who'll start (50/50?)
                 //* Keep track of boards
                 //* Change visisbility between groupboxes!
                 *
                 //* Add pictures to picBoxes,
                 //* On click, call function, applay player symbol. If it is alrady filled, do nothing.
                 //* Call oposing player of change, tell its thier turn.
                 *
                 */
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, Text);
            }
        }

        private async void initiateServerPlayer(NetworkServer server)
        {
            try
            {
                // string call = await server.translateNetworkMessage(server.client);

                // Checks pContinue every 200 ms, and awaits playeraction

                prepareGrid('X');
                // MessageBox.Show(cBx1.Text);

                await Task.Run(() =>
                {
                    do
                    {
                        Thread.Sleep(200);
                    } while(pContinue == false);
                });

                // resets pContinue
                pContinue = false;

                prepareGrid('X', 1);
                System.Diagnostics.Debug.WriteLine("-- PlayerWin Status: " + playerWin);

                // Checks if player has won
                if(playerWin == true)
                {
                    // TODO: Stop game, player won

                    server.sendData("@w" + playpeace + "X");
                    playerWon();
                    server.listener.Stop();
                    server.client.Close();

                    MessageBox.Show("Congratulations, you won over your oponent!", "End of Game");

                    return;
                } else
                {
                    server.sendData("@r" + playpeace + "X");
                }
                //!playpeace // peace chosen by player

                await serverWaitingForPlayerMove(server);
                if(playerWin == true)
                {
                    return;
                }
                initiateServerPlayer(server);
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, Text);
            }
        }

        private void playerWon()
        {
            gbxStartMenu.Visible = true;
            gbxStartMenu.Enabled = true;
            gbxPlayArea.Visible = false;
            gbxPlayArea.Enabled = false;
            prepareGrid(' ', 2);
        }

        private void prepareGrid(char player, int fun = 0)
        {// fun -> function 1 or 2
            try
            {
                ComboBox[] cbxList = {
                    cBx1,cBx2,cBx3,cBx4,cBx5,cBx6,cBx7,cBx8,cBx9
                 };

                if(fun == 0)
                {
                    fun = 0;

                    foreach(ComboBox box in cbxList)
                    {
                        if(box.Text == "" && playarea[fun] == 0)
                        {
                            box.Enabled = true;
                        }
                        fun++;
                    }
                } else if(fun == 1)
                {
                    fun = 0;
                    foreach(ComboBox box in cbxList)
                    {
                        if(box.Text == "" && playarea[fun] == 0)
                        {
                            box.Enabled = false;
                        }
                        fun++;
                    }
                } else if(fun == 2)
                {
                    foreach(ComboBox box in cbxList)
                    {
                        box.Enabled = false;
                        box.Items.Clear();
                        box.Items.Add("");
                        box.SelectedIndex = 0;
                        //box.Text = ' '.ToString();
                    }
                } else if(fun == 3)
                {
                    foreach(ComboBox box in cbxList)
                    {
                        box.Enabled = true;
                        box.Items.Clear();
                        box.Items.Add("");
                        box.SelectedIndex = 0;
                    }
                }
                //! Destroy Net Threads! Close connections after game!
                // TODO READ ABOVE!
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, Text);
            }
        }

        private async Task<bool> serverWaitingForPlayerMove(NetworkServer server)
        {
            // wait for opponets move
            string callback = await server.translateNetworkMessage(server.client);

            // Applies move
            if(callback == null) return false; // TODO: end game

            commandWorker(callback);
            if(playerWin == false)
            {
                return true;
            } else
            {
                server.listener.Stop();
                server.client.Close();
                return false;
                // something
            }
        }

        private string commandWorker(string command)
        {
            ComboBox[] cbxList =
              {
            cBx1,cBx2,cBx3,cBx4,cBx5,cBx6,cBx7,cBx8,cBx9
                 };
            //? MessageBox.Show(command);

            switch(command[1])
            {
                case 'r': // r for replace

                    cbxList[int.Parse(command[2].ToString())].Items.Clear();
                    cbxList[int.Parse(command[2].ToString())].Text = command[3].ToString();
                    cbxList[int.Parse(command[2].ToString())].Enabled = false;
                    //?  MessageBox.Show(command);
                    break;

                case 'w':

                    cbxList[int.Parse(command[2].ToString())].Items.Clear();
                    cbxList[int.Parse(command[2].ToString())].Text = command[3].ToString();
                    cbxList[int.Parse(command[2].ToString())].Enabled = false;
                    playerWon();

                    MessageBox.Show("Tearful Defeat! Your opponent won over you!", "End of Game");
                    playerWin = true;
                    //end game
                    break;

                case 't': // t for turn
                    return command.Substring(1);
                    break;
                    // easly expandible code
            }
            return null;
        }

        private async void initiateClient()
        {
            try
            {
                // Keep track if player is server or client
                playerType = e_active.client;
                NetClientUI clientUI = new NetClientUI();
                DialogResult rClient = clientUI.ShowDialog();

                if(rClient == DialogResult.OK)
                {
                    NetworkClient client = new NetworkClient(clientUI.Ip);

                    await client.connectClient();

                    string res = await client.translateNetworkMessage(client.client);
                    res = commandWorker(res);
                    MessageBox.Show(res);

                    gbxStartMenu.Visible = false;
                    gbxStartMenu.Enabled = false;
                    gbxPlayArea.Visible = true;
                    gbxPlayArea.Enabled = true;

                    if(res == "t2")
                    {
                        prepareGrid('O', 3);
                        initiateClientPlayer(client);
                    } else
                    {
                        prepareGrid('O', 2);
                        await clientWaitingForPlayerMove(client);
                        initiateClientPlayer(client);
                    }

                    comboBoxFiller('O');
                } else if(rClient == DialogResult.Cancel)
                {
                    return;
                }
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, Text);
            }
        }

        private async void initiateClientPlayer(NetworkClient client)
        {
            System.Diagnostics.Debug.WriteLine("-- Client Connected - " + client.client.Connected);
            // string call = await server.translateNetworkMessage(server.client);

            // Checks pContinue every 200 ms, and awaits playeraction

            prepareGrid('O');

            await Task.Run(() =>
            {
                System.Diagnostics.Debug.WriteLine("-- Client Connected waitimg- " + client.client.Connected);

                do
                {
                    Thread.Sleep(200);
                } while(pContinue == false);
            });
            //! BUG: Client never fully cloeses!

            // resets pContinue
            pContinue = false;

            prepareGrid('O', 1);

            System.Diagnostics.Debug.WriteLine("-- PlayerWin Status: " + playerWin);
            // Checks if player has won
            if(playerWin == true)
            {
                // TODO: Stop game, player won

                client.sendData("@w" + playpeace + "X");
                playerWon();

                MessageBox.Show("-- Closing client 1 --");
                System.Diagnostics.Debug.WriteLine("-- Client Connected bef 2--" + client.client.Connected);

                client.client.Close();
                System.Diagnostics.Debug.WriteLine("-- Client Connected af 2--" + client.client.Connected);

                return;
            } else
            {
                System.Diagnostics.Debug.WriteLine("-- Shall send msg, " + client.client.Connected + "--");
                client.sendData("@r" + playpeace + "O");
            }
            //!playpeace // peace chosen by player

            await clientWaitingForPlayerMove(client);
            if(playerWin == true)
            {
                return;
            }
            initiateClientPlayer(client);
        }

        private async Task<bool> clientWaitingForPlayerMove(NetworkClient client)
        {
            // wait for opponets move
            string callback = await client.translateNetworkMessage(client.client);

            // Applies move
            if(callback == null) return false; // TODO: end game

            commandWorker(callback);
            System.Diagnostics.Debug.WriteLine("-- PlayerWin af commandWorker --" + playerWin);
            if(playerWin == false)
            {
                return true;
            } else
            {
                System.Diagnostics.Debug.WriteLine("-- PlayerWin was true --" + playerWin);

                MessageBox.Show("-- Closing client 1 --");

                System.Diagnostics.Debug.WriteLine("-- Client Connected bef 1--" + client.client.Connected);

                client.client.Close();

                System.Diagnostics.Debug.WriteLine("-- Client Connected af 1--" + client.client.Connected);

                MessageBox.Show("-- Client Connected--" + client.client.Connected);

                return false;
                // something
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(settingsPath, "settings.hc");

            DialogResult r = settingsForm.ShowDialog();

            System.Diagnostics.Debug.WriteLine(r);

            if(r == DialogResult.OK)
            {
                applySettings(settingsForm);
            }
        }

        private void applySettings(SettingsForm settings)
        {
            if(settings.darkmode == "True")
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
                foreach(Control c in this.Controls)
                {
                    UpdateColorControls(c, Color.Black, Color.White);
                }
            } else
            {
                this.BackColor = SystemColors.Control;
                this.ForeColor = SystemColors.ControlText;
                foreach(Control c in this.Controls)
                {
                    UpdateColorControls(c, SystemColors.Control, SystemColors.ControlText);
                }
            }
        }

        // Exits application
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Code 0 are default exit
            Environment.Exit(0);
        }

        public void UpdateColorControls(Control myControl, Color bg, Color fg)
        {
            myControl.BackColor = bg;
            myControl.ForeColor = fg;
            foreach(Control subC in myControl.Controls)
            {
                UpdateColorControls(subC, bg, fg);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\c-gomoku\settings.hc";
                System.Diagnostics.Debug.WriteLine(path);
                SettingsForm settings = new SettingsForm(path);
                bool gotSettings = settings.getSettings(path);

                if(gotSettings)
                {
                    applySettings(settings);
                }
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "Error: Damnation to you!");
            }
        }

        public void comboBoxFiller(char chard, bool fillAll = true, int boxToFill = 0)
        {
            ComboBox[] cbxList =
       {
            cBx1,cBx2,cBx3,cBx4,cBx5,cBx6,cBx7,cBx8,cBx9
        };
            if(fillAll)
            {
                foreach(ComboBox box in cbxList)
                {
                    box.Items.Clear();
                    box.Items.Add("");
                    box.Items.Add(chard);
                }
            } else if(!fillAll)
            {
                cbxList[boxToFill].Enabled = false;
                /* cbxList[boxToFill].Items.Clear();
                 cbxList[boxToFill].Items.Add(chard);*/
            }
        }

        private void cBxAction(int cbx)
        {
            cbx--; // convert from cBx number to array-number
            // playpeace is accessible by server/client runner
            playpeace = cbx;
            System.Diagnostics.Debug.WriteLine("-- cbx " + cbx + " --");
            switch(playerType)
            {
                case e_active.none:
                    break;

                case e_active.server:
                    playarea[cbx] = (int)e_active.server;
                    comboBoxFiller('X', false, cbx);
                    checkForRow((int)e_active.server);

                    break;

                case e_active.client:
                    playarea[cbx] = (int)e_active.client;
                    comboBoxFiller('O', false, cbx);
                    checkForRow((int)e_active.client);
                    break;
            }
        }

        private void checkForRow(int checker)
        {
            // colums
            if(playarea[0] == checker && playarea[1] == checker && playarea[2] == checker)
            {
                playerWin = true;
            }
            if(playarea[3] == checker && playarea[4] == checker && playarea[5] == checker)
            {
                playerWin = true;
            }
            if(playarea[6] == checker && playarea[7] == checker && playarea[8] == checker)
            {
                playerWin = true;
            }

            // rows
            if(playarea[0] == checker && playarea[3] == checker && playarea[6] == checker)
            {
                playerWin = true;
            }
            if(playarea[1] == checker && playarea[4] == checker && playarea[7] == checker)
            {
                playerWin = true;
            }
            if(playarea[2] == checker && playarea[5] == checker && playarea[8] == checker)
            {
                playerWin = true;
            }

            // Diagonal
            if(playarea[0] == checker && playarea[4] == checker && playarea[8] == checker)
            {
                playerWin = true;
            }
            if(playarea[2] == checker && playarea[4] == checker && playarea[6] == checker)
            {
                playerWin = true;
            }

            pContinue = true;
        }

        private void cBx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cBx1.SelectedIndex != 0)
            {
                cBxAction(1);
            }
        }

        private void cBx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cBx2.SelectedIndex != 0)
            {
                cBxAction(2);
            }
        }

        private void cBx3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cBx3.SelectedIndex != 0)
            {
                cBxAction(3);
            }
        }

        private void cBx4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cBx4.SelectedIndex != 0)
            {
                cBxAction(4);
            }
        }

        private void cBx5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cBx5.SelectedIndex != 0)
            {
                cBxAction(5);
            }
        }

        private void cBx6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cBx6.SelectedIndex != 0)
            {
                cBxAction(6);
            }
        }

        private void cBx7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cBx7.SelectedIndex != 0)
            {
                cBxAction(7);
            }
        }

        private void cBx8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cBx8.SelectedIndex != 0)
            {
                cBxAction(8);
            }
        }

        private void cBx9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cBx9.SelectedIndex != 0)
            {
                cBxAction(9);
            }
        }
    }
}