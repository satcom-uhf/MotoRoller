
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.MotorolaScreen.SuspendLayout();
            this.SuspendLayout();
            // 
            // MotorolaScreen
            // 
            this.MotorolaScreen.Controls.Add(this.textBox1);
            this.MotorolaScreen.Location = new System.Drawing.Point(0, 0);
            this.MotorolaScreen.Name = "MotorolaScreen";
            this.MotorolaScreen.Size = new System.Drawing.Size(268, 179);
            this.MotorolaScreen.TabIndex = 0;
            this.MotorolaScreen.TabStop = false;
            this.MotorolaScreen.Text = "GM360";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(40, 43);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 0;
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.MotorolaScreen);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.MotorolaScreen.ResumeLayout(false);
            this.MotorolaScreen.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox MotorolaScreen;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox textBox1;
    }
}

