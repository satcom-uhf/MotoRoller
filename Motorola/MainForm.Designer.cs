
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
            this.StartButton = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.ListBox();
            this.MotorolaScreen = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
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
            this.log.FormattingEnabled = true;
            this.log.ItemHeight = 15;
            this.log.Location = new System.Drawing.Point(424, 111);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(334, 244);
            this.log.TabIndex = 2;
            // 
            // MotorolaScreen
            // 
            this.MotorolaScreen.BackColor = System.Drawing.Color.Black;
            this.MotorolaScreen.Font = new System.Drawing.Font("LCD", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MotorolaScreen.ForeColor = System.Drawing.Color.Lime;
            this.MotorolaScreen.Location = new System.Drawing.Point(0, 0);
            this.MotorolaScreen.Multiline = true;
            this.MotorolaScreen.Name = "MotorolaScreen";
            this.MotorolaScreen.ReadOnly = true;
            this.MotorolaScreen.Size = new System.Drawing.Size(320, 200);
            this.MotorolaScreen.TabIndex = 3;
            this.MotorolaScreen.Text = "Test";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MotorolaScreen);
            this.Controls.Add(this.log);
            this.Controls.Add(this.StartButton);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.ListBox log;
        private System.Windows.Forms.TextBox MotorolaScreen;
    }
}

