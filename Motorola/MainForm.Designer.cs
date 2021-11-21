
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
            this.httpPortNumber = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.httpPortNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(191, 207);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(92, 51);
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
            this.comPorts.Size = new System.Drawing.Size(128, 23);
            this.comPorts.TabIndex = 4;
            this.comPorts.Text = "Select COM port";
            // 
            // httpPortNumber
            // 
            this.httpPortNumber.Location = new System.Drawing.Point(72, 235);
            this.httpPortNumber.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.httpPortNumber.Name = "httpPortNumber";
            this.httpPortNumber.Size = new System.Drawing.Size(68, 23);
            this.httpPortNumber.TabIndex = 5;
            this.httpPortNumber.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "http port";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 268);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.httpPortNumber);
            this.Controls.Add(this.comPorts);
            this.Controls.Add(this.MotorolaScreen);
            this.Controls.Add(this.StartButton);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Motorola IP Cam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.httpPortNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox MotorolaScreen;
        private System.Windows.Forms.ComboBox comPorts;
        private System.Windows.Forms.NumericUpDown httpPortNumber;
        private System.Windows.Forms.Label label1;
    }
}

