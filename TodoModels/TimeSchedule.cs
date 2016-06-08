using System;


namespace TodoModels
{
    public class TimeSchedule
    {
        #region  Constructors & Destructor
        public TimeSchedule() { }

        public TimeSchedule(DateTime time, Action<object> callback)
        {
            Callback = callback;
            Time = time;
        }
        #endregion


        #region  Properties & Indexers
        public object Argument { get; set; }
        public Action<object> Callback { get; set; }
        public object Source { get; set; }
        public DateTime Time { get; set; }
        #endregion


        #region Implementation
        internal void OnTime()
        {
            Callback?.Invoke(Argument);
        }
        #endregion
    }
}