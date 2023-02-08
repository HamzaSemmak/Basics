using sorec_gamma.IHMs.ComposantsGraphique;
using System.Windows.Forms;

namespace sorec_gamma.IHMs
{
    partial class ClientForm
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
            if (disposing && (components != null))
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClient = new System.Windows.Forms.Button();
            this.btnJournal = new System.Windows.Forms.Button();
            this.btnPrepose = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblwatch = new System.Windows.Forms.Label();
            this.etatConnNokPB = new System.Windows.Forms.PictureBox();
            this.etatConnOKPB = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblIp = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblGuichet = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new sorec_gamma.IHMs.ComposantsGraphique.GradientPanel();
            this.headerLbl = new System.Windows.Forms.Label();
            this.buttonZ1 = new sorec_gamma.ButtonZ();
            this.panelVoucher = new System.Windows.Forms.Panel();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.chequeJeuBtn = new System.Windows.Forms.Button();
            this.panelPaiement = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonZ13 = new sorec_gamma.ButtonZ();
            this.button5 = new System.Windows.Forms.Button();
            this.panelCaisse2 = new System.Windows.Forms.Panel();
            this.buttonZ47 = new sorec_gamma.ButtonZ();
            this.buttonZ49 = new sorec_gamma.ButtonZ();
            this.buttonZ43 = new sorec_gamma.ButtonZ();
            this.buttonZ48 = new sorec_gamma.ButtonZ();
            this.buttonZ42 = new sorec_gamma.ButtonZ();
            this.buttonZ41 = new sorec_gamma.ButtonZ();
            this.buttonZ46 = new sorec_gamma.ButtonZ();
            this.buttonZ40 = new sorec_gamma.ButtonZ();
            this.buttonZ45 = new sorec_gamma.ButtonZ();
            this.buttonZ30 = new sorec_gamma.ButtonZ();
            this.buttonZ44 = new sorec_gamma.ButtonZ();
            this.buttonZ29 = new sorec_gamma.ButtonZ();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etatConnNokPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.etatConnOKPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel3.SuspendLayout();
            this.panelVoucher.SuspendLayout();
            this.panelPaiement.SuspendLayout();
            this.panelCaisse2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.panel1.Controls.Add(this.button7);
            this.panel1.Location = new System.Drawing.Point(-8, -1);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1045, 62);
            this.panel1.TabIndex = 65;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Transparent;
            this.button7.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(20, 8);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(82, 44);
            this.button7.TabIndex = 112;
            this.button7.TabStop = false;
            this.button7.Text = "Retour";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(127)))), ((int)(((byte)(228)))));
            this.panel2.Controls.Add(this.btnClient);
            this.panel2.Controls.Add(this.btnJournal);
            this.panel2.Controls.Add(this.btnPrepose);
            this.panel2.Location = new System.Drawing.Point(-8, 63);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1045, 61);
            this.panel2.TabIndex = 86;
            // 
            // btnClient
            // 
            this.btnClient.BackColor = System.Drawing.Color.Transparent;
            this.btnClient.BackgroundImage = global::sorec_gamma.Properties.Resources.btnSelectionMenu_Sel;
            this.btnClient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClient.FlatAppearance.BorderSize = 0;
            this.btnClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClient.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClient.ForeColor = System.Drawing.Color.White;
            this.btnClient.Location = new System.Drawing.Point(676, 10);
            this.btnClient.Margin = new System.Windows.Forms.Padding(2);
            this.btnClient.Name = "btnClient";
            this.btnClient.Size = new System.Drawing.Size(146, 42);
            this.btnClient.TabIndex = 101;
            this.btnClient.Text = "Clients";
            this.btnClient.UseVisualStyleBackColor = false;
            // 
            // btnJournal
            // 
            this.btnJournal.BackColor = System.Drawing.Color.Transparent;
            this.btnJournal.BackgroundImage = global::sorec_gamma.Properties.Resources.btnSelectionMenu_Desel;
            this.btnJournal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnJournal.FlatAppearance.BorderSize = 0;
            this.btnJournal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJournal.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJournal.ForeColor = System.Drawing.Color.Navy;
            this.btnJournal.Location = new System.Drawing.Point(496, 10);
            this.btnJournal.Margin = new System.Windows.Forms.Padding(2);
            this.btnJournal.Name = "btnJournal";
            this.btnJournal.Size = new System.Drawing.Size(151, 42);
            this.btnJournal.TabIndex = 100;
            this.btnJournal.Text = "Journal";
            this.btnJournal.UseVisualStyleBackColor = false;
            this.btnJournal.Click += new System.EventHandler(this.btnJournal_Click);
            // 
            // btnPrepose
            // 
            this.btnPrepose.BackColor = System.Drawing.Color.Transparent;
            this.btnPrepose.BackgroundImage = global::sorec_gamma.Properties.Resources.btnSelectionMenu_Desel;
            this.btnPrepose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrepose.FlatAppearance.BorderSize = 0;
            this.btnPrepose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrepose.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrepose.ForeColor = System.Drawing.Color.Navy;
            this.btnPrepose.Location = new System.Drawing.Point(316, 10);
            this.btnPrepose.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrepose.Name = "btnPrepose";
            this.btnPrepose.Size = new System.Drawing.Size(146, 42);
            this.btnPrepose.TabIndex = 99;
            this.btnPrepose.Text = "Préposé";
            this.btnPrepose.UseVisualStyleBackColor = false;
            this.btnPrepose.Click += new System.EventHandler(this.btnPrepose_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.panel5.Controls.Add(this.lblwatch);
            this.panel5.Controls.Add(this.etatConnNokPB);
            this.panel5.Controls.Add(this.etatConnOKPB);
            this.panel5.Controls.Add(this.pictureBox2);
            this.panel5.Controls.Add(this.lblIp);
            this.panel5.Controls.Add(this.lblVersion);
            this.panel5.Controls.Add(this.lblGuichet);
            this.panel5.Location = new System.Drawing.Point(-8, 716);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1045, 63);
            this.panel5.TabIndex = 94;
            // 
            // lblwatch
            // 
            this.lblwatch.AutoSize = true;
            this.lblwatch.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblwatch.ForeColor = System.Drawing.Color.White;
            this.lblwatch.Location = new System.Drawing.Point(437, 11);
            this.lblwatch.Name = "lblwatch";
            this.lblwatch.Size = new System.Drawing.Size(64, 28);
            this.lblwatch.TabIndex = 122;
            this.lblwatch.Text = "Time";
            // 
            // etatConnNokPB
            // 
            this.etatConnNokPB.BackgroundImage = global::sorec_gamma.Properties.Resources.BtnEtatCon_NOk;
            this.etatConnNokPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.etatConnNokPB.ErrorImage = null;
            this.etatConnNokPB.InitialImage = null;
            this.etatConnNokPB.Location = new System.Drawing.Point(576, 11);
            this.etatConnNokPB.Name = "etatConnNokPB";
            this.etatConnNokPB.Size = new System.Drawing.Size(31, 31);
            this.etatConnNokPB.TabIndex = 121;
            this.etatConnNokPB.TabStop = false;
            // 
            // etatConnOKPB
            // 
            this.etatConnOKPB.BackgroundImage = global::sorec_gamma.Properties.Resources.BtnEtatCon_OK;
            this.etatConnOKPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.etatConnOKPB.ErrorImage = null;
            this.etatConnOKPB.InitialImage = null;
            this.etatConnOKPB.Location = new System.Drawing.Point(576, 11);
            this.etatConnOKPB.Name = "etatConnOKPB";
            this.etatConnOKPB.Size = new System.Drawing.Size(31, 31);
            this.etatConnOKPB.TabIndex = 119;
            this.etatConnOKPB.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::sorec_gamma.Properties.Resources.BtnEtatImp_Ok;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(542, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 31);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 120;
            this.pictureBox2.TabStop = false;
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblIp.ForeColor = System.Drawing.Color.White;
            this.lblIp.Location = new System.Drawing.Point(794, 11);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(260, 28);
            this.lblIp.TabIndex = 69;
            this.lblIp.Text = "Adresse IP :  xx.xx.xx.xx";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(8, 11);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(153, 28);
            this.lblVersion.TabIndex = 68;
            this.lblVersion.Text = "V :  XX.XX.XX";
            // 
            // lblGuichet
            // 
            this.lblGuichet.AutoSize = true;
            this.lblGuichet.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblGuichet.ForeColor = System.Drawing.Color.Red;
            this.lblGuichet.Location = new System.Drawing.Point(165, 11);
            this.lblGuichet.Name = "lblGuichet";
            this.lblGuichet.Size = new System.Drawing.Size(214, 28);
            this.lblGuichet.TabIndex = 67;
            this.lblGuichet.Text = "Guichet :  XXXX - X";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this._TimeTick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.ColorBottom = System.Drawing.Color.Lavender;
            this.panel3.ColorTop = System.Drawing.Color.Navy;
            this.panel3.Controls.Add(this.headerLbl);
            this.panel3.Controls.Add(this.buttonZ1);
            this.panel3.Controls.Add(this.panelVoucher);
            this.panel3.Controls.Add(this.chequeJeuBtn);
            this.panel3.Controls.Add(this.panelPaiement);
            this.panel3.Controls.Add(this.buttonZ13);
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.panelCaisse2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(-8, 126);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1045, 589);
            this.panel3.TabIndex = 85;
            // 
            // headerLbl
            // 
            this.headerLbl.AutoSize = true;
            this.headerLbl.Font = new System.Drawing.Font("Microsoft YaHei UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLbl.ForeColor = System.Drawing.Color.White;
            this.headerLbl.Location = new System.Drawing.Point(394, 24);
            this.headerLbl.Name = "headerLbl";
            this.headerLbl.Size = new System.Drawing.Size(257, 36);
            this.headerLbl.TabIndex = 118;
            this.headerLbl.Text = "Paiement Manuel";
            // 
            // buttonZ1
            // 
            this.buttonZ1.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ1.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.buttonZ1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ1.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ1.BorderWidth = 0;
            this.buttonZ1.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ1.ButtonText = "Imprimer";
            this.buttonZ1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ1.FlatAppearance.BorderSize = 0;
            this.buttonZ1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ1.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ1.ForeColor = System.Drawing.Color.White;
            this.buttonZ1.GradientAngle = 90;
            this.buttonZ1.Location = new System.Drawing.Point(439, 396);
            this.buttonZ1.Margin = new System.Windows.Forms.Padding(2);
            this.buttonZ1.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ1.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ1.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonZ1.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonZ1.Name = "buttonZ1";
            this.buttonZ1.ShowButtontext = true;
            this.buttonZ1.Size = new System.Drawing.Size(116, 56);
            this.buttonZ1.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ1.TabIndex = 117;
            this.buttonZ1.TabStop = false;
            this.buttonZ1.Text = "buttonImprimer";
            this.buttonZ1.TextLocation_X = 11;
            this.buttonZ1.TextLocation_Y = 16;
            this.buttonZ1.Transparent1 = 250;
            this.buttonZ1.Transparent2 = 250;
            this.buttonZ1.UseVisualStyleBackColor = false;
            this.buttonZ1.Click += new System.EventHandler(this.buttonZ1_Click);
            this.buttonZ1.MouseEnter += new System.EventHandler(this.buttonZ1_MouseEnter);
            this.buttonZ1.MouseLeave += new System.EventHandler(this.buttonZ1_MouseLeave);
            // 
            // panelVoucher
            // 
            this.panelVoucher.BackColor = System.Drawing.Color.Transparent;
            this.panelVoucher.Controls.Add(this.textBox9);
            this.panelVoucher.Location = new System.Drawing.Point(276, 232);
            this.panelVoucher.Margin = new System.Windows.Forms.Padding(2);
            this.panelVoucher.Name = "panelVoucher";
            this.panelVoucher.Size = new System.Drawing.Size(478, 136);
            this.panelVoucher.TabIndex = 116;
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.Color.Navy;
            this.textBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox9.ForeColor = System.Drawing.Color.White;
            this.textBox9.Location = new System.Drawing.Point(146, 47);
            this.textBox9.Margin = new System.Windows.Forms.Padding(2);
            this.textBox9.Multiline = true;
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(162, 36);
            this.textBox9.TabIndex = 85;
            this.textBox9.Click += new System.EventHandler(this.textBox9_Click);
            this.textBox9.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox9_MouseClick);
            // 
            // chequeJeuBtn
            // 
            this.chequeJeuBtn.BackColor = System.Drawing.Color.Transparent;
            this.chequeJeuBtn.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.chequeJeuBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chequeJeuBtn.Enabled = false;
            this.chequeJeuBtn.FlatAppearance.BorderSize = 0;
            this.chequeJeuBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chequeJeuBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chequeJeuBtn.ForeColor = System.Drawing.Color.White;
            this.chequeJeuBtn.Location = new System.Drawing.Point(23, 393);
            this.chequeJeuBtn.Margin = new System.Windows.Forms.Padding(2);
            this.chequeJeuBtn.Name = "chequeJeuBtn";
            this.chequeJeuBtn.Size = new System.Drawing.Size(136, 65);
            this.chequeJeuBtn.TabIndex = 111;
            this.chequeJeuBtn.TabStop = false;
            this.chequeJeuBtn.Text = "Chèque Jeu";
            this.chequeJeuBtn.UseVisualStyleBackColor = false;
            this.chequeJeuBtn.Click += new System.EventHandler(this.chequeJeuBtn_Click);
            // 
            // panelPaiement
            // 
            this.panelPaiement.BackColor = System.Drawing.Color.Transparent;
            this.panelPaiement.Controls.Add(this.label4);
            this.panelPaiement.Controls.Add(this.textBox1);
            this.panelPaiement.Controls.Add(this.textBox2);
            this.panelPaiement.Controls.Add(this.textBox3);
            this.panelPaiement.Controls.Add(this.textBox4);
            this.panelPaiement.Controls.Add(this.label9);
            this.panelPaiement.Controls.Add(this.textBox5);
            this.panelPaiement.Controls.Add(this.label7);
            this.panelPaiement.Controls.Add(this.label5);
            this.panelPaiement.Controls.Add(this.label6);
            this.panelPaiement.Location = new System.Drawing.Point(276, 232);
            this.panelPaiement.Margin = new System.Windows.Forms.Padding(2);
            this.panelPaiement.Name = "panelPaiement";
            this.panelPaiement.Size = new System.Drawing.Size(478, 136);
            this.panelPaiement.TabIndex = 115;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(20, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 25);
            this.label4.TabIndex = 88;
            this.label4.Text = "A";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(13, 62);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(60, 36);
            this.textBox1.TabIndex = 77;
            this.textBox1.Click += new System.EventHandler(this.textBox3_Click);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(76, 62);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(94, 36);
            this.textBox2.TabIndex = 83;
            this.textBox2.Click += new System.EventHandler(this.textBox1_Click);
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(174, 62);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(71, 36);
            this.textBox3.TabIndex = 84;
            this.textBox3.Click += new System.EventHandler(this.textBox2_Click);
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(248, 62);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(139, 36);
            this.textBox4.TabIndex = 85;
            this.textBox4.Click += new System.EventHandler(this.textBox4_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(394, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 25);
            this.label9.TabIndex = 93;
            this.label9.Text = "Clé";
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(391, 62);
            this.textBox5.Margin = new System.Windows.Forms.Padding(2);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(79, 36);
            this.textBox5.TabIndex = 86;
            this.textBox5.Click += new System.EventHandler(this.textBox5_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(253, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 25);
            this.label7.TabIndex = 91;
            this.label7.Text = "D";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(98, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 25);
            this.label5.TabIndex = 89;
            this.label5.Text = "B";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(188, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 25);
            this.label6.TabIndex = 90;
            this.label6.Text = "C";
            // 
            // buttonZ13
            // 
            this.buttonZ13.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ13.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.buttonZ13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ13.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ13.BorderWidth = 0;
            this.buttonZ13.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ13.ButtonText = "  Valider";
            this.buttonZ13.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ13.FlatAppearance.BorderSize = 0;
            this.buttonZ13.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ13.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ13.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ13.ForeColor = System.Drawing.Color.White;
            this.buttonZ13.GradientAngle = 90;
            this.buttonZ13.Location = new System.Drawing.Point(439, 396);
            this.buttonZ13.Margin = new System.Windows.Forms.Padding(2);
            this.buttonZ13.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ13.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ13.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonZ13.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonZ13.Name = "buttonZ13";
            this.buttonZ13.ShowButtontext = true;
            this.buttonZ13.Size = new System.Drawing.Size(116, 56);
            this.buttonZ13.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ13.TabIndex = 114;
            this.buttonZ13.TabStop = false;
            this.buttonZ13.Text = "buttonZ13";
            this.buttonZ13.TextLocation_X = 11;
            this.buttonZ13.TextLocation_Y = 16;
            this.buttonZ13.Transparent1 = 250;
            this.buttonZ13.Transparent2 = 250;
            this.buttonZ13.UseVisualStyleBackColor = false;
            this.buttonZ13.Click += new System.EventHandler(this.buttonZ13_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(23, 181);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(136, 65);
            this.button5.TabIndex = 110;
            this.button5.TabStop = false;
            this.button5.Text = "Paiement Jeu";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // panelCaisse2
            // 
            this.panelCaisse2.BackColor = System.Drawing.Color.Transparent;
            this.panelCaisse2.Controls.Add(this.buttonZ47);
            this.panelCaisse2.Controls.Add(this.buttonZ49);
            this.panelCaisse2.Controls.Add(this.buttonZ43);
            this.panelCaisse2.Controls.Add(this.buttonZ48);
            this.panelCaisse2.Controls.Add(this.buttonZ42);
            this.panelCaisse2.Controls.Add(this.buttonZ41);
            this.panelCaisse2.Controls.Add(this.buttonZ46);
            this.panelCaisse2.Controls.Add(this.buttonZ40);
            this.panelCaisse2.Controls.Add(this.buttonZ45);
            this.panelCaisse2.Controls.Add(this.buttonZ30);
            this.panelCaisse2.Controls.Add(this.buttonZ44);
            this.panelCaisse2.Controls.Add(this.buttonZ29);
            this.panelCaisse2.Location = new System.Drawing.Point(769, 146);
            this.panelCaisse2.Margin = new System.Windows.Forms.Padding(2);
            this.panelCaisse2.Name = "panelCaisse2";
            this.panelCaisse2.Size = new System.Drawing.Size(245, 312);
            this.panelCaisse2.TabIndex = 109;
            // 
            // buttonZ47
            // 
            this.buttonZ47.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ47.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ47.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ47.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ47.BorderWidth = 0;
            this.buttonZ47.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ47.ButtonText = "DEL";
            this.buttonZ47.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ47.FlatAppearance.BorderSize = 0;
            this.buttonZ47.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ47.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ47.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ47.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ47.ForeColor = System.Drawing.Color.White;
            this.buttonZ47.GradientAngle = 90;
            this.buttonZ47.Location = new System.Drawing.Point(11, 240);
            this.buttonZ47.Margin = new System.Windows.Forms.Padding(2);
            this.buttonZ47.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ47.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ47.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ47.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ47.Name = "buttonZ47";
            this.buttonZ47.ShowButtontext = true;
            this.buttonZ47.Size = new System.Drawing.Size(64, 64);
            this.buttonZ47.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ47.TabIndex = 167;
            this.buttonZ47.TabStop = false;
            this.buttonZ47.TextLocation_X = 6;
            this.buttonZ47.TextLocation_Y = 17;
            this.buttonZ47.Transparent1 = 250;
            this.buttonZ47.Transparent2 = 250;
            this.buttonZ47.UseVisualStyleBackColor = false;
            this.buttonZ47.Click += new System.EventHandler(this.buttonZ47_Click);
            // 
            // buttonZ49
            // 
            this.buttonZ49.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ49.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ49.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ49.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ49.BorderWidth = 0;
            this.buttonZ49.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ49.ButtonText = " OK";
            this.buttonZ49.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ49.FlatAppearance.BorderSize = 0;
            this.buttonZ49.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ49.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ49.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ49.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ49.ForeColor = System.Drawing.Color.White;
            this.buttonZ49.GradientAngle = 90;
            this.buttonZ49.Location = new System.Drawing.Point(166, 240);
            this.buttonZ49.Margin = new System.Windows.Forms.Padding(2);
            this.buttonZ49.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ49.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ49.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ49.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ49.Name = "buttonZ49";
            this.buttonZ49.ShowButtontext = true;
            this.buttonZ49.Size = new System.Drawing.Size(64, 64);
            this.buttonZ49.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ49.TabIndex = 170;
            this.buttonZ49.TabStop = false;
            this.buttonZ49.TextLocation_X = 6;
            this.buttonZ49.TextLocation_Y = 17;
            this.buttonZ49.Transparent1 = 250;
            this.buttonZ49.Transparent2 = 250;
            this.buttonZ49.UseVisualStyleBackColor = false;
            this.buttonZ49.Click += new System.EventHandler(this.buttonZ49_Click);
            // 
            // buttonZ43
            // 
            this.buttonZ43.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ43.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ43.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ43.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ43.BorderWidth = 0;
            this.buttonZ43.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ43.ButtonText = "  6";
            this.buttonZ43.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ43.FlatAppearance.BorderSize = 0;
            this.buttonZ43.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ43.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ43.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ43.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ43.ForeColor = System.Drawing.Color.White;
            this.buttonZ43.GradientAngle = 90;
            this.buttonZ43.Location = new System.Drawing.Point(166, 89);
            this.buttonZ43.Margin = new System.Windows.Forms.Padding(2);
            this.buttonZ43.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ43.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ43.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ43.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ43.Name = "buttonZ43";
            this.buttonZ43.ShowButtontext = true;
            this.buttonZ43.Size = new System.Drawing.Size(64, 64);
            this.buttonZ43.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ43.TabIndex = 175;
            this.buttonZ43.TabStop = false;
            this.buttonZ43.TextLocation_X = 6;
            this.buttonZ43.TextLocation_Y = 17;
            this.buttonZ43.Transparent1 = 250;
            this.buttonZ43.Transparent2 = 250;
            this.buttonZ43.UseVisualStyleBackColor = false;
            this.buttonZ43.Click += new System.EventHandler(this.buttonZ43_Click);
            // 
            // buttonZ48
            // 
            this.buttonZ48.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ48.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ48.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ48.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ48.BorderWidth = 0;
            this.buttonZ48.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ48.ButtonText = "  0";
            this.buttonZ48.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ48.FlatAppearance.BorderSize = 0;
            this.buttonZ48.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ48.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ48.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ48.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ48.ForeColor = System.Drawing.Color.White;
            this.buttonZ48.GradientAngle = 90;
            this.buttonZ48.Location = new System.Drawing.Point(88, 240);
            this.buttonZ48.Margin = new System.Windows.Forms.Padding(2);
            this.buttonZ48.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ48.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ48.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ48.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ48.Name = "buttonZ48";
            this.buttonZ48.ShowButtontext = true;
            this.buttonZ48.Size = new System.Drawing.Size(64, 64);
            this.buttonZ48.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ48.TabIndex = 168;
            this.buttonZ48.TabStop = false;
            this.buttonZ48.TextLocation_X = 6;
            this.buttonZ48.TextLocation_Y = 17;
            this.buttonZ48.Transparent1 = 250;
            this.buttonZ48.Transparent2 = 250;
            this.buttonZ48.UseVisualStyleBackColor = false;
            this.buttonZ48.Click += new System.EventHandler(this.buttonZ48_Click);
            // 
            // buttonZ42
            // 
            this.buttonZ42.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ42.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ42.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ42.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ42.BorderWidth = 0;
            this.buttonZ42.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ42.ButtonText = "  5";
            this.buttonZ42.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ42.FlatAppearance.BorderSize = 0;
            this.buttonZ42.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ42.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ42.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ42.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ42.ForeColor = System.Drawing.Color.White;
            this.buttonZ42.GradientAngle = 90;
            this.buttonZ42.Location = new System.Drawing.Point(88, 89);
            this.buttonZ42.Margin = new System.Windows.Forms.Padding(2);
            this.buttonZ42.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ42.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ42.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ42.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ42.Name = "buttonZ42";
            this.buttonZ42.ShowButtontext = true;
            this.buttonZ42.Size = new System.Drawing.Size(64, 64);
            this.buttonZ42.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ42.TabIndex = 174;
            this.buttonZ42.TabStop = false;
            this.buttonZ42.TextLocation_X = 6;
            this.buttonZ42.TextLocation_Y = 17;
            this.buttonZ42.Transparent1 = 250;
            this.buttonZ42.Transparent2 = 250;
            this.buttonZ42.UseVisualStyleBackColor = false;
            this.buttonZ42.Click += new System.EventHandler(this.buttonZ42_Click);
            // 
            // buttonZ41
            // 
            this.buttonZ41.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ41.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ41.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ41.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ41.BorderWidth = 0;
            this.buttonZ41.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ41.ButtonText = "  4";
            this.buttonZ41.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ41.FlatAppearance.BorderSize = 0;
            this.buttonZ41.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ41.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ41.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ41.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ41.ForeColor = System.Drawing.Color.White;
            this.buttonZ41.GradientAngle = 90;
            this.buttonZ41.Location = new System.Drawing.Point(11, 89);
            this.buttonZ41.Margin = new System.Windows.Forms.Padding(2);
            this.buttonZ41.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ41.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ41.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ41.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ41.Name = "buttonZ41";
            this.buttonZ41.ShowButtontext = true;
            this.buttonZ41.Size = new System.Drawing.Size(64, 64);
            this.buttonZ41.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ41.TabIndex = 173;
            this.buttonZ41.TabStop = false;
            this.buttonZ41.TextLocation_X = 6;
            this.buttonZ41.TextLocation_Y = 17;
            this.buttonZ41.Transparent1 = 250;
            this.buttonZ41.Transparent2 = 250;
            this.buttonZ41.UseVisualStyleBackColor = false;
            this.buttonZ41.Click += new System.EventHandler(this.buttonZ41_Click);
            // 
            // buttonZ46
            // 
            this.buttonZ46.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ46.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ46.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ46.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ46.BorderWidth = 0;
            this.buttonZ46.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ46.ButtonText = "  9";
            this.buttonZ46.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ46.FlatAppearance.BorderSize = 0;
            this.buttonZ46.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ46.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ46.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ46.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ46.ForeColor = System.Drawing.Color.White;
            this.buttonZ46.GradientAngle = 90;
            this.buttonZ46.Location = new System.Drawing.Point(166, 164);
            this.buttonZ46.Margin = new System.Windows.Forms.Padding(2);
            this.buttonZ46.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ46.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ46.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ46.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ46.Name = "buttonZ46";
            this.buttonZ46.ShowButtontext = true;
            this.buttonZ46.Size = new System.Drawing.Size(64, 64);
            this.buttonZ46.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ46.TabIndex = 166;
            this.buttonZ46.TabStop = false;
            this.buttonZ46.TextLocation_X = 6;
            this.buttonZ46.TextLocation_Y = 17;
            this.buttonZ46.Transparent1 = 250;
            this.buttonZ46.Transparent2 = 250;
            this.buttonZ46.UseVisualStyleBackColor = false;
            this.buttonZ46.Click += new System.EventHandler(this.buttonZ46_Click);
            // 
            // buttonZ40
            // 
            this.buttonZ40.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ40.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ40.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ40.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ40.BorderWidth = 0;
            this.buttonZ40.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ40.ButtonText = "  3";
            this.buttonZ40.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ40.FlatAppearance.BorderSize = 0;
            this.buttonZ40.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ40.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ40.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ40.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ40.ForeColor = System.Drawing.Color.White;
            this.buttonZ40.GradientAngle = 90;
            this.buttonZ40.Location = new System.Drawing.Point(166, 13);
            this.buttonZ40.Margin = new System.Windows.Forms.Padding(2);
            this.buttonZ40.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ40.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ40.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ40.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ40.Name = "buttonZ40";
            this.buttonZ40.ShowButtontext = true;
            this.buttonZ40.Size = new System.Drawing.Size(64, 64);
            this.buttonZ40.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ40.TabIndex = 172;
            this.buttonZ40.TabStop = false;
            this.buttonZ40.TextLocation_X = 6;
            this.buttonZ40.TextLocation_Y = 17;
            this.buttonZ40.Transparent1 = 250;
            this.buttonZ40.Transparent2 = 250;
            this.buttonZ40.UseVisualStyleBackColor = false;
            this.buttonZ40.Click += new System.EventHandler(this.buttonZ40_Click);
            // 
            // buttonZ45
            // 
            this.buttonZ45.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ45.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ45.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ45.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ45.BorderWidth = 0;
            this.buttonZ45.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ45.ButtonText = "  8";
            this.buttonZ45.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ45.FlatAppearance.BorderSize = 0;
            this.buttonZ45.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ45.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ45.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ45.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ45.ForeColor = System.Drawing.Color.White;
            this.buttonZ45.GradientAngle = 90;
            this.buttonZ45.Location = new System.Drawing.Point(88, 164);
            this.buttonZ45.Margin = new System.Windows.Forms.Padding(2);
            this.buttonZ45.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ45.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ45.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ45.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ45.Name = "buttonZ45";
            this.buttonZ45.ShowButtontext = true;
            this.buttonZ45.Size = new System.Drawing.Size(64, 64);
            this.buttonZ45.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ45.TabIndex = 165;
            this.buttonZ45.TabStop = false;
            this.buttonZ45.TextLocation_X = 6;
            this.buttonZ45.TextLocation_Y = 17;
            this.buttonZ45.Transparent1 = 250;
            this.buttonZ45.Transparent2 = 250;
            this.buttonZ45.UseVisualStyleBackColor = false;
            this.buttonZ45.Click += new System.EventHandler(this.buttonZ45_Click);
            // 
            // buttonZ30
            // 
            this.buttonZ30.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ30.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ30.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ30.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ30.BorderWidth = 0;
            this.buttonZ30.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ30.ButtonText = "  2";
            this.buttonZ30.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ30.FlatAppearance.BorderSize = 0;
            this.buttonZ30.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ30.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ30.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ30.ForeColor = System.Drawing.Color.White;
            this.buttonZ30.GradientAngle = 90;
            this.buttonZ30.Location = new System.Drawing.Point(88, 13);
            this.buttonZ30.Margin = new System.Windows.Forms.Padding(2);
            this.buttonZ30.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ30.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ30.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ30.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ30.Name = "buttonZ30";
            this.buttonZ30.ShowButtontext = true;
            this.buttonZ30.Size = new System.Drawing.Size(64, 64);
            this.buttonZ30.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ30.TabIndex = 171;
            this.buttonZ30.TabStop = false;
            this.buttonZ30.TextLocation_X = 6;
            this.buttonZ30.TextLocation_Y = 17;
            this.buttonZ30.Transparent1 = 250;
            this.buttonZ30.Transparent2 = 250;
            this.buttonZ30.UseVisualStyleBackColor = false;
            this.buttonZ30.Click += new System.EventHandler(this.buttonZ30_Click);
            // 
            // buttonZ44
            // 
            this.buttonZ44.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ44.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ44.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ44.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ44.BorderWidth = 0;
            this.buttonZ44.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ44.ButtonText = "  7";
            this.buttonZ44.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ44.FlatAppearance.BorderSize = 0;
            this.buttonZ44.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ44.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ44.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ44.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ44.ForeColor = System.Drawing.Color.White;
            this.buttonZ44.GradientAngle = 90;
            this.buttonZ44.Location = new System.Drawing.Point(11, 164);
            this.buttonZ44.Margin = new System.Windows.Forms.Padding(2);
            this.buttonZ44.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ44.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ44.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ44.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ44.Name = "buttonZ44";
            this.buttonZ44.ShowButtontext = true;
            this.buttonZ44.Size = new System.Drawing.Size(64, 64);
            this.buttonZ44.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ44.TabIndex = 164;
            this.buttonZ44.TabStop = false;
            this.buttonZ44.TextLocation_X = 6;
            this.buttonZ44.TextLocation_Y = 17;
            this.buttonZ44.Transparent1 = 250;
            this.buttonZ44.Transparent2 = 250;
            this.buttonZ44.UseVisualStyleBackColor = false;
            this.buttonZ44.Click += new System.EventHandler(this.buttonZ44_Click);
            // 
            // buttonZ29
            // 
            this.buttonZ29.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ29.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ29.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ29.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ29.BorderWidth = 0;
            this.buttonZ29.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ29.ButtonText = "  1";
            this.buttonZ29.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ29.FlatAppearance.BorderSize = 0;
            this.buttonZ29.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ29.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ29.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ29.ForeColor = System.Drawing.Color.White;
            this.buttonZ29.GradientAngle = 90;
            this.buttonZ29.Location = new System.Drawing.Point(11, 13);
            this.buttonZ29.Margin = new System.Windows.Forms.Padding(2);
            this.buttonZ29.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ29.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ29.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ29.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.buttonZ29.Name = "buttonZ29";
            this.buttonZ29.ShowButtontext = true;
            this.buttonZ29.Size = new System.Drawing.Size(64, 64);
            this.buttonZ29.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ29.TabIndex = 169;
            this.buttonZ29.TabStop = false;
            this.buttonZ29.TextLocation_X = 6;
            this.buttonZ29.TextLocation_Y = 17;
            this.buttonZ29.Transparent1 = 250;
            this.buttonZ29.Transparent2 = 250;
            this.buttonZ29.UseVisualStyleBackColor = false;
            this.buttonZ29.Click += new System.EventHandler(this.buttonZ29_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(212, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 27);
            this.label1.TabIndex = 70;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1040, 634);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etatConnNokPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.etatConnOKPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelVoucher.ResumeLayout(false);
            this.panelVoucher.PerformLayout();
            this.panelPaiement.ResumeLayout(false);
            this.panelPaiement.PerformLayout();
            this.panelCaisse2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClient;
        private System.Windows.Forms.Button btnJournal;
        private System.Windows.Forms.Button btnPrepose;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblGuichet;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelCaisse2;
        private ButtonZ buttonZ47;
        private ButtonZ buttonZ49;
        private ButtonZ buttonZ43;
        private ButtonZ buttonZ48;
        private ButtonZ buttonZ42;
        private ButtonZ buttonZ41;
        private ButtonZ buttonZ46;
        private ButtonZ buttonZ40;
        private ButtonZ buttonZ45;
        private ButtonZ buttonZ30;
        private ButtonZ buttonZ44;
        private ButtonZ buttonZ29;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button chequeJeuBtn;
        private ButtonZ buttonZ13;
        private System.Windows.Forms.Panel panelPaiement;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelVoucher;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.PictureBox etatConnOKPB;
        private System.Windows.Forms.PictureBox pictureBox2;
        private GradientPanel panel3;
        private ButtonZ buttonZ1;
        private Label headerLbl;
        private PictureBox etatConnNokPB;
        private Label lblwatch;
        private Timer timer1;
    }
}