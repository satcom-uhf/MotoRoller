
namespace Motorola
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MotorolaScreen = new System.Windows.Forms.GroupBox();
            this.FreqLabel = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.RichTextBox();
            this.MotorolaScreen.SuspendLayout();
            this.SuspendLayout();
            // 
            // MotorolaScreen
            // 
            this.MotorolaScreen.BackColor = System.Drawing.Color.Black;
            this.MotorolaScreen.Controls.Add(this.FreqLabel);
            this.MotorolaScreen.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MotorolaScreen.Location = new System.Drawing.Point(0, 0);
            this.MotorolaScreen.Name = "MotorolaScreen";
            this.MotorolaScreen.Size = new System.Drawing.Size(268, 179);
            this.MotorolaScreen.TabIndex = 0;
            this.MotorolaScreen.TabStop = false;
            this.MotorolaScreen.Text = "GM360";
            // 
            // FreqLabel
            // 
            this.FreqLabel.Font = new System.Drawing.Font("LCD", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.FreqLabel.ForeColor = System.Drawing.Color.Lime;
            this.FreqLabel.Location = new System.Drawing.Point(6, 34);
            this.FreqLabel.Name = "FreqLabel";
            this.FreqLabel.Size = new System.Drawing.Size(256, 22);
            this.FreqLabel.TabIndex = 0;
            this.FreqLabel.Text = "label1";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(566, 60);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "button1";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(316, 115);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(453, 202);
            this.log.TabIndex = 2;
            this.log.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.log);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.MotorolaScreen);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MotorolaScreen.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox MotorolaScreen;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label FreqLabel;
        private System.Windows.Forms.RichTextBox log;
    }
}

