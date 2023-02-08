using System.Windows.Forms;
using sorec_gamma.IHMs.ComposantsGraphique;

namespace sorec_gamma.IHMs
{
    partial class AuthentificationForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        /// 

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel5 = new System.Windows.Forms.Panel();
            this.printerOk = new System.Windows.Forms.PictureBox();
            this.etatConnNokPB = new System.Windows.Forms.PictureBox();
            this.printerNok = new System.Windows.Forms.PictureBox();
            this.lblwatch = new System.Windows.Forms.Label();
            this.lblCaisse = new System.Windows.Forms.Label();
            this.lblIp = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.etatConnOKPB = new System.Windows.Forms.PictureBox();
            this.lblErrorMessage = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel3 = new sorec_gamma.IHMs.ComposantsGraphique.GradientPanel();
            this.buttonZ13 = new System.Windows.Forms.Button();
            this.terminalStatus = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.printerOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.etatConnNokPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.printerNok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.etatConnOKPB)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.panel5.Controls.Add(this.printerOk);
            this.panel5.Controls.Add(this.etatConnNokPB);
            this.panel5.Controls.Add(this.printerNok);
            this.panel5.Controls.Add(this.lblwatch);
            this.panel5.Controls.Add(this.lblCaisse);
            this.panel5.Controls.Add(this.lblIp);
            this.panel5.Controls.Add(this.lblVersion);
            this.panel5.Controls.Add(this.etatConnOKPB);
            this.panel5.Location = new System.Drawing.Point(-8, 716);
            this.panel5.Margin = new System.Windows.Forms.Padding(0, 2, 2, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1049, 63);
            this.panel5.TabIndex = 96;
            // 
            // printerOk
            // 
            this.printerOk.BackgroundImage = global::sorec_gamma.Properties.Resources.BtnEtatImp_Ok;
            this.printerOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.printerOk.ErrorImage = null;
            this.printerOk.InitialImage = null;
            this.printerOk.Location = new System.Drawing.Point(542, 11);
            this.printerOk.Name = "printerOk";
            this.printerOk.Size = new System.Drawing.Size(31, 31);
            this.printerOk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.printerOk.TabIndex = 101;
            this.printerOk.TabStop = false;
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
            this.etatConnNokPB.TabIndex = 100;
            this.etatConnNokPB.TabStop = false;
            // 
            // printerNok
            // 
            this.printerNok.BackgroundImage = global::sorec_gamma.Properties.Resources.BtnEtatImp_NOk;
            this.printerNok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.printerNok.ErrorImage = null;
            this.printerNok.InitialImage = null;
            this.printerNok.Location = new System.Drawing.Point(542, 11);
            this.printerNok.Name = "printerNok";
            this.printerNok.Size = new System.Drawing.Size(34, 31);
            this.printerNok.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.printerNok.TabIndex = 99;
            this.printerNok.TabStop = false;
            // 
            // lblwatch
            // 
            this.lblwatch.AutoSize = true;
            this.lblwatch.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblwatch.ForeColor = System.Drawing.Color.White;
            this.lblwatch.Location = new System.Drawing.Point(437, 11);
            this.lblwatch.Name = "lblwatch";
            this.lblwatch.Size = new System.Drawing.Size(64, 28);
            this.lblwatch.TabIndex = 72;
            this.lblwatch.Text = "Time";
            // 
            // lblCaisse
            // 
            this.lblCaisse.AutoSize = true;
            this.lblCaisse.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblCaisse.ForeColor = System.Drawing.Color.Red;
            this.lblCaisse.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblCaisse.Location = new System.Drawing.Point(190, 11);
            this.lblCaisse.Name = "lblCaisse";
            this.lblCaisse.Size = new System.Drawing.Size(214, 28);
            this.lblCaisse.TabIndex = 70;
            this.lblCaisse.Text = "Guichet :  XXXX - X";
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
            // etatConnOKPB
            // 
            this.etatConnOKPB.BackgroundImage = global::sorec_gamma.Properties.Resources.BtnEtatCon_OK;
            this.etatConnOKPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.etatConnOKPB.ErrorImage = null;
            this.etatConnOKPB.InitialImage = null;
            this.etatConnOKPB.Location = new System.Drawing.Point(576, 11);
            this.etatConnOKPB.Name = "etatConnOKPB";
            this.etatConnOKPB.Size = new System.Drawing.Size(31, 31);
            this.etatConnOKPB.TabIndex = 98;
            this.etatConnOKPB.TabStop = false;
           
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.lblErrorMessage.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblErrorMessage.ForeColor = System.Drawing.Color.White;
            this.lblErrorMessage.Location = new System.Drawing.Point(309, 0);
            this.lblErrorMessage.Margin = new System.Windows.Forms.Padding(0);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(773, 89);
            this.lblErrorMessage.TabIndex = 1;
            this.lblErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.lblErrorMessage);
            this.panel2.Location = new System.Drawing.Point(-34, -6);
            this.panel2.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1082, 89);
            this.panel2.TabIndex = 116;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.pictureBox3.BackgroundImage = global::sorec_gamma.Properties.Resources.gamma_logo_no_bg;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox3.ErrorImage = null;
            this.pictureBox3.InitialImage = null;
            this.pictureBox3.Location = new System.Drawing.Point(35, 6);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(272, 84);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.Transparent;
            this.panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel14.Location = new System.Drawing.Point(46, 17);
            this.panel14.Margin = new System.Windows.Forms.Padding(4);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(327, 76);
            this.panel14.TabIndex = 117;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.ColorBottom = System.Drawing.Color.Lavender;
            this.panel3.ColorTop = System.Drawing.Color.Navy;
            this.panel3.Controls.Add(this.buttonZ13);
            this.panel3.Controls.Add(this.terminalStatus);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.textBox2);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(0, 84);
            this.panel3.Margin = new System.Windows.Forms.Padding(0, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1048, 629);
            this.panel3.TabIndex = 97;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // buttonZ13
            // 
            this.buttonZ13.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ13.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.buttonZ13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ13.FlatAppearance.BorderSize = 0;
            this.buttonZ13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ13.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ13.ForeColor = System.Drawing.Color.White;
            this.buttonZ13.Location = new System.Drawing.Point(444, 532);
            this.buttonZ13.Margin = new System.Windows.Forms.Padding(2);
            this.buttonZ13.Name = "buttonZ13";
            this.buttonZ13.Size = new System.Drawing.Size(119, 54);
            this.buttonZ13.TabIndex = 118;
            this.buttonZ13.TabStop = false;
            this.buttonZ13.Text = "VALIDER";
            this.buttonZ13.UseVisualStyleBackColor = false;
            this.buttonZ13.Click += new System.EventHandler(this.buttonZ13_Click);
            this.buttonZ13.MouseEnter += new System.EventHandler(this.buttonZ13_MouseEnter);
            this.buttonZ13.MouseLeave += new System.EventHandler(this.buttonZ13_MouseLeave);
            // 
            // terminalStatus
            // 
            this.terminalStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold);
            this.terminalStatus.ForeColor = System.Drawing.Color.White;
            this.terminalStatus.Location = new System.Drawing.Point(278, 46);
            this.terminalStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.terminalStatus.Name = "terminalStatus";
            this.terminalStatus.Size = new System.Drawing.Size(432, 87);
            this.terminalStatus.TabIndex = 115;
            this.terminalStatus.Text = "Guichet fermé";
            this.terminalStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.Controls.Add(this.buttonZ47);
            this.panel7.Controls.Add(this.buttonZ49);
            this.panel7.Controls.Add(this.buttonZ43);
            this.panel7.Controls.Add(this.buttonZ48);
            this.panel7.Controls.Add(this.buttonZ42);
            this.panel7.Controls.Add(this.buttonZ41);
            this.panel7.Controls.Add(this.buttonZ46);
            this.panel7.Controls.Add(this.buttonZ40);
            this.panel7.Controls.Add(this.buttonZ45);
            this.panel7.Controls.Add(this.buttonZ30);
            this.panel7.Controls.Add(this.buttonZ44);
            this.panel7.Controls.Add(this.buttonZ29);
            this.panel7.Location = new System.Drawing.Point(708, 187);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(262, 292);
            this.panel7.TabIndex = 114;
            // 
            // buttonZ47
            // 
            this.buttonZ47.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ47.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ47.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ47.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ47.BorderWidth = 0;
            this.buttonZ47.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ47.ButtonText = " DEL";
            this.buttonZ47.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ47.FlatAppearance.BorderSize = 0;
            this.buttonZ47.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ47.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ47.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ47.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ47.ForeColor = System.Drawing.Color.White;
            this.buttonZ47.GradientAngle = 90;
            this.buttonZ47.Location = new System.Drawing.Point(22, 216);
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
            this.buttonZ47.MouseEnter += new System.EventHandler(this.buttonZ47_MouseEnter);
            this.buttonZ47.MouseLeave += new System.EventHandler(this.buttonZ47_MouseLeave);
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
            this.buttonZ49.Location = new System.Drawing.Point(183, 216);
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
            this.buttonZ49.MouseEnter += new System.EventHandler(this.buttonZ49_MouseEnter);
            this.buttonZ49.MouseLeave += new System.EventHandler(this.buttonZ49_MouseLeave);
            // 
            // buttonZ43
            // 
            this.buttonZ43.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ43.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ43.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ43.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ43.BorderWidth = 0;
            this.buttonZ43.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ43.ButtonText = "   6";
            this.buttonZ43.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ43.FlatAppearance.BorderSize = 0;
            this.buttonZ43.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ43.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ43.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ43.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ43.ForeColor = System.Drawing.Color.White;
            this.buttonZ43.GradientAngle = 90;
            this.buttonZ43.Location = new System.Drawing.Point(183, 76);
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
            this.buttonZ43.MouseEnter += new System.EventHandler(this.buttonZ43_MouseEnter);
            this.buttonZ43.MouseLeave += new System.EventHandler(this.buttonZ43_MouseLeave);
            // 
            // buttonZ48
            // 
            this.buttonZ48.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ48.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ48.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ48.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ48.BorderWidth = 0;
            this.buttonZ48.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ48.ButtonText = "   0";
            this.buttonZ48.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ48.FlatAppearance.BorderSize = 0;
            this.buttonZ48.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ48.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ48.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ48.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ48.ForeColor = System.Drawing.Color.White;
            this.buttonZ48.GradientAngle = 90;
            this.buttonZ48.Location = new System.Drawing.Point(102, 216);
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
            this.buttonZ48.MouseEnter += new System.EventHandler(this.buttonZ48_MouseEnter);
            this.buttonZ48.MouseLeave += new System.EventHandler(this.buttonZ48_MouseLeave);
            // 
            // buttonZ42
            // 
            this.buttonZ42.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ42.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ42.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ42.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ42.BorderWidth = 0;
            this.buttonZ42.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ42.ButtonText = "   5";
            this.buttonZ42.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ42.FlatAppearance.BorderSize = 0;
            this.buttonZ42.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ42.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ42.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ42.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ42.ForeColor = System.Drawing.Color.White;
            this.buttonZ42.GradientAngle = 90;
            this.buttonZ42.Location = new System.Drawing.Point(102, 76);
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
            this.buttonZ42.UseCompatibleTextRendering = true;
            this.buttonZ42.UseVisualStyleBackColor = false;
            this.buttonZ42.Click += new System.EventHandler(this.buttonZ42_Click);
            this.buttonZ42.MouseEnter += new System.EventHandler(this.buttonZ42_MouseEnter);
            this.buttonZ42.MouseLeave += new System.EventHandler(this.buttonZ42_MouseLeave);
            // 
            // buttonZ41
            // 
            this.buttonZ41.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ41.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ41.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ41.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ41.BorderWidth = 0;
            this.buttonZ41.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ41.ButtonText = "   4";
            this.buttonZ41.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ41.FlatAppearance.BorderSize = 0;
            this.buttonZ41.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ41.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ41.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ41.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ41.ForeColor = System.Drawing.Color.White;
            this.buttonZ41.GradientAngle = 90;
            this.buttonZ41.Location = new System.Drawing.Point(22, 76);
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
            this.buttonZ41.MouseEnter += new System.EventHandler(this.buttonZ41_MouseEnter);
            this.buttonZ41.MouseLeave += new System.EventHandler(this.buttonZ41_MouseLeave);
            // 
            // buttonZ46
            // 
            this.buttonZ46.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ46.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ46.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ46.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ46.BorderWidth = 0;
            this.buttonZ46.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ46.ButtonText = "   9";
            this.buttonZ46.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ46.FlatAppearance.BorderSize = 0;
            this.buttonZ46.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ46.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ46.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ46.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ46.ForeColor = System.Drawing.Color.White;
            this.buttonZ46.GradientAngle = 90;
            this.buttonZ46.Location = new System.Drawing.Point(183, 146);
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
            this.buttonZ46.MouseEnter += new System.EventHandler(this.buttonZ46_MouseEnter);
            this.buttonZ46.MouseLeave += new System.EventHandler(this.buttonZ46_MouseLeave);
            // 
            // buttonZ40
            // 
            this.buttonZ40.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ40.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ40.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ40.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ40.BorderWidth = 0;
            this.buttonZ40.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ40.ButtonText = "   3";
            this.buttonZ40.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ40.FlatAppearance.BorderSize = 0;
            this.buttonZ40.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ40.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ40.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ40.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ40.ForeColor = System.Drawing.Color.White;
            this.buttonZ40.GradientAngle = 90;
            this.buttonZ40.Location = new System.Drawing.Point(183, 6);
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
            this.buttonZ40.MouseEnter += new System.EventHandler(this.buttonZ40_MouseEnter);
            this.buttonZ40.MouseLeave += new System.EventHandler(this.buttonZ40_MouseLeave);
            // 
            // buttonZ45
            // 
            this.buttonZ45.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ45.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ45.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ45.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ45.BorderWidth = 0;
            this.buttonZ45.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ45.ButtonText = "   8";
            this.buttonZ45.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ45.FlatAppearance.BorderSize = 0;
            this.buttonZ45.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ45.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ45.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ45.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ45.ForeColor = System.Drawing.Color.White;
            this.buttonZ45.GradientAngle = 90;
            this.buttonZ45.Location = new System.Drawing.Point(102, 146);
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
            this.buttonZ45.MouseEnter += new System.EventHandler(this.buttonZ45_MouseEnter);
            this.buttonZ45.MouseLeave += new System.EventHandler(this.buttonZ45_MouseLeave);
            // 
            // buttonZ30
            // 
            this.buttonZ30.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ30.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ30.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ30.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ30.BorderWidth = 0;
            this.buttonZ30.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ30.ButtonText = "   2";
            this.buttonZ30.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ30.FlatAppearance.BorderSize = 0;
            this.buttonZ30.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ30.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ30.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ30.ForeColor = System.Drawing.Color.White;
            this.buttonZ30.GradientAngle = 90;
            this.buttonZ30.Location = new System.Drawing.Point(102, 6);
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
            this.buttonZ30.MouseEnter += new System.EventHandler(this.buttonZ30_MouseEnter);
            this.buttonZ30.MouseLeave += new System.EventHandler(this.buttonZ30_MouseLeave);
            // 
            // buttonZ44
            // 
            this.buttonZ44.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ44.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ44.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ44.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ44.BorderWidth = 0;
            this.buttonZ44.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ44.ButtonText = "   7";
            this.buttonZ44.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ44.FlatAppearance.BorderSize = 0;
            this.buttonZ44.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ44.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ44.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ44.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ44.ForeColor = System.Drawing.Color.White;
            this.buttonZ44.GradientAngle = 90;
            this.buttonZ44.Location = new System.Drawing.Point(22, 146);
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
            this.buttonZ44.MouseEnter += new System.EventHandler(this.buttonZ44_MouseEnter);
            this.buttonZ44.MouseLeave += new System.EventHandler(this.buttonZ44_MouseLeave);
            // 
            // buttonZ29
            // 
            this.buttonZ29.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ29.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre64_Desel;
            this.buttonZ29.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ29.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ29.BorderWidth = 0;
            this.buttonZ29.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.buttonZ29.ButtonText = "   1";
            this.buttonZ29.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.buttonZ29.FlatAppearance.BorderSize = 0;
            this.buttonZ29.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ29.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ29.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ29.ForeColor = System.Drawing.Color.White;
            this.buttonZ29.GradientAngle = 90;
            this.buttonZ29.Location = new System.Drawing.Point(22, 6);
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
            this.buttonZ29.Click += new System.EventHandler(this.buttonZ29_Click_1);
            this.buttonZ29.MouseEnter += new System.EventHandler(this.buttonZ29_MouseEnter);
            this.buttonZ29.MouseLeave += new System.EventHandler(this.buttonZ29_MouseLeave);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(100, 397);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(282, 44);
            this.textBox2.TabIndex = 97;
            this.textBox2.TabStop = false;
            this.textBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox2_MouseClick);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Navy;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(100, 258);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(282, 44);
            this.textBox1.TabIndex = 96;
            this.textBox1.TabStop = false;
            this.textBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(136, 345);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(199, 28);
            this.label8.TabIndex = 92;
            this.label8.Text = "Code confidentiel";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(142, 205);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(223, 28);
            this.label7.TabIndex = 91;
            this.label7.Text = "N° compte préposé ";
            // 
            // AuthentificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1040, 772);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AuthentificationForm";
            this.Text = "AuthentificationForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.printerOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.etatConnNokPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.printerNok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.etatConnOKPB)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lblCaisse;
        private System.Windows.Forms.Label lblErrorMessage;
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
        private System.Windows.Forms.Label lblwatch;
        private System.Windows.Forms.PictureBox etatConnOKPB;
        private System.Windows.Forms.PictureBox printerNok;
        private GradientPanel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label terminalStatus;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox etatConnNokPB;
        private System.Windows.Forms.PictureBox printerOk;
        private Button buttonZ13;
    }
}
