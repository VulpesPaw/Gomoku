
namespace Gomoku
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.gbxStartMenu = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.cBx1 = new System.Windows.Forms.ComboBox();
            this.gbxPlayArea = new System.Windows.Forms.GroupBox();
            this.cBx5 = new System.Windows.Forms.ComboBox();
            this.cBx4 = new System.Windows.Forms.ComboBox();
            this.cBx7 = new System.Windows.Forms.ComboBox();
            this.cBx8 = new System.Windows.Forms.ComboBox();
            this.cBx9 = new System.Windows.Forms.ComboBox();
            this.cBx6 = new System.Windows.Forms.ComboBox();
            this.cBx3 = new System.Windows.Forms.ComboBox();
            this.cBx2 = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSurrender = new System.Windows.Forms.Button();
            this.gbxSW = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblGameTag = new System.Windows.Forms.Label();
            this.lblUndertag = new System.Windows.Forms.Label();
            this.gbxStartMenu.SuspendLayout();
            this.gbxPlayArea.SuspendLayout();
            this.gbxSW.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(256, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Gomoku";
            // 
            // gbxStartMenu
            // 
            this.gbxStartMenu.Controls.Add(this.lblUndertag);
            this.gbxStartMenu.Controls.Add(this.lblGameTag);
            this.gbxStartMenu.Controls.Add(this.btnExit);
            this.gbxStartMenu.Controls.Add(this.btnSettings);
            this.gbxStartMenu.Controls.Add(this.btnStartGame);
            this.gbxStartMenu.Controls.Add(this.label1);
            this.gbxStartMenu.Location = new System.Drawing.Point(12, 12);
            this.gbxStartMenu.Name = "gbxStartMenu";
            this.gbxStartMenu.Size = new System.Drawing.Size(560, 537);
            this.gbxStartMenu.TabIndex = 3;
            this.gbxStartMenu.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(241, 385);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(241, 305);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnStartGame
            // 
            this.btnStartGame.Location = new System.Drawing.Point(241, 225);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(75, 23);
            this.btnStartGame.TabIndex = 3;
            this.btnStartGame.Text = "Start Game";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // cBx1
            // 
            this.cBx1.Enabled = false;
            this.cBx1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBx1.FormattingEnabled = true;
            this.cBx1.Items.AddRange(new object[] {
            " ",
            "X",
            "O"});
            this.cBx1.Location = new System.Drawing.Point(150, 75);
            this.cBx1.Name = "cBx1";
            this.cBx1.Size = new System.Drawing.Size(100, 84);
            this.cBx1.TabIndex = 4;
            this.cBx1.SelectedIndexChanged += new System.EventHandler(this.cBx1_SelectedIndexChanged);
            // 
            // gbxPlayArea
            // 
            this.gbxPlayArea.Controls.Add(this.cBx5);
            this.gbxPlayArea.Controls.Add(this.cBx4);
            this.gbxPlayArea.Controls.Add(this.cBx7);
            this.gbxPlayArea.Controls.Add(this.cBx8);
            this.gbxPlayArea.Controls.Add(this.cBx9);
            this.gbxPlayArea.Controls.Add(this.cBx6);
            this.gbxPlayArea.Controls.Add(this.cBx3);
            this.gbxPlayArea.Controls.Add(this.cBx2);
            this.gbxPlayArea.Controls.Add(this.cBx1);
            this.gbxPlayArea.Controls.Add(this.lblStatus);
            this.gbxPlayArea.Controls.Add(this.label4);
            this.gbxPlayArea.Controls.Add(this.label3);
            this.gbxPlayArea.Controls.Add(this.label2);
            this.gbxPlayArea.Controls.Add(this.btnSurrender);
            this.gbxPlayArea.Enabled = false;
            this.gbxPlayArea.Location = new System.Drawing.Point(600, 12);
            this.gbxPlayArea.Name = "gbxPlayArea";
            this.gbxPlayArea.Size = new System.Drawing.Size(560, 537);
            this.gbxPlayArea.TabIndex = 3;
            this.gbxPlayArea.TabStop = false;
            this.gbxPlayArea.Visible = false;
            // 
            // cBx5
            // 
            this.cBx5.Enabled = false;
            this.cBx5.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBx5.FormattingEnabled = true;
            this.cBx5.Items.AddRange(new object[] {
            " ",
            "X",
            "O"});
            this.cBx5.Location = new System.Drawing.Point(292, 225);
            this.cBx5.Name = "cBx5";
            this.cBx5.Size = new System.Drawing.Size(100, 84);
            this.cBx5.TabIndex = 4;
            this.cBx5.SelectedIndexChanged += new System.EventHandler(this.cBx5_SelectedIndexChanged);
            // 
            // cBx4
            // 
            this.cBx4.Enabled = false;
            this.cBx4.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBx4.FormattingEnabled = true;
            this.cBx4.Items.AddRange(new object[] {
            " ",
            "X",
            "O"});
            this.cBx4.Location = new System.Drawing.Point(292, 75);
            this.cBx4.Name = "cBx4";
            this.cBx4.Size = new System.Drawing.Size(100, 84);
            this.cBx4.TabIndex = 4;
            this.cBx4.SelectedIndexChanged += new System.EventHandler(this.cBx4_SelectedIndexChanged);
            // 
            // cBx7
            // 
            this.cBx7.Enabled = false;
            this.cBx7.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBx7.FormattingEnabled = true;
            this.cBx7.Items.AddRange(new object[] {
            " ",
            "X",
            "O"});
            this.cBx7.Location = new System.Drawing.Point(434, 75);
            this.cBx7.Name = "cBx7";
            this.cBx7.Size = new System.Drawing.Size(100, 84);
            this.cBx7.TabIndex = 4;
            this.cBx7.SelectedIndexChanged += new System.EventHandler(this.cBx7_SelectedIndexChanged);
            // 
            // cBx8
            // 
            this.cBx8.Enabled = false;
            this.cBx8.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBx8.FormattingEnabled = true;
            this.cBx8.Items.AddRange(new object[] {
            " ",
            "X",
            "O"});
            this.cBx8.Location = new System.Drawing.Point(434, 225);
            this.cBx8.Name = "cBx8";
            this.cBx8.Size = new System.Drawing.Size(100, 84);
            this.cBx8.TabIndex = 4;
            this.cBx8.SelectedIndexChanged += new System.EventHandler(this.cBx8_SelectedIndexChanged);
            // 
            // cBx9
            // 
            this.cBx9.Enabled = false;
            this.cBx9.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBx9.FormattingEnabled = true;
            this.cBx9.Items.AddRange(new object[] {
            " ",
            "X",
            "O"});
            this.cBx9.Location = new System.Drawing.Point(434, 375);
            this.cBx9.Name = "cBx9";
            this.cBx9.Size = new System.Drawing.Size(100, 84);
            this.cBx9.TabIndex = 4;
            this.cBx9.SelectedIndexChanged += new System.EventHandler(this.cBx9_SelectedIndexChanged);
            // 
            // cBx6
            // 
            this.cBx6.Enabled = false;
            this.cBx6.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBx6.FormattingEnabled = true;
            this.cBx6.Items.AddRange(new object[] {
            " ",
            "X",
            "O"});
            this.cBx6.Location = new System.Drawing.Point(292, 375);
            this.cBx6.Name = "cBx6";
            this.cBx6.Size = new System.Drawing.Size(100, 84);
            this.cBx6.TabIndex = 4;
            this.cBx6.SelectedIndexChanged += new System.EventHandler(this.cBx6_SelectedIndexChanged);
            // 
            // cBx3
            // 
            this.cBx3.Enabled = false;
            this.cBx3.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBx3.FormattingEnabled = true;
            this.cBx3.Items.AddRange(new object[] {
            " ",
            "X",
            "O"});
            this.cBx3.Location = new System.Drawing.Point(150, 375);
            this.cBx3.Name = "cBx3";
            this.cBx3.Size = new System.Drawing.Size(100, 84);
            this.cBx3.TabIndex = 4;
            this.cBx3.SelectedIndexChanged += new System.EventHandler(this.cBx3_SelectedIndexChanged);
            // 
            // cBx2
            // 
            this.cBx2.Enabled = false;
            this.cBx2.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBx2.FormattingEnabled = true;
            this.cBx2.Items.AddRange(new object[] {
            " ",
            "X",
            "O"});
            this.cBx2.Location = new System.Drawing.Point(150, 225);
            this.cBx2.Name = "cBx2";
            this.cBx2.Size = new System.Drawing.Size(100, 84);
            this.cBx2.TabIndex = 4;
            this.cBx2.SelectedIndexChanged += new System.EventHandler(this.cBx2_SelectedIndexChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(37, 262);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(92, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Waiting for Server";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "its value to your mark";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Press any box and change";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Status";
            // 
            // btnSurrender
            // 
            this.btnSurrender.Location = new System.Drawing.Point(46, 413);
            this.btnSurrender.Name = "btnSurrender";
            this.btnSurrender.Size = new System.Drawing.Size(75, 23);
            this.btnSurrender.TabIndex = 1;
            this.btnSurrender.Text = "Surrender";
            this.btnSurrender.UseVisualStyleBackColor = true;
            this.btnSurrender.Click += new System.EventHandler(this.btnSurrender_Click);
            // 
            // gbxSW
            // 
            this.gbxSW.Controls.Add(this.btnCancel);
            this.gbxSW.Controls.Add(this.label5);
            this.gbxSW.Enabled = false;
            this.gbxSW.Location = new System.Drawing.Point(1166, 12);
            this.gbxSW.Name = "gbxSW";
            this.gbxSW.Size = new System.Drawing.Size(560, 537);
            this.gbxSW.TabIndex = 3;
            this.gbxSW.TabStop = false;
            this.gbxSW.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(246, 316);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(204, 262);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Waiting for a player to connect...";
            // 
            // lblGameTag
            // 
            this.lblGameTag.AutoSize = true;
            this.lblGameTag.Location = new System.Drawing.Point(238, 167);
            this.lblGameTag.Name = "lblGameTag";
            this.lblGameTag.Size = new System.Drawing.Size(64, 13);
            this.lblGameTag.TabIndex = 4;
            this.lblGameTag.Text = "lblGameTag";
            this.lblGameTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUndertag
            // 
            this.lblUndertag.AutoSize = true;
            this.lblUndertag.Location = new System.Drawing.Point(238, 190);
            this.lblUndertag.Name = "lblUndertag";
            this.lblUndertag.Size = new System.Drawing.Size(0, 13);
            this.lblUndertag.TabIndex = 4;
            this.lblUndertag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1739, 556);
            this.Controls.Add(this.gbxSW);
            this.Controls.Add(this.gbxPlayArea);
            this.Controls.Add(this.gbxStartMenu);
            this.Name = "Form1";
            this.Text = "Gomoku";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbxStartMenu.ResumeLayout(false);
            this.gbxStartMenu.PerformLayout();
            this.gbxPlayArea.ResumeLayout(false);
            this.gbxPlayArea.PerformLayout();
            this.gbxSW.ResumeLayout(false);
            this.gbxSW.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbxStartMenu;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox gbxPlayArea;
        private System.Windows.Forms.Button btnSurrender;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBx1;
        private System.Windows.Forms.ComboBox cBx5;
        private System.Windows.Forms.ComboBox cBx4;
        private System.Windows.Forms.ComboBox cBx7;
        private System.Windows.Forms.ComboBox cBx8;
        private System.Windows.Forms.ComboBox cBx9;
        private System.Windows.Forms.ComboBox cBx6;
        private System.Windows.Forms.ComboBox cBx3;
        private System.Windows.Forms.ComboBox cBx2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbxSW;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblGameTag;
        private System.Windows.Forms.Label lblUndertag;
    }
}

