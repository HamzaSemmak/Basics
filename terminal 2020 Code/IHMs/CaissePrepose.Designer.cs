using System.Drawing;
using System.Windows.Forms;
using sorec_gamma.IHMs.ComposantsGraphique;

namespace sorec_gamma.IHMs
{
    partial class CaissePrepose
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnJournal = new System.Windows.Forms.Button();
            this.btnPrepose = new System.Windows.Forms.Button();
            this.btnClient = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblwatch = new System.Windows.Forms.Label();
            this.etatConnNokPB = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.etatConnOKPB = new System.Windows.Forms.PictureBox();
            this.lblIp = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblGuichet = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new sorec_gamma.IHMs.ComposantsGraphique.GradientPanel();
            this.majDataLbl = new System.Windows.Forms.Label();
            this.buttonZ4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblTitre = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Designation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nbr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cummul = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etatConnNokPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.etatConnOKPB)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.panel6.Controls.Add(this.button7);
            this.panel6.Location = new System.Drawing.Point(-11, -1);
            this.panel6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1393, 76);
            this.panel6.TabIndex = 3;
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
            this.button7.Location = new System.Drawing.Point(26, 10);
            this.button7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(109, 54);
            this.button7.TabIndex = 111;
            this.button7.TabStop = false;
            this.button7.Text = "Retour";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(127)))), ((int)(((byte)(228)))));
            this.panel2.Controls.Add(this.btnJournal);
            this.panel2.Controls.Add(this.btnPrepose);
            this.panel2.Controls.Add(this.btnClient);
            this.panel2.Location = new System.Drawing.Point(-11, 77);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1393, 75);
            this.panel2.TabIndex = 70;
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
            this.btnJournal.Location = new System.Drawing.Point(661, 12);
            this.btnJournal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnJournal.Name = "btnJournal";
            this.btnJournal.Size = new System.Drawing.Size(201, 52);
            this.btnJournal.TabIndex = 114;
            this.btnJournal.Text = "Journal";
            this.btnJournal.UseVisualStyleBackColor = false;
            this.btnJournal.Click += new System.EventHandler(this.btnJournal_Click);
            // 
            // btnPrepose
            // 
            this.btnPrepose.BackColor = System.Drawing.Color.Transparent;
            this.btnPrepose.BackgroundImage = global::sorec_gamma.Properties.Resources.btnSelectionMenu_Sel;
            this.btnPrepose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrepose.FlatAppearance.BorderSize = 0;
            this.btnPrepose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrepose.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrepose.ForeColor = System.Drawing.Color.White;
            this.btnPrepose.Location = new System.Drawing.Point(421, 12);
            this.btnPrepose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrepose.Name = "btnPrepose";
            this.btnPrepose.Size = new System.Drawing.Size(195, 52);
            this.btnPrepose.TabIndex = 113;
            this.btnPrepose.Text = "Préposé";
            this.btnPrepose.UseVisualStyleBackColor = false;
            this.btnPrepose.Click += new System.EventHandler(this.btnPrepose_Click);
            // 
            // btnClient
            // 
            this.btnClient.BackColor = System.Drawing.Color.Transparent;
            this.btnClient.BackgroundImage = global::sorec_gamma.Properties.Resources.btnSelectionMenu_Desel;
            this.btnClient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClient.FlatAppearance.BorderSize = 0;
            this.btnClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClient.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClient.ForeColor = System.Drawing.Color.Navy;
            this.btnClient.Location = new System.Drawing.Point(901, 12);
            this.btnClient.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClient.Name = "btnClient";
            this.btnClient.Size = new System.Drawing.Size(195, 52);
            this.btnClient.TabIndex = 115;
            this.btnClient.Text = "Clients";
            this.btnClient.UseVisualStyleBackColor = false;
            this.btnClient.Click += new System.EventHandler(this.btnClient_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.lblwatch);
            this.panel5.Controls.Add(this.etatConnNokPB);
            this.panel5.Controls.Add(this.pictureBox2);
            this.panel5.Controls.Add(this.etatConnOKPB);
            this.panel5.Controls.Add(this.lblIp);
            this.panel5.Controls.Add(this.lblVersion);
            this.panel5.Controls.Add(this.lblGuichet);
            this.panel5.Location = new System.Drawing.Point(-11, 881);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1393, 93);
            this.panel5.TabIndex = 71;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 36);
            this.label1.TabIndex = 119;
            this.label1.Text = "V :  XX.XX.XX";
            // 
            // lblwatch
            // 
            this.lblwatch.AutoSize = true;
            this.lblwatch.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblwatch.ForeColor = System.Drawing.Color.White;
            this.lblwatch.Location = new System.Drawing.Point(583, 14);
            this.lblwatch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblwatch.Name = "lblwatch";
            this.lblwatch.Size = new System.Drawing.Size(83, 36);
            this.lblwatch.TabIndex = 118;
            this.lblwatch.Text = "Time";
            // 
            // etatConnNokPB
            // 
            this.etatConnNokPB.BackgroundImage = global::sorec_gamma.Properties.Resources.BtnEtatCon_NOk;
            this.etatConnNokPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.etatConnNokPB.ErrorImage = null;
            this.etatConnNokPB.InitialImage = null;
            this.etatConnNokPB.Location = new System.Drawing.Point(768, 13);
            this.etatConnNokPB.Margin = new System.Windows.Forms.Padding(4);
            this.etatConnNokPB.Name = "etatConnNokPB";
            this.etatConnNokPB.Size = new System.Drawing.Size(41, 38);
            this.etatConnNokPB.TabIndex = 117;
            this.etatConnNokPB.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::sorec_gamma.Properties.Resources.BtnEtatImp_Ok;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(722, 13);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(41, 38);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 116;
            this.pictureBox2.TabStop = false;
            // 
            // etatConnOKPB
            // 
            this.etatConnOKPB.BackgroundImage = global::sorec_gamma.Properties.Resources.BtnEtatCon_OK;
            this.etatConnOKPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.etatConnOKPB.ErrorImage = null;
            this.etatConnOKPB.InitialImage = null;
            this.etatConnOKPB.Location = new System.Drawing.Point(768, 13);
            this.etatConnOKPB.Margin = new System.Windows.Forms.Padding(4);
            this.etatConnOKPB.Name = "etatConnOKPB";
            this.etatConnOKPB.Size = new System.Drawing.Size(41, 38);
            this.etatConnOKPB.TabIndex = 115;
            this.etatConnOKPB.TabStop = false;
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblIp.ForeColor = System.Drawing.Color.White;
            this.lblIp.Location = new System.Drawing.Point(1058, 14);
            this.lblIp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(341, 36);
            this.lblIp.TabIndex = 69;
            this.lblIp.Text = "Adresse IP :  xx.xx.xx.xx";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(-11, 155);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(196, 36);
            this.lblVersion.TabIndex = 68;
            this.lblVersion.Text = "V :  XX.XX.XX";
            // 
            // lblGuichet
            // 
            this.lblGuichet.AutoSize = true;
            this.lblGuichet.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblGuichet.ForeColor = System.Drawing.Color.Red;
            this.lblGuichet.Location = new System.Drawing.Point(220, 14);
            this.lblGuichet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGuichet.Name = "lblGuichet";
            this.lblGuichet.Size = new System.Drawing.Size(275, 36);
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
            this.panel3.ColorBottom = System.Drawing.Color.Lavender;
            this.panel3.ColorTop = System.Drawing.Color.Navy;
            this.panel3.Controls.Add(this.majDataLbl);
            this.panel3.Controls.Add(this.buttonZ4);
            this.panel3.Controls.Add(this.button6);
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.lblTitre);
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Location = new System.Drawing.Point(-11, 155);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1393, 725);
            this.panel3.TabIndex = 1;
            // 
            // majDataLbl
            // 
            this.majDataLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.majDataLbl.ForeColor = System.Drawing.Color.White;
            this.majDataLbl.Location = new System.Drawing.Point(403, 33);
            this.majDataLbl.Name = "majDataLbl";
            this.majDataLbl.Size = new System.Drawing.Size(859, 54);
            this.majDataLbl.TabIndex = 117;
            this.majDataLbl.Text = "Les données sont en cours de mise à jour. Veuillez patienter.";
            this.majDataLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonZ4
            // 
            this.buttonZ4.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ4.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.buttonZ4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ4.FlatAppearance.BorderSize = 0;
            this.buttonZ4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ4.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ4.ForeColor = System.Drawing.Color.White;
            this.buttonZ4.Location = new System.Drawing.Point(738, 580);
            this.buttonZ4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonZ4.Name = "buttonZ4";
            this.buttonZ4.Size = new System.Drawing.Size(159, 69);
            this.buttonZ4.TabIndex = 116;
            this.buttonZ4.TabStop = false;
            this.buttonZ4.Text = "Imprimer";
            this.buttonZ4.UseVisualStyleBackColor = false;
            this.buttonZ4.Click += new System.EventHandler(this.buttonZ5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(32, 515);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(198, 80);
            this.button6.TabIndex = 114;
            this.button6.TabStop = false;
            this.button6.Text = "Seuil Alerte";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
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
            this.button5.Location = new System.Drawing.Point(32, 425);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(198, 80);
            this.button5.TabIndex = 113;
            this.button5.TabStop = false;
            this.button5.Text = "Situation PDV";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(32, 334);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(198, 80);
            this.button4.TabIndex = 112;
            this.button4.TabStop = false;
            this.button4.Text = "Solde Prépayé";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(32, 244);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(198, 80);
            this.button3.TabIndex = 111;
            this.button3.TabStop = false;
            this.button3.Text = "Etat de \r\nCaisse PDV";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(32, 151);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(198, 80);
            this.button2.TabIndex = 110;
            this.button2.TabStop = false;
            this.button2.Text = "Etat de \r\nCaisse PDD";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Sel;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(32, 59);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(198, 80);
            this.button1.TabIndex = 109;
            this.button1.TabStop = false;
            this.button1.Text = "Etat de \r\nCaisse Préposé";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTitre.Location = new System.Drawing.Point(532, 87);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(0, 17);
            this.lblTitre.TabIndex = 68;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 48;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Designation,
            this.Nbr,
            this.Cummul});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.dataGridView1.Location = new System.Drawing.Point(283, 105);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.RowHeadersWidth = 4;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.RowTemplate.Height = 35;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(1054, 446);
            this.dataGridView1.TabIndex = 67;
            this.dataGridView1.TabStop = false;
            // 
            // Designation
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.NullValue = "---";
            this.Designation.DefaultCellStyle = dataGridViewCellStyle3;
            this.Designation.HeaderText = "Désignation";
            this.Designation.MinimumWidth = 6;
            this.Designation.Name = "Designation";
            this.Designation.ReadOnly = true;
            this.Designation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Designation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Designation.Width = 283;
            // 
            // Nbr
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.NullValue = "---";
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.Nbr.DefaultCellStyle = dataGridViewCellStyle4;
            this.Nbr.HeaderText = "Nombres";
            this.Nbr.MinimumWidth = 6;
            this.Nbr.Name = "Nbr";
            this.Nbr.ReadOnly = true;
            this.Nbr.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Nbr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Nbr.Width = 220;
            // 
            // Cummul
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.NullValue = "---";
            this.Cummul.DefaultCellStyle = dataGridViewCellStyle5;
            this.Cummul.HeaderText = "Cumul";
            this.Cummul.MinimumWidth = 6;
            this.Cummul.Name = "Cummul";
            this.Cummul.ReadOnly = true;
            this.Cummul.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Cummul.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Cummul.Width = 283;
            // 
            // CaissePrepose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1387, 946);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CaissePrepose";
            this.Text = "Form2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel6.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etatConnNokPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.etatConnOKPB)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblTitre;
        private Panel panel6;
        private Panel panel2;
        private Panel panel5;
        private Label lblIp;
        private Label lblVersion;
        private Label lblGuichet;
        private Button btnPrepose;
        private Button btnClient;
        private Button btnJournal;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
        private Button button7;
        private PictureBox etatConnOKPB;
        private PictureBox pictureBox2;
        private GradientPanel panel3;
        private Button buttonZ4;
        private PictureBox etatConnNokPB;
        private Label majDataLbl;
        private Label lblwatch;
        private Timer timer1;
        private Label label1;
        private DataGridViewTextBoxColumn Designation;
        private DataGridViewTextBoxColumn Nbr;
        private DataGridViewTextBoxColumn Cummul;
    }
}