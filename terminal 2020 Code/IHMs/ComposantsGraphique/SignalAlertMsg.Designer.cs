
namespace sorec_gamma.IHMs.ComposantsGraphique
{
    partial class SignalAlertMsg
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.majTitle = new System.Windows.Forms.Label();
            this.gradientPanel1 = new sorec_gamma.IHMs.ComposantsGraphique.GradientPanel();
            this.msgSignalAlert = new System.Windows.Forms.Label();
            this.buttonZ13 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.panel2.Controls.Add(this.buttonZ13);
            this.panel2.Location = new System.Drawing.Point(0, 280);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(840, 73);
            this.panel2.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.panel1.Controls.Add(this.majTitle);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(840, 67);
            this.panel1.TabIndex = 6;
            // 
            // majTitle
            // 
            this.majTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.majTitle.ForeColor = System.Drawing.Color.White;
            this.majTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.majTitle.Location = new System.Drawing.Point(0, 0);
            this.majTitle.Name = "majTitle";
            this.majTitle.Size = new System.Drawing.Size(840, 67);
            this.majTitle.TabIndex = 0;
            this.majTitle.Text = "SIGNAL COURSE";
            this.majTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.ColorBottom = System.Drawing.Color.Lavender;
            this.gradientPanel1.ColorTop = System.Drawing.Color.Navy;
            this.gradientPanel1.Controls.Add(this.msgSignalAlert);
            this.gradientPanel1.Location = new System.Drawing.Point(0, 68);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(840, 207);
            this.gradientPanel1.TabIndex = 5;
            // 
            // msgSignalAlert
            // 
            this.msgSignalAlert.BackColor = System.Drawing.Color.Transparent;
            this.msgSignalAlert.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgSignalAlert.ForeColor = System.Drawing.Color.OldLace;
            this.msgSignalAlert.Location = new System.Drawing.Point(0, 0);
            this.msgSignalAlert.Name = "msgSignalAlert";
            this.msgSignalAlert.Size = new System.Drawing.Size(840, 207);
            this.msgSignalAlert.TabIndex = 1;
            this.msgSignalAlert.Text = "ATTENTION CHANGEMENT DE COURSE";
            this.msgSignalAlert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.buttonZ13.Location = new System.Drawing.Point(350, 6);
            this.buttonZ13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonZ13.Name = "buttonZ13";
            this.buttonZ13.Size = new System.Drawing.Size(140, 61);
            this.buttonZ13.TabIndex = 119;
            this.buttonZ13.TabStop = false;
            this.buttonZ13.Text = "OK";
            this.buttonZ13.UseVisualStyleBackColor = false;
            this.buttonZ13.Click += new System.EventHandler(this.validateButton_Click);
            // 
            // SignalAlertMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 355);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SignalAlertMsg";
            this.Text = "SeuilConfirmation";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.gradientPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label majTitle;
        private GradientPanel gradientPanel1;
        private System.Windows.Forms.Label msgSignalAlert;
        private System.Windows.Forms.Button buttonZ13;
    }
}