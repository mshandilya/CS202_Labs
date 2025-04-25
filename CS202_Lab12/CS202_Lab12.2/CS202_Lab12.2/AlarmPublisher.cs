using System;

namespace AlarmWindowsFormsApp
{
    public class AlarmPublisher
    {
        public delegate void AlarmEventHandler(object sender, EventArgs e);
        public event AlarmEventHandler AlarmRaised;

        private readonly TimeSpan _alarmTime;
        private bool _alarmTriggered = false;

        public AlarmPublisher(TimeSpan alarmTime)
        {
            _alarmTime = alarmTime;
        }

        public void CheckTime()
        {
            if (_alarmTriggered) return;
            TimeSpan current = DateTime.Now.TimeOfDay;
            if (current.Hours == _alarmTime.Hours && current.Minutes == _alarmTime.Minutes && current.Seconds == _alarmTime.Seconds)
            {
                _alarmTriggered = true;
                AlarmRaised?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}