
namespace sorec_gamma.IHMs.ComposantsGraphique
{
    partial class MAJForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.majTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.retryBtn = new sorec_gamma.ButtonZ();
            this.cancelBtn = new sorec_gamma.ButtonZ();
            this.validateBtn = new sorec_gamma.ButtonZ();
            this.gradientPanel1 = new sorec_gamma.IHMs.ComposantsGraphique.GradientPanel();
            this.majProgressbar = new System.Windows.Forms.ProgressBar();
            this.msgLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.panel1.Controls.Add(this.majTitle);
            this.panel1.Location = new System.Drawing.Point(0, -3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(880, 67);
            this.panel1.TabIndex = 3;
            // 
            // majTitle
            // 
            this.majTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.majTitle.ForeColor = System.Drawing.Color.White;
            this.majTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.majTitle.Location = new System.Drawing.Point(0, 0);
            this.majTitle.Name = "majTitle";
            this.majTitle.Size = new System.Drawing.Size(877, 66);
            this.majTitle.TabIndex = 0;
            this.majTitle.Text = "Mise à jour logicielle";
            this.majTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.panel2.Controls.Add(this.retryBtn);
            this.panel2.Controls.Add(this.cancelBtn);
            this.panel2.Controls.Add(this.validateBtn);
            this.panel2.Location = new System.Drawing.Point(0, 263);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(880, 73);
            this.panel2.TabIndex = 4;
            // 
            // retryBtn
            // 
            this.retryBtn.BackColor = System.Drawing.Color.Transparent;
            this.retryBtn.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.retryBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.retryBtn.BorderColor = System.Drawing.Color.Transparent;
            this.retryBtn.BorderWidth = 0;
            this.retryBtn.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.retryBtn.ButtonText = "REESSAYER";
            this.retryBtn.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.retryBtn.FlatAppearance.BorderSize = 0;
            this.retryBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.retryBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.retryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.retryBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.retryBtn.ForeColor = System.Drawing.Color.White;
            this.retryBtn.GradientAngle = 90;
            this.retryBtn.Location = new System.Drawing.Point(710, 6);
            this.retryBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.retryBtn.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.retryBtn.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.retryBtn.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.retryBtn.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.retryBtn.Name = "retryBtn";
            this.retryBtn.ShowButtontext = true;
            this.retryBtn.Size = new System.Drawing.Size(134, 52);
            this.retryBtn.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.retryBtn.TabIndex = 116;
            this.retryBtn.TabStop = false;
            this.retryBtn.Text = "buttonZ1";
            this.retryBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.retryBtn.TextLocation_X = 13;
            this.retryBtn.TextLocation_Y = 15;
            this.retryBtn.Transparent1 = 250;
            this.retryBtn.Transparent2 = 250;
            this.retryBtn.UseVisualStyleBackColor = false;
            this.retryBtn.Visible = false;
            this.retryBtn.Click += new System.EventHandler(this.retryBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.BackColor = System.Drawing.Color.Transparent;
            this.cancelBtn.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.cancelBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelBtn.BorderColor = System.Drawing.Color.Transparent;
            this.cancelBtn.BorderWidth = 0;
            this.cancelBtn.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.cancelBtn.ButtonText = "ANNULER";
            this.cancelBtn.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.cancelBtn.FlatAppearance.BorderSize = 0;
            this.cancelBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBtn.ForeColor = System.Drawing.Color.White;
            this.cancelBtn.GradientAngle = 90;
            this.cancelBtn.Location = new System.Drawing.Point(33, 6);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelBtn.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.cancelBtn.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.cancelBtn.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.cancelBtn.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.ShowButtontext = true;
            this.cancelBtn.Size = new System.Drawing.Size(134, 52);
            this.cancelBtn.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.cancelBtn.TabIndex = 115;
            this.cancelBtn.TabStop = false;
            this.cancelBtn.Text = "cancelBtn";
            this.cancelBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cancelBtn.TextLocation_X = 13;
            this.cancelBtn.TextLocation_Y = 15;
            this.cancelBtn.Transparent1 = 250;
            this.cancelBtn.Transparent2 = 250;
            this.cancelBtn.UseVisualStyleBackColor = false;
            this.cancelBtn.Visible = false;
            this.cancelBtn.Click += new System.EventHandler(this.cancel_Click);
            // 
            // validateBtn
            // 
            this.validateBtn.BackColor = System.Drawing.Color.Transparent;
            this.validateBtn.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.validateBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.validateBtn.BorderColor = System.Drawing.Color.Transparent;
            this.validateBtn.BorderWidth = 0;
            this.validateBtn.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.validateBtn.ButtonText = "OK";
            this.validateBtn.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.validateBtn.FlatAppearance.BorderSize = 0;
            this.validateBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.validateBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.validateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.validateBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.validateBtn.ForeColor = System.Drawing.Color.White;
            this.validateBtn.GradientAngle = 90;
            this.validateBtn.Location = new System.Drawing.Point(388, 6);
            this.validateBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.validateBtn.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.validateBtn.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.validateBtn.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.validateBtn.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.validateBtn.Name = "validateBtn";
            this.validateBtn.ShowButtontext = true;
            this.validateBtn.Size = new System.Drawing.Size(105, 52);
            this.validateBtn.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.validateBtn.TabIndex = 114;
            this.validateBtn.TabStop = false;
            this.validateBtn.Text = "validateBtn";
            this.validateBtn.TextLocation_X = 10;
            this.validateBtn.TextLocation_Y = 15;
            this.validateBtn.Transparent1 = 250;
            this.validateBtn.Transparent2 = 250;
            this.validateBtn.UseVisualStyleBackColor = false;
            this.validateBtn.Visible = false;
            this.validateBtn.Click += new System.EventHandler(this.validateBtn_Click);
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.ColorBottom = System.Drawing.Color.Lavender;
            this.gradientPanel1.ColorTop = System.Drawing.Color.Navy;
            this.gradientPanel1.Controls.Add(this.majProgressbar);
            this.gradientPanel1.Controls.Add(this.msgLabel);
            this.gradientPanel1.Location = new System.Drawing.Point(0, 66);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(880, 193);
            this.gradientPanel1.TabIndex = 2;
            // 
            // majProgressbar
            // 
            this.majProgressbar.ForeColor = System.Drawing.Color.Lime;
            this.majProgressbar.Location = new System.Drawing.Point(159, 165);
            this.majProgressbar.Name = "majProgressbar";
            this.majProgressbar.Size = new System.Drawing.Size(536, 11);
            this.majProgressbar.TabIndex = 2;
            this.majProgressbar.Visible = false;
            // 
            // msgLabel
            // 
            this.msgLabel.BackColor = System.Drawing.Color.Transparent;
            this.msgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgLabel.ForeColor = System.Drawing.Color.OldLace;
            this.msgLabel.Location = new System.Drawing.Point(3, 1);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(841, 192);
            this.msgLabel.TabIndex = 1;
            this.msgLabel.Text = "Une nouvelle version en cours de téléchargement. Veuillez ne pas éteindre le terminal.";
            this.msgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MAJForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(878, 332);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MAJForm";
            this.Text = "AlertModal";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.gradientPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label msgLabel;
        private GradientPanel gradientPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ButtonZ validateBtn;
        private ButtonZ cancelBtn;
        private System.Windows.Forms.Label majTitle;
        private ButtonZ retryBtn;
        private System.Windows.Forms.ProgressBar majProgressbar;
    }
}