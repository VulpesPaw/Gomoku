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
        // global access to settingsPath
        private string @settingsPath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"c-gomoku");

        // Tells local player-code whether they are server or client
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

        private enum e_playStatus
        {
            none = 0,
            win = 1,
            surrender = 2,
            draw = 3
        }

        private e_playStatus playStatus;
        private bool pContinue = false;
        private int playpeace = 0;

        // Allows messageages to be send on Form_closing
        private NetworkServer server;

        private NetworkClient client;

        // Cancellationtokens enables thread-safe cancellation of threads/tasks
        private CancellationTokenSource token;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\c-gomoku\settings.hc";
                System.Diagnostics.Debug.WriteLine(path);
                SettingsForm settings = new SettingsForm(path);
                bool gotSettings = settings.getSettings(path);
                this.Size = new Size(600, 600);
                gbxPlayArea.Location = new Point(12, 12);
                if(gotSettings)
                {
                    applySettings(settings);
                }
            } catch(Exception)
            {
            }
        }

        // Starts game
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            try
            {
                // Resets values
                playStatus = e_playStatus.none;
                playarea = new int[]{
                    0,0,0,0,0,0,0,0,0,
                };

                // Brings up hostOptions form
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
            } catch(Exception)
            {
            }
        }

        /* Server
         * ----- */

        private async void initiateServer()
        {
            try
            {
                // Keep track if player is server or client
                playerType = e_active.server;

                server = new NetworkServer();

                gbxStartMenu.Visible = false;
                gbxStartMenu.Enabled = false;
                gbxSW.Location = new Point(12, 12);
                gbxSW.Visible = true;
                gbxSW.Enabled = true;

                // Refreshed canellationToken
                token = new CancellationTokenSource();

                // This part makes the await cancelbel
                bool call = false;
                await Task.Run(async () =>
                {
                    call = await server.waitForClient(token);
                });

                // If watingproccess is canceld, returns to main menu
                if(!call)
                {
                    server.listener.Stop();
                    ResetToMainMenu();
                    return;
                }
             

                gbxSW.Visible = false;
                gbxSW.Enabled = false;

                gbxPlayArea.Visible = true;
                gbxPlayArea.Enabled = true;

                // random between 1 or 2
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
            } catch(Exception err)
            {
                if(err is SocketException || err is IOException)
                {
                    lostConnectionToOpponent();
                }
            }
        }

        // Starts the server-players turn
        private async void initiateServerPlayer(NetworkServer server)
        {
            try
            {
                prepareGrid('X');

                /*
                 * Task.Run makes this proces running in a seperate thread, and thus not freezing the form. Then I'm awaitning this task.
                 * The task per se, will wait for the player to make a move, thus setting pContinue to True
                 */

                // Checks pContinue every 200 ms, and awaits playeraction
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

                // Checks if player has won or surrended
                if(playStatus == e_playStatus.win)
                {
                    server.sendData("@w" + playpeace + "X");
                    ResetToMainMenu();
                    server.listener.Stop();
                    server.client.Close();

                    MessageBox.Show("Congratulations, you won over your oponent!", "End of Game");

                    return;
                }
                if(playStatus == e_playStatus.surrender)
                {
                    server.sendData("@s");
                    ResetToMainMenu();
                    return;
                } else if(playStatus == e_playStatus.draw)
                {
                    Task.Run(() =>
                    {
                        MessageBox.Show("Game resulted in a Draw, none of you losers won =P", "Game Draw");
                    });
                    server.sendData("@d");
                    ResetToMainMenu();
                    return;
                } else
                {
                    server.sendData("@r" + playpeace + "X");
                }

                await serverWaitingForPlayerMove(server);
                if(playStatus == e_playStatus.surrender)
                {
                    server.sendData("@s");
                    ResetToMainMenu();
                    return;
                }
                if(playStatus == e_playStatus.win)
                {
                    return;
                }
                initiateServerPlayer(server);
            } catch(Exception err)
            {
                if(err is SocketException || err is IOException)
                {
                    lostConnectionToOpponent();
                }
            }
        }

        private async Task<bool> serverWaitingForPlayerMove(NetworkServer server)
        {
            try
            {
                // wait for opponets move
                string callback = await server.translateNetworkMessage(server.client);

                // Applies move
                if(callback == null) return false;

                // Processes commands
                commandWorker(callback);
                if(playStatus == e_playStatus.none)
                {
                    return true;
                } else
                {
                    server.listener.Stop();
                    server.client.Close();
                    return false;
                }
            } catch(Exception)
            {
                return false;
            }
        }

        /* Client
         * ---- */

        // Starts client session
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
                    client = new NetworkClient(clientUI.Ip);

                    await client.connectClient();

                    if(!client.client.Connected)
                    {
                        return;
                    }
                    string res = await client.translateNetworkMessage(client.client);

                    res = commandWorker(res);

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
                if(err is SocketException || err is IOException)
                {
                    MessageBox.Show("Seems like you have feed a none active IP address, make sure your host is online");
                }
            }
        }

        // Starts client-players turn
        private async void initiateClientPlayer(NetworkClient client)
        {
            try
            {
                prepareGrid('O');

                // Checks pContinue every 200 ms, and awaits playeraction
                await Task.Run(() =>
               {
                   do
                   {
                       Thread.Sleep(200);
                   } while(pContinue == false);
               });

                // resets pContinue
                pContinue = false;

                prepareGrid('O', 1);

                // Checks play statuses
                if(playStatus == e_playStatus.win)
                {
                    client.sendData("@w" + playpeace + "X");
                    ResetToMainMenu();

                    client.client.Close();
                    MessageBox.Show("Congratulations, you won over your oponent!", "End of Game");
                    return;
                }
                if(playStatus == e_playStatus.surrender)
                {
                    client.sendData("@s");
                    ResetToMainMenu();
                    return;
                } else if(playStatus == e_playStatus.draw)
                {
                    Task.Run(() =>
                    {
                        MessageBox.Show("Game resulted in a Draw, none of you losers won =P", "Game Draw");
                    });
                    client.sendData("@d");
                    ResetToMainMenu();
                    return;
                } else
                {
                    client.sendData("@r" + playpeace + "O");
                }

                await clientWaitingForPlayerMove(client);

                if(playStatus == e_playStatus.surrender)
                {
                    client.sendData("@s");
                    ResetToMainMenu();
                    return;
                }
                if(playStatus == e_playStatus.win)
                {
                    return;
                }
                initiateClientPlayer(client);
            } catch(Exception err)
            {
                if(err is SocketException || err is IOException)
                {
                    lostConnectionToOpponent();
                }
            }
        }

        // Client waiting for server-player to make a move
        private async Task<bool> clientWaitingForPlayerMove(NetworkClient client)
        {
            try
            {
                // wait for opponets move
                string callback = await client.translateNetworkMessage(client.client);

                // Applies move
                if(callback == null) return false;

                commandWorker(callback);

                if(playStatus == e_playStatus.none)
                {
                    return true;
                } else
                {
                    client.client.Close();

                    return false;
                }
            } catch(Exception err)
            {
                return false;
            }
        }

        // Handles diffetent commands recieved over the network
        private string commandWorker(string command)
        {
            try
            {
                ComboBox[] cbxList =
                {
                    cBx1,cBx2,cBx3,cBx4,cBx5,cBx6,cBx7,cBx8,cBx9
                };
                switch(command[1])
                {
                    case 'r': // r - replace

                        cbxList[int.Parse(command[2].ToString())].Items.Clear();
                        cbxList[int.Parse(command[2].ToString())].Text = command[3].ToString();
                        cbxList[int.Parse(command[2].ToString())].Enabled = false;

                        break;

                    case 'w': // w - win

                        cbxList[int.Parse(command[2].ToString())].Items.Clear();
                        cbxList[int.Parse(command[2].ToString())].Text = command[3].ToString();
                        cbxList[int.Parse(command[2].ToString())].Enabled = false;
                        ResetToMainMenu();

                        MessageBox.Show("Tearful Defeat! Your opponent won over you!", "End of Game");
                        playStatus = e_playStatus.win;
                        break;

                    case 'n': // n - network
                        if(command[2] == '0')
                        {
                            lostConnectionToOpponent();
                        }
                        break;

                    case 's': // s - surrender
                        ResetToMainMenu();
                        MessageBox.Show("Your opponent surrenderd, you won!", "End of Game");
                        playStatus = e_playStatus.win;
                        break;

                    case 'd': // d - draw
                        ResetToMainMenu();
                        MessageBox.Show("Game resulted in a draw, both of you lost looser!", "Gameresult Draw");
                        playStatus = e_playStatus.win;
                        break;

                    case 't': // t for turn
                        return command.Substring(1);
                        break;
                }
                return null;
            } catch(Exception)
            {
                return null;
            }
        }

        /* Settings
         * ----- */

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(settingsPath, "settings.hc");

            DialogResult r = settingsForm.ShowDialog();

            if(r == DialogResult.OK)
            {
                applySettings(settingsForm);
            }
        }

        private void applySettings(SettingsForm settings)
        {
            try
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
                if(settings.gameTag == "")
                {
                    lblGameTag.Text = "";
                } else
                {
                    lblGameTag.Text = "Welcome: " + settings.gameTag;
                    lblUndertag.Text = "Ready to beat some noobs?";
                }
            } catch(Exception)
            {
                MessageBox.Show("An error ocured while applieng settings", Text);
            }
        }

        // Updates UI colours
        public void UpdateColorControls(Control myControl, Color bg, Color fg)
        {
            try
            {
                myControl.BackColor = bg;
                myControl.ForeColor = fg;
                foreach(Control subC in myControl.Controls)
                {
                    UpdateColorControls(subC, bg, fg);
                }
            } catch(Exception)
            {
            }
        }

        // Fills comboxes with values
        public void comboBoxFiller(char chard, bool fillAll = true, int boxToFill = 0)
        {
            try
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
                }
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, Text);
            }
        }

        /* Grid / UI handlers
         * ----- */

        private void prepareGrid(char player, int fun = 0)
        {
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
            } catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        private void ResetToMainMenu()
        {
            gbxStartMenu.Visible = true;
            gbxStartMenu.Enabled = true;
            gbxPlayArea.Visible = false;
            gbxPlayArea.Enabled = false;
            gbxSW.Enabled = false;
            gbxSW.Visible = false;
            prepareGrid(' ', 2);
        }

        // The normal randomgenerator was for some reaseon predictible, thus true random generator. Normally used for crypto seeds.
        private static int trueRandom(int min, int max)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[4];

            rng.GetBytes(buffer);
            int result = BitConverter.ToInt32(buffer, 0);

            return new Random(result).Next(min, max);
        }

        /* Combobox handlers
         * ----- */

        // Handles combobox actions for different situations
        private void cBxAction(int cbx)
        {
            try
            {
                cbx--;
                playpeace = cbx;
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
            } catch(Exception)
            {
            }
        }

        // Checks for playerwin and for draw
        private void checkForRow(int checker)
        {
            try
            {
                // colums
                if(playarea[0] == checker && playarea[1] == checker && playarea[2] == checker)
                {
                    playStatus = e_playStatus.win;
                }
                if(playarea[3] == checker && playarea[4] == checker && playarea[5] == checker)
                {
                    playStatus = e_playStatus.win;
                }
                if(playarea[6] == checker && playarea[7] == checker && playarea[8] == checker)
                {
                    playStatus = e_playStatus.win;
                }

                // rows
                if(playarea[0] == checker && playarea[3] == checker && playarea[6] == checker)
                {
                    playStatus = e_playStatus.win;
                }
                if(playarea[1] == checker && playarea[4] == checker && playarea[7] == checker)
                {
                    playStatus = e_playStatus.win;
                }
                if(playarea[2] == checker && playarea[5] == checker && playarea[8] == checker)
                {
                    playStatus = e_playStatus.win;
                }

                // Diagonal
                if(playarea[0] == checker && playarea[4] == checker && playarea[8] == checker)
                {
                    playStatus = e_playStatus.win;
                }
                if(playarea[2] == checker && playarea[4] == checker && playarea[6] == checker)
                {
                    playStatus = e_playStatus.win;
                }

                // Checks for draw
                bool draw = true;
                foreach(int i in playarea)
                {
                    if(i == 0)
                    {
                        draw = false;
                    }
                }
                if(draw)
                {
                    playStatus = e_playStatus.draw;
                }

                pContinue = true;
            } catch(Exception)
            {
                MessageBox.Show("Winn checkproccess threw an Error", Text);
            }
        }

        /* Comboboxes
         * ---- */

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                token.Cancel();
            } catch(Exception)
            {
            }
        }

        /* Closing Arguments
         * ---- */

        // Exits application
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Code 0 are default exit
            Environment.Exit(0);
        }

        // Surrenders
        private void btnSurrender_Click(object sender, EventArgs e)
        {
            // Shows messagebox while rest of code can exicute
            Task.Run(() =>
            {
                MessageBox.Show("You have surrenderd, your opponent won!", "End of Game");
            });

            playStatus = e_playStatus.surrender;
            pContinue = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(playerType == e_active.client)
            {
                client.sendData("@s");
            } else if(playerType == e_active.server)
            {
                server.sendData("@s");
            }
        }

        private void lostConnectionToOpponent()
        {
            ResetToMainMenu();
            MessageBox.Show("It seems you lost connection to your opponent, therefore the game canceled", "Lost connection to opponent");
        }
    }
}