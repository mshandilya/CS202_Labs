using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlarmWindowsFormsApp
{
    public partial class Form1 : Form
    {
        private TimeSpan _alarmTime;
        private bool _alarmSet = false;
        private System.Windows.Forms.Timer _timer;
        private AlarmPublisher _publisher;
        private Random _random = new Random();

        public Form1()
        {
            InitializeComponent();
            _timer = new System.Windows.Forms.Timer { Interval = 1000 };
            _timer.Tick += Timer_Tick;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (TimeSpan.TryParse(timeTextBox.Text, out TimeSpan alarmTime))
            {
                _alarmTime = alarmTime;
                _alarmSet = true;
                _publisher = new AlarmPublisher(_alarmTime);
                _publisher.AlarmRaised += Publisher_AlarmRaised;
                _timer.Start();
            }
            else
            {
                MessageBox.Show("Invalid time format. Please enter HH:MM:SS.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!_alarmSet) return;
            this.BackColor = Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));
            _publisher.CheckTime();
        }

        private void Publisher_AlarmRaised(object sender, EventArgs e)
        {
            _timer.Stop();
            MessageBox.Show("Alarm! The specified time has been reached!", "Alarm", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}