﻿namespace RaffRegServer
{
    partial class LoadForm
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
            this.bProgresso = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bProgresso
            // 
            this.bProgresso.Location = new System.Drawing.Point(12, 37);
            this.bProgresso.Name = "bProgresso";
            this.bProgresso.Size = new System.Drawing.Size(251, 23);
            this.bProgresso.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.bProgresso.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(13, 13);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Aguarde...";
            // 
            // LoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 72);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.bProgresso);
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Name = "LoadForm";
            this.Text = "Aguarde Carregando...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar bProgresso;
        private System.Windows.Forms.Label lblStatus;
    }
}