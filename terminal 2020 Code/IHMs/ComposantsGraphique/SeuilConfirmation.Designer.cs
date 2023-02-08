
namespace sorec_gamma.IHMs.ComposantsGraphique
{
    partial class SeuilConfirmation
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
            this.msgLabel = new System.Windows.Forms.Label();
            this.validateButton = new sorec_gamma.ButtonZ();
            this.cancelBtn = new sorec_gamma.ButtonZ();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.panel2.Controls.Add(this.validateButton);
            this.panel2.Controls.Add(this.cancelBtn);
            this.panel2.Location = new System.Drawing.Point(-39, 265);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(880, 73);
            this.panel2.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(104)))));
            this.panel1.Controls.Add(this.majTitle);
            this.panel1.Location = new System.Drawing.Point(-40, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(880, 67);
            this.panel1.TabIndex = 6;
            // 
            // majTitle
            // 
            this.majTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.majTitle.ForeColor = System.Drawing.Color.White;
            this.majTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.majTitle.Location = new System.Drawing.Point(2, 0);
            this.majTitle.Name = "majTitle";
            this.majTitle.Size = new System.Drawing.Size(877, 66);
            this.majTitle.TabIndex = 0;
            this.majTitle.Text = "Confirmation de seuil";
            this.majTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.ColorBottom = System.Drawing.Color.Lavender;
            this.gradientPanel1.ColorTop = System.Drawing.Color.Navy;
            this.gradientPanel1.Controls.Add(this.msgLabel);
            this.gradientPanel1.Location = new System.Drawing.Point(-40, 68);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(880, 193);
            this.gradientPanel1.TabIndex = 5;
            // 
            // msgLabel
            // 
            this.msgLabel.BackColor = System.Drawing.Color.Transparent;
            this.msgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgLabel.ForeColor = System.Drawing.Color.OldLace;
            this.msgLabel.Location = new System.Drawing.Point(19, 1);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(862, 192);
            this.msgLabel.TabIndex = 1;
            this.msgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // validateButton
            // 
            this.validateButton.BackColor = System.Drawing.Color.Transparent;
            this.validateButton.BackgroundImage = global::sorec_gamma.Properties.Resources.btnChiffre134_Desel;
            this.validateButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.validateButton.BorderColor = System.Drawing.Color.Transparent;
            this.validateButton.BorderWidth = 0;
            this.validateButton.ButtonShape = sorec_gamma.ButtonZ.ButtonsShapes.RoundRect;
            this.validateButton.ButtonText = "VALIDER";
            this.validateButton.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.validateButton.FlatAppearance.BorderSize = 0;
            this.validateButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.validateButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.validateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.validateButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.validateButton.ForeColor = System.Drawing.Color.White;
            this.validateButton.GradientAngle = 90;
            this.validateButton.Location = new System.Drawing.Point(190, 11);
            this.validateButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.validateButton.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.validateButton.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.validateButton.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.validateButton.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.validateButton.Name = "validateButton";
            this.validateButton.ShowButtontext = true;
            this.validateButton.Size = new System.Drawing.Size(104, 52);
            this.validateButton.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.validateButton.TabIndex = 116;
            this.validateButton.TabStop = false;
            this.validateButton.Text = "buttonZ1";
            this.validateButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.validateButton.TextLocation_X = 10;
            this.validateButton.TextLocation_Y = 15;
            this.validateButton.Transparent1 = 250;
            this.validateButton.Transparent2 = 250;
            this.validateButton.UseVisualStyleBackColor = false;
            this.validateButton.Click += new System.EventHandler(this.validateButton_Click);
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
            this.cancelBtn.Location = new System.Drawing.Point(610, 11);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelBtn.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.cancelBtn.MouseClickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.cancelBtn.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.cancelBtn.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(93)))), ((int)(((byte)(18)))));
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.ShowButtontext = true;
            this.cancelBtn.Size = new System.Drawing.Size(115, 52);
            this.cancelBtn.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(150)))), ((int)(((byte)(31)))));
            this.cancelBtn.TabIndex = 115;
            this.cancelBtn.TabStop = false;
            this.cancelBtn.Text = "cancelBtn";
            this.cancelBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cancelBtn.TextLocation_X = 11;
            this.cancelBtn.TextLocation_Y = 15;
            this.cancelBtn.Transparent1 = 250;
            this.cancelBtn.Transparent2 = 250;
            this.cancelBtn.UseVisualStyleBackColor = false;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // SeuilConfirmation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 339);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SeuilConfirmation";
            this.Text = "SeuilConfirmation";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.gradientPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private ButtonZ validateButton;
        private ButtonZ cancelBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label majTitle;
        private GradientPanel gradientPanel1;
        private System.Windows.Forms.Label msgLabel;
    }
}