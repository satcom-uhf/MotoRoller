
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
            this.MotorolaScreen = new System.Windows.Forms.TextBox();
            this.comPorts = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(200, 207);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
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
            // comPorts
            // 
            this.comPorts.FormattingEnabled = true;
            this.comPorts.Location = new System.Drawing.Point(12, 207);
            this.comPorts.Name = "comPorts";
            this.comPorts.Size = new System.Drawing.Size(121, 23);
            this.comPorts.TabIndex = 4;
            this.comPorts.Text = "Select COM port";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 238);
            this.Controls.Add(this.comPorts);
            this.Controls.Add(this.MotorolaScreen);
            this.Controls.Add(this.StartButton);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Motorola IP Cam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox MotorolaScreen;
        private System.Windows.Forms.ComboBox comPorts;
    }
}

