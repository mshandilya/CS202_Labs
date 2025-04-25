using System;
using System.Threading;

namespace AlarmConsoleApp
{
    // Publisher class that checks the time and raises an event when the alarm time is reached
    class AlarmPublisher
    {
        // Define a delegate for the alarm event
        public delegate void AlarmEventHandler(object sender, EventArgs e);
        // Define the event based on the delegate
        public event AlarmEventHandler AlarmRaised;

        private readonly TimeSpan _alarmTime;
        private bool _alarmTriggered = false;

        public AlarmPublisher(TimeSpan alarmTime)
        {
            _alarmTime = alarmTime;
        }

        // Start monitoring the system time
        public void Start()
        {
            while (!_alarmTriggered)
            {
                TimeSpan current = DateTime.Now.TimeOfDay;
                // Compare hours, minutes, seconds
                if (current.Hours == _alarmTime.Hours &&
                    current.Minutes == _alarmTime.Minutes &&
                    current.Seconds == _alarmTime.Seconds)
                {
                    OnAlarmRaised();
                    _alarmTriggered = true;
                }
                Thread.Sleep(500); // Check twice every second
            }
        }

        // Method to raise the event
        protected virtual void OnAlarmRaised()
        {
            AlarmRaised?.Invoke(this, EventArgs.Empty);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter alarm time (HH:MM:SS): ");
            string input = Console.ReadLine();

            if (!TimeSpan.TryParse(input, out TimeSpan alarmTime))
            {
                Console.WriteLine("Invalid time format. Please use HH:MM:SS.");
                return;
            }

            // Create publisher
            AlarmPublisher publisher = new AlarmPublisher(alarmTime);
            // Subscribe to the alarm event
            publisher.AlarmRaised += Ring_alarm;

            Console.WriteLine($"Alarm set for {alarmTime:hh\\:mm\\:ss}. Waiting...");
            // Start monitoring
            publisher.Start();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        // Subscriber method that gets called when alarm is raised
        static void Ring_alarm(object sender, EventArgs e)
        {
            Console.WriteLine("\n*** Alarm! The specified time has been reached! ***");
        }
    }
}
