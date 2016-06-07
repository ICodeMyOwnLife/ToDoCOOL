using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;


namespace Todos
{
    public static class Clock
    {
        #region Fields
        private static readonly ICollection<TimeSchedule> _scheduleList = new List<TimeSchedule>();
        private static readonly Timer _timer;
        #endregion


        #region  Constructors & Destructor
        static Clock()
        {
            _timer = new Timer(1000)
            {
                AutoReset = true,
                Enabled = true,
                Interval = 1000,
            };
            _timer.Elapsed += Timer_Elapsed;
        }
        #endregion


        #region  Properties & Indexers
        public static ISynchronizeInvoke SynchronizeInvoke
        {
            get { return _timer.SynchronizingObject; }
            set { _timer.SynchronizingObject = value; }
        }
        #endregion


        #region Methods
        public static void Schedule(TimeSchedule schedule)
        {
            _scheduleList.Add(schedule);
        }

        public static void Unschedule(TimeSchedule schedule)
        {
            _scheduleList.Remove(schedule);
        }

        public static void Unschedule(object source)
        {
            foreach (var schedule in _scheduleList.Where(s=>s.Source == source).ToArray())
            {
                Unschedule(schedule);
            }
        }
        #endregion


        #region Event Handlers
        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var now = DateTime.Now;
            foreach (var schedule in _scheduleList.Where(s => s.Time < now).ToArray())
            {
                schedule.OnTime();
                Unschedule(schedule);
            }
        }
        #endregion
    }
}