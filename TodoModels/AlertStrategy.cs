using System;
using CB.Model.Common;


namespace TodoModels
{
    /*public class AlertStrategy: BindableObject
    {
        #region Fields
        private string _message;
        private readonly Todo _owner;
        private TimeSpan _timeLeft;
        private readonly TimeSchedule _timeSchedule;
        #endregion


        #region  Constructors & Destructor
        internal AlertStrategy() { }

        internal AlertStrategy(Todo owner)
        {
            _owner = owner;
            _timeSchedule = new TimeSchedule
            {
                Source = _owner,
                Callback = OnScheduled
            };
            Clock.Schedule(_timeSchedule);
        }
        #endregion


        #region  Properties & Indexers
        public string Message
        {
            get { return _message; }
            set
            {
                if (SetProperty(ref _message, value))
                {
                    _timeSchedule.Argument = value;
                }
            }
        }

        public TimeSpan TimeLeft
        {
            get { return _timeLeft; }
            set
            {
                if (SetProperty(ref _timeLeft, value))
                {
                    _timeSchedule.Time = _owner.Deadline.Subtract(value);
                }
            }
        }
        #endregion


        #region Implementation
        private void OnScheduled(object obj)
        {
            var msg = obj as string;
            _owner.OnAlert(msg);
        }
        #endregion
    }*/
}