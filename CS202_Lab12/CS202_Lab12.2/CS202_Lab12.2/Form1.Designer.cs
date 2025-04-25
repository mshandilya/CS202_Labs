using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AlarmWindowsFormsApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.components = new Container();
            this.timeTextBox = new TextBox();
            this.startButton = new Button();
            this.SuspendLayout();
            // 
            // timeTextBox
            // 
            this.timeTextBox.Location = new Point(20, 20);
            this.timeTextBox.Name = "timeTextBox";
            this.timeTextBox.Size = new Size(100, 20);
            this.timeTextBox.TabIndex = 0;
            // 
            // startButton
            // 
            this.startButton.Location = new Point(140, 18);
            this.startButton.Name = "startButton";
            this.startButton.Size = new Size(75, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new EventHandler(this.startButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(260, 60);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.timeTextBox);
            this.Name = "Form1";
            this.Text = "Alarm Clock";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private TextBox timeTextBox;
        private Button startButton;
    }
}