namespace sorec_gamma.IHMs.ComposantsGraphique
{
    partial class PariAlert
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonZ13 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.majTitle = new System.Windows.Forms.Label();
            this.gradientPanel1 = new sorec_gamma.IHMs.ComposantsGraphique.GradientPanel();
            this.alertLbl = new System.Windows.Forms.Label();
            this.msgAlert = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.panel2.Controls.Add(this.buttonZ13);
            this.panel2.Location = new System.Drawing.Point(0, 280);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(840, 70);
            this.panel2.TabIndex = 7;
            // 
            // buttonZ13
            // 
            this.buttonZ13.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ13.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.buttonZ13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZ13.FlatAppearance.BorderSize = 0;
            this.buttonZ13.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ13.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ13.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZ13.ForeColor = System.Drawing.Color.White;
            this.buttonZ13.Location = new System.Drawing.Point(350, 7);
            this.buttonZ13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonZ13.Name = "buttonZ13";
            this.buttonZ13.Size = new System.Drawing.Size(140, 55);
            this.buttonZ13.TabIndex = 119;
            this.buttonZ13.TabStop = false;
            this.buttonZ13.Text = "OK";
            this.buttonZ13.UseVisualStyleBackColor = false;
            this.buttonZ13.Click += new System.EventHandler(this.validateButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.majTitle);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(840, 67);
            this.panel1.TabIndex = 6;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::sorec_gamma.Properties.Resources.alert;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(740, 8);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 50);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::sorec_gamma.Properties.Resources.alert;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(3, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // majTitle
            // 
            this.majTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.majTitle.ForeColor = System.Drawing.Color.Red;
            this.majTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.majTitle.Location = new System.Drawing.Point(0, 0);
            this.majTitle.Name = "majTitle";
            this.majTitle.Size = new System.Drawing.Size(840, 70);
            this.majTitle.TabIndex = 0;
            this.majTitle.Text = "PARI ALERT";
            this.majTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.ColorBottom = System.Drawing.Color.DodgerBlue;
            this.gradientPanel1.ColorTop = System.Drawing.Color.Navy;
            this.gradientPanel1.Controls.Add(this.alertLbl);
            this.gradientPanel1.Controls.Add(this.msgAlert);
            this.gradientPanel1.Location = new System.Drawing.Point(0, 68);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(840, 210);
            this.gradientPanel1.TabIndex = 5;
            // 
            // alertLbl
            // 
            this.alertLbl.AutoSize = true;
            this.alertLbl.BackColor = System.Drawing.Color.Transparent;
            this.alertLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F, System.Drawing.FontStyle.Bold);
            this.alertLbl.ForeColor = System.Drawing.Color.Red;
            this.alertLbl.Location = new System.Drawing.Point(287, 18);
            this.alertLbl.Name = "alertLbl";
            this.alertLbl.Size = new System.Drawing.Size(250, 37);
            this.alertLbl.TabIndex = 2;
            this.alertLbl.Text = "ATTENTION !!!";
            // 
            // msgAlert
            // 
            this.msgAlert.BackColor = System.Drawing.Color.Transparent;
            this.msgAlert.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgAlert.ForeColor = System.Drawing.Color.White;
            this.msgAlert.Location = new System.Drawing.Point(3, 67);
            this.msgAlert.Name = "msgAlert";
            this.msgAlert.Size = new System.Drawing.Size(834, 134);
            this.msgAlert.TabIndex = 1;
            this.msgAlert.Text = "La transaction a été annulée au serveur. Vous ne devez pas remettre le ticket de " +
    "cette transaction au parieur.";
            this.msgAlert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PariAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 349);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PariAlert";
            this.Text = "SeuilConfirmation";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label majTitle;
        private GradientPanel gradientPanel1;
        private System.Windows.Forms.Label msgAlert;
        private System.Windows.Forms.Button buttonZ13;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label alertLbl;
    }
}