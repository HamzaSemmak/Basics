
namespace sorec_gamma.IHMs.ComposantsGraphique
{
    partial class LoadingForm
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
            this.gradientPanel1 = new sorec_gamma.IHMs.ComposantsGraphique.GradientPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.copyrigthLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.ColorBottom = System.Drawing.Color.Lavender;
            this.gradientPanel1.ColorTop = System.Drawing.Color.Navy;
            this.gradientPanel1.Controls.Add(this.label1);
            this.gradientPanel1.Controls.Add(this.label2);
            this.gradientPanel1.Controls.Add(this.progressBar);
            this.gradientPanel1.Controls.Add(this.copyrigthLbl);
            this.gradientPanel1.Location = new System.Drawing.Point(-6, -3);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(1000, 470);
            this.gradientPanel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(78, 314);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(843, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "L\'application s\'ouvrira dans quelques secondes merci de patienter...";
            // 
            // progressBar
            // 
            this.progressBar.ForeColor = System.Drawing.Color.Lime;
            this.progressBar.Location = new System.Drawing.Point(216, 359);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(536, 11);
            this.progressBar.TabIndex = 2;
            // 
            // copyrigthLbl
            // 
            this.copyrigthLbl.AutoSize = true;
            this.copyrigthLbl.BackColor = System.Drawing.Color.Transparent;
            this.copyrigthLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyrigthLbl.Location = new System.Drawing.Point(369, 426);
            this.copyrigthLbl.Name = "copyrigthLbl";
            this.copyrigthLbl.Size = new System.Drawing.Size(230, 24);
            this.copyrigthLbl.TabIndex = 1;
            this.copyrigthLbl.Text = "© Copyright 2021, SOREC";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Image = global::sorec_gamma.Properties.Resources.gamma_logo_no_bg;
            this.label2.Location = new System.Drawing.Point(70, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(832, 223);
            this.label2.TabIndex = 2;
            // 
            // LoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(196)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(995, 467);
            this.Controls.Add(this.gradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoadingForm";
            this.Text = "Loading";
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label copyrigthLbl;
        private System.Windows.Forms.Label label2;
        private GradientPanel gradientPanel1;
        private System.Windows.Forms.ProgressBar progressBar;
    }

}